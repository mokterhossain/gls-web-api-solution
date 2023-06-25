using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLSPM.Data
{
    public class Response<T>
    {
        public string Message { get; set; }
        public T Result { get; set; }
        public List<T> ResultList { get; set; }
        public int Count { get; set; }
        public bool IsSuccess { get; set; }
    }
}
