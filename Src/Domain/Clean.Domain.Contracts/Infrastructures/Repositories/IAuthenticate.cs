using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Contracts.Infrastructures.Repositories
{
    public interface IAuthenticate
    {
        Task<string> FindUserByPhone(string phone);

        Task<string> LoginWithUserNameAndPassword(string userName, string password);

        Task<string> LoginWithPhoneAndCode(string phone, string code);

        Task<string> GetCurrentUser();

        Task<string> GetUserRole(string userId);

        Task RegisterUserByUserNameAndPassword(string userName, string password, string roleId);

        Task RegisterUserWithPhoneAndCode(string phone);

        Task<IList<string>> GetCurrentPermissions();
    }
}
