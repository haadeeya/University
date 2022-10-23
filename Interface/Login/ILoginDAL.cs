using System.Data;
using System.Threading.Tasks;

namespace Interface.Login
{
    public interface ILoginDAL
    {
        DataTable Authenticate(Model.Login login);
    }
}
