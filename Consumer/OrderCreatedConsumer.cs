using MassTransit;
using RabbitMQ.Data;
using RabbitMQ.Messages;
using RabbitMQ.Models;

namespace RabbitMQ.Consumer
{
    public class OrderCreatedConsumer : IConsumer<OrderCreated>
    {
        private readonly AppDbContext _dbContext;

        public OrderCreatedConsumer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<OrderCreated> context)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Perform database operations here using context.Message properties
                    var order = new Order
                    {
                        Name = context.Message.Name,
                        // Set other properties
                    };

                    _dbContext.Set<Order>().Add(order);
                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
