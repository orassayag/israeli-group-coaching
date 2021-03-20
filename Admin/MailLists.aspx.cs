using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Reflection;
using System.Text;

public partial class Admin_MailLists : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (this.mailListMailSelector.Items.Count == 0)
            {
                foreach (MailList mail in (IEnumerable<MailList>)this.Master._CoachingDal.GetAll("mailList"))
                {
                    this.mailListMailSelector.Items.Add
                    (new ListItem(mail.MailListName + " " + mail.MailListMail, "s" + mail.MailListID));
                }
            }

            ListItem q = null;
            ListItem u = null;
            ListItem j = null;

            for (int i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case 0:
                        q = new ListItem("--Select Action--", i.ToString());
                        j = new ListItem("--Select Language--", i.ToString());
                        break;
                    case 1:
                        q = new ListItem("Mailing List", i.ToString());
                        u = new ListItem("Add Mail", i.ToString());
                        j = new ListItem("עברית", i.ToString());
                        break;
                    case 2:
                        q = new ListItem("Send Mail", i.ToString());
                        u = new ListItem("Remove Mail", i.ToString());
                        j = new ListItem("English", i.ToString());
                        break;
                    case 3:
                        q = new ListItem("Add/Remove/Update Mail", i.ToString());
                        u = new ListItem("Update Mail", i.ToString());
                        break;
                    case 4:
                        q = new ListItem("Get All Mailing Addresses", i.ToString());
                        break;
                    case 5:
                        q = new ListItem("Statistics", i.ToString());
                        break;
                    default:
                        break;
                }

                if (q != null)
                {
                    this.mailListSelector.Items.Add(q);
                }

                if (u != null)
                {
                    this.mailActionSelector.Items.Add(u);
                }

                if (j != null)
                {
                    this.mailLanguageSelector.Items.Add(j);
                }

                q = null;
                u = null;
                j = null;
            }
        }
    }

    private void ClearSelection(string pageType)
    {
        if (pageType == "" || pageType == null)
        {
            this.Master._Logger.Error(new AdminException
                            (". pageType == \"\" || pageType == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (pageType)
        {
            case "main":
                foreach (ListItem m in this.mailListSelector.Items)
                {
                    m.Selected = false;
                }
                this.mailListSelector.Items[0].Selected = true;
                break;
            case "action":
                foreach (ListItem m in this.mailActionSelector.Items)
                {
                    m.Selected = false;
                }
                this.mailActionSelector.Items[0].Selected = true;
                break;
            default:
                break;
        }
    }

    private void AddMail(string name, string mailAddress)
    {
        if (!this.ValidateFields("mail4"))
        {
            return;
        }

        if (!ValidateMailBeforeAddUpdate(name, mailAddress))
        {
            return;
        }

        try
        {
            MailList mail = new MailList
            {
                Active = 1,
                MailListID = this.Master._CoachingDal.GetNextAvailableID("mailList"),
                MailListJoinTime = TimeNow.TheTimeNow,
                spMailListJoinTime = TimeNow.TheTimeNow.ToString(),
                spActvie = "Enable",
                MailListMail = mailAddress,
                MailRemoveCode = this.Master._GlobalFunctions.GetRemoveCode(),
                MailListName = name
            };

            this.Master._CoachingDal.Add("mailList", mail, TimeNow.TheTimeNow);
            this.Notify(this.Master._Notifier.Notify(26, "White", mail.MailListName + " " + mail.MailListMail));
            this.Master._Logger.Log(new AdminException(mail.MailListName + " " + mail.MailListMail + " Was Successfully Added"),
                            MethodBase.GetCurrentMethod().Name);
            this.mailListMailSelector.Items.Add(new ListItem(mail.MailListName + " " + mail.MailListMail, "s" + mail.MailListID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(41, "Red", name + " " + mailAddress));
        }
        finally
        {
            this.ClearFields(4);
        }
    }

    private void RemoveMail()
    {
        if (this.mailActionHiddenRe2.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                    (". this.mailActionHidden2.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        MailList mail = (MailList)this.Master._CoachingDal.Get("mailList", this.mailActionHiddenRe2.Value);
        if (mail == null)
        {
            this.Master._Logger.Error(new AdminException
                    (". mail == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        try
        {
            this.Master._CoachingDal.Delete("mailList", this.mailActionHiddenRe2.Value);
            this.Notify(this.Master._Notifier.Notify(28, "White", mail.MailListName + " " + mail.MailListMail));
            this.Master._Logger.Log(new AdminException(". " + mail.MailListName + " " + mail.MailListMail +
                                " Was Successfully Removed"), MethodBase.GetCurrentMethod().Name);

            this.mailListMailSelector.Items.Remove(this.mailListMailSelector.Items.FindByValue("s" + mail.MailListID));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(38, "Red", mail.MailListName + " " + mail.MailListMail));
        }
        finally
        {
            this.ClearFields(5);
            this.ClearFields(7);
        }
    }

    private bool ValidateMailBeforeAddUpdate(string name, string mailAddress)
    {
        if (name == "" || name == null)
        {
            this.Master._Logger.Error(new AdminException
                    (". name == \"\" || name == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return false;
        }

        if (mailAddress == "" || mailAddress == null)
        {
            this.Master._Logger.Error(new AdminException
                    (". mailAddress == \"\" || mailAddress == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return false;
        }

        if (this.mailActionHiddenUp2.Value == "")
        {
            if (this.Master._CoachingDal.GetMailListByMailAddress(mailAddress) != null)
            {
                this.Master._Logger.Warn(new AdminException(". this.Master._CoachingDal.GetMailListByMailAddress(this.mailAddress.Text) != null"),
                    MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(40, "Red", this.mailAddress.Text));
                return false;
            }
        }
        else
        {
            MailList mail = (MailList)this.Master._CoachingDal.Get("mailList", this.mailActionHiddenUp2.Value);
            if (mail == null)
            {
                this.Master._Logger.Error(new AdminException
                        (". mail == null"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
                this.ClearFields(6);
                return false;
            }

            if (!this.Master._CoachingDal.GetMailListByMailAddressExcept
                (mail.MailListMail, mailAddress))
            {
                this.Master._Logger.Warn(new AdminException
                (". !this.Master._CoachingDal.GetMailListByMailAddressExcept(mail.MailListMail, mailAddress)"),
                        MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(40, "Red", this.mailAddress.Text));
                return false;
            }
        }

        if (!this.Master._GlobalFunctions.ValidateMailAddress(mailAddress))
        {
            this.Master._Logger.Warn(new AdminException(". !this.Master._CoachingDal.ValidateMailAddress(mailAddress)"),
                     MethodBase.GetCurrentMethod().Name);
            this.mailAddLabel.Text = "Illegal Mail Address";
            return false;
        }
        return true;
    }

    private void UpdateMail()
    {
        if (this.mailActionHiddenUp2.Value == "")
        {
            this.Master._Logger.Error(new AdminException
                (". this.mailActionHidden2.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.ClearFields(6);
            return;
        }

        if (!ValidateMailBeforeAddUpdate(this.mailName.Text, this.mailAddress.Text))
        {
            return;
        }

        MailList mail = (MailList)this.Master._CoachingDal.Get("mailList", this.mailActionHiddenUp2.Value);
        if (mail == null)
        {
            this.Master._Logger.Error(new AdminException
                    (". mail == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.ClearFields(6);
            return;
        }

        try
        {
            mail.MailListMail = this.mailAddress.Text;
            mail.MailListName = this.mailName.Text;

            this.Master._CoachingDal.Update("mailList", mail, TimeNow.TheTimeNow);
            this.Notify(this.Master._Notifier.Notify(27, "White", mail.MailListName + " " + mail.MailListMail));
            this.Master._Logger.Log(new AdminException(". " + mail.MailListName + " " + mail.MailListMail +
                                " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);

            this.mailListMailSelector.Items.FindByValue("s" + mail.MailListID).Text = mail.MailListName + " " + mail.MailListMail;
            this.MailEnableDisableButtons(true);
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(39, "Red", mail.MailListName + " " + mail.MailListMail));
        }
    }

    protected bool ValidateFields(string type)
    {
        if (type == "" || type == null)
        {
            this.Master._Logger.Error(new AdminException
                (". type is null or empty"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return false;
        }

        switch (type)
        {
            case "mail1":
                if (this.mailListSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.mailListSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(3, "Red", ""));
                    return false;
                }
                break;
            case "mail3":
                if (this.mailSearchText.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException
                        (". this.mailSearchText.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.mailError.Text = "Please Enter Mail Address";
                    this.ClearFields(5);
                    this.ClearFields(6);
                    return false;
                }
                break;
            case "mail4":
                if (this.mailName.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException
                         (". this.mailName.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.mailAddLabel.Text = "Please Enter Mail Name";
                    return false;
                }

                if (this.mailAddress.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException
                         (". this.mailAddress.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.mailAddLabel.Text = "Please Enter Mail Address";
                    return false;
                }
                break;
            case "mail6":
                if (this.mailLanguageSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.mailLanguageSelector.SelectedIndex == -1"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(18, "Red", ""));
                    return false;
                }

                if (this.mailTitle.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.mailTitle.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(44, "Red", ""));
                    return false;
                }

                if (this.sendMailEditor.Value == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.sendMailEditor.Value == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(45, "Red", ""));
                    return false;
                }
                break;
            case "mail7":
                if (this.geMailsHidden.Value == "yes")
                {
                    if (this.fromDateBox.Text == "")
                    {
                        this.Master._Logger.Warn(new AdminException(". this.fromDateBox.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                        this.Notify(this.Master._Notifier.Notify(47, "Red", ""));
                        return false;
                    }

                    if (this.toDateBox.Text == "")
                    {
                        this.Master._Logger.Warn(new AdminException(". this.toDateBox.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                        this.Notify(this.Master._Notifier.Notify(48, "Red", ""));
                        return false;
                    }

                    DateTime fromDate = default(DateTime);
                    DateTime toDate = default(DateTime);

                    if (!DateTime.TryParse(this.fromDateBox.Text, out fromDate))
                    {
                        this.Master._Logger.Warn(new AdminException(". (!DateTime.TryParse(this.fromDateBox.Text, out testDate)"), MethodBase.GetCurrentMethod().Name);
                        this.Notify(this.Master._Notifier.Notify(49, "Red", this.fromDateBox.Text));
                        return false;
                    }

                    if (!DateTime.TryParse(this.toDateBox.Text, out toDate))
                    {
                        this.Master._Logger.Warn(new AdminException(". (!DateTime.TryParse(this.toDateBox.Text, out testDate)"), MethodBase.GetCurrentMethod().Name);
                        this.Notify(this.Master._Notifier.Notify(49, "Red", this.toDateBox.Text));
                        return false;
                    }

                    if (fromDate != default(DateTime) && toDate != default(DateTime))
                    {
                        if (toDate < fromDate)
                        {
                            this.Master._Logger.Warn(new AdminException(". (!DateTime.TryParse(this.toDateBox.Text, out testDate)"), MethodBase.GetCurrentMethod().Name);
                            this.Notify(this.Master._Notifier.Notify(51, "Red", fromDate.ToShortDateString() + "," + toDate.ToShortDateString()));
                            return false;
                        }
                    }
                }
                break;
            default:
                break;
        }
        return true;
    }

    protected void okGetMails_Click(object sender, EventArgs e)
    {
        this.ClearFields(8);
        this.MailListDivSelector(-1);
        this.ClearSelection("main");
    }

    protected void mailListSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields("mail1"))
        {
            return;
        }

        this.MailListSelectorAction(this.mailListSelector.SelectedIndex);
    }

    private void MailListSelectorAction(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException
                            (". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (action)
        {
            case 1:
                this.mailGrid.DataSource = this.Master._CoachingDal.GetAll("mailList");
                break;
            case 2:
                this.sendMail.Visible = true;
                this.sendMail.Attributes["class"] = "visi";

                this.mailsSelector.Items.Clear();
                foreach (MailList mail in this.Master._CoachingDal.GetAllActiveMailList())
                {
                    this.mailsSelector.Items.Add(new ListItem(mail.MailListName, mail.MailListID));
                }
                break;
            default:
                break;
        }
        this.mailGrid.DataBind();
        this.MailListDivSelector(action);
    }

    private void MailListDivSelector(int action)
    {
        this.mailData.Visible = false;
        this.mailActions.Visible = false;
        this.getMails.Visible = false;
        this.mailListsNotify.Visible = false;
        this.sendMail.Visible = false;

        this.mailData.Attributes["class"] = "unVisi";
        this.mailActions.Attributes["class"] = "unVisi";
        this.getMails.Attributes["class"] = "unVisi";
        this.mailListsNotify.Attributes["class"] = "unVisi";
        this.sendMail.Attributes["class"] = "unVisi";

        switch (action)
        {
            case 1:
                this.mailData.Visible = true;
                this.mailData.Attributes["class"] = "visi";
                break;
            case 2:
                this.sendMail.Visible = true;
                this.sendMail.Attributes["class"] = "visi";
                break;
            case 3:
                this.mailActions.Visible = true;
                this.mailActions.Attributes["class"] = "visi";
                break;
            case 4:
                this.getMails.Visible = true;
                this.getMails.Attributes["class"] = "visi";
                break;
            case 7:
                this.mailListsNotify.Visible = true;
                this.mailListsNotify.Attributes["class"] = "visi";
                break;
            default:
                break;
        }
    }

    private MailList ValidateEnableDisableMail(string mailListID, int status)
    {
        if (mailListID == "" || mailListID == null)
        {
            this.Master._Logger.Error(new AdminException
                (". mailListID  == \"\" || mailListID  == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return null;
        }

        if (status <= 0 || status > 2)
        {
            this.Master._Logger.Error(new AdminException
                (". status <= 0 || status > 2"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return null;
        }

        MailList mail = (MailList)this.Master._CoachingDal.Get("mailList", mailListID);

        if (mail == null)
        {
            this.Master._Logger.Error(new AdminException
            (". mail == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return null;
        }

        if (mail.Active == status)
        {
            this.Master._Logger.Warn(new AdminException
            (". mail.Active == int.Parse(status)"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(35, "Red", mail.MailListName + " " + mail.MailListMail));
            return null;
        }
        return mail;
    }

    protected void mailEnableBut_Click(object sender, EventArgs e)
    {
        string mailListID = "";

        try
        {
            mailListID = ((LinkButton)sender).CommandName;

        }
        catch (Exception j)
        {
            this.Master._Logger.Error(j, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        MailList mail = (MailList)this.Master._CoachingDal.Get("mailList", mailListID);
        if (mail == null)
        {
            this.Master._Logger.Error(new AdminException
                            (". mail == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        if (mail.Active == 1)
        {
            this.Master._Logger.Error(new AdminException
                (". mail.Active == 1"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(35, "Red", mail.MailListName + " " + mail.MailListMail));
            return;
        }

        try
        {
            this.Master._CoachingDal.EnableMailList(mail.MailListID);
            this.Notify(this.Master._Notifier.Notify(32, "White", mail.MailListName + " " + mail.MailListMail));
            this.Master._Logger.Log(new AdminException(". " + mail.MailListName + " " + mail.MailListMail +
                                                        " Was Successfully Enabled"), MethodBase.GetCurrentMethod().Name);

            this.mailGrid.DataSource = this.Master._CoachingDal.GetAll("mailList");
            this.mailGrid.DataBind();
        }
        catch (Exception m)
        {
            this.Master._Logger.Error(m, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
        }
    }

    protected void mailDisableBut_Click(object sender, EventArgs e)
    {
        string mailListID = "";

        try
        {
            mailListID = ((LinkButton)sender).CommandName;

        }
        catch (Exception j)
        {
            this.Master._Logger.Error(j, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }


        if (mailListID == "" || mailListID == null)
        {
            this.Master._Logger.Error(new AdminException
                (". mailListID  == \"\" || mailListID  == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        MailList mail = (MailList)this.Master._CoachingDal.Get("mailList", mailListID);
        if (mail == null)
        {
            this.Master._Logger.Error(new AdminException
                            (". mail == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        if (mail.Active == 2)
        {
            this.Master._Logger.Warn(new AdminException
                 (". mail.Active == 2"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(34, "Red", mail.MailListName + " " + mail.MailListMail));
            return;
        }

        try
        {
            this.Master._CoachingDal.DisableMailList(mail.MailListID);
            this.Notify(this.Master._Notifier.Notify(33, "White", mail.MailListName + " " + mail.MailListMail));
            this.Master._Logger.Log(new AdminException(". " + mail.MailListName + " " + mail.MailListMail +
                                            " Was Successfully Disabled"), MethodBase.GetCurrentMethod().Name);

            this.mailGrid.DataSource = this.Master._CoachingDal.GetAll("mailList");
            this.mailGrid.DataBind();
        }
        catch (Exception m)
        {
            this.Master._Logger.Error(m, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
        }
    }

    protected void mailActionSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.ClearFields(6);
        this.ClearFields(7);
        this.ClearFields(1);
        this.MailActionDivSwitcher(this.mailActionSelector.SelectedValue);
    }

    private void MailActionDivSwitcher(string action)
    {
        if (action == "" || action == null)
        {
            this.Master._Logger.Error(new AdminException
                            (". action == \"\" || action == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.addMail.Visible = false;
        this.removeUpdateMail.Visible = false;

        this.addMail.Attributes["class"] = "unVisi";
        this.removeUpdateMail.Attributes["class"] = "unVisi";

        switch (action)
        {
            case "1":
                this.addMail.Visible = true;
                this.addMail.Attributes["class"] = "visi";
                break;
            case "2":
                this.removeUpdateMail.Visible = true;
                this.removeUpdateMail.Attributes["class"] = "visi";
                this.mailActionHiddenRe1.Value = action;
                break;
            case "3":
                this.removeUpdateMail.Visible = true;
                this.removeUpdateMail.Attributes["class"] = "visi";
                this.mailActionHiddenUp1.Value = action;
                break;
            default:
                break;
        }
    }

    protected void mailSaveActionButton_Click(object sender, EventArgs e)
    {
        if (this.mailActionHiddenUp2.Value == "")
        {
            this.AddMail(this.mailName.Text, this.mailAddress.Text);
        }
        else
        {
            this.UpdateMail();
        }
    }

    protected void mailCancelActionAddButton_Click(object sender, EventArgs e)
    {
        this.AfterUpdate();
    }

    protected void mailCancelActionRemoveButton_Click(object sender, EventArgs e)
    {
        this.MailActionDivSwitcher("2");
    }

    protected void mailSelectActionButton_Click(object sender, EventArgs e)
    {
        string value = this.mailListMailSelector.SelectedValue.Remove(0, 1);

        if (this.mailActionHiddenRe1.Value == "")
        {
            this.mailActionHiddenUp2.Value = value;

            MailList mail = (MailList)this.Master._CoachingDal.Get("mailList", value);
            if (mail == null)
            {
                this.Master._Logger.Warn(new AdminException(". mail == null"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(23, "Red", this.mailSearchText.Text));
                return;
            }
            this.UpdateMailInit(mail);
        }
        else
        {
            this.mailActionHiddenRe2.Value = value;
            this.Notify(this.Master._Notifier.Notify(37, "Red", this.mailListMailSelector.SelectedItem.Text));
        }
    }

    protected void mailEnableActionButton_Click(object sender, EventArgs e)
    {
        if (this.mailActionHiddenUp2.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.mailActionHiddenUp2.Value == \"\""),
                        MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        MailList mail = this.ValidateEnableDisableMail(this.mailActionHiddenUp2.Value, 1);
        if (mail == null)
        {
            return;
        }

        try
        {
            this.Master._CoachingDal.EnableMailList(mail.MailListID);
            this.Notify(this.Master._Notifier.Notify(32, "White", mail.MailListName + " " + mail.MailListMail));
            this.Master._Logger.Log(new AdminException(". " + mail.MailListName + " " + mail.MailListMail +
                                " Was Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
        }
        catch (Exception m)
        {
            this.Master._Logger.Error(m, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(42, "Red", mail.MailListName + " " + mail.MailListMail));
        }
    }

    protected void mailDisableActionButton_Click(object sender, EventArgs e)
    {
        if (this.mailActionHiddenUp2.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.mailActionHiddenUp2.Value == \"\""),
                        MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        MailList mail = this.ValidateEnableDisableMail(this.mailActionHiddenUp2.Value, 2);
        if (mail == null)
        {
            return;
        }

        try
        {
            this.Master._CoachingDal.DisableMailList(mail.MailListID);
            this.Notify(this.Master._Notifier.Notify(33, "White", mail.MailListName + " " + mail.MailListMail));
            this.Master._Logger.Log(new AdminException(". " + mail.MailListName + " " + mail.MailListMail +
                                " Was Successfully Disabled"), MethodBase.GetCurrentMethod().Name);
        }
        catch (Exception m)
        {
            this.Master._Logger.Error(m, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(43, "White", mail.MailListName + " " + mail.MailListMail));
        }
    }

    protected void mailSearchSearchButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields("mail3"))
        {
            this.ClearFields(5);
            this.ClearFields(6);
            return;
        }

        MailList mail = (MailList)this.Master._CoachingDal.GetMailListByMailAddress(this.mailSearchText.Text);
        if (mail == null)
        {
            this.Master._Logger.Warn(new AdminException(". mail == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(36, "Red", this.mailSearchText.Text));
            return;
        }

        if (this.mailActionHiddenRe1.Value == "2")
        {
            this.Notify(this.Master._Notifier.Notify(37, "Red", mail.MailListName + " " + mail.MailListMail));
            this.mailActionHiddenRe2.Value = mail.MailListID;
        }
        else
        {
            this.UpdateMailInit(mail);
        }
    }

    private void UpdateMailInit(MailList mail)
    {
        if (mail == null)
        {
            this.Master._Logger.Warn(new AdminException(". mail == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", this.mailSearchText.Text));
            return;
        }

        this.MailEnableDisableButtons(true);
        this.mailStatus.Text = mail.spActvie;
        this.mailJoinDate.Text = mail.spMailListJoinTime;
        this.mailAddress.Text = mail.MailListMail;
        this.mailName.Text = mail.MailListName;
        this.mailAdditionalInfo.Visible = true;
        this.MailActionDivSwitcher("1");

        this.mailActionHiddenUp2.Value = mail.MailListID;
    }

    protected void mailSearchCancelButton_Click(object sender, EventArgs e)
    {
        this.AfterRemove();
    }

    private void ClearFields(int section)
    {
        if (section <= 0)
        {
            this.Master._Logger.Error(new AdminException(". section <= 0"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (section)
        {
            case 1:
                this.mailAddress.Text = "";
                this.mailName.Text = "";
                this.mailJoinDate.Text = "";
                this.mailStatus.Text = "";
                break;
            case 2:
                foreach (ListItem mail in this.mailsSelector.Items)
                {
                    mail.Selected = false;
                }
                this.sendMailEditor.Value = "";
                this.mailTitle.Text = "";
                this.mailLanguageSelector.SelectedIndex = 0;
                break;
            case 3:
                this.mailsResult.Text = "";
                break;
            case 4:
                this.mailAddLabel.Text = "";
                this.mailName.Text = "";
                this.mailAddress.Text = "";
                break;
            case 5:
                this.mailActionHiddenRe1.Value = "";
                this.mailActionHiddenRe2.Value = "";
                break;
            case 6:
                this.mailActionHiddenUp1.Value = "";
                this.mailActionHiddenUp2.Value = "";
                break;
            case 7:
                this.mailSearchText.Text = "";
                break;
            case 8:
                this.mailsResult.Text = "";
                this.fromDateBox.Text = "";
                this.toDateBox.Text = "";
                break;
            default:
                break;
        }
    }

    protected void getMailsButton_Click(object sender, EventArgs e)
    {
        this.ClearFields(3);

        if (!this.ValidateFields("mail7"))
        {
            return;
        }

        if (this.geMailsHidden.Value == "no")
        {
            foreach (MailList mail in this.Master._CoachingDal.GetAllActiveMailList())
            {
                this.mailsResult.Text += mail.MailListMail + ", ";
            }
        }
        else
        {
            DateTime fromDate = DateTime.Parse(this.fromDateBox.Text);
            DateTime toDate = DateTime.Parse(this.toDateBox.Text);
            foreach (MailList mail in this.Master._CoachingDal.GetJoinMailListFromDateToDate(fromDate, toDate))
            {
                this.mailsResult.Text += mail.MailListMail + ", ";
            }
        }
    }

    protected void sendMailCancelEditorButton_Click(object sender, EventArgs e)
    {
        this.AfterRemove();
    }

    protected void sendMailSendEditorButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields("mail6"))
        {
            return;
        }

        Dictionary<int, List<string>> list = new Dictionary<int, List<string>>();

        foreach (ListItem mail in this.mailsSelector.Items)
        {
            if (mail.Selected)
            {
                MailList m = (MailList)this.Master._CoachingDal.Get("mailList", mail.Value);
                if (m == null)
                {
                    this.Master._Logger.Error(new AdminException(". m == null"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
                    return;
                }

                try
                {
                    this.Master._GlobalFunctions.SendMailToMailList(m, this.sendMailEditor.Value, this.mailTitle.Text);
                    if (list.ContainsKey(1))
                    {
                        list[1].Add(m.MailListName + " " + m.MailListMail);
                    }
                    else
                    {
                        list.Add(1, new List<string>());
                        list[1].Add(m.MailListName + " " + m.MailListMail);
                    }
                }
                catch (Exception)
                {
                    if (list.ContainsKey(2))
                    {
                        list[2].Add(m.MailListName + " " + m.MailListMail);
                    }
                    else
                    {
                        list.Add(2, new List<string>());
                        list[2].Add(m.MailListName + " " + m.MailListMail);
                    }
                }
            }
        }

        StringBuilder build = new StringBuilder("<table><tr><td>Mail " + this.mailTitle.Text + "</td></tr>");
        StringBuilder good = new StringBuilder("<tr><td>Successfully Sent To:</td></tr>");
        StringBuilder bad = new StringBuilder("<tr><td>Failed To Send To:</td></tr>");
        StringBuilder message = new StringBuilder();

        if (list.ContainsKey(1))
        {
            foreach (string mail in list[1])
            {
                message.Append(" Good: " + mail);
                good.Append("<tr><td>" + mail + "</td></tr>");
            }
        }
        if (list.ContainsKey(2))
        {
            foreach (string mail in list[2])
            {
                message.Append(" Bad: " + mail);
                bad.Append("<tr><td>" + mail + "</td></tr>");
            }
        }

        build.Append(good.ToString()).Append(bad.ToString()).Append("</table>");

        this.Master._Logger.Log(new AdminException(". " + message), MethodBase.GetCurrentMethod().Name);
        this.Notify(this.Master._Notifier.Notify(46, "e", build.ToString()));
        this.ClearFields(2);
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (this.mailHiddenSelection.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". mailHiddenSelection.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.AfterOkClick(this.mailHiddenSelection.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.AfterRemove();
    }

    private void AfterOkClick(string selection)
    {
        if (selection == "")
        {
            this.Master._Logger.Error(new AdminException(". selection == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.mailHiddenSelection.Value = "";
            return;
        }

        switch (selection)
        {
            case "1":
                this.AfterRemove();
                this.mailHiddenSelection.Value = "";
                break;
            case "3":
                this.MailListDivSelector(8);
                break;
            case "26":
            case "41":
            case "36":
            case "40":
                this.MailListDivSelector(3);
                break;
            case "32":
            case "33":
            case "34":
            case "35":
            case "42":
            case "43":
                if (this.mailActionHiddenUp2.Value == "")
                {
                    this.MailListDivSelector(1);
                    this.ClearSelection("main");
                }
                else
                {
                    this.AfterUpdate();
                }
                break;
            case "44":
            case "45":
            case "46":
            case "18":
                this.MailListDivSelector(2);
                break;
            case "39":
            case "27":
                this.AfterUpdate();
                break;
            case "37":
                this.RemoveMail();
                break;
            case "28":
                this.AfterRemove();
                break;
            case "47":
            case "48":
            case "49":
            case "51":
                this.ClearFields(8);
                this.MailListDivSelector(4);
                break;
            case "23":
                this.ClearFields(1);
                this.ClearFields(2);
                this.ClearFields(3);
                this.ClearFields(4);
                this.ClearFields(5);
                this.ClearFields(6);
                this.ClearFields(7);
                this.mailAdditionalInfo.Visible = false;
                this.MailEnableDisableButtons(false);
                this.ClearSelection("main");
                this.ClearSelection("action");
                this.Master.Exit();
                break;
            default:
                break;
        }

        if (selection != "37")
        {
            this.mailHiddenSelection.Value = "";
        }
    }

    private void AfterRemove()
    {
        this.ClearFields(5);
        this.ClearFields(7);
        this.ClearSelection("action");
        this.MailActionDivSwitcher("1");
        this.MailListDivSelector(3);
    }

    private void AfterUpdate()
    {
        this.ClearFields(7);
        this.ClearFields(6);
        this.ClearFields(1);
        this.mailAdditionalInfo.Visible = false;
        this.ClearSelection("action");
        this.MailActionDivSwitcher("1");
        this.MailListDivSelector(3);
        this.MailEnableDisableButtons(false);
    }

    private void MailEnableDisableButtons(bool active)
    {
        this.mailEnableActionButton.Visible = active;
        this.mailDisableActionButton.Visible = active;
    }

    private void Notify(string[] message)
    {
        this.test.InnerHtml = "";
        this.cancelBut.Visible = false;

        try
        {
            if (message == null || message.Count() != 3)
            {
                this.mailHiddenSelection.Value = "";
                throw new AdminException(". message == null || message.Count() != 3");
            }

            if (message[0] == "" || message[0] == null ||
                message[1] == "" || message[1] == null ||
                message[2] == "" || message[2] == null)
            {
                this.mailHiddenSelection.Value = "";
                throw new AdminException(@". message[0] == """" || message[0] == null || message[1] == """" ||
                                            message[1] == null || message[2] == """" || message[2] == null");
            }

            switch (message[1])
            {
                case "Red":
                    this.mailListsNotifyLabel.ForeColor = Color.Red;
                    break;
                case "White":
                    this.mailListsNotifyLabel.ForeColor = Color.Green;
                    break;
                default:
                    this.mailListsNotifyLabel.ForeColor = Color.Black;
                    break;
            }

            this.mailListsNotifyLabel.Text = message[0];
            this.mailHiddenSelection.Value = message[2];

            switch (message[2])
            {
                case "1":
                case "37":
                    this.cancelBut.Visible = true;
                    break;
                case "46":
                    this.test.InnerHtml = message[0];
                    this.mailListsNotifyLabel.Text = "";
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.mailListsNotifyLabel.ForeColor = Color.Red;
            this.mailListsNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.Master._MainSelector = false;
            this.MailListDivSelector(7);
        }
    }
}
