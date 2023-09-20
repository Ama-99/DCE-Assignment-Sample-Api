namespace Test.Model
{
    public class Order
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int OrderStatus { get; set; }
        public int OrderType { get; set; }
        public Guid OrderBy { get; set; }
        public DateTime OrderedOn { get; set; }
        public DateTime ShippedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
