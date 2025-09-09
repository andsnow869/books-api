namespace Api.Books.DeleteBook;

public record DeleteBookResponse(bool IsSuccess);

public class DeleteBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/books/{id}", async (
            string id,
            ISender sender
        ) =>
        {
            DeleteBookCommand command = new DeleteBookCommand(id);
            DeleteBookResult result = await sender.Send(command);
            DeleteBookResponse response = result.Adapt<DeleteBookResponse>();
            return Results.Ok(response);
        });
    }
}
