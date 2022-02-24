using RYB.DataAccess;
using RYB.Model.Dto;
using RYB.Model.ViewModel;

namespace RYB.Business
{
    public class UserRepo : IUserRepo
    {
        private readonly IDbAccess _db;

        public UserRepo(IDbAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<UserProfile>> GetUsers()
        {
            string Sql = "SELECT * FROM [UserProfile]";

            // this will make db call
            IEnumerable<UserProfileDto> userProfileDtos = await _db.LoadData<UserProfileDto, dynamic>(Sql, new { });

            return userProfileDtos.Select(x => new UserProfile
            {
                Email = x.Email,
                UserId = x.GUID,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber ?? "",
                PrimaryBranchId = x.PrimaryBranchId,
                PrimaryBusinessId = x.PrimaryBusinessId,
                PrimaryOrganizationId = x.PrimaryOrganizationId
            });
        }

        public async Task<IEnumerable<UserProfile>> GetUserByEmail(string userEmail)
        {
            string storedProcedure = "GET_USER_BY_EMAIL";

            // this will make db call
            IEnumerable<UserProfileDto> userProfileDtos = await _db.LoadData<UserProfileDto, dynamic>(storedProcedure, new { UserEmail = userEmail }, System.Data.CommandType.StoredProcedure);

            return userProfileDtos.Select(x => new UserProfile
            {
                Email = x.Email,
                UserId = x.GUID,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber ?? "",
                PrimaryBranchId = x.PrimaryBranchId,
                PrimaryBusinessId = x.PrimaryBusinessId,
                PrimaryOrganizationId = x.PrimaryOrganizationId
            });
        }
    }
}