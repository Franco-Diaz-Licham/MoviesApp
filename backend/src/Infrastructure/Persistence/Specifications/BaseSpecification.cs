namespace backend.src.Infrastructure.Persistence.Specifications;

public class BaseSpecification <T> : ISpecification<T>
{
    public BaseSpecification() { }

    /// <summary>
    /// Constructor to pass filter criteria from the params query.
    /// </summary>
    public BaseSpecification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    /// <summary>
    /// Criteria to query with entity framework. Used in the WHERE function call.
    /// </summary>
    public Expression<Func<T, bool>>? Criteria { get; }

    /// <summary>
    /// Eager loading criteria.
    /// </summary>
    public List<Expression<Func<T, object>>> Includes { get; } = new();

    /// <summary>
    /// Method which adds include to the Includes property.
    /// </summary>
    protected void AddInclude(Expression<Func<T, object>> expression)
    {
        Includes.Add(expression);
    }
}
