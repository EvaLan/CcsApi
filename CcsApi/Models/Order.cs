namespace CcsApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Dictionary<int, int> Products { get; set; } = new Dictionary<int, int>();
    }
}
