using Api.Data.Seed;
using Marten;
using Carter;

var builder = WebApplication.CreateBuilder(args);
//Чтение строки подключения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!; //берем из файла Development.json

//Подключение Marten
builder.Services.AddMarten(option =>   // Add.Marten сервис для работы с PostgreSQL.
{
    option.Connection(connectionString); //указываем для Marten, какую строку подключения использовать для Postgres.
}).UseLightweightSessions().InitializeWith<InitializeBookDatabase>();


// Подключаем MediatR, чтобы можно было использовать запросы и обработчики
// Он сам найдёт все классы-хэндлеры в этом проекте
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddCarter();

var app = builder.Build(); //сборка

// Регистрируем Carter, чтобы все модули (CarterModule) автоматически подключились
// Все маршруты (endpoints) внутри этих модулей станут доступными
app.MapCarter();
app.Run();
