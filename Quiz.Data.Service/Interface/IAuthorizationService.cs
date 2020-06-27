using Quiz.Data.Model.System.Authorization;

namespace Quiz.Data.Service.Interface
{
    public interface IAuthorizationService
    {
        Authorize Get(string token);
    }
}