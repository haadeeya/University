using Interface.Repository;
using Model;
using System.Data;

namespace Interface
{
    public interface IUserDal
    {
        bool Create(User user);
        DataTable Authenticate(Model.Login login);
    }
}
