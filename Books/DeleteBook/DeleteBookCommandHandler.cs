using Api.Exceptions;

namespace Api.Books.DeleteBook;

public record DeleteBookCommand(Guid Id) : ICommand<DeleteBookResult>;

public record DeleteBookResult(bool IsSuccess);

public class DeleteBookCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteBookCommand, DeleteBookResult>
{
    public async Task<DeleteBookResult> Handle(
        DeleteBookCommand request,
        CancellationToken cancellationToken)
    {
        var book = await session.LoadAsync<Book>(request.Id, cancellationToken);

        if (book is null)
        {
            throw new BookNotFoundException(request.Id);
        }

        session.Delete(book);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteBookResult(true);

    }

}
