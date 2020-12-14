using System.Collections.Generic;

namespace CQRS.Demo.Domain.Models
{
    public class Category : BaseModel
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}
