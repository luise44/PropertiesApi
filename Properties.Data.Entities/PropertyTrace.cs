namespace Properties.Data.Entities
{
    public class PropertyTrace : BaseEntity
    {
        public DateTime DateSale { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public float Tax { get; set; }
        public Property Property { get; set; }
    }
}
