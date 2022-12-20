namespace CcsApi.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public List<ProductEntry> Products { get; set; } = new List<ProductEntry>();

        public class ProductEntry
        { 
            public int Id { get; set; }
            
            public string Name { get; set; }

            public int Quantity { get; set; }
        }

    }
}

