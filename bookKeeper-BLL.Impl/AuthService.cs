using bookKeeper_DAL.Abstract;
using bookKeeper_DAL.Abstract.IInterfaces;
using bookKeeper_DTO;
using bookKeeper_Entity;
using System;
using System.Web.Security;

namespace bookKeeper_BLL.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository UserRepo;

        public AuthService(IUserRepository userRepository)
        {
            UserRepo = userRepository;
        }

        public bool Registration(RegisterDto regInfo, out int id)
        {
            try
            {
                User user = new User();
                user.Name = regInfo.Name;
                user.Email = regInfo.Email;
                user.Password = regInfo.Password;
                UserRepo.Create(user);
                UserRepo.Save();
                id = user.Id;
                FormsAuthentication.SetAuthCookie(user.Name, true);
                return true;
            }
            catch (Exception)
            {
                FormsAuthentication.SignOut();
                id = 0;
                return false;
            }
        }

        public bool Authentication(LoginDto logInfo, out int id)
        {
            try
            {
                User user = UserRepo.GetByEmail(logInfo.Email);
                id = user.Id;

                if (user.Password == logInfo.Password)
                {
                    FormsAuthentication.SetAuthCookie(user.Name, true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                FormsAuthentication.SignOut();
                id = 0;
                return false;
            }
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }


        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    UserRepo.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}