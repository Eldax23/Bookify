using System.Data;
using System.Data.Common;

namespace Bookify.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}