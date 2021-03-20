using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;

public partial class Admin_Contents : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ListItem d = null;
            ListItem h = null;
            ListItem j = null;
            ListItem y = null;

            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        h = new ListItem("--Select Page--", i.ToString());
                        y = new ListItem("--Select Page Type--", "None");
                        d = new ListItem("--Select Action--", i.ToString());
                        j = new ListItem("--Select Language--", i.ToString());
                        break;
                    case 1:
                        y = new ListItem("Content Page", "Content");
                        d = new ListItem("Add New Page", i.ToString());
                        j = new ListItem("עברית", i.ToString());
                        break;
                    case 2:
                        y = new ListItem("Home Page", "Home");
                        d = new ListItem("Add New Link Page", i.ToString());
                        j = new ListItem("English", i.ToString());
                        break;
                    case 3:
                        y = new ListItem("Graduates Page", "Graduates");
                        d = new ListItem("Delete/Update Page", i.ToString());
                        break;
                    default:
                        break;
                }

                if (d != null)
                {
                    this.contentSelector.Items.Add(d);
                }

                if (j != null)
                {
                    this.contentLanguageSelector.Items.Add(j);
                }

                if (h != null)
                {
                    this.contentRemoveUpdateSelector.Items.Add(h);
                }

                if (y != null)
                {
                    this.contentPageTypeSelector.Items.Add(y);
                }

                d = null;
                h = null;
                j = null;
                y = null;
            }

            foreach (ContentP content in (IEnumerable<ContentP>)this.Master._CoachingDal.GetAll("content"))
            {
                ListItem t = new ListItem(content.ContentButtonTitle, "s" + content.ContentID);
                this.contentRemoveUpdateSelector.Items.Add(t);
            }

            this.Start();
        }
    }

    protected void contentSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.contentSelector.SelectedIndex == 0)
        {
            this.Master._Logger.Warn(new AdminException(". this.contentSelector.SelectedIndex == 0"),
                             MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(3, "Red", ""));
            return;
        }

        this.ContentDivSelector(this.contentSelector.SelectedValue);
    }

    protected void contentRemoveButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            this.ClearFields(1);
            this.contentHiddenRe.Value = "";
            return;
        }

        this.ContentDivSelector("2");
        this.contentHiddenRe.Value = this.contentRemoveUpdateSelector.SelectedValue.Remove(0, 1);

        this.Notify(this.Master._Notifier.Notify(1, "Red", this.contentRemoveUpdateSelector.SelectedItem.Text));
    }


    protected void contentCancelButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    protected void contentUpdateButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            return;
        }

        ContentP p = (ContentP)this.Master._CoachingDal.Get("content", this.contentRemoveUpdateSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException(". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        if (p.IsLinkPage == 1)
        {
            this.UpdatePageLinkInit();
        }
        else
        {
            this.UpdatePageInit();
        }
    }

    private void UpdatePageInit()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearFields(1);
            this.contentHiddenUp.Value = "";
            return;
        }

        ContentP p = (ContentP)this.Master._CoachingDal.Get("content", this.contentRemoveUpdateSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException(". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.ClearFields(1);

        this.contentUpdateTime.Text = p.spLastUpdate;
        this.contentDescription.Text = p.ContentDescription;
        this.contentTitle.Text = p.ContentTitle;
        this.contentLanguageSelector.SelectedIndex = int.Parse(p.ContentLanguageID.Trim());
        this.contentKeyWords.Text = p.ContentKeyWords;
        this.contentEditor.Value = p.ContentContent;
        this.contentButtonTitle.Text = p.ContentButtonTitle;
        this.contentPageTypeSelector.Items.FindByValue(p.ContentPageType.Trim()).Selected = true;
        this.contentPlace.Text = p.ContentMenuPlace.ToString();
        this.contentLink.Text = p.ContentLink;
        this.contentFullPageLink.Text = p.ContentFullPageLink;

        this.contentHiddenUp.Value = p.ContentID;

        this.ContentDivSelector("1");
    }


    private void ClearSelection()
    {
        foreach (ListItem m in this.contentSelector.Items)
        {
            m.Selected = false;
        }
        this.contentSelector.Items[0].Selected = true;

        foreach (ListItem m in this.contentRemoveUpdateSelector.Items)
        {
            m.Selected = false;
        }
        this.contentRemoveUpdateSelector.Items[0].Selected = true;
    }

    private void ContentDivSelector(string action)
    {
        this.addContent.Attributes["class"] = "unVisi";
        this.removeUpdateContent.Attributes["class"] = "unVisi";
        this.contentsNotify.Attributes["class"] = "unVisi";
        this.contentSelectorDiv.Attributes["class"] = "unVisi";
        this.addLinkContent.Attributes["class"] = "unVisi";

        this.addContent.Visible = false;
        this.removeUpdateContent.Visible = false;
        this.contentsNotify.Visible = false;
        this.contentSelectorDiv.Visible = false;
        this.addLinkContent.Visible = false;

        switch (action)
        {
            case "start":
                this.contentSelectorDiv.Visible = true;
                this.contentSelectorDiv.Attributes["class"] = "visi";
                break;
            case "1":
                this.addContent.Attributes["class"] = "visi";
                this.addContent.Visible = true;
                break;
            case "2":
                this.addLinkContent.Attributes["class"] = "visi";
                this.addLinkContent.Visible = true;
                break;
            case "3":
                this.removeUpdateContent.Attributes["class"] = "visi";
                this.removeUpdateContent.Visible = true;
                break;
            case "4":
                this.contentsNotify.Visible = true;
                this.contentsNotify.Attributes["class"] = "visi";
                break;
            default:
                break;
        }
    }

    private void RemovePage()
    {
        if (this.contentHiddenRe.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                    (". this.contentHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.ClearFields(1);
            this.contentHiddenRe.Value = "";
            return;
        }

        try
        {
            ContentP p = (ContentP)(this.Master._CoachingDal.Get("content", this.contentHiddenRe.Value));
            if (p == null)
            {
                this.Master._Logger.Error(new AdminException
                    (". p == null"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(11, "Red", ""));
                return;
            }

            this.Master._CoachingDal.Delete("content", p.ContentID);
            this.Master._Logger.Log(new AdminException(string.Format(". Page content {0} Was Successfully Removed", p.ContentID)),
                            MethodBase.GetCurrentMethod().Name);
            this.contentRemoveUpdateSelector.Items.Remove(this.contentRemoveUpdateSelector.Items.FindByValue("s" + p.ContentID));

            this.Notify(this.Master._Notifier.Notify(2, "White", p.ContentButtonTitle));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
        }
        finally
        {
            this.ClearFields(1);
            this.contentHiddenRe.Value = "";
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
                if (this.contentLanguageSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentLanguageSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(18, "Red", ""));
                    return false;
                }

                if (this.contentPageTypeSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentPageTypeSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(69, "Red", ""));
                    return false;
                }

                if (this.contentPlace.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(65, "Red", ""));
                    return false;
                }

                int i = -1;
                if (!int.TryParse(this.contentPlace.Text, out i))
                {
                    this.Master._Logger.Warn(new AdminException(". !int.TryParse(this.contentPlace.Text, out i)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(67, "Red", ""));
                    return false;
                }

                if (this.contentTitle.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentTitle.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(14, "Red", ""));
                    return false;
                }

                if (this.contentKeyWords.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentKeyWords.Tex == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(16, "Red", ""));
                    return false;
                }

                if (this.contentDescription.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentDescription.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(15, "Red", ""));
                    return false;
                }

                if (this.contentEditor.Value == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentEditor.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(17, "Red", ""));
                    return false;
                }

                if (this.contentButtonTitle.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentButtonTitle.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(30, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.contentRemoveUpdateSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentRemoveUpdateSelector.SelectedIndex == 0"),
                                MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(62, "Red", ""));
                    return false;
                }
                break;
            case 3:
                if (this.contentLinkPlace.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentLinkPlace.Text == \"\""),
                             MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(65, "Red", ""));
                    return false;
                }

                int b = -1;
                if (!int.TryParse(this.contentLinkPlace.Text, out b))
                {
                    this.Master._Logger.Warn(new AdminException(". !int.TryParse(this.contentPlace.Text, out b)"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(67, "Red", ""));
                    return false;
                }

                if (this.contentLinkButtonTitle.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentLinkButtonTitle.Text == \"\""),
                             MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(30, "Red", ""));
                    return false;
                }

                if (this.contentLinkUrl.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.contentLinkUrl.Text == \"\""),
                             MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(68, "Red", ""));
                    return false;
                }
                break;
            default:
                break;
        }
        return true;
    }

    protected void contentEditorSaveButton_Click(object sender, EventArgs e)
    {
        if (this.contentHiddenUp.Value == "")
        {
            this.AddPage();
        }
        else
        {
            this.UpdatePage();
        }
    }

    protected void contentLinkAddButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(3))
        {
            this.ClearFields(2);
            return;
        }

        if (this.contentLinkHiddenUp.Value == "")
        {
            this.AddLinkContent();
        }
        else
        {
            this.UpdateLinkContent();
            this.contentLinkHiddenUp.Value = "";
        }
    }

    protected void contentLinkCancelButton_Click(object sender, EventArgs e)
    {
        this.ClearFields(2);
        this.Start();
    }

    private void UpdatePageLinkInit()
    {
        if (!this.ValidateFields(2))
        {
            this.ClearFields(2);
            this.contentLinkHiddenUp.Value = "";
            return;
        }

        ContentP p = (ContentP)this.Master._CoachingDal.Get("content", this.contentRemoveUpdateSelector.SelectedValue.Remove(0, 1));
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException(". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.contentLinkUpdateTime.Text = p.spLastUpdate;
        this.contentLinkButtonTitle.Text = p.ContentButtonTitle;
        this.contentLinkUrl.Text = p.ContentLink;
        this.contentLinkPlace.Text = p.ContentMenuPlace.ToString();

        this.contentLinkHiddenUp.Value = p.ContentID;

        this.ContentDivSelector("2");
    }

    private void AddLinkContent()
    {
        if (!this.Master._CoachingDal.CheckIfContentPlaceAvailable(int.Parse(this.contentLinkPlace.Text)))
        {
            this.Master._Logger.Warn(new AdminException(". (!this.Master._CoachingDal.CheckIfContentPlaceAvailable(int.Parse(this.contentLinkPlace.Text)))"),
                        MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(66, "Red", ""));
            this.ClearFields(2);
            return;
        }

        if (!this.Master._CoachingDal.CheckAvailableButtonTitle(this.contentLinkButtonTitle.Text))
        {
            this.Master._Logger.Warn(new AdminException(". !this.Master._CoachingDal.CheckAvailableButtonTitle(this.contentButtonTitle.Text)"),
                                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(31, "Red", this.contentButtonTitle.Text));
            this.ClearFields(2);
            return;
        }

        try
        {
            string url = this.Master._GlobalFunctions.FixUrl(this.contentLinkUrl.Text);

            ContentP content = new ContentP()
            {
                ContentButtonTitle = this.contentLinkButtonTitle.Text,
                ContentContent = "",
                ContentDescription = "",
                ContentID = this.Master._CoachingDal.GetNextAvailableID("content"),
                ContentKeyWords = "",
                ContentLanguageID = "1",
                ContentLink = url,
                ContentFullPageLink = "",
                ContentMenuPlace = int.Parse(this.contentLinkPlace.Text),
                ContentTitle = "",
                ContentPageType = "Content",
                CreationTime = TimeNow.TheTimeNow,
                IsLinkPage = 1,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._CoachingDal.Add("content", content, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". Page" + content.ContentButtonTitle +
                                    " Was Successfully Added"),MethodBase.GetCurrentMethod().Name);

            ListItem m = new ListItem(content.ContentButtonTitle, "s" + content.ContentID);
            this.contentRemoveUpdateSelector.Items.Add(m);

            this.Notify(this.Master._Notifier.Notify(9,"White",content.ContentButtonTitle + "," + content.ContentID));
        }
        catch (Exception p)
        {
            this.Master._Logger.Error(p, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "Red", this.contentLinkButtonTitle.Text));
        }
        finally
        {
            this.ClearFields(2);
        }
    }

    private void UpdateLinkContent()
    {
        if (this.contentLinkHiddenUp.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.contentLinkHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.ClearFields(2);
            this.contentLinkHiddenUp.Value = "";
            return;
        }

        ContentP p = (ContentP)this.Master._CoachingDal.Get("content", this.contentLinkHiddenUp.Value);
        if (p == null)
        {
            this.Master._Logger.Error(new AdminException(". p == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.ClearFields(2);
            this.contentLinkHiddenUp.Value = "";
            return;
        }

        if (!this.Master._CoachingDal.CheckIfContentPlaceAvailableExcept
            (p.ContentMenuPlace, int.Parse(this.contentLinkPlace.Text)))
        {
            this.Master._Logger.Warn(new AdminException(@". !this.Master._CoachingDal.CheckIfContentPlaceAvailableExcept
            (p.ContentMenuPlace, int.Parse(this.contentLinkPlace.Text))"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(66, "Red", ""));
            this.ClearFields(2);
            this.contentLinkHiddenUp.Value = "";
            return;
        }

        if (!this.Master._CoachingDal.CheckAvailableButtonTitleExcept(p.ContentButtonTitle, this.contentLinkButtonTitle.Text))
        {
            this.Master._Logger.Warn(new AdminException(". !this.Master._CoachingDal.CheckAvailableButtonTitleExcept(p.ContentButtonTitle, this.contentLinkButtonTitle.Text)"),
                                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(31, "Red", this.contentButtonTitle.Text));
            this.ClearFields(2);
            this.contentLinkHiddenUp.Value = "";
            return;
        }

        try
        {
            ListItem m = this.contentRemoveUpdateSelector.Items.FindByValue("s" + p.ContentID);
            this.contentRemoveUpdateSelector.Items.Remove(m);

            string url = this.Master._GlobalFunctions.FixUrl(this.contentLinkUrl.Text);

            p.ContentMenuPlace = int.Parse(this.contentLinkPlace.Text);
            p.ContentLink = url;
            p.ContentButtonTitle = this.contentLinkButtonTitle.Text;
            p.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
            p.LastUpdate = TimeNow.TheTimeNow;

            this.Master._CoachingDal.Update("content", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new Exception(". " + p.ContentID + " "
                            + p.ContentTitle + " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);

            m = new ListItem(p.ContentButtonTitle, "s" + p.ContentID);
            this.contentRemoveUpdateSelector.Items.Add(m);

            this.Notify(this.Master._Notifier.Notify(29, "White", p.ContentButtonTitle));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", this.contentLinkButtonTitle.Text));
        }
        finally
        {
            this.ClearFields(2);
            this.contentLinkHiddenUp.Value = "";
        }
    }

    private void UpdatePage()
    {
        if (this.contentHiddenUp.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                        (". this.contentHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.ClearFields(1);
            this.ClearLanguageSelector();
            this.contentHiddenUp.Value = "";
            return;
        }

        if (!this.ValidateFields(1))
        {
            this.ClearFields(1);
            this.ClearLanguageSelector();
            this.contentHiddenUp.Value = "";
            return;
        }

        try
        {
            ContentP p = (ContentP)(this.Master._CoachingDal.Get("content", this.contentHiddenUp.Value));
            if (p == null)
            {
                this.Master._Logger.Error(new AdminException
                    (". p == null"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(11, "Red", ""));
                this.ClearFields(1);
                this.ClearLanguageSelector();
                this.contentHiddenUp.Value = "";
                return;
            }

            if (!this.Master._CoachingDal.GetContentByTypeExcept(p.ContentPageType, this.contentPageTypeSelector.SelectedValue))
            {
                this.Master._Logger.Error(new AdminException
                             (". !this.Master._CoachingDal.GetContentByTypeExcept(p.ContentPageType, this.contentPageTypeSelector.SelectedValue)")
                             , MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(70, "Red", this.contentPageTypeSelector.SelectedValue));
                this.ClearFields(1);
                this.ClearLanguageSelector();
                this.contentHiddenUp.Value = "";
                return;
            }

            if (!this.Master._CoachingDal.CheckAvailableButtonTitleExcept(p.ContentButtonTitle, this.contentButtonTitle.Text))
            {
                this.Master._Logger.Warn(new AdminException(". !this.Master._CoachingDal.CheckAvailableButtonTitleExcept(p.ContentButtonTitle, this.contentButtonTitle.Text)"),
                                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(31, "Red", this.contentButtonTitle.Text));
                this.ClearFields(1);
                this.ClearLanguageSelector();
                this.contentHiddenUp.Value = "";
                return;
            }

            if (!this.Master._CoachingDal.CheckIfContentPlaceAvailableExcept(p.ContentMenuPlace,int.Parse(this.contentPlace.Text)))
            {
                this.Master._Logger.Warn(new AdminException(". !this.Master._CoachingDal.CheckIfContentPlaceAvailable(i)"),
                            MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(66, "Red", ""));
                this.ClearFields(1);
                this.ClearLanguageSelector();
                this.contentHiddenUp.Value = "";
                return;
            }

            ListItem m = this.contentRemoveUpdateSelector.Items.FindByValue("s" + p.ContentID);
            this.contentRemoveUpdateSelector.Items.Remove(m);

            p.ContentID = p.ContentID;
            p.ContentContent = this.contentEditor.Value;
            p.ContentButtonTitle = this.contentButtonTitle.Text;
            p.ContentDescription = this.contentDescription.Text;
            p.ContentKeyWords = this.contentKeyWords.Text;
            p.ContentLanguageID = this.contentLanguageSelector.SelectedIndex.ToString();
            p.ContentTitle = this.contentTitle.Text;
            p.LastUpdate = TimeNow.TheTimeNow;
            p.ContentPageType = this.contentPageTypeSelector.SelectedValue;
            p.spLastUpdate = TimeNow.TheTimeNow.ToString();
            p.IsLinkPage = 2;
            p.ContentMenuPlace = int.Parse(this.contentPlace.Text);

            this.Master._CoachingDal.Update("content", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new Exception(". " + p.ContentID + " "
                            + p.ContentTitle + " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);

            m = new ListItem(p.ContentButtonTitle, "s" + p.ContentID);
            this.contentRemoveUpdateSelector.Items.Add(m);

            this.Notify(this.Master._Notifier.Notify(29, "White", p.ContentButtonTitle));

        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(13, "Red", this.contentButtonTitle.Text));
        }
        finally
        {
            this.ClearFields(1);
            this.ClearLanguageSelector();
            this.contentHiddenUp.Value = "";
        }
    }

    private void AddPage()
     {
        if (!ValidateFields(1))
        {
            this.ClearFields(1);
            this.ClearLanguageSelector();
            return;
        }

        if (!this.Master._CoachingDal.GetContentByType(this.contentPageTypeSelector.SelectedValue))
        {
            this.Master._Logger.Error(new AdminException
                         (". !this.Master._CoachingDal.GetContentByType(this.contentPageTypeSelector.SelectedValue)")
                         , MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(70, "Red", this.contentPageTypeSelector.SelectedValue));
            this.ClearFields(1);
            this.ClearLanguageSelector();
            return;
        }

        if (!this.Master._CoachingDal.CheckAvailableButtonTitle(this.contentButtonTitle.Text))
        {
            this.Master._Logger.Warn(new AdminException(". !this.Master._CoachingDal.CheckAvailableButtonTitle(this.contentButtonTitle.Text)"),
                                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(31, "Red", this.contentButtonTitle.Text));
            this.ClearFields(1);
            this.ClearLanguageSelector();
            return;
        }

        if (!this.Master._CoachingDal.CheckIfContentPlaceAvailable(int.Parse(this.contentPlace.Text)))
        {
            this.Master._Logger.Warn(new AdminException(". (!this.Master._CoachingDal.CheckIfContentPlaceAvailable(int.Parse(this.contentPlace.Text)))"),
                        MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(66, "Red", ""));
            this.ClearFields(1);
            this.ClearLanguageSelector();
            return;
        }

        string title = "";

        try
        {
            title = this.contentTitle.Text;
            string id = this.Master._CoachingDal.GetNextAvailableID("content");

            ContentP p = new ContentP
            {
                ContentID = id,
                ContentContent = this.contentEditor.Value,
                ContentMenuPlace = int.Parse(this.contentPlace.Text),
                ContentButtonTitle = contentButtonTitle.Text,
                ContentDescription = this.contentDescription.Text,
                ContentKeyWords = this.contentKeyWords.Text,
                ContentLanguageID = this.contentLanguageSelector.SelectedIndex.ToString(),
                ContentTitle = this.contentTitle.Text,
                CreationTime = TimeNow.TheTimeNow,
                ContentPageType = this.contentPageTypeSelector.SelectedValue,
                ContentLink = "http://www.malliere.com/ContactPage.aspx?pageID=" + id,
                ContentFullPageLink = "http://www.malliere.com/FullPage.aspx?pageID=" + id + "&type=content",
                IsLinkPage = 2,
                LastUpdate = TimeNow.TheTimeNow,
                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._CoachingDal.Add("content", p, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new Exception("Page" + p.ContentTitle + " Was Successfully Added"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(4, "White", p.ContentButtonTitle + "," + p.ContentID));

            ListItem m = new ListItem(p.ContentButtonTitle, "s" + p.ContentID);
            this.contentRemoveUpdateSelector.Items.Add(m);
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(12, "Red", this.contentButtonTitle.Text));
        }
        finally
        {
            this.ClearFields(1);
            this.ClearLanguageSelector();
        }
    }

    protected void contentEditorCancelButton_Click(object sender, EventArgs e)
    {
        this.ClearFields(1);
        this.Start();
    }

    private void ClearLanguageSelector()
    {
        this.contentKeyWords.Attributes["class"] = "";
        this.contentTitle.Attributes["class"] = "";
        this.contentDescription.Attributes["class"] = "";
    }

    private void ClearFields(int index)
    {
        if (index <= 0)
        {
            this.Master._Logger.Error(new AdminException(". index <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (index)
        {
            case 1:
                this.contentKeyWords.Text = "";
                this.contentButtonTitle.Text = "";
                this.contentDescription.Text = "";
                this.contentEditor.Value = "";
                this.contentTitle.Text = "";
                this.contentUpdateTime.Text = "";
                this.contentPlace.Text = "";
                this.contentLink.Text = "";
                this.contentFullPageLink.Text = "";

                foreach (ListItem m in this.contentSelector.Items)
                {
                    m.Selected = false;
                }

                foreach (ListItem m in this.contentLanguageSelector.Items)
                {
                    m.Selected = false;
                }

                foreach (ListItem m in this.contentRemoveUpdateSelector.Items)
                {
                    m.Selected = false;
                }

                foreach (ListItem m in this.contentPageTypeSelector.Items)
                {
                    m.Selected = false;
                }
                break;
            case 2:
                this.contentLinkUpdateTime.Text = "";
                this.contentLinkPlace.Text = "";
                this.contentLinkButtonTitle.Text = "";
                this.contentLinkUrl.Text = "";
                this.contentLinkPageLabel.Text = "";
                break;
            default:
                break;
        }
    }


    protected void okBut_Click(object sender, EventArgs e)
    {
        if (this.contentHiddenRe.Value == "")
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

    private void Start()
    {
        this.ContentDivSelector("start");
        this.ClearSelection();
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
                this.contentsNotifyLabel.ForeColor = Color.Red;
            }
            else
            {
                this.contentsNotifyLabel.ForeColor = Color.Green;
            }

            this.contentsNotifyLabel.Text = message[0];

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
            this.contentsNotifyLabel.ForeColor = Color.Red;
            this.contentsNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.Master._MainSelector = false;
            this.ContentDivSelector("4");
        }
    }
}
