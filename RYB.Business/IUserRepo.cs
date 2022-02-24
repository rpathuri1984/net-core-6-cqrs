using RYB.Model.ViewModel;

namespace RYB.Business
{
    public interface IUserRepo
    {
        Task<IEnumerable<UserProfile>> GetUsers();
        Task<IEnumerable<UserProfile>> GetUserByEmail(string userEmail);
    }
}