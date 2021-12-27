using System;
using System.Collections.Generic;

namespace Groot.Model
{
    public class General<T>
    {
        public bool IsSuccess { get; set; }
        public T Entity { get; set; }
        public string ExceptionMessage { get; set; }
    }
}

