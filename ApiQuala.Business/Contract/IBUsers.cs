using ApiQuala.Entities.Class.Models;

namespace ApiQuala.Business.Contract
{
    public interface IBUsers
    {
        EUsers Getuser(string username, string pass);
    }
}
