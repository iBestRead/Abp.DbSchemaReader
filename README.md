# Abp VNext DbSchemaReader 模块

[![Build](https://github.com/iBestRead/Abp.DbSchemaReader/workflows/Build/badge.svg)](https://github.com/iBestRead/Abp.DbSchemaReader/actions?query=workflow%3A%22Build%22)
[![NuGetVersion](https://img.shields.io/nuget/v/iBestRead.Abp.DbSchemaReader)](https://www.nuget.org/packages/iBestRead.Abp.DbSchemaReader)
[![NuGet Download](https://img.shields.io/nuget/dt/iBestRead.Abp.DbSchemaReader.svg)](https://www.nuget.org/packages/iBestRead.Abp.DbSchemaReader)

# 安装Nuget包

```shell
dotnet add package iBestRead.Abp.DbSchemaReader
```

# 使用

引用模块

```csharp
[DependsOn(typeof(AbpDbSchemaReaderModule))]
public class LdapTestModule : AbpModule
{
  
}
```

注入服务

```csharp
private readonly IDbSchemaRepository _dbSchemaRepository;
public YourApplicationService(IDbSchemaRepository dbSchemaRepository)
{
    _dbSchemaRepository = dbSchemaRepository;
}

```

# 更多

请参考[单元测试](test/iBestRead.Abp.DbSchemaReader.Tests/iBestRead/Abp/DbSchemaReader)



