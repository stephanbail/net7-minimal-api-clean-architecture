using Movies.Api.Auth;
using Movies.Application.Services;
using Movies.Contracts.Requests;

namespace Movies.Api.Endpoints.Ratings;

public static class RateMovieEndpoint
{
    public const string Name = "RateMovie";
    
    public static IEndpointRouteBuilder MapRateMovie(this IEndpointRouteBuilder app)
    {
        app.MapPut(ApiEndpoints.Movies.Rate,
                async (Guid id, RateMovieRequest request,
                    HttpContext context, IRatingService ratingService,
                    CancellationToken token) =>
                {
                    var userId = context.GetUserId();
                    var result = await ratingService.RateMovieAsync(id, request.Rating, userId!.Value, token);
                    return result ? Results.Ok() : Results.NotFound();
                })
            .WithName(Name)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .RequireAuthorization();
        
        return app;
    }
}
