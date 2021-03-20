using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;

public partial class Admin_Articles : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ListItem d = null;
            ListItem h = null;
            ListItem j = null;

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        h = new ListItem("--Select Page--", i.ToString());
                        d = new ListItem("--Select Action--", i.ToString());
                        j = new ListItem("--Select Language--", i.ToString());
                        break;
                    case 1:
                        d = new ListItem("Add New Page", i.ToString());
                        j = new ListItem("עברית", i.ToString());
                        break;
                    case 2:
                        d = new ListItem("Delete/Update Page", i.ToString());
                        j = new ListItem("English", i.ToString());
                        break;

                    default:
                        break;
                }

                if (d != null)
                {
                    this.articleSelector.Items.Add(d);
                }

                if (j != null)
                {
                    this.articleLanguageSelector.Items.Add(j);
                }

                if (h != null)
                {
                    this.articleRemoveUpdateSelector.Items.Add(h);
                }

                d = null;
                h = null;
                j = null;
            }

            foreach (Article article in (IEnumerable<Article>)this.Master._CoachingDal.GetAll("article"))
            {
                ListItem t = new ListItem(article.ArticleTitle, "s" + article.ArticleID);
                this.articleRemoveUpdateSelector.Items.Add(t);
            }

            this.Start();
        }
    }

    protected void articleSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.articleSelector.SelectedIndex == 0)
        {
            this.Master._Logger.Warn(new AdminException(". this.articleSelector.SelectedIndex == 0"),
                             MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(3, "Red", ""));
            return;
        }

        this.articleDivSelector(this.articleSelector.SelectedValue);
    }

    protected void articleRemoveButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            this.ClearFields();
            this.articleHiddenRe.Value = "";
            return;
        }

        this.articleDivSelector("2");
        this.articleHiddenRe.Value = this.articleRemoveUpdateSelector.SelectedValue.Remove(0, 1);

        this.Notify(this.Master._Notifier.Notify(1, "Red", this.articleRemoveUpdateSelector.SelectedItem.Text));
    }


    protected void articleCancelButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void articleUpdateButton_Click(object sender, EventArgs e)
    {
        this.UpdatePageInit();
    }

    private void UpdatePageInit()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearFields();
            this.articleHiddenUp.Value = "";
            return;
        }

        Article p = (Article)this.Master._CoachingDal.Get("article", this.articleRemoveUpdateSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException(". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.articleUpdateTime.Text = p.spLastUpdate;
        this.articleDescription.Text = p.ArticleDescription;
        this.articleTitle.Text = p.ArticleTitle;
        this.articleLanguageSelector.SelectedIndex = int.Parse(p.ArticleLanguageID.Trim());
        this.articleKeyWords.Text = p.ArticleKeyWords;
        this.articleEditor.Value = p.ArticleContent;
        this.articleLink.Text = p.ArticleLink;
        this.articleFullPageLink.Text = p.ArticleFullPageLink;

        this.articleHiddenUp.Value = p.ArticleID;

        this.articleDivSelector("1");
    }


    private void ClearSelection()
    {
        foreach (ListItem m in this.articleSelector.Items)
        {
            m.Selected = false;
        }
        this.articleSelector.Items[0].Selected = true;

        foreach (ListItem m in this.articleRemoveUpdateSelector.Items)
        {
            m.Selected = false;
        }
        this.articleRemoveUpdateSelector.Items[0].Selected = true;
    }

    private void articleDivSelector(string action)
    {
        this.addArticle.Attributes["class"] = "unVisi";
        this.removeUpdateArticle.Attributes["class"] = "unVisi";
        this.articlesNotify.Attributes["class"] = "unVisi";
        this.articleSelectorDiv.Attributes["class"] = "unVisi";

        this.addArticle.Visible = false;
        this.removeUpdateArticle.Visible = false;
        this.articlesNotify.Visible = false;
        this.articleSelectorDiv.Visible = false;

        switch (action)
        {
            case "start":
                this.articleSelectorDiv.Visible = true;
                this.articleSelectorDiv.Attributes["class"] = "visi";
                break;
            case "1":
                this.addArticle.Attributes["class"] = "visi";
                this.addArticle.Visible = true;
                break;
            case "2":
                this.removeUpdateArticle.Attributes["class"] = "visi";
                this.removeUpdateArticle.Visible = true;
                break;
            case "3":
                this.articlesNotify.Visible = true;
                this.articlesNotify.Attributes["class"] = "visi";
                break;
            default:
                break;
        }
    }

    private void RemovePage()
    {
        if (this.articleHiddenRe.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                    (". this.articleHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.ClearFields();
            this.articleHiddenRe.Value = "";
            return;
        }

        try
        {
            Article p = (Article)(this.Master._CoachingDal.Get("article", this.articleHiddenRe.Value));
            if (p == null)
            {
                this.Master._Logger.Error(new AdminException
                    (". p == null"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(11, "Red", ""));
                return;
            }

            this.Master._CoachingDal.Delete("article", p.ArticleID);
            this.Master._Logger.Log(new AdminException(string.Format(". Page article {0} Was Successfully Removed", p.ArticleID)),
                            MethodBase.GetCurrentMethod().Name);
            this.articleRemoveUpdateSelector.Items.Remove(this.articleRemoveUpdateSelector.Items.FindByValue("s" + p.ArticleID));

            this.Notify(this.Master._Notifier.Notify(2, "White", p.ArticleTitle));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
        }
        finally
        {
            this.ClearFields();
            this.articleHiddenRe.Value = "";
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
                if (this.articleLanguageSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.articleLanguageSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(18, "Red", ""));
                    return false;
                }

                if (this.articleTitle.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.articleTitle.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(14, "Red", ""));
                    return false;
                }

                if (this.articleKeyWords.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.articleKeyWords.Tex == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(16, "Red", ""));
                    return false;
                }

                if (this.articleDescription.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.articleDescription.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(15, "Red", ""));
                    return false;
                }

                if (this.articleEditor.Value == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.articleEditor.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(17, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.articleRemoveUpdateSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.articleRemoveUpdateSelector.SelectedIndex == 0"),
                                MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(62, "Red", ""));
                    return false;
                }
                break;
            default:
                break;
        }
        return true;
    }

    protected void articleEditorSaveButton_Click(object sender, EventArgs e)
    {
        if (this.articleHiddenUp.Value == "")
        {
            this.AddPage();
        }
        else
        {
            this.UpdatePage();
        }
    }

    private void UpdatePage()
    {
        if (this.articleHiddenUp.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                        (". this.articleHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.ClearFields();
            this.ClearLanguageSelector();
            this.articleHiddenUp.Value = "";
            return;
        }

        if (!this.ValidateFields(1))
        {
            this.ClearFields();
            this.ClearLanguageSelector();
            this.articleHiddenUp.Value = "";
            return;
        }

        try
        {
            Article p = (Article)(this.Master._CoachingDal.Get("article", this.articleHiddenUp.Value));
            if (p == null)
            {
                this.Master._Logger.Error(new AdminException
                    (". p == null"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(11, "Red", ""));
                return;
            }

            ListItem m = this.articleRemoveUpdateSelector.Items.FindByValue("s" + p.ArticleID);
            this.articleRemoveUpdateSelector.Items.Remove(m);

            p.ArticleID = p.ArticleID;
            p.ArticleContent = this.articleEditor.Value;
            p.ArticleDescription = this.articleDescription.Text;
            p.ArticleKeyWords = this.articleKeyWords.Text;
            p.ArticleLanguageID = this.articleLanguageSelector.SelectedIndex.ToString();
            p.ArticleTitle = this.articleTitle.Text;
            p.LastUpdate = TimeNow.TheTimeNow;
            p.spLastUpdate = TimeNow.TheTimeNow.ToString();

            this.Master._CoachingDal.Update("article", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new Exception(". " + p.ArticleID + " "
                            + p.ArticleTitle + " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);

            m = new ListItem(p.ArticleTitle, "s" + p.ArticleID);
            this.articleRemoveUpdateSelector.Items.Add(m);

            this.Notify(this.Master._Notifier.Notify(29, "White", p.ArticleTitle));

        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", this.articleTitle.Text));
        }
        finally
        {
            this.ClearFields();
            this.ClearLanguageSelector();
            this.articleHiddenUp.Value = "";
        }
    }

    private void AddPage()
    {
        if (!ValidateFields(1))
        {
            this.ClearFields();
            this.ClearLanguageSelector();
            return;
        }

        string title = "";

        try
        {
            title = this.articleTitle.Text;

            string id = this.Master._CoachingDal.GetNextAvailableID("article");

            Article p = new Article
            {
                ArticleID = id,
                ArticleContent = this.articleEditor.Value,
                ArticleTitle = articleTitle.Text,
                ArticleDescription = this.articleDescription.Text,
                ArticleKeyWords = this.articleKeyWords.Text,
                ArticleLink = "http://www.malliere.com/ArticlePage.aspx?pageID=" + id,
                ArticleFullPageLink = "http://www.malliere.com/FullPage.aspx?pageID=" + id + "&type=article",
                ArticleLanguageID = this.articleLanguageSelector.SelectedIndex.ToString(),
                CreationTime = TimeNow.TheTimeNow,
                LastUpdate = new DateTime(1900, 1, 1),
                spLastUpdate = "1900/1/1"
            };

            this.Master._CoachingDal.Add("article", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new Exception("Page" + p.ArticleTitle + " Was Successfully Added"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(72, "White", p.ArticleTitle + "," + p.ArticleID));

            ListItem m = new ListItem(p.ArticleTitle, "s" + p.ArticleID);
            this.articleRemoveUpdateSelector.Items.Add(m);
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(11, "Red", this.articleTitle.Text));
        }
        finally
        {
            this.ClearFields();
            this.ClearLanguageSelector();
        }
    }

    protected void articleEditorCancelButton_Click(object sender, EventArgs e)
    {
        this.ClearFields();
        this.Start();
    }

    private void ClearLanguageSelector()
    {
        this.articleKeyWords.Attributes["class"] = "";
        this.articleTitle.Attributes["class"] = "";
        this.articleDescription.Attributes["class"] = "";
    }

    private void ClearFields()
    {
        this.articleFullPageLink.Text = "";
        this.articleLink.Text = "";
        this.articleKeyWords.Text = "";
        this.articleDescription.Text = "";
        this.articleEditor.Value = "";
        this.articleTitle.Text = "";
        this.articleUpdateTime.Text = "";
        this.articleSelector.SelectedIndex = 0;
        this.articleLanguageSelector.SelectedIndex = 0;
        this.articleRemoveUpdateSelector.SelectedIndex = 0;
    }


    protected void okBut_Click(object sender, EventArgs e)
    {
        if (this.articleHiddenRe.Value == "")
        {
            this.Start();
        }
        else
        {
            this.RemovePage();
        }
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
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
                this.articlesNotifyLabel.ForeColor = Color.Red;
            }
            else
            {
                this.articlesNotifyLabel.ForeColor = Color.Green;
            }

            this.articlesNotifyLabel.Text = message[0];

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
            this.articlesNotifyLabel.ForeColor = Color.Red;
            this.articlesNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.Master._MainSelector = false;
            this.articleDivSelector("3");
        }
    }

    private void Start()
    {
        this.articleDivSelector("start");
        this.ClearSelection();
    }
}
