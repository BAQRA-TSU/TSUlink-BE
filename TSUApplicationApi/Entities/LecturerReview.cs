namespace TSUApplicationApi.Entities
{
    public class LecturerReview
    {
        public int Id { get; set; }
        //public string Name { get; set; }
        public string Text { get; set; }
        public int? LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }   // User entity-სთან კავშირი
    }
}
