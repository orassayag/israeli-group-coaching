using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class ContactPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadPage();
    }

    private void LoadPage()
    {
        if (Request["pageID"] == null)
        {
            this.Master._Logger.Error(new AdminException
            (". Request[\"pageID\"] == null"), MethodBase.GetCurrentMethod().Name);
            Response.Redirect("Default.aspx?error=true");
        }

        try
        {
            ContentP page = (ContentP)this.Master._CoachingDal.Get("content", (string)Request["pageID"]);
            if (page == null)
            {
                throw new AdminException(". page == null");
            }

            if (page.ContentLanguageID.Trim() == "1")
            {
                this.contact.Attributes["class"] = "hebrew";
            }

            if (page.ContentLanguageID.Trim() == "2")
            {
                this.contact.Attributes["class"] = "english";
            }

            this.Master._GlobalFunctions.SetPageSEO
            (page.ContentTitle, page.ContentDescription, page.ContentKeyWords, this.Page);

            this.contact.InnerHtml = page.ContentContent;

            this.Session["tab"] = "c" + page.ContentID;
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            Response.Redirect("Default.aspx?error=true");
        }
    }
}
