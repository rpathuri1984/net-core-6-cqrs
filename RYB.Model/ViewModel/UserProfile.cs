namespace RYB.Model.ViewModel
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PrimaryBusinessId { get; set; }
        public int PrimaryOrganizationId { get; set; }
        public int PrimaryBranchId { get; set; }
        public Guid UserId { get; set; }
    }
}
