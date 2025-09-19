using System.Threading.Tasks;
using Bogus;
using Marten;
using Marten.Schema;

public class PostgresEfFakerInitializer : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        var faker = new Faker<Book>("ru")
            .RuleFor(b => b.Id, f => Guid.NewGuid())
            .RuleFor(b => b.Title, f => f.Commerce.ProductName())
            .RuleFor(b => b.Name, f => f.Name.FullName())
            .RuleFor(b => b.Description, f => f.Lorem.Sentence())
            .RuleFor(b => b.Price, f => Math.Round(f.Random.Decimal(650, 2500)))
            //.RuleFor(b => b.ImageUrl, f => f.Image.PicsumUrl())
            .RuleFor(b => b.Category, f => f.Make(f.Random.Int(1, 3),
            () =>
            {
                var category = f.Commerce.Categories(1)[0];
                return char.ToUpper(category[0]) + category.Substring(1);
            })
            );

        var fakeBooks = faker.Generate(60);

        using var session = store.QuerySession();
        var anyBooks = session.Query<Book>().Any();

        if (!anyBooks)
        {
            using var write = store.LightweightSession();
            write.Store<Book>(fakeBooks); ;
            await write.SaveChangesAsync(cancellation);

        }


    }
}