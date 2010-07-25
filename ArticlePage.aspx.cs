using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class ArticlePage : System.Web.UI.Page
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
            Article page = (Article)this.Master._CoachingDal.Get("article", (string)Request["pageID"]);
            if (page == null)
            {
                throw new AdminException(". page == null");
            }

            if (page.ArticleLanguageID.Trim() == "1")
            {
                this.article.Attributes["class"] = "hebrew";
            }

            if (page.ArticleLanguageID.Trim() == "2")
            {
                this.article.Attributes["class"] = "english";
            }

            this.Master._GlobalFunctions.SetPageSEO
            (page.ArticleTitle, page.ArticleDescription, page.ArticleKeyWords, this.Page);

            this.article.InnerHtml = page.ArticleContent;
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            Response.Redirect("Default.aspx?error=true");
        }
    }
}
