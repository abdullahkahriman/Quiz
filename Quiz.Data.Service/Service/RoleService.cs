﻿using Quiz.Core;
using Quiz.Data.Model.System;
using System;
using System.Linq;

namespace Quiz.Data.Service
{
    public class RoleService : Repository<Role>
    {
        public RoleService(IServiceProvider serviceProvider) : base(serviceProvider)
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
                var list = this._GetWhere(c => !c.IsDeleted).Select(c => new
                {
                    ID = c.ID,
                    Name = c.Name,
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
        /// Single role
        /// </summary>
        /// <param name="id">Role ID</param>
        /// <returns></returns>
        public Result<object> GetByID(long id)
        {
            Result<object> result;

            try
            {
                Role role = this._GetSingle(c => !c.IsDeleted && c.ID == id);
                var find = new
                {
                    ID = role.ID,
                    Name = role.Name
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
        /// Role save or update
        /// </summary>
        /// <param name="model">Role model</param>
        /// <returns></returns>
        public Result<object> Save(Role model)
        {
            Result<object> result;

            try
            {
                if (string.IsNullOrEmpty(model.Name))
                    return new Result<object>(false, "Role name is required");

                Role role = this._GetSingle(c => c.ID == model.ID);
                if (role == null)
                    this._Add(model);
                else
                {
                    if (this._GetAny<Role>(c => c.ID != model.ID && c.Name.ToLower().Equals(model.Name.ToLower())))
                        return new Result<object>(false, "Already exists");
                    else
                    {
                        role.Name = model.Name;
                        role.UpdatedAt = DateTime.Now;
                        this._Update(role);
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