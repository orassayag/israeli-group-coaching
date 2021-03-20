using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;

public partial class Admin_Leads : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.leadGrid.DataSource = this.Master._CoachingDal.GetAll("lead");
        this.leadGrid.DataBind();

        this.DivSwitcher(1);
    }

    protected void leadReadBut_Click(object sender, EventArgs e)
    {
        string leadID = "";

        try
        {
            leadID = ((LinkButton)sender).CommandName;

        }
        catch (Exception j)
        {
            this.Master._Logger.Error(j, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        Lead lead = (Lead)this.Master._CoachingDal.Get("lead", leadID);
        if (lead == null)
        {
            this.Master._Logger.Error(new AdminException(". lead == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.leadHidden.Value = lead.LeadID;
        this.leadsReadDate.Text = lead.spLeadDate;
        this.leadsReadName.Text = lead.LeadName;
        this.leadsReadTitle.Text = lead.LeadTitle;
        this.leadsReadBody.Text = lead.LeadBody;
        this.leadsReadMail.Text = lead.LeadMail;

        this.DivSwitcher(2);
    }

    protected void leadRemoveBut_Click(object sender, EventArgs e)
    {
        string leadID = "";

        try
        {
            leadID = ((LinkButton)sender).CommandName;

        }
        catch (Exception j)
        {
            this.Master._Logger.Error(j, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        Lead lead = (Lead)this.Master._CoachingDal.Get("lead", leadID);
        if (lead == null)
        {
            this.Master._Logger.Error(new AdminException(". lead == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.Notify(this.Master._Notifier.Notify(1, "Red", lead.LeadTitle));
        this.leadHidden.Value = leadID;
    }

    private void RemoveLead()
    {
        if (this.leadHidden.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.leadHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        Lead lead = (Lead)this.Master._CoachingDal.Get("lead", this.leadHidden.Value);
        if (lead == null)
        {
            this.Master._Logger.Error(new AdminException(". lead == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        try
        {
            this.Master._CoachingDal.Delete("lead", lead.LeadID);
            this.Master._Logger.Log(new AdminException(". " + lead.LeadName + " Was Successfully Removed"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(28, "White", lead.LeadName));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(38, "Red", lead.LeadName));
        }
        finally
        {
            this.leadGrid.DataBind();
        }
    }

    protected void leadsBackButton_Click(object sender, EventArgs e)
    {
        this.leadsHidden.Value = "";
        this.DivSwitcher(1);
    }

    protected void leadsDeleteButton_Click(object sender, EventArgs e)
    {
        if (this.leadHidden.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.leadHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.Notify(this.Master._Notifier.Notify(1, "Red", this.leadsReadTitle.Text));
    }

    private void DivSwitcher(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException
                (". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.leadsData.Visible = false;
        this.leadsRead.Visible = false;
        this.leadsNotify.Visible = false;

        this.leadsData.Attributes["class"] = "unVisi";
        this.leadsRead.Attributes["class"] = "unVisiHeb";
        this.leadsNotify.Attributes["class"] = "unVisi";

        switch (action)
        {
            case 1:
                this.leadsData.Visible = true;
                this.leadsData.Attributes["class"] = "visi";
                break;
            case 2:
                this.leadsRead.Visible = true;
                this.leadsRead.Attributes["class"] = "visiPro";
                break;
            case 3:
                this.leadsNotify.Visible = true;
                this.leadsNotify.Attributes["class"] = "visi";
                break;
            default:
                break;
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (this.leadsHidden.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                            (". this.leadsHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
        }

        this.AfterOk(this.leadsHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.DivSwitcher(1);
        this.leadsHidden.Value = "";
    }

    private void AfterOk(string selector)
    {
        if (selector == "")
        {
            this.Master._Logger.Error(new AdminException
                            (". selector == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
        }

        switch (selector)
        {
            case "1":
                this.RemoveLead();
                break;
            case "38":
            case "28":
                this.DivSwitcher(1);
                break;
            case "23":
                this.Master.Exit();
                break;
            default:
                break;
        }

        if (selector != "1")
        {
            this.leadsHidden.Value = "";
        }
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
                this.leadsNotifyLabel.ForeColor = Color.Red;
            }
            else
            {
                this.leadsNotifyLabel.ForeColor = Color.Green;
            }

            this.leadsNotifyLabel.Text = message[0];
            this.leadsHidden.Value = message[2];

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
            this.leadsNotifyLabel.ForeColor = Color.Red;
            this.leadsNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.Master._MainSelector = false;
            this.DivSwitcher(3);
        }
    }
}
