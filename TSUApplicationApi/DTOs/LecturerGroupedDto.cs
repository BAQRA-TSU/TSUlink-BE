namespace TSUApplicationApi.DTOs
{
    public class LecturerGroupedDto
    {
        public List<LecturerDto> Lecture { get; set; }
        public List<LecturerDto> Practical { get; set; }
        public List<LecturerDto> Lab { get; set; }
    }
}
