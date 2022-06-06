using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Resources.Exceptions
{
    public class CleanException : Exception
    {
        public Commons.ExceptionLevelEnum Level { get; set; }

        public Commons.ExceptionTypeEnum Type { get; set; }

        public CleanException(Commons.ExceptionLevelEnum level, Commons.ExceptionTypeEnum type, string message, Exception? inner = null) : base(message, inner)
        {
            this.Level = level;
            this.Type = type;
        }
    }
}
