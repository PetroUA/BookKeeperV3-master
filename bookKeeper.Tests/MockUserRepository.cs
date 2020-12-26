using bookKeeper_DAL.Abstract.IInterfaces;
using bookKeeper_Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bookKeeper.Tests
{
    class MockUserRepository : MockRepository<int, User>, IUserRepository
    {

        public User GetByEmail(string email)
        {
            return dic.Values.FirstOrDefault(user => user.Email == email);
        }
    }
}
