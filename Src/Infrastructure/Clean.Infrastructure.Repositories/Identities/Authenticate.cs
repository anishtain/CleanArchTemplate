using Clean.Domain.Contracts.Infrastructures.Repositories;
using Clean.Domain.Resources.Exceptions;
using Clean.Infrastructure.Datas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Infrastructure.Repositories.Identities
{
    internal class Authenticate : IAuthenticate
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IOptions<Models.TokenConfig> _appSettings;
        private readonly IHttpContextAccessor _httpcontextAccessor;

        public Authenticate(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<Models.TokenConfig> appSettings, IHttpContextAccessor httpcontextAccessor, RoleManager<Role> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appSettings = appSettings;
            _httpcontextAccessor = httpcontextAccessor;
            _roleManager = roleManager;
        }

        public async Task<string> FindUserByPhone(string phone)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phone);

            return user?.PhoneNumber ?? string.Empty;
        }

        public async Task<string> LoginWithUserNameAndPassword(string userName, string password)
        {
            var signinResult = await _signInManager.PasswordSignInAsync(userName, password, false, false);

            if (signinResult.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(userName);

                var role = await _userManager.GetRolesAsync(user);

                string token = await GenerateToken(user.Id, role.First());

                return token;
            }
            else
                throw new CleanException(Domain.Resources.Exceptions.Commons.ExceptionLevelEnum.Infrastructure, Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.NotFound, "کاربر با اطلاعات وارد شده یافت نشد.");
        }

        public async Task<string> LoginWithPhoneAndCode(string phone, string code)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phone && x.Code == code);

            if (user == null)
                throw new CleanException(Domain.Resources.Exceptions.Commons.ExceptionLevelEnum.Infrastructure, Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.NotFound, "کاربر با نام کاربری و کلمه عبور وارد شده یافت نشد.");

            await _signInManager.SignInAsync(user, false);

            var role = await _userManager.GetRolesAsync(user);

            return await GenerateToken(user.Id, role.First());
        }

        public async Task<string> GetCurrentUser()
        {
            bool hasToken = _httpcontextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var token);

            if (!hasToken)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token.ToString().Split(' ')[1]);
            string userId = decodedToken.Claims.First(x => x.Type == "nameid" || x.Type == ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        public async Task<string> GetUserRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var role = await _userManager.GetRolesAsync(user);

            return role.First();
        }

        public async Task<IList<string>> GetCurrentPermissions()
        {
            var currentUser = await GetCurrentUser();

            var currentRole = await GetUserRole(currentUser);

            var permissions = await _roleManager.GetClaimsAsync(await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == currentRole));

            return permissions.Select(x => x.Value).ToList();
        }

        public async Task RegisterUserByUserNameAndPassword(string userName, string password, string roleId)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Name = $"user{DateTime.Now.Ticks.ToString()}",
                ConcurrencyStamp = DateTime.Now.Ticks.ToString(),
                SecurityStamp = DateTime.Now.Ticks.ToString(),
                NormalizedUserName = userName.ToLower(),
                Code = "1234",
                IsActive = true,
                IsDeleted = false
            };
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);

            var role = await _roleManager.FindByIdAsync(roleId);

            await _userManager.CreateAsync(user);

            await _userManager.AddToRoleAsync(user, role.Name);
        }

        public async Task RegisterUserWithPhoneAndCode(string phone)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = phone,
                Name = $"user{DateTime.Now.Ticks.ToString()}",
                ConcurrencyStamp = DateTime.Now.Ticks.ToString(),
                SecurityStamp = DateTime.Now.Ticks.ToString(),
                NormalizedUserName = phone,
                PhoneNumber = phone,
                Code = "1234",
                IsActive = true,
                IsDeleted = false
            };
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "(noapay@123)");

            await _userManager.CreateAsync(user);

            await _userManager.AddToRoleAsync(user, "member");
        }

        private async Task<string> GenerateToken(string userId, string role)
        {
            Models.TokenConfig securityModel = _appSettings.Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityModel.Secret));
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }, "CookieAuth");

            var claimPrincipal = new ClaimsPrincipal(claims);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = securityModel.Issuer,
                Audience = securityModel.Audience,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
