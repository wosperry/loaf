# 摸鱼

## 一、 EFCore CLI 迁移命令

### VS

``` VS
Add-Migration XXX
Update-Database
```

### Windows PowerShell

``` powershell

$NAME = "TEST"
dotnet ef migrations add `
    --startup-project .\applications\Loaf.Admin\Loaf.Admin.csproj `
    --project .\applications\Loaf.Admin\Loaf.Admin.csproj `
    $NAME
dotnet ef database update `
    --startup-project .\applications\Loaf.Admin\Loaf.Admin.csproj `
    --project .\applications\Loaf.Admin\Loaf.Admin.csproj

```


### Linux Shell

``` shell

NAME = "TEST" \
dotnet ef migrations add ${NAME} \
    --startup-project .\applications\Loaf.Admin\Loaf.Admin.csproj \
    --project .\applications\Loaf.Admin\Loaf.Admin.csproj 

    
dotnet ef datebase update \
    --startup-project .\applications\Loaf.Admin\Loaf.Admin.csproj \
    --project .\applications\Loaf.Admin\Loaf.Admin.csproj

```