using Marten;

var builder = WebApplication.CreateBuilder(args);
//Чтение строки подключения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!; //берем из файла Development.json

//Подключение Marten
builder.Services.AddMarten(option =>   // Add.Marten сервис для работы с PostgreSQL.
{
    option.Connection(connectionString); //указываем для Marten, какую строку подключения использовать для Postgres.
});

var app = builder.Build(); //сборка



app.Run();
