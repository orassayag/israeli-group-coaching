using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Configuration;
using System.Text;

public partial class Admin_News : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.newsSelector.Items.Add(new ListItem("--Select Action--", "0"));
            this.newsSelector.Items.Add(new ListItem("Add News", "1"));
            this.newsSelector.Items.Add(new ListItem("Remove/Update News", "2"));
            this.newsSelector.Items.Add(new ListItem("General Options", "3"));

            foreach (New news in (IEnumerable<New>)this.Master._CoachingDal.GetAll("news"))
            {
                this.removeUpdateNewsSelector.Items.Add(new ListItem
                (news.NewsContentOriginal, "s" + news.NewsID));
            }

            this.SetOrderSelector();

            for (int i = 1; i <= 20; i++)
            {
                this.newsSpeedSelector.Items.Add(new ListItem(i.ToString(), i.ToString() + "000"));
                this.newsPauseTimeSelector.Items.Add(new ListItem(i.ToString(), i.ToString() + "000"));
                this.newsNumberItemsSelector.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.LoadNewsSettings();
        }
    }

    private bool ValidateFields(int index)
    {
        if (index <= 0)
        {
            this.Master._Logger.Error(new AdminException(". index <= 0"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return false;
        }

        switch (index)
        {
            case 1:
                if (this.newsSelector.SelectedValue == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.newsSelector.SelectedValue == \"\""),
                         MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(3, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.newsBody.Value == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.newsBody.Text == \"\""),
                             MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(64, "Red", ""));
                    return false;
                }

                if (this.newsNameInMenu.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.newsNameInMenu.Text == \"\""),
                    MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(75, "Red", ""));
                    return false;
                }
                break;
            case 3:
                if (this.newsSpeedSelector.SelectedValue == "" || this.newsPauseTimeSelector.SelectedValue == "" ||
                    this.newsNumberItemsSelector.SelectedValue == "" || this.newsPauseOnMouseHover.Value == "")
                {
                    this.Master._Logger.Warn(new AdminException(@". (this.newsSpeedSelector.SelectedValue == "" ||
                                                                     this.newsPauseTimeSelector.SelectedValue == "" ||
                                                                     this.newsNumberItemsSelector.SelectedValue == "" ||
                                                                     this.newsPauseOnMouseHover.Value == "")"),
                                                MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
                    return false;
                }
                break;
            case 4:
                if (this.removeUpdateNewsSelector.SelectedValue == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.removeUpdateNewsSelector.SelectedValue == \"\""),
                         MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(71, "Red", ""));
                    return false;
                }
                break;
            default:
                break;
        }

        return true;
    }

    private void ClearFields(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException(". action <= 0"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (action)
        {
            case 1:
                this.newsLastUpdateLabel.Text = "";
                this.newsBody.Value = "";
                this.newsNameInMenu.Text = "";
                this.SetOrderSelector();
                break;
            case 2:
                foreach (ListItem i in this.removeUpdateNewsSelector.Items)
                {
                    i.Selected = false;
                }
                this.newsHiddenRe.Value = "";
                this.newsHiddenUp.Value = "";
                break;
            case 3:
                foreach (ListItem i in this.newsSpeedSelector.Items)
                {
                    i.Selected = false;
                }
                foreach (ListItem i in this.newsPauseTimeSelector.Items)
                {
                    i.Selected = false;
                }
                foreach (ListItem i in this.newsNumberItemsSelector.Items)
                {
                    i.Selected = false;
                }
                this.newsPauseOnMouseOver.Checked = true;
                break;
            case 4:
                foreach (ListItem l in this.newsSelector.Items)
                {
                    l.Selected = false;
                }
                break;
            default:
                break;
        }
    }

    private void DivSwitcher(int action)
    {
        this.addNews.Visible = false;
        this.removeUpdateNews.Visible = false;
        this.newsGeneralOptions.Visible = false;
        this.newsNotify.Visible = false;

        this.addNews.Attributes["class"] = "unVisi";
        this.removeUpdateNews.Attributes["class"] = "unVisi";
        this.newsGeneralOptions.Attributes["class"] = "unVisi";
        this.newsNotify.Attributes["class"] = "unVisi";

        switch (action)
        {
            case 1:
                this.addNews.Visible = true;
                this.addNews.Attributes["class"] = "visi";
                break;
            case 2:
                this.removeUpdateNews.Visible = true;
                this.removeUpdateNews.Attributes["class"] = "visi";
                break;
            case 3:
                this.newsGeneralOptions.Visible = true;
                this.newsGeneralOptions.Attributes["class"] = "visi";
                break;
            case 4:
                this.newsNotify.Visible = true;
                this.newsNotify.Attributes["class"] = "visi";
                break;
            default:
                break;
        }
    }

    private void SetOrderSelector()
    {
        this.newsOrderSelector.Items.Clear();
        this.newsOrderSelector.Items.Add(new ListItem("--Select Place--", "0"));
        foreach (int i in this.Master._CoachingDal.GetAvailableNewsPlaces())
        {
            this.newsOrderSelector.Items.Add(new ListItem(i.ToString(), "s" + i.ToString()));
        }
    }

    protected void newsSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.StartOptions();
        this.ClearFields(1);
        this.ClearFields(2);

        this.DivSwitcher(this.newsSelector.SelectedIndex);
    }

    private void AddNews()
    {

        string text = this.Master._GlobalFunctions.FixNews(this.newsBody.Value);

        try
        {
            New news = new New
            {
                NewsID = this.Master._CoachingDal.GetNextAvailableID("news"),
                NewsContentLink = text,
                NewsContentOriginal = this.newsNameInMenu.Text,
                NewsCreationDate = TimeNow.TheTimeNow,
                spNewsCreationDate = TimeNow.TheTimeNow.ToShortDateString(),
                NewsLastUpdate = TimeNow.TheTimeNow,
                spNewsLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
                NewsPlace = int.Parse(this.newsOrderSelector.SelectedValue.Remove(0, 1))
            };

            this.Master._CoachingDal.Add("news", news, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". news " + news.NewsID +
                            " Was Successfully Added"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(26, "White", "News Number " + news.NewsID));

            this.removeUpdateNewsSelector.Items.Add(new ListItem
            (this.newsNameInMenu.Text, "s" + news.NewsID));

            this.ClearFields(1);
        }
        catch (Exception p)
        {
            this.Master._Logger.Error(p, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(41, "Red", "The Current News"));
        }
    }

    private void RemoveNews()
    {
        if (this.newsHiddenRe.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.newsHiddenRe.Value == \"\""),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.newsHiddenRe.Value = "";
            return;
        }

        try
        {
            this.Master._CoachingDal.Delete("news", this.newsHiddenRe.Value);
            this.Master._Logger.Log(new AdminException(". News Number " + this.newsHiddenRe.Value +
                                    " Was Successfully Removed"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(28, "White", "News Number " + this.newsHiddenRe.Value));

            this.removeUpdateNewsSelector.Items.Remove
            (this.removeUpdateNewsSelector.Items.FindByValue("s" + this.newsHiddenRe.Value));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Master.Notify(this.Master._Notifier.Notify(38, "Red", "News Number " + this.newsHiddenRe.Value));
        }
    }

    private void UpdateNews()
    {
        if (this.newsHiddenUp.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.newsHiddenRe.Value == \"\""),
                         MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.newsHiddenRe.Value = "";
            return;
        }

        if (!this.ValidateFields(2))
        {
            this.newsHiddenRe.Value = "";
            return;
        }

        New news = (New)this.Master._CoachingDal.Get("news", this.newsHiddenUp.Value);
        if (news == null)
        {
            this.Master._Logger.Error(new AdminException(". news == null"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.newsHiddenRe.Value = "";
            return;
        }

        string text = this.Master._GlobalFunctions.FixNews(this.newsBody.Value);

        try
        {
            news.NewsContentLink = text;
            news.NewsContentOriginal = this.newsNameInMenu.Text;
            news.NewsPlace = int.Parse(this.newsOrderSelector.SelectedValue.Remove(0, 1));
            news.NewsLastUpdate = TimeNow.TheTimeNow;
            news.spNewsLastUpdate = TimeNow.TheTimeNow.ToShortDateString();

            this.Master._CoachingDal.Update("news", news, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". News Number " + this.newsHiddenRe.Value +
                         " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(27, "White", "News Number " + this.newsHiddenRe.Value));

            this.removeUpdateNewsSelector.Items.Remove
            (this.removeUpdateNewsSelector.Items.FindByValue("s" + news.NewsID));

            this.removeUpdateNewsSelector.Items.Add(new ListItem
            (this.newsNameInMenu.Text, "s" + news.NewsID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(25, "Red", "News Number " + this.newsHiddenRe.Value));
        }
    }

    private void RestoreDefaultSettings()
    {
        this.ClearFields(3);

        this.newsSpeedSelector.Items[3].Selected = true;
        this.newsPauseTimeSelector.Items[5].Selected = true;
        this.newsNumberItemsSelector.Items[10].Selected = true;
        this.newsPauseOnMouseOver.Checked = true;
    }

    private void UpdateNewsInit()
    {
        if (this.newsHiddenUp.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.newsHiddenRe.Value == \"\""),
                         MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.newsHiddenRe.Value = "";
            return;
        }

        New news = (New)this.Master._CoachingDal.Get("news", this.newsHiddenUp.Value);
        if (news == null)
        {
            this.Master._Logger.Error(new AdminException(". news == null"),
                MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.newsHiddenRe.Value = "";
            return;
        }

        this.ClearFields(1);

        this.newsLastUpdateLabel.Text = "Last Update: " + news.spNewsLastUpdate;
        this.newsBody.Value = news.NewsContentLink;
        this.newsNameInMenu.Text = news.NewsContentOriginal;
        ListItem m = new ListItem(news.NewsPlace.ToString(), "s" + news.NewsPlace.ToString());
        m.Selected = true;
        this.newsOrderSelector.Items.Add(m);
    }

    protected void addAddNews_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            return;
        }

        if (this.newsHiddenUp.Value == "")
        {
            this.AddNews();
        }
        else
        {
            this.UpdateNews();
            this.newsHiddenUp.Value = "";
        }
    }

    protected void cancelAddNews_Click(object sender, EventArgs e)
    {
        this.ClearFields(1);
        this.ClearFields(4);
        this.DivSwitcher(0);
    }

    protected void cancelRemoveUpdateNews_Click(object sender, EventArgs e)
    {
        this.ClearFields(4);
    }

    protected void updateRemoveUpdateNews_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(4))
        {
            return;
        }

        this.newsHiddenUp.Value = this.removeUpdateNewsSelector.SelectedValue.Remove(0, 1);
        this.UpdateNewsInit();
        this.DivSwitcher(1);
    }

    protected void removeRemoveUpdateNews_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(4))
        {
            return;
        }

        this.newsHiddenRe.Value = this.removeUpdateNewsSelector.SelectedValue.Remove(0, 1);

        this.Notify(this.Master._Notifier.Notify(1, "Red", "News Number " + this.newsHiddenRe.Value));
    }

    protected void newsOptionsOkButton_Click(object sender, EventArgs e)
    {
        this.SaveNewsSettings();
        this.ClearFields(4);
    }

    protected void newsOptionsDefaultButton_Click(object sender, EventArgs e)
    {
        this.RestoreDefaultSettings();
        this.SaveNewsSettings();
        this.ClearFields(4);
    }

    protected void newsOptionsCancelButton_Click(object sender, EventArgs e)
    {
        this.StartOptions();
        this.DivSwitcher(0);
        this.ClearFields(4);
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (this.newsHidden.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.newsHidden.Value == \"\""),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.AfterOk(this.newsHidden.Value);
    }

    private void LoadNewsSettings()
    {
        if (ConfigurationSettings.AppSettings["JscriptFile"] == null)
        {
            this.Master._Logger.Error(new AdminException(". ConfigurationSettings.AppSettings[\"JscriptFile\"] == null"),
                        MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.StartOptions();
            return;
        }

        string path = ConfigurationSettings.AppSettings["JscriptFile"];

        if (!File.Exists(path))
        {
            this.RestoreDefaultSettings();
            this.SaveNewsSettings();
            return;
        }

        StreamReader reader = null;
        StringBuilder builder = new StringBuilder();

        try
        {
            reader = new StreamReader(path);
            for (int i = 0, y = 0; i < 12; i++)
            {
                if (i > 6)
                {
                    string m = reader.ReadLine();
                    builder.Append(m.Substring(m.IndexOf(':'), m.Length - m.IndexOf(':')).Remove(0, 2));
                    y++;
                }
                else
                {
                    reader.ReadLine();
                }
            }

            string[] lister = builder.ToString().Split(',');
            if (lister.Count() != 5)
            {
                throw new AdminException(". lister.Count() != 5");
            }

            this.ClearFields(3);

            this.newsSpeedSelector.SelectedValue = lister[0];
            this.newsPauseTimeSelector.SelectedValue = lister[1];
            this.newsNumberItemsSelector.SelectedValue = lister[2];
            if (lister[4] == "true")
            {
                this.newsPauseOnMouseOver.Checked = true;
            }
            else
            {
                this.newsPauseOnMouseOver.Checked = false;
            }
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.StartOptions();
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }
    }

    private void SaveNewsSettings()
    {
        StreamWriter writer = null;

        if (ConfigurationSettings.AppSettings["JscriptFile"] == null)
        {
            this.Master._Logger.Error(new AdminException(". ConfigurationSettings.AppSettings[\"JscriptFile\"] == null"),
                        MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.StartOptions();
            return;
        }

        string path = ConfigurationSettings.AppSettings["JscriptFile"];

        if (!File.Exists(path))
        {
            File.Create(path);
        }

        try
        {
            writer = new StreamWriter(path, false);
            writer.WriteLine(
@"$(function() {
    $('.news-container').vTicker();
});

(function() {
    $.fn.vTicker = function(options) {
    var defaults = {
    speed: " + this.newsSpeedSelector.SelectedValue + @",
    pause: " + this.newsPauseTimeSelector.SelectedValue + @",
    showItems: " + this.newsNumberItemsSelector.SelectedValue + @",
    animation: '',
    mousePause: " + (this.newsPauseOnMouseOver.Checked == true ? "true" : "false") + @"
};

        var options = $.extend(defaults, options);

        moveUp = function(obj, height) {
            obj = obj.children('ul');
            first = obj.children('li:first').clone(true);

            obj.animate({ top: '-=' + height + 'px' }, options.speed, function() {
                $(this).children('li:first').remove();
                $(this).css('top', '0px');
            });

            if (options.animation == 'fade') {
                obj.children('li:first').fadeOut(options.speed);
                obj.children('li:last').hide().fadeIn(options.speed);
            }

            first.appendTo(obj);
        };

        return this.each(function() {
            obj = $(this);
            maxHeight = 70;

            obj.css({ overflow: 'hidden', position: 'relative' })
			.children('ul').css({ position: 'absolute', margin: 0, padding: 0 })
			.children('li').css({ margin: 0, padding: 0 });

            obj.children('ul').children('li').each(function() {
                if ($(this).height() > maxHeight) {
                    maxHeight = $(this).height();
                }
            });

            obj.children('ul').children('li').each(function() {
                $(this).height(maxHeight);
            });

            obj.height(maxHeight * 2);

            interval = setInterval('moveUp(obj, maxHeight)', options.pause);

            if (options.mousePause) {
                obj.bind(""mouseenter"", function() {
                    clearInterval(interval);
                }).bind(""mouseleave"", function() {
                    interval = setInterval('moveUp(obj, maxHeight)', options.pause);
                });
            }
        });
    };
})(jQuery);");

            this.Master._Logger.Log(new AdminException(". News Options Were Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(27, "White", "News Options"));

        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(25, "Red", "News Options"));
            this.StartOptions();
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }
        }
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.ClearFields(2);
        this.DivSwitcher(0);
        this.ClearFields(4);
    }

    private void StartOptions()
    {
        this.ClearFields(3);
        this.LoadNewsSettings();
    }

    private void AfterOk(string selector)
    {
        if (selector == "" || selector == null)
        {
            this.Master._Logger.Error(new AdminException(". selector == \"\" || selector == null"),
                        MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (selector)
        {
            case "1":
                this.RemoveNews();
                this.newsHiddenRe.Value = "";
                break;
            case "41":
            case "26":
            case "63":
            case "64":
            case "75":
                this.ClearFields(1);
                this.Start();
                break;
            case "28":
            case "38":
            case "27":
            case "25":
            case "71":
                //failed to update settings
                this.ClearFields(1);
                this.Start();
                break;
            case "23":
                this.ClearFields(1);
                this.ClearFields(2);
                this.ClearFields(3);
                this.ClearFields(4);
                this.DivSwitcher(0);
                this.Master.Exit();
                break;
            default:
                break;
        }

        if (selector != "1")
        {
            this.newsHidden.Value = "";
        }
    }

    private void Start()
    {
        this.ClearFields(2);
        this.ClearFields(4);
        this.SetOrderSelector();
        this.DivSwitcher(0);
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
                this.newsNotifyLabel.ForeColor = Color.Red;
            }
            else
            {
                this.newsNotifyLabel.ForeColor = Color.Green;
            }

            this.newsNotifyLabel.Text = message[0];
            this.newsHidden.Value = message[2];

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
            this.newsNotifyLabel.ForeColor = Color.Red;
            this.newsNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.Master._MainSelector = false;
            this.DivSwitcher(4);
        }
    }
}
