using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Text;
using System.Net;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Net.NetworkInformation;

/// <summary>
/// Summary description for GlobalFunctions
/// </summary>
public class GlobalFunctions
{
    private CoachDal coachingDal = new CoachDal();

    public GlobalFunctions() { }

    public void SetPageSEO(string title, string description, string keywords, Page p)
    {
        if (p != null)
        {
            p.Title = "Group Coaching - " + title;
            //Sets language meta tag
            HtmlMeta metaLang = new HtmlMeta();
            metaLang.Attributes.Add("http-equiv", "content-language");
            metaLang.Attributes.Add("content", "text/html; charset=windows-1255"); //Hebrew
            p.Header.Controls.Add(metaLang);

            //Creates Dynamic Description
            HtmlMeta metaDesc = new HtmlMeta();
            metaDesc.Attributes.Add("name", "description");
            metaDesc.Attributes.Add("content", description);
            p.Header.Controls.Add(metaDesc);

            //Creates Dynamic Keys for Engines
            HtmlMeta metaKey = new HtmlMeta();
            metaKey.Attributes.Add("name", "keywords");
            metaKey.Attributes.Add("content", keywords);
            p.Header.Controls.Add(metaKey);

            //Tells Bots where to go
            HtmlMeta metaBot = new HtmlMeta();
            metaBot.Attributes.Add("name", "Robots");
            metaBot.Attributes.Add("content", "Index, All");
            p.Header.Controls.Add(metaBot);

        }
    }


    /// <summary>
    /// This method check if the uploaded file is with the ending of
    /// jpg,jpeg,png,gif
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public bool ValidatePicEnd(string path)
    {
        if (path != "")
        {
            Regex r = new Regex(@"(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$");
            return r.IsMatch(path);
        }
        return false;
    }

    public string FixNews(string news)
    {
        if (news == "")
        {
            return "";
        }

        return Regex.Replace(news, @"</?(?i:p)(.|\n)*?>", "");
    }

    public string FixBackUrl(string pageContent)
    {
        if (pageContent == "")
        {
            return "";
        }

        Regex r = new Regex(@"<a target\=[^>]*>([^<]*)</a>");

        return r.Replace(pageContent, " ");
    }

    public string FixUrl(string url)
    {
        if (url == "" || url == null)
        {
            return null;
        }

        string a = "http://";

        if (!url.Contains(a))
        {
            url = a + url;
        }
        return url;
    }

    public bool ValidateMailAddress(string mailAddress)
    {
        if (mailAddress == "" || mailAddress == null)
        {
            return false;
        }

        Regex reg = new Regex(@"/^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/");
        return !reg.IsMatch(mailAddress);
    }

    public bool SendMailToMailList(MailList mailList, string editorValue, string title)
    {
        if (mailList == null || editorValue == "" ||
            editorValue == null || title == "" || title == null)
        {
            return false;
        }

        MailMessage m = new MailMessage("webmaster@malliere.com", mailList.MailListMail); // itay gmail


        m.Subject = title;
        m.IsBodyHtml = true;

        editorValue = editorValue + string.Format(@"<br /><a href=""http://www.malliere.com/RemoveFromMailList.aspx?RemoveCode={0}"">לחץ כאן כדי להסיר עצמך מרשימת התפוצה</a>", mailList.MailRemoveCode);

        m.Body = editorValue;

        SmtpClient ss = new SmtpClient("mail.malliere.com"); // put here the address of where you send the mail from
        //(this is the place for the host mail address)
        ss.EnableSsl = false;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("carmella@malliere.com", "carmella"); //put here the user name and the password of the mail server host
        ss.Send(m);
        return true;
    }

    public string GetRemoveCode()
    {
        return Guid.NewGuid().ToString();
    }

    public IEnumerable<FileInfo> GetLogToFiles()
    {
        if (ConfigurationSettings.AppSettings["ErrorLogPath"] == null)
        {
            return null;
        }

        string errorPath = ConfigurationSettings.AppSettings["ErrorLogPath"];

        DirectoryInfo directory = new DirectoryInfo(errorPath);

        if (directory == null)
        {
            return null;
        }

        return directory.GetFiles();
    }

    public bool SendLogToMail(string mailAddress, string type)
    {
        if (mailAddress == "" || mailAddress == null ||
            type == "" || type == null)
        {
            return false;
        }

        //The Recommended way is to send him the mail by the mail server
        //Mail Server include: Host Address, Username and Password

        StringBuilder bodyBuild = new StringBuilder(string.Format("All Logs Of Type {0}", type));

        foreach (Log lo in this.coachingDal.GetAllLogsByType(type))
        {
            bodyBuild.Append(string.Format(@"Error ID: {0}\n\rError Type:
                {1}\n\r\Received In: {2}\n\rError: {3}\n\r",
            lo.LogID, lo.LogType, lo.LogDate, lo.LogMessage));
        }

        MailMessage m = new MailMessage("webmaster@malliere.com", mailAddress); // itay gmail
        m.Subject = "All Logs";
        m.Body = bodyBuild.ToString();

        SmtpClient ss = new SmtpClient("mail.malliere.com"); // put here the address of where you send the mail from
        //(this is the place for the host mail address)
        ss.EnableSsl = false;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("carmella@malliere.com", "carmella"); //put here the user name and the password of the mail server host
        ss.Send(m);
        return true;
    }

    private string ConvertFileName(string fileName)
    {
        if (fileName == "" || fileName == null)
        {
            return null;
        }

        while (fileName.Contains('/'))
        {
            fileName = fileName.Remove(fileName.IndexOf('/'), 1);
        }

        return fileName + ".txt";
    }

    public void WriteLogToFile(DateTime time)
    {
        if (ConfigurationSettings.AppSettings["ErrorLogPath"] == null || time == default(DateTime))
        {
            return;
        }

        string errorPath = ConfigurationSettings.AppSettings["ErrorLogPath"];

        if (time == default(DateTime))
        {
            return;
        }

        string pathFile = "";
        DirectoryInfo directory = null;

        directory = new DirectoryInfo(errorPath);


        if (directory == null)
        {
            return;
        }

        foreach (FileInfo file in directory.GetFiles())
        {
            if (file.Name == this.ConvertFileName(time.ToShortDateString()))
            {
                pathFile = file.FullName;
                break;
            }
        }

        FileStream fileSt = null;
        string date = "";

        if (pathFile == "")
        {
            date = this.ConvertFileName(time.ToShortDateString());

            try
            {
                fileSt = File.Create(directory.FullName + date);
            }
            finally
            {
                if (fileSt != null)
                {
                    pathFile = fileSt.Name;
                    fileSt.Close();
                }
            }
            if (fileSt == null)
            {
                return;
            }
        }

        StreamWriter writer = null;
        try
        {
            writer = new StreamWriter(pathFile, true);
            writer.WriteLine(string.Format("This Is Coaching's Logs From {0}:\n\r", time.ToString()));

            foreach (Log l in (IEnumerable<Log>)this.coachingDal.GetAll("log"))
            {
                writer.WriteLine("{0}, {1} ==> {2}\n\r", l.LogDate, l.LogID, l.LogMessage);
            }
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }

            if (fileSt != null)
            {
                fileSt.Close();
            }
        }
    }

    public bool SendLogToMail(string mailAddress, string type, DateTime timeFrom)
    {
        if (mailAddress == "" || mailAddress == null ||
            type == "" || type == null || timeFrom == default(DateTime))
        {
            return false;
        }

        //The Recommended way is to send him the mail by the mail server
        //Mail Server include: Host Address, Username and Password

        StringBuilder bodyBuild = new StringBuilder(string.Format("Logs From {0} Of Type {2}:\n\r", timeFrom.ToString(), type));

        foreach (Log lo in this.coachingDal.GetAllLogsByType(type))
        {
            bodyBuild.Append(string.Format(@"Error ID: {0}\n\rError Type:
                {1}\n\r\Received In: {2}\n\rError: {3}\n\r",
            lo.LogID, lo.LogType, lo.LogDate, lo.LogMessage));
        }

        MailMessage m = new MailMessage("carmella@malliere.com", mailAddress);

        m.Subject = "All Logs From " + timeFrom.ToString();
        m.Body = bodyBuild.ToString();

        SmtpClient ss = new SmtpClient("mail.malliere.com"); // put here the address of where you send the mail from
        //(this is the place for the host mail address)
        ss.EnableSsl = false;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("carmella@malliere.com", "carmella"); //put here the user name and the password of the mail server host
        ss.Send(m);
        return true;
    }

    public bool SendLogToMail(string mailAddress, string type, DateTime timeFrom, DateTime toTime)
    {
        if (mailAddress == "" || mailAddress == null ||
            type == "" || type == null ||
            timeFrom == default(DateTime) || toTime == default(DateTime))
        {
            return false;
        }

        //The Recommended way is to send him the mail by the mail server
        //Mail Server include: Host Address, Username and Password

        StringBuilder bodyBuild = new StringBuilder(string.Format("Logs Between {0} And {1} Of Type {2}:\n\r", timeFrom.ToString(), toTime.ToString(), type));

        foreach (Log lo in this.coachingDal.GetAllLogsByType(type))
        {
            bodyBuild.Append(string.Format(@"Error ID: {0}\n\rError Type:
                {1}\n\r\Received In: {2}\n\rError: {3}\n\r",
            lo.LogID, lo.LogType, lo.LogDate, lo.LogMessage));
        }

        MailMessage m = new MailMessage("webmaster@malliere.com", mailAddress); // itay gmail

        m.Subject = "All Logs Between " + timeFrom.ToString() + " And " + toTime.ToString();
        m.Body = bodyBuild.ToString();

        SmtpClient ss = new SmtpClient("mail.malliere.com"); // put here the address of where you send the mail from
        //(this is the place for the host mail address)
        ss.EnableSsl = false;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("carmella@malliere.com", "carmella"); //put here the user name and the password of the mail server host
        ss.Send(m);
        return true;
    }

    public bool SendMailToAdministrator(string title, string message)
    {
        if (title == "" || title == null ||
            message == "" || message == null)
        {
            return false;
        }

        //The Recommended way is to send him the mail by the mail server
        //Mail Server include: Host Address, Username and Password

        MailMessage m = new MailMessage("carmella@malliere.com", "orassyag@walla.co.il"); // my walla

        m.Subject = title;
        m.Body = message;

        SmtpClient ss = new SmtpClient("mail.malliere.com"); // put here the address of where you send the mail from
        //(this is the place for the host mail address)
        ss.EnableSsl = false;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("carmella@malliere.com", "carmella"); //put here the user name and the password of the mail server host
        ss.Send(m);
        return true;
    }


    public bool SendMailAdmins(string mailAddress)
    {
        if (mailAddress == "" || mailAddress == null)
        {
            return false;
        }

        //The Recommended way is to send him the mail by the mail server
        //Mail Server include: Host Address, Username and Password


        StringBuilder bodyBuild = new StringBuilder("This Is Coaching's All Admin's:\n\r");

        foreach (AdminUser c in (IEnumerable<AdminUser>)this.coachingDal.GetAll("admin"))
        {
            bodyBuild.Append("User ID: " + c.UserID + "\n\rPassword: " + c.Password + "\n\r\n\r");
        }

        MailMessage m = new MailMessage("webmaster@malliere.com", mailAddress); // mail

        m.Subject = "Admin User ID And Password Recovery";
        m.Body = bodyBuild.ToString();

        SmtpClient ss = new SmtpClient("mail.malliere.com"); // put here the address of where you send the mail from
        //(this is the place for the host mail address)
        ss.EnableSsl = false;
        ss.Timeout = 10000;
        ss.DeliveryMethod = SmtpDeliveryMethod.Network;
        ss.UseDefaultCredentials = false;
        ss.Credentials = new NetworkCredential("carmella@malliere.com", "carmella"); //put here the user name and the password of the mail server host
        ss.Send(m);
        return true;
    }
}