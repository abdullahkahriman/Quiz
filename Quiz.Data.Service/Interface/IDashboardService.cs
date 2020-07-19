using Quiz.Core;

namespace Quiz.Data.Service.Interface
{
    public interface IDashboardService
    {
        Result<object> GetCount();
    }
}