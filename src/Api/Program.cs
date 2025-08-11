var builder = WebApplication.CreateBuilder(args);


var app = builder.Build();

app.MapGet("/hello", () => "Hello World!"); // эндпоинт на котором выведется "Hello world!"
app.MapGet("/foo", () => "foo");
app.MapGet("/bar", () => "foo");
app.MapGet("/bar1", () => "foo");

app.Run();
