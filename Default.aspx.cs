using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Request["error"] != null)
        {
            this.SetError("Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator");
            return;
        }

        ContentP p = null;
        foreach (ContentP page in (IEnumerable<ContentP>)this.Master._CoachingDal.GetAll("content"))
        {
            if (page.ContentPageType == "Home")
            {
                p = page;

                if (page.ContentLanguageID.Trim() == "1")
                {
                    this.home.Attributes["class"] = "hebrew";
                }

                if (page.ContentLanguageID.Trim() == "2")
                {
                    this.home.Attributes["class"] = "english";
                }
                break;
            }
        }

        if (p == null)
        {
            this.SetError("You Don't Have Home Page, Please Create One In Admin.aspx And Try Again");
            return;
        }
        try
        {
            HtmlGenericControl css;
            css = new HtmlGenericControl();
            css.TagName = "link";
            css.Attributes.Add("rel", "stylesheet");

            if (p.ContentPageType == "Home")
            {
                css.Attributes.Add("href", "style1.css");

            }
            else
            {
                css.Attributes.Add("href", "style.css");
            }

            this.Master.Header.Controls.Add(css);

            this.home.InnerHtml = p.ContentContent;
            this.Master._GlobalFunctions.SetPageSEO(p.ContentTitle, p.ContentDescription, p.ContentKeyWords, this.Page);

            this.Session["tab"] = "c" + p.ContentID;
        }
        catch (Exception t)
        {
            this.Master._Logger.Error(t, MethodBase.GetCurrentMethod().Name);
            this.SetError("Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator");
        }
    }

    private void SetError(string error)
    {
        this.home.Controls.Clear();
        this.home.InnerText = "";
        this.home.InnerHtml = "";
        Label l = new Label();
        l.CssClass = "english";
        l.ForeColor = Color.Red;
        l.Text = error;
        this.home.Controls.Add(l);
    }
}
