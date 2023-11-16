namespace Properties.Client.Api.Models
{
    public class PropertyImageModel
    {
        public IFormFile File { get; set; }
        public bool Enabled { get; set; }
        public int PropertyId { get; set; }
    }
}
