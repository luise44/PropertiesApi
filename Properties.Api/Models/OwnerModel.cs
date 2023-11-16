namespace Properties.Client.Api.Models
{
    public class OwnerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public IFormFile Photo { get; set; }

    }
}
