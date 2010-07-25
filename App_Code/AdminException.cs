using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdminException
/// </summary>
public class AdminException : ApplicationException
{
    public AdminException(string message) : base(message) { }
}
