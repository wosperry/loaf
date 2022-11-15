# Loaf

## 开始 

## TODO
- [x] 抽离SoftDelete实现，Loaf.EntityFrameworkCore 由于 IModelCustomizer、SaveChangesInterceptor 考虑加一层接口或者配置基类，以便于上层应用成对添加过滤
- [ ] 根据数据生成表达式的代码，考虑增加类似于AutoMapper的Profile配置？
- [ ] 表达式目录树，考虑加Or，因为一个属性打两个特性的时候，大概率是不能两个都满足的
- [ ] 给repository直接一个分页结果的拓展方法
