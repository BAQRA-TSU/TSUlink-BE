namespace TSUApplicationApi.DTOs
{
    public class SubjectDetailDto
    {
        public string Name { get; set; }
        public LecturerGroupedDto Lecturers { get; set; }
        public string Description { get; set; }
        public List<ReviewDto> Reviews { get; set; }
        public List<FileDto> Files { get; set; } = new();
    }
}
