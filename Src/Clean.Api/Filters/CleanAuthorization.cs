using Clean.Domain.Contracts.Infrastructures.Repositories;
using Clean.Domain.Resources.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;

namespace Clean.Api.Filters
{
    public class CleanAuthorization : Attribute, IAuthorizationFilter
    {
        private readonly string _permission;

        public CleanAuthorization(string permission)
        {
            _permission = permission;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var authenticateService = (IAuthenticate)context.HttpContext.RequestServices.GetService(typeof(IAuthenticate));

            var currentUserId = authenticateService.GetCurrentUser().GetAwaiter().GetResult();

            if (string.IsNullOrEmpty(currentUserId))
                throw new CleanException(Domain.Resources.Exceptions.Commons.ExceptionLevelEnum.Api, Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.Unauthorize, " برای دسترسی به این سرویس نیاز به احراز هویت است.");

            var allowedPermissions = await authenticateService.GetCurrentPermissions();

            if (allowedPermissions == null || allowedPermissions.Count() == 0 || !allowedPermissions.Any(x => x == _permission))
                throw new CleanException(Domain.Resources.Exceptions.Commons.ExceptionLevelEnum.Api, Domain.Resources.Exceptions.Commons.ExceptionTypeEnum.Unauthorize, "شما اجازه دسترسی به این سرور را ندارید.");

        }
    }
}
