using System;
using System.Collections.Generic;

namespace Quiz.Core
{
    public class Result<RType> where RType : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public RType Data { get; set; }
        public bool ShowAlert { get; set; }

        public Result(bool isSuccess, string message, bool showAlert = true)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.ShowAlert = showAlert;
        }

        public Result(bool isSuccess, string message, RType data, bool showAlert = true)
        {
            Type dataType = data.GetType();
            showAlert = !(dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(List<>));
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.Data = data;
            this.ShowAlert = showAlert;
        }

        public Result(bool isSuccess, RType data)
        {
            Type dataType = data.GetType();
            this.IsSuccess = isSuccess;
            this.Data = data;
        }
    }
}