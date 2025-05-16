namespace TSUApplicationApi.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }

        public int SemesterId { get; set; }
        public Semester Semester { get; set; }

        public ICollection<LecturerSubject> LecturerSubjects { get; set; }
        public ICollection<SubjectReview> SubjectReviews { get; set; }
        public ICollection<SubjectFile> Files { get; set; }
    }
}
