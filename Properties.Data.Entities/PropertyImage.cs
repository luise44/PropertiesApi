namespace Properties.Data.Entities
{
    public class PropertyImage : BaseEntity
    {
        public Byte[] File { get; set; }
        public bool Enabled { get; set; }
        public Property Property { get; set; }
    }
}
