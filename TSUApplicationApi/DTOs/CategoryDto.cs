namespace TSUApplicationApi.DTOs
{
    public class CategoryDto
    {
        public string Course { get; set; }
        public List<SemesterDto> Semesters { get; set; }
    }
}
