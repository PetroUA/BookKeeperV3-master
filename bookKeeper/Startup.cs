using bookKeeper_BLL.Abstract;
using bookKeeper_BLL.Impl;
using bookKeeper_DAL.Abstract;
using bookKeeper_DAL.Abstract.IInterfaces;
using bookKeeper_DAL.Impl;
using bookKeeper_DAL.Impl.Contexts;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(bookKeeper.Startup))]

namespace bookKeeper
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            IoC.Register<DataBaseContext, MsSqlDataBaseContext>();
            IoC.Register<IUserRepository, UserRepository>();
            IoC.Register<IBookRepository, BookRepository>();
            IoC.Register<IAuthService, AuthService>();
            IoC.Register<IBookService, BookService>();
        }
    }
}
