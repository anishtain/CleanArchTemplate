using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Domain.Resources.Exceptions.Commons
{
    public enum ExceptionTypeEnum
    {
        NotFound,
        Douplicate,
        BadArgument,
        Unauthorize,
        Validation,
        ApiFailed
    }
}
