namespace Properties.Data.Entities
{
    public class Owner : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public byte[] Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
