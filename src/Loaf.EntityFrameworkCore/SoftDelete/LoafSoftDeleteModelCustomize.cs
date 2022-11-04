using System.Linq.Expressions;
using Loaf.EntityFrameworkCore.SoftDelete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Loaf.EntityFrameworkCore;

public class LoafModelCustomize : ModelCustomizer
{
    public LoafModelCustomize(ModelCustomizerDependencies dependencies) : base(dependencies)
    {
    }

    public override void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        base.Customize(modelBuilder, context);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType))
            {
                var parameterExpression = Expression.Parameter(entityType.ClrType, "t");

                var lambdaExpression = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameterExpression, nameof(ISoftDelete.IsDeleted)),
                        Expression.Constant(false)
                    ), parameterExpression);
                entityType.SetQueryFilter(lambdaExpression);
            }
        }
    }
}