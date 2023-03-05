using ApiQuala.Entities.Class.Models;

namespace ApiQuala.DataAccess.Contract
{
    public interface IUsers
    {
        EUsers Getuser(string username, string pass);
    }
}
