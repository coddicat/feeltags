using FeelTags.WebApi.ApiServices;
using FeelTags.WebApi.Dal;
using FeelTags.WebApi.Dal.DTOs;
using FeelTags.WebApi.Dal.Entities;
using FeelTags.WebApi.Models;
using FeelTags.WebApi.Startup;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
api.MapDelete("auth", (
    HttpContext httpContext,
    IAuthApiService authApi) =>
{
    authApi.Deauthenticate(httpContext);
    return Results.Ok();
});


api.MapGet("question", async (IQuestionApiService questionApi) =>
{
    QuestionDTO question = await questionApi.GetRandomQuestionAsync();
    return Results.Ok(question);
});

api.MapPut("question", async (
    HttpContext httpContext, 
    IFeelTagsContext context, 
    IQuestionApiService questionApi, 
    [FromBody] ResponseDTO response) =>
{
    await questionApi.RespondToQuestionAsync(httpContext, response);
    return Results.Ok();
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



app.Run();
