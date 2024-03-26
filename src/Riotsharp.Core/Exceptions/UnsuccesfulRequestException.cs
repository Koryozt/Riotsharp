using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riotsharp.Core.Exceptions;

public class UnsuccesfulRequestException(Status statusError) : Exception(
    $"Request returned a {statusError.Code} with message: {statusError.Message}")
{
}
