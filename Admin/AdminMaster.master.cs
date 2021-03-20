using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;
using System.Web.UI.HtmlControls;

public partial class Admin_AdminMaster : System.Web.UI.MasterPage
{
    private CoachDal coachingDal;
    private LoginUser loginUser;
    private Notifier notifier;
    private Logger logger;
    private GlobalFunctions globalFunctions;

    public CoachDal _CoachingDal
    {
        get { return this.coachingDal; }
        set
        {
            if (value != null)
            {
                this.coachingDal = value;
            }
        }
    }

    public LoginUser _LoginUser
    {
        get { return this.loginUser; }
        set
        {
            if (value != null)
            {
                this.loginUser = value;
            }
        }
    }

    public Notifier _Notifier
    {
        get { return this.notifier; }
        set
        {
            if (value != null)
            {
                this.notifier = value;
            }
        }
    }

    public Logger _Logger
    {
        get { return this.logger; }
        set
        {
            if (value != null)
            {
                this.logger = value;
            }
        }
    }

    public bool _MainSelector
    {
        get { return this.mainSelector.Visible; }
        set
        {
            if (value != this.mainSelector.Visible)
            {
                this.mainSelector.Visible = value;
            }
        }
    }

    public bool _Login
    {
        get { return this.login.Visible; }
        set
        {
            if (value != this.login.Visible)
            {
                this.login.Visible = value;
            }
        }
    }

    public GlobalFunctions _GlobalFunctions
    {
        get { return this.globalFunctions; }
        set
        {
            if (value != null)
            {
                this.globalFunctions = value;
            }
        }
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        this.coachingDal = new CoachDal();
        this.notifier = new Notifier();
        this.logger = new Logger();
        this.globalFunctions = new GlobalFunctions();

        if (!Page.IsPostBack)
        {
            AdminUser f = new AdminUser
            {
                AdminUserID = this.coachingDal.GetNextAvailableID("admin"),
                CreationTime = TimeNow.TheTimeNow,
                spCreationTime = TimeNow.TheTimeNow.ToString(),
                LastLogin = new DateTime(1900, 1, 1),
                spLastLogin = "1900/1/1",
                LastUpdate = new DateTime(1900, 1, 1),
                spLastUpdate = "1900/1/1",
                Password = "keet",
                UserID = "carmella"
            };

            try
            {
                if (this.coachingDal.GetAdminUser(f.UserID, f.Password) == null)
                {
                    this.coachingDal.Add("admin", f, TimeNow.TheTimeNow);
                    this.notifier.Notify(9, "White", f.UserID);
                    this.logger.Log(new AdminException(string.Format(". {0} Was Added Successfully On {1}", f.UserID, TimeNow.TheTimeNow)),
                    MethodBase.GetCurrentMethod().Name);
                }
            }
            catch (Exception g)
            {
                this.logger.Error(g, MethodBase.GetCurrentMethod().Name);
                this.notifier.Notify(10, "Red", f.UserID);
                return;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.coachingDal.GetCount("log") >= 100)
        {
            try
            {
                this.globalFunctions.WriteLogToFile(TimeNow.TheTimeNow);
                this.coachingDal.DeleteAllByType("log");
            }
            catch (Exception p)
            {
                this.logger.Error(p, MethodBase.GetCurrentMethod().Name);
                this.notifier.Notify(23, "Red", "");
            }
        }

        if (this.Session["login"] != null)
        {
            this.loginUser = (LoginUser)this.Session["login"];
            if (this.loginUser.IsLoggedIn)
            {
                this.mainSelector.Visible = true;
                this.login.Visible = false;
                this.main.Style["visibility"] = "visible";
                this.main.Visible = true;
            }
        }
        else
        {
            this.mainSelector.Visible = false;
            this.login.Visible = true;
            this.main.Style["visibility"] = "hidden";
            this.main.Visible = false;
        }

        if (this.Session["tab"] != null)
        {
            this.SelectTab((string)this.Session["tab"]);
        }

    }

    public void forgot_Click(object sender, EventArgs e)
    {
        if (this.recoverMail.Text == "")
        {
            this.logger.Warn(new AdminException(". this.recoverMail.Text == \"\""),
            MethodBase.GetCurrentMethod().Name);
            this.errorLabel.Text = "Please Enter Mail Address";
            return;
        }

        if (!this.globalFunctions.ValidateMailAddress(this.recoverMail.Text))
        {
            this.logger.Warn(new AdminException(". !this.ValidateMailAddress(this.recoverMail.Text)"),
            MethodBase.GetCurrentMethod().Name);
            this.errorLabel.Text = "Illegal Mail Address";
            return;
        }

        try
        {
            this.globalFunctions.SendMailAdmins(this.recoverMail.Text);
            this.logger.Log(new AdminException(". Recovery E-Mail Was Successfully Sent"),
            MethodBase.GetCurrentMethod().Name);
            this.errorLabel.Text = "Recovery E-Mail Was Successfully Sent";
        }
        catch (Exception t)
        {
            this.logger.Error(t, MethodBase.GetCurrentMethod().Name);
            this.notifier.Notify(6, "Red", this.recoverMail.Text);
            return;
        }
    }


    protected void loginBut_Click(object sender, EventArgs e)
    {
        if (this.getUserID.Text == "")
        {
            this.logger.Warn(new AdminException
            (". this.getUserID.Text == \"\""), MethodBase.GetCurrentMethod().Name);
            this.errorLoginLabel.Text = "Please Enter User ID";
            return;
        }

        if (this.getPassword.Text == "")
        {
            this.logger.Warn(new AdminException
            (". this.getPassword.Text == \"\""), MethodBase.GetCurrentMethod().Name);
            this.errorLoginLabel.Text = "Please Enter Password";
            return;
        }

        AdminUser m = this.coachingDal.GetAdminUser(this.getUserID.Text, this.getPassword.Text);
        if (m != null)
        {
            this.loginUser = new LoginUser(m.AdminUserID, TimeNow.TheTimeNow);
            this.loginUser.Login(TimeNow.TheTimeNow);
            this.logger.Log(new AdminException
                    (". User " + m.UserID + " Was Successfully Logged In"), MethodBase.GetCurrentMethod().Name);

            this.Session["login"] = this.loginUser;

            this.mainSelector.Visible = true;
            this.login.Visible = false;

            this.Session["tab"] = "tabs1";
            Response.Redirect("~/Admin/Contents.aspx");
        }
        else
        {
            this.logger.Warn(new AdminException(". Incorrect User ID " + this.getUserID.Text + " Or Password " + this.getPassword.Text),
            MethodBase.GetCurrentMethod().Name);
            this.errorLoginLabel.Text = "Wrong User ID Or Password";
        }

    }

    protected void tabs1_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs1");
        Response.Redirect("~/Admin/Contents.aspx");
    }
    protected void tabs2_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs2");
        Response.Redirect("~/Admin/Articles.aspx");
    }
    protected void tabs3_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs3");
        Response.Redirect("~/Admin/MailLists.aspx");
    }
    protected void tabs4_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs4");
        Response.Redirect("~/Admin/Graduates.aspx");
    }
    protected void tabs5_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs5");
        Response.Redirect("~/Admin/Logs.aspx");
    }
    protected void tabs6_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs6");
        Response.Redirect("~/Admin/Leads.aspx");
    }

    protected void tabs7_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs7");
        Response.Redirect("~/Admin/News.aspx");
    }

    protected void tabs8_Click(object sender, EventArgs e)
    {
        this.SelectTab("tabs8");
        Response.Redirect("~/Admin/Admins.aspx");
    }

    private void SelectTab(string tabID)
    {
        if (tabID == "" || tabID == null)
        {
            this.logger.Error(new AdminException
            (". tabID == \"\" || tabID == null"), MethodBase.GetCurrentMethod().Name);
            this.notifier.Notify(23, "Red", "");
            return;
        }

        this.Session["tab"] = tabID;
        this.ClearTabs();

        switch (tabID)
        {
            case "tabs1":
                this.tabs1.CssClass = "defaulttab2";
                break;
            case "tabs2":
                this.tabs2.CssClass = "defaulttab2";
                break;
            case "tabs3":
                this.tabs3.CssClass = "defaulttab2";
                break;
            case "tabs4":
                this.tabs4.CssClass = "defaulttab2";
                break;
            case "tabs5":
                this.tabs5.CssClass = "defaulttab2";
                break;
            case "tabs6":
                this.tabs6.CssClass = "defaulttab2";
                break;
            case "tabs7":
                this.tabs7.CssClass = "defaulttab2";
                break;
            case "tabs8":
                this.tabs8.CssClass = "defaulttab2";
                break;
        }
    }

    private void ClearTabs()
    {
        this.tabs1.CssClass = "";
        this.tabs2.CssClass = "";
        this.tabs3.CssClass = "";
        this.tabs4.CssClass = "";
        this.tabs5.CssClass = "";
        this.tabs6.CssClass = "";
        this.tabs7.CssClass = "";
        this.tabs8.CssClass = "";
    }

    public void Exit()
    {
        this.loginUser.Logoff();
        this.Session["login"] = null;
        this.mainSelector.Visible = false;
        this.login.Visible = true;
        this.main.Style["visibility"] = "hidden";
        this.main.Visible = false;
        this.getUserID.Text = "";
    }

    protected void exit_Click(object sender, EventArgs e)
    {
        this.logger.Log(new AdminException("User " + this.loginUser.LoginID +
        " Was Successfully Logoff"), MethodBase.GetCurrentMethod().Name);

        this.Exit();
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
    }


    public void Notify(string[] message)
    {
        this.cancelBut.Visible = false;

        try
        {
            if (message == null || message.Count() != 3)
            {
                throw new AdminException(". message == null || message.Count() != 3");
            }

            if (message[0] == "" || message[0] == null ||
                message[1] == "" || message[1] == null ||
                message[2] == "" || message[2] == null)
            {
                throw new AdminException(@". message[0] == """" || message[0] == null || message[1] == """" ||
                                            message[1] == null || message[2] == """" || message[2] == null");
            }

            if (message[1] == "Red")
            {
                this.masterNotifyLabel.ForeColor = Color.Red;
            }
            if (message[1] == "White")
            {
                this.masterNotifyLabel.ForeColor = Color.White;
            }

            this.masterNotifyLabel.Text = message[0];
        }
        catch (Exception e)
        {
            this.logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.masterNotifyLabel.ForeColor = Color.Red;
            this.masterNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
    }
}