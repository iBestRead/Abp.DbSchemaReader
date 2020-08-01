# Abp Vnext Ldap 模块

[![Build](https://github.com/iBestRead/Abp.Ldap/workflows/Build/badge.svg?style=flat-square)](https://github.com/iBestRead/Abp.Ldap/actions?query=workflow%3A%22Build%22)
[![NuGetVersion](https://img.shields.io/nuget/v/iBestRead.Abp.Ldap)](https://www.nuget.org/packages/iBestRead.Abp.Ldap)
[![NuGet Download](https://img.shields.io/nuget/dt/iBestRead.Abp.Ldap.svg)](https://www.nuget.org/packages/iBestRead.Abp.Ldap)

# 安装Nuget包

```shell
dotnet add package iBestRead.Abp.Ldap
```

# 配置

修改`appsettings.json`文件:

```json
"LDAP": {
    "ServerHost": "127.0.0.1",}
    "ServerPort": 636,```
    "UseSSL": true,
    "Credentials": {
        "DomainUserName": "yourUser@yourdomain.com.cn",
        "Password": "yourPassword"
    },
    "SearchBase": "CN=Users,DC=yourDomain,DC=com,DC=cn",
    "DomainName": "yourDomain.com.cn",
    "DomainDistinguishedName": "DC=yourDomain,DC=com,DC=cn"
}
```

# 使用

引用模块

```csharp
[DependsOn(typeof(AbpLdapModule))]
public class LdapTestModule : AbpModule
{
  
}
```

注入服务

```csharp
private readonly ILdapAuthenticateManager _authenticateManager;
public YourApplicationService(ILdapAuthenticateManager authenticateManager)
{
    _authenticateManager = authenticateManager;
}

```

# 核心对象

## 认证 ILdapAuthenticateManager

```csharp
public interface ILdapAuthenticateManager
{
    /// <summary>
    /// Authenticate 
    /// </summary>
    /// <param name="userDomainName">E.g administrator@yourdomain.com.cn </param>
    /// <param name="password"></param>
    /// <returns></returns>
    bool Authenticate(string userDomainName, string password);
}
```

## 组织 ILdapOrganizationManager

```csharp
public interface ILdapOrganizationManager
{
    /// <summary>
    /// query the specified organizations.
    /// 
    /// filter: (&(ou=xxx)(objectClass=organizationalUnit)) when organizationName is not null
    /// filter: (&(objectClass=organizationalUnit)) when organizationName is null
    /// 
    /// </summary>
    /// <param name="organizationName"></param>
    /// <returns></returns>
    IList<LdapOrganization> GetAll(string organizationName = null);

    /// <summary>
    /// query the specified organization.
    /// 
    /// filter: (&(|(dn=xxx)(distinguishedName=xxx))(objectClass=organizationalUnit)) when organizationName is not null
    /// 
    /// </summary>
    /// <param name="distinguishedName"></param>
    /// <returns></returns>
    LdapOrganization Get(string distinguishedName);
}
```

## 用户 ILdapUserManager

```csharp
public interface ILdapUserManager
{
    /// <summary>
    /// query the specified users.
    /// 
    /// filter: (&(cn=xxx)(objectClass=user)) when userCommonName is not null
    /// filter: (&(objectClass=user)) when userCommonName is null
    /// 
    /// </summary>
    /// <param name="cn"></param>
    /// <returns></returns>
    IList<LdapUser> GetAll(string cn = null);

    /// <summary>
    /// query the specified users.
    /// 
    /// filter: (&((mail=mail)) when mail is not null
    /// 
    /// </summary>
    /// <param name="mail"></param>
    /// <returns></returns>
    IList<LdapUser> GetAllByMail(string mail);

    /// <summary>
    /// query the specified users.
    /// 
    /// filter: (&((sAMAccountName=sAMAccountName)) when sAMAccountName is not null
    /// 
    /// </summary>
    /// <param name="samAccountName"></param>
    /// <returns></returns>
    IList<LdapUser> GetAllBySamAccountName(string samAccountName);

    /// <summary>
    /// query the specified User.
    /// 
    /// filter: (&(dn=xxx)(objectCategory=person)(objectClass=user)) when dn is not null
    /// 
    /// </summary>
    /// <param name="dn"></param>
    /// <returns></returns>
    LdapUser Get(string dn);

}
```

# 更多

请参考[单元测试](test/iBestRead.Abp.Ldap.Tests/iBestRead/Abp/Ldap)



