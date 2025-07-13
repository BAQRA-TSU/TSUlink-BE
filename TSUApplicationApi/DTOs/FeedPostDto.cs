namespace TSUApplicationApi.DTOs
{
    public class FeedPostDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public bool CanDelete { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
