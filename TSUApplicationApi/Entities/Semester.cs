namespace TSUApplicationApi.Entities
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public string Course { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Subject> Subjects { get; set; }
    }
}
