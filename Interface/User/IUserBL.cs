using Model;

namespace Interface
{
    public interface IUserBL
    {
        bool Create(User user);
        User Authenticate(Model.Login login);
    }
}
