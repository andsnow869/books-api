using Api.Data.Seed;
using Marten;
using Carter;
using Api.Behaviors;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
//Чтение строки подключения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!; //берем из файла Development.json

//Подключение Marten
builder.Services.AddMarten(option =>   // Add.Marten сервис для работы с PostgreSQL.
{
    option.Connection(connectionString); //указываем для Marten, какую строку подключения использовать для Postgres.
}).UseLightweightSessions().InitializeWith<InitializeBookDatabase>();


var assembly = typeof(Program).Assembly;
// Подключаем MediatR, чтобы можно было использовать запросы и обработчики
// Он сам найдёт все классы-хэндлеры в этом проекте
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //То есть не нужно вручную писать «для каждой команды подключить валидатор» — система делает это сама для любых команд.
});

builder.Services.AddValidatorsFromAssembly(assembly); //Подключают все валидаторы в проекте к DI, чтобы ValidationBehavior мог их использовать.

builder.Services.AddCarter();

var app = builder.Build(); //сборка

// Регистрируем Carter, чтобы все модули (CarterModule) автоматически подключились
// Все маршруты (endpoints) внутри этих модулей станут доступными
app.MapCarter();
app.Run();
