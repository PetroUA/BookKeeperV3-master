using bookKeeper_DAL.Abstract.IInterfaces;
using bookKeeper_DAL.Impl.Repositories;
using bookKeeper_Entity;
using System.Linq;
using bookKeeper_DAL.Impl.Contexts;

namespace bookKeeper_DAL.Impl
{
    public class UserRepository : EFCoreRepository<int, User>, IUserRepository
    {
        public UserRepository(DataBaseContext context) : base(context)
        { }

        public User GetByEmail(string email)
        {
            return Db.Users.First(user => user.Email == email);
        }
    }
}