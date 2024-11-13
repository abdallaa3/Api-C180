namespace Day4tackC180.models
{
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public int CatalogId { get; set; }

        public virtual Catalog Catalog { get; set; }
    }
}
