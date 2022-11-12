using System;

namespace Loaf.EntityFrameworkCore;

public class ConnectionStringNotSetException : Exception
{
    public ConnectionStringNotSetException(string message) : base(message)
    {
    }
}