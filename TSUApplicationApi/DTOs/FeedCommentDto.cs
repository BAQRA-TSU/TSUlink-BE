using System.Text.Json.Serialization;

namespace TSUApplicationApi.DTOs
{
    public class FeedCommentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }

        public bool CanDelete { get; set; }

        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //public bool? IsApproved { get; set; }
    }
}
