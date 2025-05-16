namespace TSUApplicationApi.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Course { get; set; }
        public ICollection<Semester> Semesters { get; set; }
    }
}
