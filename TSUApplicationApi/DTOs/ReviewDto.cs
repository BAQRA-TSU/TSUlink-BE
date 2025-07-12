using System.Text.Json.Serialization;

namespace TSUApplicationApi.DTOs
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsApproved { get; set; } // Admin-სთვის გამოჩნდება

        public bool CanDelete { get; set; }

        public string? Status { get; set; }
    }
}
