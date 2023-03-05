using ApiQuala.Business.Contract;
using ApiQuala.DataAccess.Contract;
using ApiQuala.Entities.Class.Models;

namespace ApiQuala.Business.Implement
{
    public class BUsers: IBUsers
    {
        private readonly IUsers _Users;
        public BUsers(IUsers users)
        {
            _Users = users;
        }

        public EUsers Getuser(string username, string pass)
        {
            return _Users.Getuser(username, pass);
        }

    }
}
