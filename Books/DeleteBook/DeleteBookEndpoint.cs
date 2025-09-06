namespace Api.Books.DeleteBook;


public record DeleteBookRequest(Guid Id);

public record DeleteBookResponse(bool IsSuccess);

public class DeleteBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/books", () =>
        {
            return Results.BadRequest("Id не может быть пустым");
        });

        app.MapDelete("/books/{id}", async (
             string id,
             ISender sender


        ) =>
        {

            if (!Guid.TryParse(id, out var guid))
            {
                return Results.BadRequest("Неверный формат Id");
            }

            var command = new DeleteBookCommand(guid);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteBookResponse>();
            return Results.Ok(response);
        });
    }
}
