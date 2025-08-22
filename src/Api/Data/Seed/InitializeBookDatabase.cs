using Api.Model;
using Marten;
using Marten.Schema;

namespace Api.Data.Seed;

public class InitializeBookDatabase : IInitialData  //класс InitializeBookDatabase, который нужен, чтобы при запуске приложения проверить: ➡️ есть ли книги в базе?
//Он реализует интерфейс IInitialData (значит, должен уметь загружать данные при старте приложения).
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        //Метод Populate — именно его Marten вызывает при инициализации.
        //store → это «магазин документов», соединение с базой Postgres через Marten.
        //cancellation → «стоп-кнопка», если приложение вдруг закрывается и нужно прервать операцию.
        using var session = store.LightweightSession();
        //Открываем сессию с базой (как «короткое соединение»).
        //Через session можно читать/писать в базу.

        if (!await session.Query<Book>().AnyAsync()) //если никаких данных нет
        //session.Query<Book>() → запросить все записи типа Book
        //.AnyAsync() → вернёт true, если хоть одна книга есть.
        {
            session.Store<Book>(new List<Book>());
            //Сейчас в new List<Book>() список пустой, но обычно тут кладут «начальные книги» — например, одну-две тестовые записи)
            //session.Store<Book>(...) → подготовить данные к сохранению
            await session.SaveChangesAsync(cancellation);
            //SaveChangesAsync → реально записать в базу.
        }
    }
}
