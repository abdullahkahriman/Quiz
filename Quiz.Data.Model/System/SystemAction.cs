using Quiz.Core;

namespace Quiz.Data.Model.System
{
    public class SystemAction : Superior
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
    }
}