namespace TSUApplicationApi.Entities
{
    public class SubjectFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public byte[] FileContent { get; set; } = null!;
        public string ContentType { get; set; } = null!;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
