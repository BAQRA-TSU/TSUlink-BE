namespace TSUApplicationApi.Entities
{
    public class SubjectReview
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Text { get; set; }

        //public int? LecturerId { get; set; }
        //public Lecturer Lecturer { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }   // User entity-სთან კავშირი

        public bool IsApproved { get; set; } = false;
    }
}
