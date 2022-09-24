using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApi.Domain.Base
{
    public interface IRequestPageBase
    {
        public int PageIndex { get; set; }
        public string? CurrentIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class RequestPageBase : IRequestPageBase
    {
        public int PageIndex { get; set; } = 1;
        public string? CurrentIndex { get; set; }
        public int PageSize { get; set; } = 10;
    }
    public class ResponsePageBase<T>
    {
        public ResponsePageBase()
        {

        }
        public ResponsePageBase(RequestPageBase request)
        {
            PageIndex = request.PageIndex;
            PageSize = request.PageSize;
        }
        public IEnumerable<T> List { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageTotal { get; set; }
    }
}
