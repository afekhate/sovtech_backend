using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSolution.Infrastructure.ApiResponse
{
    

    public class BaseResult<T>
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public string metadata { get; set; }

      
        public List<string> Output { get; set; }

    }

    public class StarWarBase<T>
    {
       
        public bool HasError { get; set; }
        public string Message { get; set; }
        public string StatusCode { get; set; }
        public string metadata { get; set; }
        public T Result { get; set; }

    }
}
