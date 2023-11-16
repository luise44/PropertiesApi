namespace Properties.Data.Entities
{
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public float Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public Owner? Owner { get; set; }
    }
}
