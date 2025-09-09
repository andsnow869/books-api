
using Api.Exceptions;

namespace Api.Books.GetBookById;


public record GetBookByIdQuery(string Id) : IQuery<GetBookByIdResult>;

public record GetBookByIdResult(Book Item);

public class GetBookByIdQueryHandler(IDocumentSession session)
    : IQueryHandel<GetBookByIdQuery, GetBookByIdResult>
{
    public async Task<GetBookByIdResult> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var isSuccess = Guid.TryParse(query.Id, out var id);

        var book = await session.LoadAsync<Book>(id, cancellationToken);

        if (book is null)
        {
            throw new BookNotFoundException(id);
        }

        return new GetBookByIdResult(book);

    }
}
