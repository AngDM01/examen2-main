using CoffeeMachine.Application.Commands;
using CoffeeMachine.Application.Services.CoffeeMachine;
using CoffeeMachine.Application.Services.CoffeeMoneyChange;
using CoffeeMachine.Infrastructure.CoffeeRepository;
using CoffeeMachine.Infrastructure.MoneyRepository;

var MyAllowSpecificCoffeeMachine = "_myAllowSpecificCoffeeMachine";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
  options.AddPolicy(name: MyAllowSpecificCoffeeMachine,
    policy =>
    {
      policy.WithOrigins("http://localhost:8080")
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});

// Add services to the container.
// Infrastructure
builder.Services.AddScoped<CoffeeMachineRepository>();
builder.Services.AddScoped<CoffeeMoneyRepository>();

// Application
builder.Services.AddScoped<ICoffeMachineChangeService, CoffeeMachineChangeService>();
builder.Services.AddScoped<ICoffeMachineService, CoffeMachineService>();
builder.Services.AddScoped<IBuyCoffeeCommand, BuyCoffeeCommand>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<CoffeeMachineRepository>();
builder.Services.AddSingleton<CoffeeMoneyRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(MyAllowSpecificCoffeeMachine);

app.MapControllers();

app.Run();
