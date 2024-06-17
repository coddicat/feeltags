using FeelTags.WebApi.Dal.DTOs;
using FeelTags.WebApi.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace FeelTags.WebApi.Dal.Repositories
{
    public interface IQuestionRepo
    {
        Task<QuestionDTO?> GetRandomQuestionAsync();
        Task<long> AddRespondAsync(long accountId, ResponseDTO respondDto);
    }
    public class QuestionRepo(IFeelTagsContext context) : IQuestionRepo
    {
        public async Task<QuestionDTO?> GetRandomQuestionAsync()
        {
            var model = await context.Questions                
                .OrderByDescending(x => SqlFunctions.BiasRandomByDate(x.CreatedAt))
                .Include(x => x.AnswerOptions)
                .Include(x => x.Account)
                .Select(x => new 
                {
                    x.Content,
                    x.CreatedAt,
                    AnswerOptions = x.AnswerOptions!.Select(y => new
                    {
                        y.Id,
                        y.Text
                    }),
                    //AnswerOptions = x.AnswerOptions!.ToDictionary(y => y.Id, y => y.Text),
                    AuthorName = x.Account!.Name,
                    AuthorPicture = x.Account!.Picture
                })
                .FirstOrDefaultAsync();
            
            return model is not null ? new QuestionDTO 
            { 
                Content = model.Content,
                AuthorName = model.AuthorName,
                AuthorPicture = model.AuthorPicture,
                CreatedAt = model.CreatedAt,
                AnswerOptions = model.AnswerOptions.ToDictionary(x => x.Id, x => x.Text)
            } : null;
        }
        public async Task<long> AddRespondAsync(long accountId, ResponseDTO respondDto)
        {
            var point = respondDto.Location is null ? null : 
                new Point(respondDto.Location.Longitude, respondDto.Location.Latitude) { SRID = 4326 };

            Response response = new Response
            {
                AccountId = accountId,
                AnswerOptionId = respondDto.AnswerOptionId,
                Location = point,
            };
            await context.Responses.AddAsync(response);

            await context.SaveChangesAsync();

            return response.Id;
        }
    }
}
