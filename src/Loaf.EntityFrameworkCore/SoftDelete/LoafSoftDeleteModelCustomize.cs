using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Loaf.EntityFrameworkCore.SoftDelete;

/// <summary>
/// 替换为客制化Model创建，增加自己代码
/// 此方式实现，可以保证调用方不需要控制base.OnModelCreating的调用位置
/// </summary>
public class LoafModelCustomize : ModelCustomizer
{
    public LoafModelCustomize(ModelCustomizerDependencies dependencies) : base(dependencies)
    {
    }

    public override void Customize(ModelBuilder modelBuilder, DbContext context)
    {
        base.Customize(modelBuilder, context);
        
        // 默认发现DbContext所在程序集的所有IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(context.GetType().Assembly);
        
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