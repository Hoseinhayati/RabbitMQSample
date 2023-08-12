namespace RabbitMQ.Messages
{
    public class OrderCreated
    {
        public int OrderId { get; set; }
        public string Name { get; set; }
        // Other properties related to the order
    }
}
