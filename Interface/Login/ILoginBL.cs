using Model;
using System.Threading.Tasks;

namespace Interface.Login
{
    public interface ILoginBL
    {
        User Authenticate(Model.Login login);
    }
}
