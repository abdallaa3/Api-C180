namespace Day4tackC180.models
{
    public class Catalog
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
