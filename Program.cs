using Api.Data.Seed;
using Marten;
using Carter;
using Api.Behaviors;
using FluentValidation;
using Api.Exceptions.Handler;

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
    config.AddOpenBehavior(typeof(TimeoutBehavior<,>));
    //Применяем TimeoutBehavior ко всем запросам и их ответам

    //каждый раз когда идет какой-то запрос через MediatR, он проходит через TimeoutBehavior, а потом уже идет в настоящий обработчик

    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    //То есть не нужно вручную писать «для каждой команды подключить валидатор» — система делает это сама для любых команд.
});

builder.Services.AddValidatorsFromAssembly(assembly); //Подключают все валидаторы в проекте к DI, чтобы ValidationBehavior мог их использовать.

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>(); //добавляем свой обработчик в систему

var app = builder.Build(); //сборка

app.UseExceptionHandler(opt => { }); //включаем механизм глобальной обработки ошибок, чтобы мой обработчик реально работал при падениях

// Регистрируем Carter, чтобы все модули (CarterModule) автоматически подключились
// Все маршруты (endpoints) внутри этих модулей станут доступными
app.MapCarter();
app.Run();
