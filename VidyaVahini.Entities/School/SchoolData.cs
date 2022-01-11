namespace VidyaVahini.Entities.School
{
    public class SchoolData : BaseSchoolData
    {
        public string Code { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public int StateId { get; set; }
    }
}
