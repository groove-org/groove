namespace GroovE.Api.Endpoints.Weather;

//public class GetWeatherEndpoint : IEndpoint
//{
//    public record Request(string Country);
//    public record Response(string Description);

//    public class RequestValidator : AbstractValidator<Request>
//    {
//        public RequestValidator() => RuleFor(c => c.Country).NotEmpty();
//    }

//    public static void Map(IEndpointRouteBuilder app) => app
//        .MapGet("/", Handle)
//        .WithRequestValidation<Request>()
//        .WithSummary("Returns the weather")
//        .RequireAuthorization(Policies.AdminOnly);

//    public static async Task<Results<Ok<Response>, NotFound>> Handle([AsParameters] Request request, ClaimsPrincipal claimsPrincipal, IWeatherService service, CancellationToken cancellationToken)
//    {
//        var result = await service.GetWeatherDescriptionAsync(request.Country, cancellationToken);
//        if (result is null)
//            return TypedResults.NotFound();

//        return TypedResults.Ok(new Response(result));
//    }
//}