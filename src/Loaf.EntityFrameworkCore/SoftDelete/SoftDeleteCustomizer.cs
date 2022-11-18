using System.Linq;
using System.Linq.Expressions;
using Loaf.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Loaf.EntityFrameworkCore.SoftDelete;

public class SoftDeleteCustomizer : IModelCustomizing, ISingleton
{
    public void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        // 干预查询过程，找到所有标记ISoftDelete的EntityType
        var entityTypes = modelBuilder.Model.GetEntityTypes().Where(t => typeof(ISoftDelete).IsAssignableFrom(t.ClrType));
        
        foreach (var entityType in entityTypes)
        {
            // t=>t.IsDeleted == false
            var parameterExpression = Expression.Parameter(entityType.ClrType, "t");
            var lambdaExpression = Expression.Lambda(
                Expression.Equal(
                    Expression.Property(parameterExpression, nameof(ISoftDelete.IsDeleted)),
                    Expression.Constant(false)
                ), parameterExpression);

            // 添加了一个过滤
            entityType.SetQueryFilter(lambdaExpression);
        }
    }
}