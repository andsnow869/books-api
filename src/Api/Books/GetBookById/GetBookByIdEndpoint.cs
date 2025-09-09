
using Microsoft.AspNetCore.Mvc;

namespace Api.Books.GetBookById;

public record GetBookByIdRequest(string Id);

public record GetBookByIdResponse(Book Item);


public class GetBookByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/book", async (
            [AsParameters] GetBookByIdRequest request,
             ISender sender
        ) =>
        {
            GetBookByIdQuery query = request.Adapt<GetBookByIdQuery>();
            GetBookByIdResult result = await sender.Send(query);

            GetBookByIdResponse response = result.Adapt<GetBookByIdResponse>();

            return Results.Ok(response);
        });
    }
}
