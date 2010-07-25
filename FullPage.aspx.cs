using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.UI.HtmlControls;

public partial class FullPage : System.Web.UI.Page
{
    private Logger _logger = new Logger();
    private CoachDal _coachingDal = new CoachDal();
    private GlobalFunctions _globalFunctions = new GlobalFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["type"] == null)
        {
            this._logger.Error(new AdminException
            (". Request[\"type\"] == null"), MethodBase.GetCurrentMethod().Name);
            Response.Redirect("Default.aspx?error=true");
        }

        if (Request["pageID"] == null)
        {
            this._logger.Error(new AdminException
            (". Request[\"pageID\"] == null"), MethodBase.GetCurrentMethod().Name);
            Response.Redirect("Default.aspx?error=true");
        }

        if (Request["type"] == "content")
        {
            try
            {
                ContentP page = (ContentP)this._coachingDal.Get("content", (string)Request["pageID"]);
                if (page == null)
                {
                    throw new AdminException(". page == null");
                }

                if (page.ContentLanguageID.Trim() == "1")
                {
                    this.content.Attributes["class"] = "hebrew";
                }

                if (page.ContentLanguageID.Trim() == "2")
                {
                    this.content.Attributes["class"] = "english";
                }

                this._globalFunctions.SetPageSEO
                (page.ContentTitle, page.ContentDescription, page.ContentKeyWords, this.Page);

                this.content.InnerHtml = this._globalFunctions.FixBackUrl(page.ContentContent);

                this.Session["tab"] = "c" + page.ContentID;
            }
            catch (Exception y)
            {
                this._logger.Error(y, MethodBase.GetCurrentMethod().Name);
                Response.Redirect("Default.aspx?error=true");
            }
        }

        if (Request["type"] == "article")
        {
            try
            {
                Article page = (Article)this._coachingDal.Get("article", (string)Request["pageID"]);
                if (page == null)
                {
                    throw new AdminException(". page == null");
                }

                if (page.ArticleLanguageID.Trim() == "1")
                {
                    this.content.Attributes["class"] = "hebrew";
                }

                if (page.ArticleLanguageID.Trim() == "2")
                {
                    this.content.Attributes["class"] = "english";
                }

                this._globalFunctions.SetPageSEO
                (page.ArticleTitle, page.ArticleDescription, page.ArticleKeyWords, this.Page);

                this.content.InnerHtml = this._globalFunctions.FixBackUrl(page.ArticleContent); 
            }
            catch (Exception u)
            {
                this._logger.Error(u, MethodBase.GetCurrentMethod().Name);
                Response.Redirect("Default.aspx?error=true");
            }
        }

        if (Request["type"] == "graduates")
        {
            if (Request["sessionID"] == null)
            {
                this._logger.Error(new AdminException
                (". Request[\"sessionID\"] == null"), MethodBase.GetCurrentMethod().Name);
                Response.Redirect("Default.aspx?error=true");
            }

            GraduateSession session = (GraduateSession)this._coachingDal.Get("session", Request["sessionID"]);
            if (session == null)
            {
                this._logger.Error(new AdminException
                (". session == null"), MethodBase.GetCurrentMethod().Name);
                Response.Redirect("Default.aspx?error=true");
            }

            this.content.Attributes["class"] = "graduate";

            StringBuilder build = new StringBuilder(@"<div class=""space3""></div>");

            foreach (Graduate g in this._coachingDal.GetAllGraduatesBySession(Request["sessionID"]))
            {
                build.Append(g.GraduateName + "<br />");
            }


            this.content.InnerHtml = build.ToString();

        }
    }
}
