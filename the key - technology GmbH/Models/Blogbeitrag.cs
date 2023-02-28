namespace the_key___technology_GmbH.Models
{
    public class Blogbeitrag
    {
        public int Id { get; set; }
        public string Guid { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Link { get; set; }
        public string Date{ get; set; }
        public string LastModified { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string[] Categories { get; set; }
        public string[] Tags { get; set; }

    }
}
