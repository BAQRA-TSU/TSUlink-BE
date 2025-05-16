namespace TSUApplicationApi.DTOs
{
    public class LecturerDetailDto
    {
        public string Name { get; set; }
        public List<SubjectItemDto> Subjects { get; set; }
        public ContactInfoDto Information { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}
