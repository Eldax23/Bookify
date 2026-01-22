using System.Data;
using Dapper;

namespace Bookify.Infrastructure.Data;

// since there is no db implementation of DateOnly in Sql , we have to create a handler.
public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        parameter.Value = value;
        parameter.DbType = DbType.Date;
    }

    public override DateOnly Parse(object value) => DateOnly.FromDateTime((DateTime)value);
}