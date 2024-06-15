using FeelTags.WebApi.ApiServices;
using FeelTags.WebApi.Dal;
using FeelTags.WebApi.Dal.DTOs;
using FeelTags.WebApi.Dal.Entities;
using FeelTags.WebApi.Models;
using FeelTags.WebApi.Startup;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
IServiceCollection services = builder.Services;

configuration.InitFirebaseAuth();

services
    .AddCors()
    .ConfigureAuthentication(configuration)
    .ConfigureSwaggerAndHsts(builder.Environment)
    .ConfigureServices()
    .ConfigureSettings()
    .ConfigureDbContext(configuration);

WebApplication app = builder.Build();
app.ConfigureWebApplication();

RouteGroupBuilder api = app.MapGroup("api");

api.MapPost("auth/google", async (
    HttpContext httpContext, 
    IAuthApiService authApi, 
    SignInWithGoogleRequest request) =>
{
    AccountDTO account = await authApi.SignInWithGoogleAsync(httpContext, request.IdToken);
    return Results.Ok(account);
});

api.MapGet("auth", async (
    HttpContext httpContext,
    IAuthApiService authApi) =>
{
    AccountDTO? account = await authApi.CheckAsync(httpContext);
    return Results.Ok(account);
});



app.MapGet("distance/{lat}/{lon}/{distance}", async (IFeelTagsContext context, double lat, double lon, double distance) =>
{
    Point point = new Point(lon, lat) { SRID = 4326 };
    Point[] res = await context.Responses
        .Where(x => x.Location != null)
        .Where(x => x.Location!.IsWithinDistance(point, distance))
        .Select(x => x.Location!)
        .ToArrayAsync();

    return res.Select(x => new { x.Coordinate.X, x.Coordinate.Y }).ToArray();
});

app.MapGet("question", async (IFeelTagsContext context) =>
{
    Question? res = await context.Questions
        .OrderByDescending(x => SqlFunctions.BiasRandomByDate(x.CreatedAt))        
        .FirstOrDefaultAsync();    
    
    return res;
});

app.MapPost("question", async (IFeelTagsContext context, NewQuestionRequest request) =>
{
    Question question = new Question
    {
        AccountId = 1, //Admin
        Content = request.Content,
        AnswerOptions = request.AnswerOptions.Select(x => new AnswerOption { Text = x }).ToList()
    };
    await context.Questions.AddAsync(question);
    await context.SaveChangesAsync();
    
    return question.Id;
});

app.MapPost("respond", async (IFeelTagsContext context, NewAnswerRequest request) =>
{
    Response response = new Response
    {
        AccountId = 1, //Admin,
        AnswerOptionId = request.AnswerOptionId,
        Location = request.Location is not null ? new Point(request.Location.Longitude, request.Location.Latitude) { SRID = 4326 } : null,
    };
    await context.Responses.AddAsync(response);
    
    return await context.SaveChangesAsync();
});


app.Run();
