using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;
using System.Reflection;

public partial class LeadPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Session["tab"] = "cLead";
    }

    protected void sendLead_Click(object sender, EventArgs e)
    {
        if (!this.CheckLead())
        {
            return;
        }

        this.SendLead();
    }

    private void SendLead()
    {
        UTF8Encoding leadConvertor = new UTF8Encoding();
        byte[] le1 = leadConvertor.GetBytes(this.getLeadName.Text);
        string name = leadConvertor.GetString(le1);

        byte[] le2 = leadConvertor.GetBytes(this.getLeadTitle.Text);
        string title = leadConvertor.GetString(le1);

        byte[] le3 = leadConvertor.GetBytes(this.getLeadBody.Text);
        string body = leadConvertor.GetString(le1);

        try
        {
            Lead l = new Lead()
            {
                LeadBody = body,
                LeadDate = TimeNow.TheTimeNow,
                LeadID = this.Master._CoachingDal.GetNextAvailableID("lead"),
                LeadMail = this.getLeadMail.Text,
                LeadName = name,
                LeadTitle = title,
                spLeadDate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._CoachingDal.Add("lead", l, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". Lead " + title +
                        "Was Successfully Received"), MethodBase.GetCurrentMethod().Name);
            this.leadLabel.ForeColor = Color.Blue;
            this.leadLabel.Text = "ההודעה נשלחה בהצלחה";

        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.leadLabel.ForeColor = Color.Red;
            this.leadLabel.Text = "אנא נס/י לשלוח שוב";
        }
        finally
        {
            this.ClearFields(1);
        }
    }

    protected void cancelLead_Click(object sender, EventArgs e)
    {
        this.ClearFields(2);
        Response.Redirect("Default.aspx");
    }

    private void ClearFields(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException
            (". action <= 0"), MethodBase.GetCurrentMethod().Name);
            Response.Redirect("Default.aspx?error=true");
        }

        this.getLeadName.Text = "";
        this.getLeadMail.Text = "";
        this.getLeadTitle.Text = "";
        this.getLeadBody.Text = "";
        if (action == 2)
        {
            this.leadLabel.Text = "";
        }
    }

    private bool CheckLead()
    {
        if (this.getLeadName.Text == "")
        {
            this.leadLabel.Text = "אנא הכנס/י את שמך";
            return false;
        }

        if (this.getLeadMail.Text == "")
        {
            this.leadLabel.Text = "אנא הכנס/י דואר אלקטרוני";
            return false;
        }

        if (this.getLeadTitle.Text == "")
        {
            this.leadLabel.Text = "אנא הכנס/י נושא";
            return false;
        }

        if (this.getLeadBody.Text == "")
        {
            this.leadLabel.Text = "אנא הכנס/י הודעה";
            return false;
        }
        return true;
    }
}
