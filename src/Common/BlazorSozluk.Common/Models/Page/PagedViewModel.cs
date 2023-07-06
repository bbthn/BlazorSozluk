using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.Page
{
    public class PagedViewModel<T> where T : class
    {
        public PagedViewModel():this(new List<T>(), new Page())
        {
            
        }
        public PagedViewModel(List<T> result, Page pageInfo)
        {
            Result = result;
            PageInfo = pageInfo;
        }

        public List<T> Result{ get; set; }
        public Page PageInfo { get; set; }
    }
}
