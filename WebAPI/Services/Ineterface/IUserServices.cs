using WebAPI.Authenticetion;

namespace WebAPI.Services.Ineterface
{
    public interface IUserServices
    {
        string Login(UserModel user);
    }
}
