using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Drawing;

public partial class Admin_Graduates : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (this.graduatesSelector.Items.Count == 0)
            {
                this.graduatesSelector.Items.Add(new ListItem("--Select Action--", "0"));
                this.graduatesSelector.Items.Add(new ListItem("Add New Session", "1"));
                this.graduatesSelector.Items.Add(new ListItem("Remove/Update Session", "2"));
                this.graduatesSelector.Items.Add(new ListItem("Add New Graduate", "3"));
                this.graduatesSelector.Items.Add(new ListItem("Remove/Update Graduate", "4"));
            }

            ListItem start = new ListItem("--Select Session--", "0");

            if (this.graduatesRemoveUpdateSessionSelector.Items.Count == 0)
            {
                this.graduatesRemoveUpdateSessionSelector.Items.Add(start);
                foreach (GraduateSession session in (IEnumerable<GraduateSession>)this.Master._CoachingDal.GetAll("session"))
                {
                    this.graduatesRemoveUpdateSessionSelector.Items.Add(new ListItem(session.GraduateYearNumber +
                            ", " + session.GraduateYearHebrew, "s" + session.GraduateSessionID));
                }
            }

            if (this.addGraduateSelector.Items.Count == 0)
            {
                this.addGraduateSelector.Items.Add(start);
                foreach (GraduateSession session in (IEnumerable<GraduateSession>)this.Master._CoachingDal.GetAll("session"))
                {
                    this.addGraduateSelector.Items.Add(new ListItem(session.GraduateYearNumber +
                            ", " + session.GraduateYearHebrew, "s" + session.GraduateSessionID));
                }
            }

            if (this.selectSessionGraduate.Items.Count == 0)
            {
                this.selectSessionGraduate.Items.Add(start);
                foreach (GraduateSession session in (IEnumerable<GraduateSession>)this.Master._CoachingDal.GetAll("session"))
                {
                    this.selectSessionGraduate.Items.Add(new ListItem(session.GraduateYearNumber +
                            ", " + session.GraduateYearHebrew, "s" + session.GraduateSessionID));
                }
            }
        }
    }

    protected void graduatesSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields("graduate1"))
        {
            return;
        }

        this.ClearFields(1);
        this.ClearFields(2);
        this.ClearFields(3);
        this.ClearFields(4);
        this.ClearFields(6);
        this.SelectGraduateDiv(false);


        this.SwitchDivs(this.graduatesSelector.SelectedIndex);
    }

    private void ClearFields(int action)
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
                foreach (ListItem m in this.graduatesYearNumberSelector.Items)
                {
                    m.Selected = false;
                }
                this.graduatesYearHebrew.Text = "";
                this.addSessionLabel.Text = "";
                this.graduatesPlaceSelector.Text = "";
                break;
            case 2:
                foreach (ListItem m in this.graduatesRemoveUpdateSessionSelector.Items)
                {
                    m.Selected = false;
                }
                this.removeSessionHidden.Value = "";
                break;
            case 3:
                foreach (ListItem m in this.addGraduateSelector.Items)
                {
                    m.Selected = false;
                }
                this.addGraduateLabel.Text = "";
                this.addGraduateName.Text = "";
                break;
            case 4:
                foreach (ListItem m in this.selectSessionGraduate.Items)
                {
                    m.Selected = false;
                }
                foreach (ListItem m in this.selectGraduatesGraduate.Items)
                {
                    m.Selected = false;
                }
                this.addGraduateName.Text = "";
                this.graduateHiddenRe.Value = "";
                break;
            case 5:
                foreach (ListItem item in this.graduatesSelector.Items)
                {
                    item.Selected = false;
                }
                break;
            case 6:
                foreach (ListItem item in this.selectGraduatesGraduate.Items)
                {
                    item.Selected = false;
                }
                break;
            default:
                break;
        }
    }

    private void SwitchDivs(int index)
    {
        if (index <= 0)
        {
            this.Master._Logger.Error(new AdminException
                (". index <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.graduatesAddNewSession.Visible = false;
        this.graduatesUpdateRemoveSession.Visible = false;
        this.graduatesAddNewGraduate.Visible = false;
        this.graduatesRemoveGraduate.Visible = false;
        this.graduatesNotify.Visible = false;

        this.graduatesAddNewSession.Attributes["class"] = "unVisi";
        this.graduatesUpdateRemoveSession.Attributes["class"] = "unVisi";
        this.graduatesAddNewGraduate.Attributes["class"] = "unVisi";
        this.graduatesRemoveGraduate.Attributes["class"] = "unVisi";
        this.graduatesNotify.Attributes["class"] = "unVisi";

        switch (index)
        {
            case 1:
                this.graduatesAddNewSession.Visible = true;
                this.graduatesAddNewSession.Attributes["class"] = "visi";

                if (this.graduatesYearNumberSelector.Items.Count == 0)
                {
                    this.graduatesYearNumberSelector.Items.Add(new ListItem("--Select Year--", "0"));
                    for (int i = 2010; i > 1980; i--)
                    {
                        this.graduatesYearNumberSelector.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                }

                if (this.sessionHiddenUp.Value != "")
                {
                    GraduateSession session = (GraduateSession)this.Master._CoachingDal.Get("session", this.sessionHiddenUp.Value);
                    if (session == null)
                    {
                        this.Master._Logger.Warn(new AdminException(". session == null"), MethodBase.GetCurrentMethod().Name);
                        this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
                        return;
                    }

                    this.graduatesYearHebrew.Text = session.GraduateYearHebrew;
                    this.graduatesYearNumberSelector.Items.FindByValue(session.GraduateYearNumber).Selected = true;
                }
                break;
            case 2:
                this.graduatesUpdateRemoveSession.Visible = true;
                this.graduatesUpdateRemoveSession.Attributes["class"] = "visi";
                break;
            case 3:
                this.graduatesAddNewGraduate.Visible = true;
                this.graduatesAddNewGraduate.Attributes["class"] = "visi";
                break;
            case 4:
                this.graduatesRemoveGraduate.Visible = true;
                this.graduatesRemoveGraduate.Attributes["class"] = "visi";
                break;
            case 5:
                this.graduatesNotify.Visible = true;
                this.graduatesNotify.Attributes["class"] = "visi";
                break;
            default:
                break;
        }
    }

    protected bool ValidateFields(string type)
    {
        if (type == "" || type == null)
        {
            this.Master._Logger.Error(new AdminException
                (". type == \"\" || type == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return false;
        }

        switch (type)
        {
            case "graduate1":
                if (graduatesSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". graduatesSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(3, "Red", ""));
                    return false;
                }
                break;
            case "graduate2":
                if (this.graduatesYearNumberSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.graduatesYearNumberSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(52, "Red", ""));
                    return false;
                }
                if (this.graduatesYearHebrew.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.graduatesYearHebrew.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(50, "Red", ""));
                    return false;
                }

                if (this.graduatesPlaceSelector.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.graduatesPlaceSelector.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(73, "Red", ""));
                    return false;
                }
                break;
            case "graduate3":
                if (this.graduatesRemoveUpdateSessionSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.graduatesRemoveUpdateSessionSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(53, "Red", ""));
                    return false;
                }
                break;
            case "graduate4":
                if (this.addGraduateSelector.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.addGraduateSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(53, "Red", ""));
                    return false;
                }

                if (this.addGraduateName.Text == "")
                {
                    this.Master._Logger.Warn(new AdminException(". this.addGraduateName.Text == \"\""), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(54, "Red", ""));
                    return false;
                }
                break;
            case "graduate5":
                if (this.selectSessionGraduate.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.selectSessionGraduate.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(53, "Red", ""));
                    return false;
                }
                break;
            case "graduate6":
                if (this.selectSessionGraduate.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.selectSessionGraduate.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(53, "Red", ""));
                    return false;
                }

                if (this.selectGraduatesGraduate.SelectedIndex == 0)
                {
                    this.Master._Logger.Warn(new AdminException(". this.selectGraduatesGraduate.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(55, "Red", ""));
                    return false;
                }
                break;
            case "graduate7":
                {
                    int b = -1;
                    if (!int.TryParse(this.graduatesPlaceSelector.Text, out b))
                    {
                        this.Master._Logger.Warn(new AdminException(". !int.TryParse(this.graduatesPlaceSelector.Text, out b)"),
                        MethodBase.GetCurrentMethod().Name);
                        this.Notify(this.Master._Notifier.Notify(74, "Red", ""));
                        return false;
                    }
                }
                break;
            default:
                break;
        }
        return true;
    }

    protected void graduatesSaveSession_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields("graduate2"))
        {
            return;
        }

        if (!this.ValidateFields("graduate7"))
        {
            return;
        }

        if (this.sessionHiddenUp.Value != "")
        {
            this.UpdateSession();
            this.sessionHiddenUp.Value = "";
        }
        else
        {
            this.AddSession();
        }
    }

    protected void graduatesCancelSession_Click(object sender, EventArgs e)
    {
        this.Start(2);
    }

    protected void graduatesRemoveSessionButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields("graduate3"))
        {
            return;
        }

        this.Notify(this.Master._Notifier.Notify(1, "Red",
        this.graduatesRemoveUpdateSessionSelector.SelectedItem.Text));
        this.sessionHiddenRe.Value =
        this.graduatesRemoveUpdateSessionSelector.SelectedValue.Remove(0, 1).ToString();
    }

    protected void graduatesUpdateSessionButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields("graduate3"))
        {
            return;
        }

        this.sessionHiddenUp.Value =
            this.graduatesRemoveUpdateSessionSelector.SelectedValue.Remove(0, 1).ToString();
        this.SwitchDivs(1);

        GraduateSession session = (GraduateSession)this.Master._CoachingDal.Get("session", this.sessionHiddenUp.Value);
        if (session == null)
        {
            this.Master._Logger.Warn(new AdminException(". session == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }
        this.ClearFields(1);
        this.graduatesYearHebrew.Text = session.GraduateYearHebrew;
        this.graduatesYearNumberSelector.Items.FindByValue(session.GraduateYearNumber).Selected = true;
        this.graduatesPlaceSelector.Text = session.GraduatePlace.ToString();
    }

    protected void graduatesRemoveUpdateCancelButton_Click(object sender, EventArgs e)
    {
        this.Start(2);
    }

    protected void graduatesSaveGraduate_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields("graduate4"))
        {
            return;
        }

        if (this.graduateHiddenUp.Value != "")
        {
            this.UpdateGraduate();
            this.graduateHiddenUp.Value = "";
        }
        else
        {
            this.AddGraduate();
        }
    }

    protected void graduatesCancelGraduate_Click(object sender, EventArgs e)
    {
        this.ClearFields(3);
        this.Start(2);
    }

    protected void selectSessionGraduate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!ValidateFields("graduate5"))
        {
            return;
        }

        GraduateSession session = (GraduateSession)this.Master._CoachingDal.Get("session",
                                   this.selectSessionGraduate.SelectedValue.Remove(0, 1));
        if (session == null)
        {
            this.Master._Logger.Warn(new AdminException(". session == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.SelectGraduateDiv(true);

        this.selectGraduatesGraduate.Items.Clear();
        this.selectGraduatesGraduate.Items.Add(new ListItem("--Select Graduate--", "0"));
        foreach (Graduate gra in this.Master._CoachingDal.GetAllGraduatesBySession(session.GraduateSessionID))
        {
            this.selectGraduatesGraduate.Items.Add(new ListItem(gra.GraduateName, "s" + gra.GraduateID));
        }
    }

    protected void graduatesRemoveCancelButton_Click(object sender, EventArgs e)
    {
        this.SelectGraduateDiv(false);
        this.Start(4);
    }

    protected void graduatesRemoveGraduateButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields("graduate6"))
        {
            return;
        }

        this.graduateHiddenRe.Value = this.selectGraduatesGraduate.SelectedValue.Remove(0, 1);
        this.Notify(this.Master._Notifier.Notify(1, "Red", this.selectGraduatesGraduate.SelectedItem.Text));
    }

    protected void graduatesUpdateGraduateButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields("graduate6"))
        {
            return;
        }

        this.graduateHiddenUp.Value = this.selectGraduatesGraduate.SelectedValue.Remove(0, 1).ToString();
        this.SwitchDivs(3);

        Graduate graduate = (Graduate)this.Master._CoachingDal.Get("graduate", this.graduateHiddenUp.Value);
        if (graduate == null)
        {
            this.Master._Logger.Warn(new AdminException(". graduate == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }
        this.ClearFields(3);

        this.addGraduateName.Text = graduate.GraduateName;
        this.addGraduateSelector.Items.FindByValue("s" + graduate.GraduateSessionID).Selected = true;
    }

    private void AddSession()
    {
        if (!this.Master._CoachingDal.CheckIfSessionPlaceAvailable(int.Parse(this.graduatesPlaceSelector.Text)))
        {
            this.Master._Logger.Warn(new AdminException
            (". !this.Master._CoachingDal.CheckIfSessionPlaceAvailable(int.Parse(this.graduatesPlaceSelector.Text))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(74, "Red", ""));
            return;
        }

        try
        {
            GraduateSession session = new GraduateSession
            {
                GraduatePlace = int.Parse(this.graduatesPlaceSelector.Text),
                GraduateSessionID = this.Master._CoachingDal.GetNextAvailableID("session"),
                GraduateYearHebrew = this.graduatesYearHebrew.Text,
                GraduateYearNumber = this.graduatesYearNumberSelector.SelectedValue,
                CreationTime = TimeNow.TheTimeNow,
                spCreationTime = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._CoachingDal.Add("session", session, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + this.graduatesYearHebrew.Text + " " +
            this.graduatesYearNumberSelector.SelectedValue + "Was Successfully Added"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(26, "White", this.graduatesYearHebrew.Text + " " + this.graduatesYearNumberSelector.SelectedValue));

            ListItem m = new ListItem(session.GraduateYearNumber +
                ", " + session.GraduateYearHebrew, "s" + session.GraduateSessionID);

            this.graduatesRemoveUpdateSessionSelector.Items.Add(m);
            this.addGraduateSelector.Items.Add(m);
            this.selectSessionGraduate.Items.Add(m);
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(41, "Red", this.graduatesYearHebrew.Text + " " + this.graduatesYearNumberSelector.SelectedValue));
        }
    }

    private void RemoveSession()
    {
        if (this.sessionHiddenRe.Value == "")
        {
            this.Master._Logger.Warn(new AdminException(". this.sessionHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.sessionHiddenRe.Value = "";
            return;
        }

        GraduateSession session = (GraduateSession)this.Master._CoachingDal.Get("session", this.sessionHiddenRe.Value);
        if (session == null)
        {
            this.Master._Logger.Warn(new AdminException(". session == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.sessionHiddenRe.Value = "";
            return;
        }

        try
        {
            this.Master._CoachingDal.DeleteAllGraduatesFromSession(this.sessionHiddenRe.Value);
            this.Master._CoachingDal.Delete("session", this.sessionHiddenRe.Value);
            this.Master._Logger.Log(new AdminException("." + session.GraduateYearHebrew +
            " " + session.GraduateYearNumber + " Was Successfully Removed"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(28, "White", session.GraduateYearHebrew +
            " " + session.GraduateYearNumber));

            ListItem r = (this.graduatesRemoveUpdateSessionSelector.Items.FindByValue("s" + session.GraduateSessionID));

            this.graduatesRemoveUpdateSessionSelector.Items.Remove(r);
            this.addGraduateSelector.Items.Remove(r);
            this.selectSessionGraduate.Items.Remove(r);
        }
        catch (Exception)
        {
            try
            {
                this.Master._CoachingDal.DeleteAllGraduatesFromSession(this.sessionHiddenRe.Value);
                this.Master._CoachingDal.Delete("session", this.sessionHiddenRe.Value);
                this.Master._Logger.Log(new AdminException("." + session.GraduateYearHebrew +
                " " + session.GraduateYearNumber + " Was Successfully Removed"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(28, "White", session.GraduateYearHebrew +
                " " + session.GraduateYearNumber));

                ListItem r = (this.graduatesRemoveUpdateSessionSelector.Items.FindByValue("s" + session.GraduateSessionID));

                this.graduatesRemoveUpdateSessionSelector.Items.Remove(r);
                this.addGraduateSelector.Items.Remove(r);
                this.selectSessionGraduate.Items.Remove(r);
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(38, "Red", session.GraduateYearHebrew +
                " " + session.GraduateYearNumber));
            }
        }
    }

    private void UpdateSession()
    {
        if (this.sessionHiddenUp.Value == "")
        {
            this.Master._Logger.Warn(new AdminException(". this.sessionHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        GraduateSession session = (GraduateSession)this.Master._CoachingDal.Get("session", this.sessionHiddenUp.Value);
        if (session == null)
        {
            this.Master._Logger.Warn(new AdminException(". session == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        if (!this.Master._CoachingDal.CheckIfSessionPlaceAvailableExcept(session.GraduatePlace,
            int.Parse(this.graduatesPlaceSelector.Text)))
        {
            this.Master._Logger.Warn(new AdminException
            (@". this.Master._CoachingDal.CheckIfSessionPlaceAvailableExcept(session.GraduatePlace,
            int.Parse(this.graduatesPlaceSelector.Text)))"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(74, "Red", ""));
            return;
        }

        try
        {
            session.GraduateYearHebrew = this.graduatesYearHebrew.Text;
            session.GraduateYearNumber = this.graduatesYearNumberSelector.SelectedValue;
            session.GraduatePlace = int.Parse(this.graduatesPlaceSelector.Text);

            this.Master._CoachingDal.Update("session", session, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + this.graduatesYearHebrew.Text + " " +
            this.graduatesYearNumberSelector.SelectedValue + "Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(27, "White", this.graduatesYearHebrew.Text + " " + this.graduatesYearNumberSelector.SelectedValue));

            ListItem r = (this.graduatesRemoveUpdateSessionSelector.Items.FindByValue("s" + session.GraduateSessionID));

            this.graduatesRemoveUpdateSessionSelector.Items.Remove(r);
            this.addGraduateSelector.Items.Remove(r);
            this.selectSessionGraduate.Items.Remove(r);

            ListItem m = new ListItem(session.GraduateYearNumber +
                     ", " + session.GraduateYearHebrew, "s" + session.GraduateSessionID);

            this.graduatesRemoveUpdateSessionSelector.Items.Add(m);
            this.addGraduateSelector.Items.Add(m);
            this.selectSessionGraduate.Items.Add(m);
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(25, "Red", this.graduatesYearHebrew.Text + " " + this.graduatesYearNumberSelector.SelectedValue));
        }
    }

    private void AddGraduate()
    {
        try
        {
            Graduate graduate = new Graduate
            {
                GraduateSessionID = this.addGraduateSelector.SelectedValue.Remove(0, 1),
                GraduateID = this.Master._CoachingDal.GetNextAvailableID("graduate"),
                GraduateName = this.addGraduateName.Text,
                CreationTime = TimeNow.TheTimeNow,
                spCreationTime = TimeNow.TheTimeNow.ToShortDateString()
            };

            this.Master._CoachingDal.Add("graduate", graduate, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + this.addGraduateName.Text + "Was Successfully Added"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(26, "White", this.addGraduateName.Text));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(41, "Red", this.addGraduateName.Text));
        }
    }

    private void RemoveGraduate()
    {
        if (this.graduateHiddenRe.Value == "")
        {
            this.Master._Logger.Warn(new AdminException(". this.removeGraduatesHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        Graduate graduate = (Graduate)this.Master._CoachingDal.Get("graduate", this.graduateHiddenRe.Value);
        if (graduate == null)
        {
            this.Master._Logger.Warn(new AdminException(". graduate == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            this.graduateHiddenRe.Value = "";
            return;
        }

        try
        {
            this.Master._CoachingDal.Delete("graduate", this.graduateHiddenRe.Value);
            this.Master._Logger.Log(new AdminException("." + graduate.GraduateName +
            " " + graduate.GraduateID + " Was Successfully Removed"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(28, "White", graduate.GraduateName));
        }
        catch (Exception)
        {
            try
            {
                this.Master._CoachingDal.Delete("graduate", this.graduateHiddenRe.Value);
                this.Master._Logger.Log(new AdminException("." + graduate.GraduateName +
                " " + graduate.GraduateID + " Was Successfully Removed"), MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(28, "White", graduate.GraduateName));
            }
            catch (Exception e)
            {
                this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
                this.Notify(this.Master._Notifier.Notify(38, "Red", graduate.GraduateName));
            }
        }
    }

    private void UpdateGraduate()
    {
        if (this.graduateHiddenUp.Value == "")
        {
            this.Master._Logger.Warn(new AdminException(". this.graduateHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        Graduate graduate = (Graduate)this.Master._CoachingDal.Get("graduate", this.graduateHiddenUp.Value);
        if (graduate == null)
        {
            this.Master._Logger.Warn(new AdminException(". graduate == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        try
        {
            graduate.GraduateName = this.addGraduateName.Text;
            graduate.GraduateSessionID = this.addGraduateSelector.SelectedValue.Remove(0, 1);

            this.Master._CoachingDal.Update("graduate", graduate, TimeNow.TheTimeNow);
            this.Master._Logger.Log(new AdminException(". " + this.addGraduateName.Text + " " +
            this.graduateHiddenUp.Value + "Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(27, "White", this.addGraduateName.Text));
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(25, "Red", this.addGraduateName.Text));
        }
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (this.graduatesHiddenSelection.Value == "")
        {
            this.Master._Logger.Warn(new AdminException(". this.graduatesHiddenSelection.Value == \"\""),
                                    MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.AfterOk(this.graduatesHiddenSelection.Value);
    }

    private void AfterOk(string selection)
    {
        if (selection == "" || selection == null)
        {
            this.Master._Logger.Warn(new AdminException(". selection == \"\" || selection == null"),
                        MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        switch (selection)
        {
            case "1":
                if (this.sessionHiddenRe.Value != "")
                {
                    this.RemoveSession();
                    this.sessionHiddenRe.Value = "";
                }

                if (this.graduateHiddenRe.Value != "")
                {
                    this.RemoveGraduate();
                    this.graduateHiddenRe.Value = "";
                }
                break;
            case "3":
                //select action
                this.SwitchDivs(8);
                break;
            case "23":
                this.ClearFields(1);
                this.ClearFields(2);
                this.ClearFields(3);
                this.ClearFields(4);
                this.Master.Exit();
                break;
            case "50":
            case "52":
            case "73":
            case "74":
                //select year session
                this.ClearFields(1);
                this.SwitchDivs(1);
                break;
            case "41":
            case "26":
                //add Session
                //add Gra
                this.ClearFields(1);
                this.ClearFields(5);
                this.ClearFields(3);
                this.SwitchDivs(8);
                break;
            case "53":
                this.ClearFields(3);
                this.Start(2);
                this.SelectGraduateDiv(false);
                //select session to update
                break;
            case "54":
                this.ClearFields(3);
                this.SwitchDivs(3);
                //enter gra name
                break;
            case "55":
                //select gra to update
                this.Start(4);
                this.SelectGraduateDiv(false);
                break;
            case "25":
            case "27":
                this.ClearFields(1);
                this.ClearFields(2);
                this.SelectGraduateDiv(false);
                this.Start(4);
                //update Gra
                //update session
                break;
            case "28":
            case "38":
                //remove Gra
                //remove Session
                this.SelectGraduateDiv(false);
                this.ClearFields(4);
                this.Start(2);
                break;
            case "37":
                break;
            default:
                break;
        }

        if (selection != "1")
        {
            this.graduatesHiddenSelection.Value = "";
        }
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.SelectGraduateDiv(false);
        this.ClearFields(2);
        this.Start(4);
    }

    private void Start(int clear)
    {
        if (clear <= 0)
        {
            this.Master._Logger.Warn(new AdminException(". clear <= 0"),
            MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
        }

        this.ClearFields(clear);
        this.ClearFields(5);
        this.SwitchDivs(8);
    }

    private void SelectGraduateDiv(bool action)
    {
        this.ClearFields(6);
        this.selectGraduate.Visible = action;
        if (action)
        {
            this.selectGraduate.Attributes["class"] = "visi";
        }
        else
        {
            this.selectGraduate.Attributes["class"] = "unVisi";
        }
    }

    private void Notify(string[] message)
    {
        this.cancelBut.Visible = false;
        try
        {
            if (message == null || message.Count() != 3)
            {
                this.graduatesHiddenSelection.Value = "";
                throw new AdminException(". message == null || message.Count() != 3");
            }

            if (message[0] == "" || message[0] == null ||
                message[1] == "" || message[1] == null ||
                message[2] == "" || message[2] == null)
            {
                this.graduatesHiddenSelection.Value = "";
                throw new AdminException(@". message[0] == """" || message[0] == null || message[1] == """" ||
                                            message[1] == null || message[2] == """" || message[2] == null");
            }

            if (message[1] == "Red")
            {
                this.graduatesNotifyLabel.ForeColor = Color.Red;
            }
            else
            {
                this.graduatesNotifyLabel.ForeColor = Color.Green;
            }

            this.graduatesNotifyLabel.Text = message[0];
            this.graduatesHiddenSelection.Value = message[2];

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
            this.graduatesNotifyLabel.ForeColor = Color.Red;
            this.graduatesNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.Master._MainSelector = false;
            this.SwitchDivs(5);
        }
    }
}
