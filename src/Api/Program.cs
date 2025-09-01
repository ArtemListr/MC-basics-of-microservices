using Api.Data.Seed;



var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!; //получение строки подключения из конфигурационного файла
builder.Services.AddMarten(option =>
{
    option.Connection(connectionString); //подключение к БД
}
).UseLightweightSessions().InitializeWith<InitializeBookDatabase>(); //добавляем инициализирующий список книг в базу при инициализ, если его нет


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();

var app = builder.Build();

app.MapCarter();

app.Run();
