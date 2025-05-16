namespace TSUApplicationApi.Entities
{
    public class Lecturer
    {

        public int Id { get; set; }
        public string FullName { get; set; }

        //public string Role { get; set; } // Lecture, Practical, Lab

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Office { get; set; }
        public ICollection<LecturerSubject> LecturerSubjects { get; set; }
        public ICollection<LecturerReview> LecturerReviews { get; set; }
    }
}
