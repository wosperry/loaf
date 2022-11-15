# Loaf

## 开始

#### 1. 创建一个 API 项目
[![创建项目](http://wosperry.com:8090/img/2022/11/15/6372f2cc30dd6.png)](http://wosperry.com:8090/img/2022/11/15/6372f2cc30dd6.png)
#### 2. 添加依赖
打开Nuget包管理工具
[![添加依赖](http://wosperry.com:8090/img/2022/11/15/6372f40fd13be.png)](http://wosperry.com:8090/img/2022/11/15/6372f40fd13be.png)
 
## TODO
- [ ] 抽离SoftDelete实现，Loaf.EntityFrameworkCore 由于 IModelCustomizer、SaveChangesInterceptor 考虑加一层接口或者配置基类，以便于上层应用成对添加过滤