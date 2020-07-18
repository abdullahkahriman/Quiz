using Quiz.Core;
using Quiz.Data.Model.System;
using System;
using System.Linq;

namespace Quiz.Data.Service
{
    public class SystemActionService : Repository<SystemAction>
    {
        public SystemActionService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        /// <summary>
        /// All list
        /// </summary>
        /// <returns></returns>
        public Result<object> Get()
        {
            Result<object> result;

            try
            {
                var list = this._GetWhere(c => !c.IsDeleted)
                    .OrderByDescending(c => c.UpdatedAt)
                    .OrderByDescending(c => c.CreatedAt)
                    .Select(c => new
                    {
                        ID = c.ID,
                        ControllerName = c.ControllerName,
                        ActionName = c.ActionName,
                        CreatedAt = c.CreatedAt,
                        UpdatedAt = c.UpdatedAt
                    });
                result = new Result<object>(true, string.Empty, list);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }

        /// <summary>
        /// Single system action
        /// </summary>
        /// <param name="id">SystemAction ID</param>
        /// <returns></returns>
        public Result<object> GetByID(long id)
        {
            Result<object> result;

            try
            {
                SystemAction systemAction;

                if (id == 0)
                    systemAction = new SystemAction();
                else
                    systemAction = this._GetSingle(c => !c.IsDeleted && c.ID == id);

                var find = new
                {
                    ID = systemAction.ID,
                    ControllerName = systemAction.ControllerName,
                    ActionName = systemAction.ActionName
                };
                result = new Result<object>(true, string.Empty, find);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }

        /// <summary>
        /// SystemAction save or update
        /// </summary>
        /// <param name="model">SystemAction model</param>
        /// <returns></returns>
        public Result<object> Save(SystemAction model)
        {
            Result<object> result;

            try
            {
                if (string.IsNullOrEmpty(model.ControllerName) || string.IsNullOrEmpty(model.ActionName))
                    return new Result<object>(false, "Controller and Action Name is required");

                SystemAction systemAction = this._GetSingle(c => c.ID == model.ID);
                if (systemAction == null)
                    this._Add(model);
                else
                {
                    model.ControllerName = model.ControllerName.ToLower();
                    model.ActionName = model.ActionName.ToLower();

                    if (this._GetAny<SystemAction>(c => c.ID != model.ID && c.ControllerName.ToLower().Equals(model.ControllerName) && c.ActionName.ToLower().Equals(model.ActionName)))
                        return new Result<object>(false, "Already exists");
                    else
                    {
                        systemAction.ControllerName = model.ControllerName;
                        systemAction.ActionName = model.ActionName;
                        systemAction.UpdatedAt = DateTime.Now;
                        this._Update(systemAction);
                    }
                }
                result = new Result<object>(true, string.Empty);
            }
            catch (Exception ex)
            {
                result = new Result<object>(false, "Something went wrong!");
            }

            return result;
        }
    }
}
