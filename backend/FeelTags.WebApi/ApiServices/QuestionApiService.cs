using FeelTags.WebApi.Dal.DTOs;
using FeelTags.WebApi.Dal.Repositories;

namespace FeelTags.WebApi.ApiServices
{
    public interface IQuestionApiService
    {
        Task<QuestionDTO> GetRandomQuestionAsync();
        Task<long> RespondToQuestionAsync(HttpContext httpContext, ResponseDTO respond);
    }
    public class QuestionApiService(IAuthApiService authApi, IQuestionRepo questionRepo) : IQuestionApiService
    {
        public async Task<QuestionDTO> GetRandomQuestionAsync()
        {
            QuestionDTO? question = await questionRepo.GetRandomQuestionAsync();
            ArgumentNullException.ThrowIfNull(question, nameof(question));

            return question;
        }

        public async Task<long> RespondToQuestionAsync(HttpContext httpContext, ResponseDTO respond)
        {
            long? accountId = await authApi.GetAccountIdAsync(httpContext);
            
            if (accountId is null)
            {
                throw new UnauthorizedAccessException();
            }

            return await questionRepo.AddRespondAsync(accountId.Value, respond);
        }
    }
}
