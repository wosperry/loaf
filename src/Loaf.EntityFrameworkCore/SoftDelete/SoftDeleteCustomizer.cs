using System.Linq.Expressions;
using Loaf.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.SoftDelete;

public class SoftDeleteCustomizer : IModelCustomizing, ISingleton
{
    public void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        // 干预查询过程，如果标记ISoftDelete，则不返回结果
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(ISoftDelete).IsAssignableFrom(entityType.ClrType)) continue;

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