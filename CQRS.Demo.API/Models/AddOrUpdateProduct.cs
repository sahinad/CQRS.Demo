namespace CQRS.Demo.API.Models
{
    public class AddOrUpdateProduct
    {
        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public int UnitsOnOrder { get; set; }

        public int ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public string CategoryName { get; set; }

        public string SupplierCompanyName { get; set; }
    }
}
