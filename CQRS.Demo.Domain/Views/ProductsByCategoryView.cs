using System;

namespace CQRS.Demo.Domain.Views
{
    public class ProductsByCategoryView
    {
        public Guid Identifier { get; set; }

        public string CategoryName { get; set; }

        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public int UnitsInStock { get; set; }

        public bool Discontinued { get; set; }
    }
}
