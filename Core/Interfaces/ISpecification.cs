using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> Includes { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPageingEnabled { get; }
}
