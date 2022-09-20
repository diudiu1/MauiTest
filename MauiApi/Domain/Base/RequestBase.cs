using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApi.Domain.Base
{
    public class ResponseBase
    {
        public string Code { get; set; } = ResponseBaseCode.Success;
        public string? Message { get; set; }

        public static ResponseBase Success = new ResponseBase()
        {
            Code = ResponseBaseCode.Success
        };
        public static ResponseBase ServerError = new ResponseBase()
        {
            Code = ResponseBaseCode.ServerError
        };
    }
    public class ResponseBase<T> : ResponseBase
    {
        public T? Data { get; set; }
    }
    public class ResponseBaseCode
    {
        public static string Success = "success";
        public static string Fail = "fail";
        public static string Existed = "existed";
        public static string ServerError = "server_error";
    }
}
