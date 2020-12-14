namespace CQRS.Demo.API.Models
{
    public class GetProductsByCategory
    {
        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public int UnitsInStock { get; set; }

        public bool Discontinued { get; set; }
    }
}
