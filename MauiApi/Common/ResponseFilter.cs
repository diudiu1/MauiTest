using MauiApi.Domain.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MauiApi.Common
{
    public class ResponseFilter : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult != null)
                {
                    if (objectResult.StatusCode == null)
                    {
                        context.Result = new OkObjectResult(new ResponseBase<object>()
                        {
                            Code = ResponseBaseCode.Success,
                            Message = ResponseBaseCode.Success,
                            Data = objectResult.Value
                        });
                    }
                }
            }
            //throw new NotImplementedException();
        }
    }
}
