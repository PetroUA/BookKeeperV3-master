using bookKeeper_DTO;
using System;

namespace bookKeeper_DAL.Abstract
{
    public interface IAuthService : IDisposable
    {
        bool Registration(RegisterDto regInfo, out int id);
        bool Authentication(LoginDto logInfo, out int id);
        void SignOut();
    }
}