using Api.Exceptions;

namespace Api.Books.DeleteBook;

public record DeleteBookCommand(string Id) : ICommand<DeleteBookResult>;

public record DeleteBookResult(bool IsSuccess);

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id не может быть пустым")
            .Must(id => Guid.TryParse(id, out _))
            .WithMessage("Id должен быть валидным GUID");
    }
}

public class DeleteBookCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteBookCommand, DeleteBookResult>
{
    public async Task<DeleteBookResult> Handle(
        DeleteBookCommand request,
        CancellationToken cancellationToken)
    {
        var IsSuccess = Guid.TryParse(request.Id, out var id);
        var book = await session.LoadAsync<Book>(id, cancellationToken);

        if (book is null)
        {
            return new DeleteBookResult(false);
        }

        session.Delete(book);

        await session.SaveChangesAsync(cancellationToken);

        return new DeleteBookResult(true);

    }

}
