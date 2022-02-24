namespace RYB.Model.Dto
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PrimaryBusinessId { get; set; }
        public int PrimaryOrganizationId { get; set; }
        public int PrimaryBranchId { get; set; }
        public Guid GUID { get; set; }
        public DateTime CreatedTimeUtc { get; set; }
        public DateTime UpdateTimeUtc { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
    }
}
