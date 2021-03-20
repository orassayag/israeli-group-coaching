using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.IO;
using System.Drawing;

public partial class Admin_Logs : System.Web.UI.Page
{
    private List<FileInfo> filesList = new List<FileInfo>();

    protected void Page_Load(object sender, EventArgs e)
    {
        this.GetLogs();
        this.DivSwitch(1);
    }

    private void GetLogs()
    {
        try
        {
            IEnumerable<FileInfo> files = this.Master._GlobalFunctions.GetLogToFiles();
            foreach (FileInfo file in files)
            {
                filesList.Add(file);
            }

            if ((this.logsDateSelector.Items.Count - 1) < files.Count())
            {
                int i = 0;
                this.logsDateSelector.Items.Clear();
                this.logsDateSelector.Items.Add(new ListItem("--Select Log File--", "s" + i.ToString()));
                foreach (FileInfo file in this.Master._GlobalFunctions.GetLogToFiles())
                {
                    i++;
                    this.logsDateSelector.Items.Add(new ListItem(file.Name, "s" + i.ToString()));
                }
            }
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
        }
    }

    private void RemoveLog()
    {
        if (!this.ValidateFields(2))
        {
            return;
        }

        string name = this.logsDateSelector.SelectedItem.Text;

        FileInfo f = this.filesList.SingleOrDefault(g => g.Name == this.logsDateSelector.SelectedItem.Text);
        if (f == null)
        {
            this.Master._Logger.Error(new AdminException(". f == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }
        try
        {
            f.Delete();

            this.Master._Logger.Log(new AdminException(". " + name +
                        " Was Successfully Removed"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(28, "White", name));

            this.logsDateSelector.Items.Remove(this.logsDateSelector.Items.FindByText(name));
            this.filesList.Remove(f);
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(38, "Red", name));
        }
    }

    private void RemoveAllLogs()
    {
        try
        {
            for (int i = 0; i < this.filesList.Count; i++)
            {
                if (this.filesList[i] != null)
                {
                    filesList[i].Delete();
                }
            }

            this.Master._Logger.Log(new AdminException(". All Log Files Has Been Successfully Removed"),
                                    MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(28, "White", "All Logs Files"));

            this.logsDateSelector.Items.Clear();
            this.filesList.Clear();
        }
        catch (Exception r)
        {
            this.Master._Logger.Error(r, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(38, "Red", "All Logs Files"));
        }
    }

    private void DivSwitch(int action)
    {
        this.getLogs.Visible = false;
        this.logsNotify.Visible = false;

        this.getLogs.Attributes["class"] = "unVisi";
        this.logsNotify.Attributes["class"] = "unVisi";

        switch (action)
        {
            case 1:
                this.getLogs.Visible = true;
                this.getLogs.Attributes["class"] = "visi";
                break;
            case 2:
                this.logsNotify.Attributes["class"] = "visi";
                this.logsNotify.Visible = true;
                break;
            default:
                break;
        }
    }

    protected void logsDateSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            return;
        }

        FileInfo file = this.filesList.SingleOrDefault
                        (g => g.Name == this.logsDateSelector.SelectedItem.Text);

        if (file == null)
        {
            this.Master._Logger.Error(new AdminException(". file == null"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        StreamReader reader = null;

        try
        {
            reader = new StreamReader(file.FullName);

            this.logsBody.Text = "";

            while (!reader.EndOfStream)
            {
                this.logsBody.Text += reader.ReadLine() + "\n\r";
            }
        }
        catch (Exception p)
        {
            this.Master._Logger.Error(p, MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(58, "Red", file.Name));
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
        }
    }

    protected void okLogsButton_Click(object sender, EventArgs e)
    {
        this.Start();
    }

    private void Start()
    {
        foreach (ListItem file in this.logsDateSelector.Items)
        {
            file.Selected = false;
        }
        this.logsBody.Text = "";
    }

    protected void deleteLogButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(2))
        {
            return;
        }

        this.Notify(this.Master._Notifier.Notify(1, "Red", this.logsDateSelector.SelectedItem.Text));
    }

    protected void deleteAllLogsButton_Click(object sender, EventArgs e)
    {
        if (!this.ValidateFields(1))
        {
            return;
        }

        this.Notify(this.Master._Notifier.Notify(60, "Red", ""));
    }

    private bool ValidateFields(int action)
    {
        if (action <= 0)
        {
            this.Master._Logger.Error(new AdminException(". action <= 0"), MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return false;
        }

        switch (action)
        {
            case 1:
                if (filesList.Count == 0)
                {
                    this.Master._Logger.Error(new AdminException(". filesList.Count == 0"),
                                                  MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(56, "Red", ""));
                    return false;
                }
                break;
            case 2:
                if (this.logsDateSelector.SelectedValue == "")
                {
                    this.Master._Logger.Error(new AdminException(". this.logsDateSelector.SelectedValue == \"\""),
                                      MethodBase.GetCurrentMethod().Name);
                    this.Notify(this.Master._Notifier.Notify(57, "Red", ""));
                    return false;
                }
                break;
            default:
                break;
        }
        return true;
    }

    protected void okBut_Click(object sender, EventArgs e)
    {
        if (this.logsHidden.Value == "")
        {
            this.Master._Logger.Error(new AdminException(". this.logsHidden.Value == \"\""),
                    MethodBase.GetCurrentMethod().Name);
            this.Notify(this.Master._Notifier.Notify(23, "Red", ""));
            return;
        }

        this.AfterOk(this.logsHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
        this.Start();
        this.DivSwitch(1);
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
                this.RemoveLog();
                break;
            case "28":
            case "29":
                //remove file
                break;
            case "57":
                //select log to read
                break;
            case "56":
                //no logs to read
                break;
            case "58":
                //failed to read log
                break;
            case "60":
                //delete all logs
                this.RemoveAllLogs();
                break;
            case "23":
                this.Master.Exit();
                break;
            default:
                break;
        }

        if (selector != "1" && selector != "60")
        {
            this.logsHidden.Value = "";
            this.DivSwitch(1);
        }
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
                this.logsNotifyLabel.ForeColor = Color.Red;
            }
            else
            {
                this.logsNotifyLabel.ForeColor = Color.Green;
            }

            this.logsNotifyLabel.Text = message[0];
            this.logsHidden.Value = message[2];

            switch (message[2])
            {
                case "1":
                case "60":
                    this.cancelBut.Visible = true;
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
            this.logsNotifyLabel.ForeColor = Color.Red;
            this.logsNotifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
        }
        finally
        {
            this.Master._MainSelector = false;
            this.DivSwitch(2);
            this.logsBody.Text = "";
        }
    }
}
