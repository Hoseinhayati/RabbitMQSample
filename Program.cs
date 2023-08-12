using MassTransit;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Consumer;
using RabbitMQ.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.SetKebabCaseEndpointNameFormatter();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost");

        cfg.ReceiveEndpoint("order-created", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        connectionString: builder.Configuration.GetConnectionString("AppConnection"));
},
                  contextLifetime: ServiceLifetime.Scoped,
                  optionsLifetime: ServiceLifetime.Scoped);

builder.Services.AddScoped<DbContext>(s => s.GetRequiredService<AppDbContext>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
