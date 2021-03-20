using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;

public partial class Admin_Admins : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            adminSelector.Items.Add(new ListItem("--Select Action--", "0"));
            adminSelector.Items.Add(new ListItem("Add Admin", "1"));
            adminSelector.Items.Add(new ListItem("Remove/Update Admin", "2"));

            ListItem m = new ListItem("--Select Admin--", "0");
            m.Selected = true;
            foreach (AdminUser k in (IEnumerable<AdminUser>)this.Master._CoachingDal.GetAll("admin"))
            {
                ListItem a = new ListItem(k.UserID, "s" + k.AdminUserID);
                this.removeUpdateAdminSelector.Items.Add(a);
            }
        }
    }

    protected void cancelRemoveUpdateAdmin_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void updateRemoveUpdateAdmin_Click(object sender, EventArgs e)
    {
        AdminUser m = (AdminUser)this.Master._CoachingDal.Get
                ("admin", this.removeUpdateAdminSelector.SelectedValue.Remove(0, 1));
        if (m == null)
        {
            this.Master._Logger.Error(new AdminException
                (". m == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.generalGetUserID.Text = m.UserID;
        this.generalGetPassword.Text = m.Password;
        this.generalGetPassword2.Text = m.Password;

        this.SwitchDivs(1);
        this.loginIDUp.Value = m.AdminUserID;
    }

    protected void removeRemoveUpdateAdmin_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(3))
        {
            this.RemoveAdmin();
            return;
        }

        AdminUser m = (AdminUser)this.Master._CoachingDal.Get
                      ("admin",this.removeUpdateAdminSelector.SelectedValue.Remove(0, 1));
        if (m == null)
        {
            this.Master._Logger.Error(new AdminException
                    (". m == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.loginIDRe.Value = m.AdminUserID;
        this.Notify(this.Master._Notifier.Notify(1,"Red",m.UserID));
    }

    protected void cancelAddAdmin_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void addAddAdmin_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        if (this.loginIDUp.Value == "")
        {
            this.AddAdmin();
        }
        else
        {
            this.UpdateAdmin();
            this.loginIDUp.Value = "";
        }
    }

    protected bool ValidateFields(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException
                (". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return false;
        }

        switch (action)
        {
            case 1:
                if (this.generalGetUserID.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.generalGetUserID.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(22, "Red", ""));
                    return false;
                }

                if (this.generalGetPassword.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.generalGetPassword.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(19, "Red", ""));
                    return false;
                }

                if (this.generalGetPassword2.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.generalGetPassword2.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(20, "Red", ""));
                    return false;
                }

                if (this.generalGetPassword.Text != this.generalGetPassword2.Text)
                {
                    this.Master._Logger.Warn(new AdminException(". this.generalGetPassword.Text != this.generalGetPassword2.Text"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(21, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.adminSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.adminSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(3, "Red", ""));
                    return false;
                }
                break;
            default:
                break;
        }
        return true;
    }

    protected void adminSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            return;
        }

        this.ClearFields(2);
        this.SwitchDivs(this.adminSelector.SelectedIndex);
    }

    private void SwitchDivs(int action)
    {
        this.addAdmin.Visible = false;
        this.removeUpdateAdmin.Visible = false;
        this.adminsNotify.Visible = false;

        this.addAdmin.Attributes["class"] = "unVisi";
        this.removeUpdateAdmin.Attributes["class"] = "unVisi";
        this.adminsNotify.Attributes["class"] = "unVisi";

        switch (action)
        {
            case 1:
                this.addAdmin.Visible = true;
                this.addAdmin.Attributes["class"] = "visi";
                break;
            case 2:
                this.removeUpdateAdmin.Visible = true;
                this.removeUpdateAdmin.Attributes["class"] = "visi";
                break;
            case 3:
                this.adminsNotify.Visible = true;
                this.adminsNotify.Attributes["class"] = "visi";
                break;
            default:
                break;
        }
    }

    public void AddAdmin()
    {
        if (this.Master._CoachingDal.GetAdminUser(this.generalGetUserID.Text, this.generalGetPassword2.Text) != null)
        {
            this.Master._Logger.Warn(new AdminException("User Trying To Insert " + this.generalGetUserID.Text + " And " +
                            this.generalGetPassword2.Text + "And They Are Already Exists"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red",
                                this.generalGetUserID.Text + " And " + this.generalGetPassword2.Text));
            return;
        }

        AdminUser h = new AdminUser
        {
            AdminUserID = this.Master._CoachingDal.GetNextAvailableID("admin"),
            UserID = this.generalGetUserID.Text,
            Password = this.generalGetPassword2.Text,
            CreationTime = TimeNow.TheTimeNow,
            LastLogin = TimeNow.TheTimeNow,
            spCreationTime = TimeNow.TheTimeNow.ToShortDateString(),
            spLastLogin = TimeNow.TheTimeNow.ToShortDateString(),
            LastUpdate = TimeNow.TheTimeNow,
            spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
        };

        try
        {
            this.Master._CoachingDal.Add("admin", h, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(h.UserID + " Was Successfully Added"),
                                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(26, "White", h.UserID));

            this.removeUpdateAdminSelector.Items.Add(new ListItem(h.UserID, "s" + h.AdminUserID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(10, "Red", h.UserID));
        }
    }

    public void RemoveAdmin()
    {
        if (this.loginIDRe.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                    (". this.loginIDRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.loginIDRe.Value = "";
            return;
        }

        AdminUser m = (AdminUser)this.Master._CoachingDal.Get("admin", this.loginIDRe.Value);

        if (m == null)
        {
            this.Master._Logger.Error(new AdminException
                    (". m == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.loginIDRe.Value = "";
            return;
        }

        if (this.Master._CoachingDal.GetCount("admin") == 1)
        {
            this.Master._Logger.Warn(new AdminException("User Tyring To Remove Last Remaining Admin " +
                            m.UserID), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(24, "Red", m.UserID));
            this.loginIDRe.Value = "";
            return;
        }

        ListItem s = null;
        try
        {
            s = this.removeUpdateAdminSelector.SelectedItem;
            this.Notify(this.Master._Notifier.Notify(28, "White", m.UserID));
            this.Master._CoachingDal.Delete("admin", m.AdminUserID);
            this.removeUpdateAdminSelector.Items.Remove(s);
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(38, "Red", m.UserID));
            this.loginIDRe.Value = "";
        }
    }

    public void UpdateAdmin()
    {
        if (this.loginIDUp.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                    (". this.loginIDUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.loginIDUp.Value = "";
            return;
        }

        AdminUser m = (AdminUser)this.Master._CoachingDal.Get("admin", this.loginIDUp.Value);

        if (m == null)
        {
            this.Master._Logger.Error(new AdminException
                    (". m == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.loginIDUp.Value = "";
            return;
        }

        if (!this.ValidateFields(1))
        {
            this.loginIDUp.Value = "";
            return;
        }

        if (this.Master._CoachingDal.GetAdminUserExceptUserID(m.UserID, this.generalGetUserID.Text) != null)
        {
            this.Master._Logger.Warn(new AdminException(@". this.Master._CoachingDal.GetAdminUserExceptUserID
                                                        (m.UserID, this.generalGetUserID.Text) != null)"),
                                                             MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", this.generalGetUserID.Text));
            this.loginIDUp.Value = "";
            return;
        }

        if (this.Master._CoachingDal.GetAdminUserExceptPassword(m.Password, this.generalGetPassword2.Text) != null)
        {
            this.Master._Logger.Warn(new AdminException(@". this.Master._CoachingDal.GetAdminUserExceptUserID
                                                        (m.Password, this.generalGetPassword2.Text) != null)"),
                                                             MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(8, "Red", this.generalGetPassword2.Text));
            this.loginIDUp.Value = "";
            return;
        }

        this.removeUpdateAdminSelector.Items.FindByValue("s" + m.AdminUserID).Text = this.generalGetUserID.Text;

        m.UserID = this.generalGetUserID.Text;
        m.Password = this.generalGetPassword2.Text;
        try
        {
            this.Master._CoachingDal.Update("admin", m, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(m.UserID + " Was Successfully Updated"),
                                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(27, "White", m.UserID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(25, "Red", m.UserID));
            this.loginIDUp.Value = "";
        }
    }

    public void ClearFields(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException
                (". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (action)
        {
            case 1:
                this.generalGetUserID.Text = "";
                this.generalGetPassword.Text = "";
                this.generalGetPassword2.Text = "";
                this.adminLabel.Text = "";
                foreach (ListItem l in this.adminSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            case 2:
                foreach (ListItem l in this.removeUpdateAdminSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            default:
                break;
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (this.adminsHidden.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                 (". this.adminsHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.AfterOk(this.adminsHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void AfterOk(string selector)
    {
        if (selector == "")
        {
            this.Master._Logger.Error(new AdminException
                 (". selector == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (selector)
        {
            case "1":
                this.RemoveAdmin();
                break;
            case "25":
            case "27":
            //update
            case "8":
                //already exist
            case "28":
            case "38":
                //remove
            case "59":
                //select admin
            case "3":
                //select action
            case "10":
            case "26":
                //add
            case "19":
            case "20":
            case "21":
            case "22":
                this.Start();
                //validation add/update
                break;
            case "24":
                this.SwitchDivs(2);
                //cant remove last one
                break;
            case "23":
                this.ClearFields(1);
                this.ClearFields(2);
                this.Master.Exit();
                break;
            default:
                break;
        }

        if (selector != "1")
        {
            this.adminsHidden.Value = "";
            this.SwitchDivs(-1);
        }
    }

    private void Start()
    {
        this.ClearFields(1);
        this.SwitchDivs(-1);
    }

    private void Notify(string[] message)
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
                this.adminsNotifyLabel.ForeColor = Color.Red;
            }
            else
            {
                this.adminsNotifyLabel.ForeColor = Color.White;
            }

            this.adminsNotifyLabel.Text = message[0];
            this.adminsHidden.Value = message[2];

            switch (message[2])
            {
                case "1":
                    this.cancelBut.Visible = true;
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.adminsNotifyLabel.ForeColor = Color.Red;
            this.adminsNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.Master._MainSelector = false;
            this.SwitchDivs(3);
        }
    }
}