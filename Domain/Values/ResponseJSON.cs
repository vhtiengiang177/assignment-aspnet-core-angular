using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Values
{
    public class ResponseJSON<T>
    {
        public int TotalPage { get; set; }
        public List<T> Data { get; set; }
    }
}
