using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LoginUser
/// </summary>
public class LoginUser
{
    private string _loginID;
    private static bool _isLoggedIn;
    private DateTime _loginTime;

    public LoginUser(string _loginID, DateTime _loginTime)
    {
        if (_loginID == "" || _loginID == null ||
            _loginTime == default(DateTime))
        {
            return;
        }

        this._loginID = _loginID;
        this._loginTime = _loginTime;
    }

    public string LoginID
    {
        get { return this._loginID; }
    }

    public bool IsLoggedIn
    {
        get { return _isLoggedIn; }
    }

    public void Login(DateTime time)
    {
        _isLoggedIn = true;
        this._loginTime = time;
    }

    public void Logoff()
    {
        this._loginID = null;
        _isLoggedIn = false;
        this._loginTime = default(DateTime);
    }
}
