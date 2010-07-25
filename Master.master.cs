using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Text;

public partial class Master : System.Web.UI.MasterPage
{
    private CoachDal coachingDal = new CoachDal();
    private GlobalFunctions globalFunctions = new GlobalFunctions();
    private Logger logger = new Logger();

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

    public HtmlHead Header
    {
        get { return this.Head1; }
        set
        {
            if (value != null)
            {
                this.Head1 = value;
            }
        }
    }

    private void SpaceCreator()
    {
        HtmlGenericControl space = new HtmlGenericControl("li");
        HtmlGenericControl spanSpace = new HtmlGenericControl("span");
        spanSpace.InnerText = "|";

        space.Controls.Add(spanSpace);
        this.contentMenu.Controls.Add(space);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int x = 0;

        foreach (ContentP page in (IEnumerable<ContentP>)this.coachingDal.GetAll("content"))
        {
            if (x == 1)
            {
                this.SpaceCreator();
                x = 0;
            }
            x++;

            HtmlGenericControl li1 = new HtmlGenericControl("li");
            li1.ID = "c" + page.ContentID;

            HtmlGenericControl a = new HtmlGenericControl("a");


            if (page.IsLinkPage == 1)
            {
                a.Attributes.Add("href", page.ContentLink);
            }
            else
            {
                switch (page.ContentPageType)
                {
                    case "Content":
                        a.Attributes.Add("href", "ContactPage.aspx?pageID=" + page.ContentID);
                        break;
                    case "Home":
                        a.Attributes.Add("href", "Default.aspx");
                        break;
                    case "Graduates":
                        a.Attributes.Add("href", "Graduates.aspx");
                        break;
                    default:
                        break;
                }
            }


            HtmlGenericControl span1 = new HtmlGenericControl("span");
            span1.InnerText = page.ContentButtonTitle;

            a.Controls.Add(span1);
            li1.Controls.Add(a);
            this.contentMenu.Controls.Add(li1);
        }

        HtmlGenericControl li = new HtmlGenericControl("li");
        li.ID = "cLead";

        HtmlGenericControl link = new HtmlGenericControl("a");
        link.Attributes.Add("href", "LeadPage.aspx");

        HtmlGenericControl span = new HtmlGenericControl("span");
        span.InnerText = "צור קשר";

        this.SpaceCreator();

        link.Controls.Add(span);
        li.Controls.Add(link);
        this.contentMenu.Controls.Add(li);

        foreach (New news in (IEnumerable<New>)this.coachingDal.GetAll("news"))
        {
            HtmlGenericControl li2 = new HtmlGenericControl("li");
            li2.InnerHtml = news.NewsContentLink;
            this.textNews.Controls.Add(li2);
        }

        if (this.Session["tab"] != null)
        {
            HtmlGenericControl control = (HtmlGenericControl)this.FindControl((string)this.Session["tab"]);
            control.Style.Add("font-weight", "bold");
        }
    }

    private void ClearMail()
    {
        this.getMail.Text = "";
        this.getName.Text = "";
    }

    protected void submitBut_OnClick(object sender, EventArgs e)
    {
        if (this.getName.Text == "")
        {
            this.messageLabel.Text = "אנא הכנס/י את שמך";
            return;
        }

        if (this.getMail.Text == "")
        {
            this.messageLabel.Text = "כתובת אימייל לא חוקית";
            return;
        }

        if (!this.globalFunctions.ValidateMailAddress(this.getMail.Text))
        {
            this.messageLabel.Text = "כתובת אימייל לא חוקית";
            return;
        }

        if (this.coachingDal.GetMailListByMailAddress(this.getMail.Text) != null)
        {
            this.logger.Warn(new AdminException(". this.coachingDal.GetMailListByMailAddress(this.getMail.Text) != null"),
                MethodBase.GetCurrentMethod().Name);
            this.messageLabel.Text = "תודה רבה!";
            this.ClearMail();
            return;
        }

        try
        {
            MailList mail = new MailList
            {
                Active = 1,
                MailListID = this.coachingDal.GetNextAvailableID("mailList"),
                MailListJoinTime = TimeNow.TheTimeNow,
                MailListLastUpdate = TimeNow.TheTimeNow,
                MailListMail = this.getMail.Text,
                MailListName = this.getName.Text,
                MailRemoveCode = this.globalFunctions.GetRemoveCode(),
                spActvie = "Enable",
                spMailListJoinTime = TimeNow.TheTimeNow.ToShortDateString(),
                spMailListLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };


            this.coachingDal.Add("mailList", mail, TimeNow.TheTimeNow);
            this.logger.Log(new AdminException(". " + mail.MailListName + " " +
            mail.MailListMail + " Was Successfully Added"), MethodBase.GetCurrentMethod().Name);
            this.messageLabel.Text = "!תודה רבה";
        }
        catch (Exception r)
        {
            this.logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.messageLabel.Text = "הצטרפותך נכשלה, אנא נסה שוב מאוחר יותר";
        }
        finally
        {
            this.ClearMail();
        }
    }
}
