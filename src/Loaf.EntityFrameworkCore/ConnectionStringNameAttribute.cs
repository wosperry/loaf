using System;

namespace Loaf.EntityFrameworkCore;

[AttributeUsage(AttributeTargets.Class)]
public class ConnectionStringNameAttribute : Attribute
{
    public ConnectionStringNameAttribute(string connectionStringName)
    {
        ConnectionStringName = connectionStringName;
    }

    public string ConnectionStringName { get; }
}