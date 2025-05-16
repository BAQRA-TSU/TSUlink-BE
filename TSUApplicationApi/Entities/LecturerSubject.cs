namespace TSUApplicationApi.Entities
{
    public class LecturerSubject
    {
        public int LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public string Type { get; set; }
    }
}
