using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;

public partial class Graduates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ContentP p = null;
        foreach (ContentP page in (IEnumerable<ContentP>)this.Master._CoachingDal.GetAll("content"))
        {
            if (page.ContentPageType == "Graduates")
            {
                p = page;

                if (page.ContentLanguageID.Trim() == "1")
                {
                    this.intro.Attributes["class"] = "hebrew";
                }

                if (page.ContentLanguageID.Trim() == "2")
                {
                    this.intro.Attributes["class"] = "english";
                }
                break;
            }
        }

        if (p == null)
        {
            this.SetError("You Don't Have Graduates Page, Please Create One In Admin.aspx And Try Again");
            return;
        }
        try
        {
            this.intro.InnerHtml = p.ContentContent;
            this.Master._GlobalFunctions.SetPageSEO(p.ContentTitle, p.ContentDescription, p.ContentKeyWords, this.Page);
        }
        catch (Exception t)
        {
            this.Master._Logger.Error(t, MethodBase.GetCurrentMethod().Name);
            this.SetError("Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator");
        }

        if (!Page.IsPostBack)
        {
            this.graduatesRepeater.DataSource = this.Master._CoachingDal.GetAll("session");
        }

        this.Session["tab"] = "c" + p.ContentID;
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        this.graduatesRepeater.DataBind();
    }

    private void SetError(string error)
    {
        this.intro.Controls.Clear();
        this.intro.InnerText = "";
        this.intro.InnerHtml = "";
        Label l = new Label();
        l.CssClass = "english";
        l.ForeColor = Color.Red;
        l.Text = error;
        this.intro.Controls.Add(l);
    }
}
