using System;

namespace CQRS.Demo.Domain.Views
{
    public class CurrentProductListView
    {
        public Guid Identifier { get; set; }

        public string ProductName { get; set; }
    }
}
