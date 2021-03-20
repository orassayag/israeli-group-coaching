using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class CoachDal
{
    private CoachingDalDataContext coachingDal = new CoachingDalDataContext();

    public CoachDal() { }

    public void Add(string type, object item, DateTime time)
    {
        if (type == "" || type == null || item == null ||
            time == default(DateTime) || time == default(DateTime))
        {
            return;
        }
        switch (type)
        {
            case "admin":
                AdminUser ad = (AdminUser)item;
                if (this.Get(type, ad.AdminUserID) == null)
                {
                    ad.CreationTime = time;
                    ad.spCreationTime = time.ToShortDateString();
                    this.coachingDal.AdminUsers.InsertOnSubmit(ad);
                }
                break;
            case "article":
                Article ar = (Article)item;
                if (this.Get(type, ar.ArticleID) == null)
                {
                    ar.CreationTime = time;
                    this.coachingDal.Articles.InsertOnSubmit(ar);
                }
                break;
            case "content":
                ContentP co = (ContentP)item;
                if (this.Get(type, co.ContentID) == null)
                {
                    co.CreationTime = time;
                    this.coachingDal.ContentPs.InsertOnSubmit(co);
                }
                break;
            case "mailList":
                MailList ma = (MailList)item;
                if (this.Get(type, ma.MailListID) == null)
                {
                    ma.MailListJoinTime = time;
                    ma.spMailListLastUpdate = time.ToShortDateString();
                    this.coachingDal.MailLists.InsertOnSubmit(ma);
                }
                break;
            case "log":
                Log lo = (Log)item;
                if (this.Get(type, lo.LogID.ToString()) == null)
                {
                    lo.LogDate = time;
                    lo.spLogDate = time.ToShortDateString();
                    this.coachingDal.Logs.InsertOnSubmit(lo);
                }
                break;
            case "graduate":
                Graduate gr = (Graduate)item;
                if (this.Get(type, gr.GraduateID.ToString()) == null)
                {
                    gr.CreationTime = time;
                    gr.spCreationTime = time.ToShortDateString();
                    this.coachingDal.Graduates.InsertOnSubmit(gr);
                }
                break;
            case "session":
                GraduateSession se = (GraduateSession)item;
                if (this.Get(type, se.GraduateSessionID.ToString()) == null)
                {
                    se.CreationTime = time;
                    se.spCreationTime = time.ToShortDateString();
                    this.coachingDal.GraduateSessions.InsertOnSubmit(se);
                }
                break;
            case "lead":
                Lead le = (Lead)item;
                if (this.Get(type, le.LeadID.ToString()) == null)
                {
                    le.LeadDate = time;
                    le.spLeadDate = time.ToString();
                    this.coachingDal.Leads.InsertOnSubmit(le);
                }
                break;
            case "news":
                New ne = (New)item;
                if (this.Get(type, ne.NewsID) == null)
                {
                    ne.NewsCreationDate = time;
                    ne.spNewsCreationDate = time.ToShortDateString();
                    this.coachingDal.News.InsertOnSubmit(ne);
                }
                break;
            default:
                break;
        }

        this.coachingDal.SubmitChanges();
    }

    public bool GetContentByType(string pageType)
    {
        if (pageType == "" || pageType == null)
        {
            return false;
        }

        if (pageType == "Content")
        {
            return true;
        }

        ContentP p = this.coachingDal.ContentPs.SingleOrDefault(g => g.ContentPageType == pageType);
        if (p == null)
        {
            return true;
        }
        return false;
    }

    public bool GetContentByTypeExcept(string existingType, string newType)
    {
        if (existingType == "Content" || newType == "Content")
        {
            return true;
        }

        if (existingType == "" || existingType == null || newType == "" || newType == null)
        {
            return false;
        }



        ContentP p = this.coachingDal.ContentPs.SingleOrDefault
        (g => g.ContentPageType != existingType && g.ContentPageType == newType);
        if (p == null)
        {
            return true;
        }
        return false;
    }

    public MailList GetMailListByRemoveCode(string removeCode)
    {
        if (removeCode == "" || removeCode == null)
        {
            return null;
        }

        MailList mail = this.coachingDal.MailLists.SingleOrDefault(g => g.MailRemoveCode == removeCode);
        if (mail == null)
        {
            return null;
        }
        return mail;
    }

    public List<int> GetAvailableNewsPlaces()
    {
        List<int> list = new List<int>();

        var news = from ne in this.coachingDal.News
                   select ne.NewsPlace;

        for (int i = 1; i <= 20; i++)
        {
            if (!news.Contains(i))
            {
                list.Add(i);
            }
        }
        return list;
    }

    public bool CheckIfContentPlaceAvailable(int contentPlace)
    {
        if (contentPlace < 0)
        {
            return false;
        }

        ContentP p = this.coachingDal.ContentPs.SingleOrDefault
                     (g => g.ContentMenuPlace == contentPlace);
        return p == null;
    }

    public bool CheckIfContentPlaceAvailableExcept(int existContentPlace, int newContentPlace)
    {
        if (existContentPlace < 0 || newContentPlace < 0)
        {
            return false;
        }

        ContentP p = this.coachingDal.ContentPs.SingleOrDefault
        (g => g.ContentMenuPlace != existContentPlace && g.ContentMenuPlace == newContentPlace);

        return p == null;
    }

    public bool CheckIfSessionPlaceAvailable(int sessionPlace)
    {
        if (sessionPlace < 0)
        {
            return false;
        }

        GraduateSession p = this.coachingDal.GraduateSessions.SingleOrDefault
                        (g => g.GraduatePlace == sessionPlace);
        return p == null;
    }

    public bool CheckIfSessionPlaceAvailableExcept(int existSessionPlace, int newSessionPlace)
    {
        if (existSessionPlace < 0 || newSessionPlace < 0)
        {
            return false;
        }

        GraduateSession p = this.coachingDal.GraduateSessions.SingleOrDefault
        (g => g.GraduatePlace != existSessionPlace && g.GraduatePlace == newSessionPlace);

        return p == null;
    }

    public IEnumerable<Log> GetAllLogsByType(string type)
    {
        if (type == "" || type == null)
        {
            return null;
        }

        IEnumerable<Log> logs = null;
        if (type == "all")
        {
            logs = (IEnumerable<Log>)this.GetAll("log");
            return logs;
        }

        logs = from log in this.coachingDal.Logs
               where log.LogType == type
               select log;

        return logs;
    }

    public IEnumerable<Log> GetAllLogsByType(string type, DateTime timeFrom)
    {
        if (type == "" || type == null || timeFrom == default(DateTime))
        {
            return null;
        }

        IEnumerable<Log> logs = null;
        if (type == "all")
        {
            logs = from log in this.coachingDal.Logs
                   where log.LogDate >= timeFrom
                   select log;
            return logs;
        }

        logs = from log in this.coachingDal.Logs
               where log.LogDate >= timeFrom && log.LogType == type
               select log;
        return logs;
    }

    public bool CheckAvailableButtonTitle(string contentButtonTitle)
    {
        if (contentButtonTitle == "" || contentButtonTitle == null)
        {
            return false;
        }

        ContentP p = this.coachingDal.ContentPs.SingleOrDefault
                    (g => g.ContentButtonTitle == contentButtonTitle);
        if (p != null)
        {
            return false;
        }
        return true;
    }

    public bool CheckAvailableButtonTitleExcept(string existingContentButtonTitle, string newContentButtonTitle)
    {
        if (existingContentButtonTitle == "" || existingContentButtonTitle == null ||
            newContentButtonTitle == "" || newContentButtonTitle == null)
        {
            return false;
        }

        ContentP p = this.coachingDal.ContentPs.SingleOrDefault
                     (g => g.ContentButtonTitle != existingContentButtonTitle && g.ContentButtonTitle == newContentButtonTitle);
        if (p != null)
        {
            return false;
        }
        return true;
    }

    public IEnumerable<Log> GetAllLogsByType(string type, DateTime timeFrom, DateTime toTime)
    {
        if (type == "" || type == null ||
            timeFrom == default(DateTime) || toTime == default(DateTime))
        {
            return null;
        }

        IEnumerable<Log> logs = null;
        if (type == "all")
        {
            logs = from log in this.coachingDal.Logs
                   where log.LogDate >= timeFrom && log.LogDate <= toTime
                   select log;
            return logs;
        }

        logs = from log in this.coachingDal.Logs
               where log.LogDate >= timeFrom && log.LogDate <= toTime && log.LogType == type
               select log;

        return logs;
    }

    public void DeleteAllGraduatesFromSession(string sessionID)
    {
        if (sessionID == "" || sessionID == null)
        {
            return;
        }

        GraduateSession session = (GraduateSession)this.Get("session", sessionID);
        if (session == null)
        {
            return;
        }

        IEnumerable<Graduate> graduates = from session in this.coachingDal.Graduates
                                          where session.GraduateSessionID == sessionID
                                          select session;
        for (int i = 0; i < graduates.Count(); i++)
        {
            this.coachingDal.Graduates.DeleteOnSubmit(graduates.ElementAt(i));
        }

        this.coachingDal.SubmitChanges();

    }

    public IEnumerable<Graduate> GetAllGraduatesBySession(string sessionID)
    {
        if (sessionID == "" || sessionID == null)
        {
            return null;
        }

        GraduateSession session = (GraduateSession)this.Get("session", sessionID);
        if (session == null)
        {
            return null;
        }

        IEnumerable<Graduate> graduates = from session in this.coachingDal.Graduates
                                          where session.GraduateSessionID == sessionID
                                          select session;
        return graduates;
    }

    public IEnumerable<MailList> GetJoinMailListFromDate(DateTime timeFrom)
    {
        if (timeFrom == default(DateTime))
        {
            return null;
        }

        var mails = from mail in this.coachingDal.MailLists
                    where mail.MailListJoinTime >= timeFrom
                    select mail;
        return mails;
    }

    public IEnumerable<MailList> GetJoinMailListFromDateToDate(DateTime timeFrom, DateTime toTime)
    {
        if (timeFrom == default(DateTime) || toTime == default(DateTime))
        {
            return null;
        }

        var mails = from mail in this.coachingDal.MailLists
                    where mail.MailListJoinTime > timeFrom && mail.MailListJoinTime < toTime &&
                    mail.Active == 1
                    select mail;
        return mails;
    }

    public int GetJoinMailListCountFromDate(DateTime timeFrom)
    {
        if (timeFrom == default(DateTime))
        {
            return -1;
        }

        var mails = (from mail in this.coachingDal.MailLists
                     where mail.MailListJoinTime >= timeFrom
                     select mail).Count();
        return mails;
    }

    public int GetJoinMailListCountFromDateToDate(DateTime timeFrom, DateTime toTime)
    {
        if (timeFrom == default(DateTime) || toTime == default(DateTime))
        {
            return -1;
        }

        var mails = (from mail in this.coachingDal.MailLists
                     where mail.MailListJoinTime >= timeFrom && mail.MailListJoinTime <= toTime
                     select mail).Count();
        return mails;
    }

    public IEnumerable<Log> GetAllLogsFromDateToDate(DateTime timeFrom, DateTime toTime)
    {
        if (timeFrom == default(DateTime) || toTime == default(DateTime))
        {
            return null;
        }

        var logs = from log in this.coachingDal.Logs
                   where log.LogDate >= timeFrom && log.LogDate <= toTime
                   select log;
        return logs;
    }

    public void EnableMailList(string mailID)
    {
        if (mailID == "" || mailID == null)
        {
            return;
        }

        MailList mail = (MailList)this.Get("mailList", mailID);
        if (mail == null)
        {
            return;
        }

        if (mail.Active == 1)
        {
            return;
        }

        mail.Active = 1;
        mail.spActvie = "Enable";

        this.coachingDal.SubmitChanges();
    }

    public void DisableMailList(string mailID)
    {
        if (mailID == "" || mailID == null)
        {
            return;
        }

        MailList mail = (MailList)this.Get("mailList", mailID);
        if (mail == null)
        {
            return;
        }

        if (mail.Active == 2)
        {
            return;
        }

        mail.Active = 2;
        mail.spActvie = "Disable";

        this.coachingDal.SubmitChanges();
    }

    public object GetAll(string type)
    {
        object result = null;

        if (type == "" || type == null)
        {
            return result;
        }

        switch (type)
        {
            case "admin":
                var admins = from admin in this.coachingDal.AdminUsers
                             select admin;
                result = admins;
                break;
            case "article":
                var articles = from article in this.coachingDal.Articles
                               select article;
                result = articles;
                break;
            case "content":
                var ContentPs = from content in this.coachingDal.ContentPs
                                orderby content.ContentMenuPlace ascending
                                select content;
                result = ContentPs;
                break;
            case "mailList":
                var mailLists = from mailList in this.coachingDal.MailLists
                                select mailList;
                result = mailLists;
                break;
            case "log":
                var logs = from log in this.coachingDal.Logs
                           select log;
                result = logs;
                break;
            case "graduate":
                var graduates = from gra in this.coachingDal.Graduates
                                orderby gra.GraduateName ascending
                                select gra;
                result = graduates;
                break;
            case "session":
                var sessions = from ses in this.coachingDal.GraduateSessions
                               orderby ses.GraduatePlace ascending
                               select ses;
                result = sessions;
                break;
            case "lead":
                var leads = from lead in this.coachingDal.Leads
                            orderby lead.LeadDate descending
                            select lead;
                result = leads;
                break;
            case "news":
                var news = from ne in this.coachingDal.News
                           orderby ne.NewsPlace ascending
                           select ne;
                result = news;
                break;
            default:
                break;
        }

        return result;
    }

    public IEnumerable<MailList> GetAllActiveMailList()
    {
        var mails = from mail in this.coachingDal.MailLists
                    where mail.Active == 1
                    orderby mail.MailListName ascending
                    select mail;
        return mails;
    }

    public IEnumerable<MailList> GetAllActiveMailList(DateTime fromTime)
    {
        if (fromTime == default(DateTime))
        {
            return null;
        }

        var mails = from mail in this.coachingDal.MailLists
                    where mail.Active == 1 && mail.MailListJoinTime >= fromTime
                    orderby mail.MailListName ascending
                    select mail;
        return mails;
    }

    public IEnumerable<MailList> GetAllActiveMailList(DateTime fromTime, DateTime toTime)
    {
        if (fromTime == default(DateTime) || toTime == default(DateTime))
        {
            return null;
        }

        var mails = from mail in this.coachingDal.MailLists
                    where mail.Active == 1 && mail.MailListJoinTime >= fromTime &&
                    mail.MailListJoinTime <= toTime
                    orderby mail.MailListName ascending
                    select mail;
        return mails;
    }

    public IEnumerable<MailList> GetAllInactiveMailList()
    {
        var mails = from mail in this.coachingDal.MailLists
                    where mail.Active == 2
                    orderby mail.MailListName ascending
                    select mail;
        return mails;
    }

    public IEnumerable<MailList> GetAllInactiveMailList(DateTime fromTime)
    {
        if (fromTime == default(DateTime))
        {
            return null;
        }

        var mails = from mail in this.coachingDal.MailLists
                    where mail.Active == 2 && mail.MailListJoinTime >= fromTime
                    orderby mail.MailListName ascending
                    select mail;
        return mails;
    }

    public IEnumerable<MailList> GetAllInactiveMailList(DateTime fromTime, DateTime toTime)
    {
        if (fromTime == default(DateTime) || toTime == default(DateTime))
        {
            return null;
        }

        var mails = from mail in this.coachingDal.MailLists
                    where mail.Active == 2 && mail.MailListJoinTime >= fromTime &&
                    mail.MailListJoinTime <= toTime
                    orderby mail.MailListName ascending
                    select mail;
        return mails;
    }

    public object Get(string type, string itemID)
    {
        object result = null;

        if (type == "" || type == null ||
            itemID == "" || itemID == null)
        {

            return result;
        }

        switch (type)
        {
            case "admin":
                result = this.coachingDal.AdminUsers.SingleOrDefault(g => g.AdminUserID == itemID);
                break;
            case "article":
                result = this.coachingDal.Articles.SingleOrDefault(g => g.ArticleID == itemID);
                break;
            case "content":
                result = this.coachingDal.ContentPs.SingleOrDefault(g => g.ContentID == itemID);
                break;
            case "mailList":
                result = this.coachingDal.MailLists.SingleOrDefault(g => g.MailListID == itemID);
                break;
            case "log":
                long m = -1;
                long.TryParse(itemID, out m);
                if (m == -1)
                {
                    return result;
                }
                result = this.coachingDal.Logs.SingleOrDefault(g => g.LogID == m);
                break;
            case "graduate":
                result = this.coachingDal.Graduates.SingleOrDefault(g => g.GraduateID == itemID);
                break;
            case "session":
                result = this.coachingDal.GraduateSessions.SingleOrDefault(g => g.GraduateSessionID == itemID);
                break;
            case "lead":
                result = this.coachingDal.Leads.SingleOrDefault(g => g.LeadID == itemID);
                break;
            case "news":
                result = this.coachingDal.News.SingleOrDefault(g => g.NewsID == itemID);
                break;
            default:
                break;
        }
        return result;
    }

    public void Delete(string type, string itemID)
    {
        if (type == "" || type == null ||
            itemID == null || itemID == "")
        {
            return;
        }

        switch (type)
        {
            case "admin":
                AdminUser m = null;
                if ((m = (AdminUser)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.AdminUsers.DeleteOnSubmit(m);
                }
                break;
            case "article":
                Article ar = null;
                if ((ar = (Article)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.Articles.DeleteOnSubmit(ar);
                }
                break;
            case "content":
                ContentP co = null;
                if ((co = (ContentP)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.ContentPs.DeleteOnSubmit(co);
                }
                break;
            case "mailList":
                MailList ma = null;
                if ((ma = (MailList)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.MailLists.DeleteOnSubmit(ma);
                }
                break;
            case "log":
                Log lo = null;
                if ((lo = (Log)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.Logs.DeleteOnSubmit(lo);
                }
                break;
            case "graduate":
                Graduate gr = null;
                if ((gr = (Graduate)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.Graduates.DeleteOnSubmit(gr);
                }
                break;
            case "session":
                GraduateSession se = null;
                if ((se = (GraduateSession)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.GraduateSessions.DeleteOnSubmit(se);
                }
                break;
            case "lead":
                Lead le = null;
                if ((le = (Lead)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.Leads.DeleteOnSubmit(le);
                }
                break;
            case "news":
                New ne = null;
                if ((ne = (New)this.Get(type, itemID)) != null)
                {
                    this.coachingDal.News.DeleteOnSubmit(ne);
                }
                break;
            default:
                break;
        }
        this.coachingDal.SubmitChanges();
    }

    public void Update(string type, object item, DateTime time)
    {
        if (type == "" || type == null || time == default(DateTime))
        {
            return;
        }

        switch (type)
        {
            case "admin":
                AdminUser ad1 = (AdminUser)item;
                AdminUser ad2 = null;
                if ((ad2 = (AdminUser)this.Get(type, ad1.AdminUserID)) != null)
                {
                    ad2.Update(ref ad1, time);
                    ad2.LastUpdate = time;
                    ad2.spLastUpdate = time.ToString();
                }
                break;
            case "article":
                Article ar1 = (Article)item;
                Article ar2 = null;
                if ((ar2 = (Article)this.Get(type, ar1.ArticleID)) != null)
                {
                    ar2.Update(ref ar1, time);
                    ar2.LastUpdate = time;
                    ar2.spLastUpdate = time.ToString();
                }
                break;
            case "content":
                ContentP co1 = (ContentP)item;
                ContentP co2 = null;
                if ((co2 = (ContentP)this.Get(type, co1.ContentID)) != null)
                {
                    co2.Update(ref co1, time);
                    co2.LastUpdate = time;
                    co2.spLastUpdate = time.ToString();
                }
                break;
            case "log":
                Log lo1 = (Log)item;
                Log lo2 = null;
                if ((lo2 = (Log)this.Get(type, lo1.LogID.ToString())) != null)
                {
                    lo2.Update(ref lo1, time);
                }
                break;
            case "graduate":
                Graduate gr1 = (Graduate)item;
                Graduate gr2 = null;
                if ((gr2 = (Graduate)this.Get(type, gr1.GraduateID)) != null)
                {
                    gr2.Update(ref gr1, time);
                }
                break;
            case "session":
                GraduateSession ss1 = (GraduateSession)item;
                GraduateSession ss2 = null;
                if ((ss2 = (GraduateSession)this.Get(type, ss1.GraduateSessionID)) != null)
                {
                    ss2.Update(ref ss1, time);
                }
                break;
            case "lead":
                Lead le1 = (Lead)item;
                Lead le2 = null;
                if ((le2 = (Lead)this.Get(type, le1.LeadID.ToString())) != null)
                {
                    le2.Update(ref le1, time);
                }
                break;
            case "news":
                New ne1 = (New)item;
                New ne2 = null;
                if ((ne2 = (New)this.Get(type, ne1.NewsID.ToString())) != null)
                {
                    ne2.Update(ref ne1, time);
                }
                break;
            default:
                break;
        }
        this.coachingDal.SubmitChanges();
    }

    public void Login(string adminID, DateTime time)
    {
        if (adminID == "" || adminID == null || time == default(DateTime))
        {
            return;
        }

        AdminUser m = (AdminUser)this.Get("admin", adminID);
        if (m == null)
        {
            return;
        }

        m.LastLogin = time;
        this.coachingDal.SubmitChanges();
    }

    public int GetCount(string type)
    {
        int result = -1;

        if (type == "" || type == null)
        {
            return result;
        }

        switch (type)
        {
            case "admin":
                result = this.coachingDal.AdminUsers.Count();
                break;
            case "article":
                result = this.coachingDal.Articles.Count();
                break;
            case "content":
                result = this.coachingDal.Articles.Count();
                break;
            case "mailList":
                result = this.coachingDal.MailLists.Count();
                break;
            case "log":
                result = this.coachingDal.Logs.Count();
                break;
            case "graduate":
                result = this.coachingDal.Leads.Count();
                break;
            case "session":
                result = this.coachingDal.GraduateSessions.Count();
                break;
            case "lead":
                result = this.coachingDal.Leads.Count();
                break;
            case "news":
                result = this.coachingDal.News.Count();
                break;
            default:
                break;
        }
        return result;
    }

    public string GetNextAvailableID(string type)
    {
        if (type == "" || type == null)
        {
            return type;
        }

        int result = 0;

        switch (type)
        {
            case "admin":
                AdminUser ad = null;
                while ((ad = (AdminUser)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "article":
                Article ar = null;
                while ((ar = (Article)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "content":
                ContentP co = null;
                while ((co = (ContentP)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "mailList":
                MailList ma = null;
                while ((ma = (MailList)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "log":
                Log lo = null;
                while ((lo = (Log)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "graduate":
                Graduate gr = null;
                while ((gr = (Graduate)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "session":
                GraduateSession ss = null;
                while ((ss = (GraduateSession)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "lead":
                Lead le = null;
                while ((le = (Lead)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            case "news":
                New ne = null;
                while ((ne = (New)this.Get(type, result.ToString())) != null)
                {
                    result += 1;
                }
                break;
            default:
                break;
        }
        return result.ToString();
    }

    public void DeleteAllByType(string type)
    {
        if (type == "" || type == null)
        {
            return;
        }

        switch (type)
        {
            case "admin":
                this.coachingDal.AdminUsers.DeleteAllOnSubmit(this.coachingDal.AdminUsers);
                break;
            case "article":
                this.coachingDal.Articles.DeleteAllOnSubmit(this.coachingDal.Articles);
                break;
            case "content":
                this.coachingDal.ContentPs.DeleteAllOnSubmit(this.coachingDal.ContentPs);
                break;
            case "mailList":
                this.coachingDal.MailLists.DeleteAllOnSubmit(this.coachingDal.MailLists);
                break;
            case "log":
                this.coachingDal.Logs.DeleteAllOnSubmit(this.coachingDal.Logs);
                break;
            case "graduate":
                this.coachingDal.Graduates.DeleteAllOnSubmit(this.coachingDal.Graduates);
                break;
            case "session":
                foreach (GraduateSession s in this.coachingDal.GraduateSessions)
                {
                    this.DeleteAllGraduatesFromSession(s.GraduateSessionID);
                }
                this.coachingDal.GraduateSessions.DeleteAllOnSubmit(this.coachingDal.GraduateSessions);
                break;
            case "lead":
                this.coachingDal.Leads.DeleteAllOnSubmit(this.coachingDal.Leads);
                break;
            case "news":
                this.coachingDal.News.DeleteAllOnSubmit(this.coachingDal.News);
                break;
            default:
                break;
        }
        this.coachingDal.SubmitChanges();
    }

    public void DeleteAllDataBase()
    {
        this.coachingDal.DeleteDatabase();
        this.coachingDal.SubmitChanges();
    }

    public AdminUser GetAdminUser(string userID, string password)
    {
        if (userID == "" || userID == null ||
            password == "" || password == null)
        {
            return null;
        }

        AdminUser ad = null;

        ad = this.coachingDal.AdminUsers.SingleOrDefault
        (g => g.UserID == userID && g.Password == password);

        return ad;
    }

    public AdminUser GetAdminUserExceptUserID(string userIDExist, string userIDToCheck)
    {
        if (userIDExist == "" || userIDExist == null ||
            userIDToCheck == "" || userIDToCheck == null)
        {
            return null;
        }

        AdminUser ad = null;

        ad = this.coachingDal.AdminUsers.SingleOrDefault
        (g => g.UserID != userIDExist && g.UserID == userIDToCheck);

        return ad;
    }

    public AdminUser GetAdminUserExceptPassword(string passwordExist, string passwordToCheck)
    {
        if (passwordExist == "" || passwordExist == null ||
            passwordToCheck == "" || passwordToCheck == null)
        {
            return null;
        }

        AdminUser ad = null;

        ad = this.coachingDal.AdminUsers.SingleOrDefault
        (g => g.Password != passwordExist && g.Password == passwordToCheck);

        return ad;
    }

    public MailList GetMailListByMailAddress(string mailAddress)
    {
        if (mailAddress == "" || mailAddress == null)
        {
            return null;
        }

        MailList m = null;
        try
        {
            m = this.coachingDal.MailLists.SingleOrDefault(g => g.MailListMail == mailAddress);
        }
        catch (Exception) { }
        return m;
    }

    public bool GetMailListByMailAddressExcept(string existingAddress, string addressToCheck)
    {
        if (existingAddress == "" || existingAddress == null ||
            addressToCheck == "" || addressToCheck == null)
        {
            return false;
        }

        MailList m = null;
        try
        {
            m = this.coachingDal.MailLists.SingleOrDefault(g => g.MailListMail != existingAddress &&
                                                                g.MailListMail == addressToCheck);
        }
        catch (Exception) { }

        if (m == null)
        {
            return true;
        }
        return false;
    }
}

public partial class AdminUser
{
    public AdminUser Update(ref AdminUser m, DateTime time)
    {
        this.AdminUserID = m.AdminUserID;
        this.CreationTime = m.CreationTime;
        this.LastLogin = m.LastLogin;
        this.LastUpdate = time;
        this.Password = m.Password;
        this.spCreationTime = m.spCreationTime;
        this.spLastLogin = m.spLastLogin;
        this.spLastUpdate = m.spLastUpdate;
        this.UserID = m.UserID;
        return this;
    }
}

public partial class Article
{
    public Article Update(ref Article m, DateTime time)
    {
        this.ArticleID = m.ArticleID;
        this.CreationTime = m.CreationTime;
        this.ArticleContent = m.ArticleContent;
        this.ArticleDescription = m.ArticleDescription;
        this.ArticleKeyWords = m.ArticleKeyWords;
        this.ArticleLanguageID = m.ArticleLanguageID;
        this.ArticleTitle = m.ArticleTitle;
        this.spLastUpdate = time.ToString();
        this.LastUpdate = time;
        return this;
    }
}

public partial class ContentP
{
    public ContentP Update(ref ContentP m, DateTime time)
    {
        this.ContentID = m.ContentID;
        this.CreationTime = m.CreationTime;
        this.ContentContent = m.ContentContent;
        this.ContentButtonTitle = m.ContentButtonTitle;
        this.ContentDescription = m.ContentDescription;
        this.ContentKeyWords = m.ContentKeyWords;
        this.ContentLanguageID = m.ContentLanguageID;
        this.ContentTitle = m.ContentTitle;
        this.ContentMenuPlace = m.ContentMenuPlace;
        this.IsLinkPage = m.IsLinkPage;
        this.ContentLink = m.ContentLink;
        this.spLastUpdate = time.ToString();
        this.LastUpdate = time;
        return this;
    }
}

public partial class Lead
{
    public Lead Update(ref Lead m, DateTime time)
    {
        this.LeadBody = m.LeadBody;
        this.LeadDate = m.LeadDate;
        this.LeadID = m.LeadID;
        this.LeadMail = m.LeadMail;
        this.LeadName = m.LeadName;
        this.LeadTitle = m.LeadTitle;
        this.spLeadDate = m.spLeadDate;
        return this;
    }
}

public partial class Graduate
{
    public Graduate Update(ref Graduate m, DateTime time)
    {
        this.CreationTime = m.CreationTime;
        this.GraduateID = m.GraduateID;
        this.GraduateName = m.GraduateName;
        this.GraduateSessionID = m.GraduateSessionID;
        this.spCreationTime = m.spCreationTime;
        return this;
    }
}

public partial class GraduateSession
{
    public GraduateSession Update(ref GraduateSession m, DateTime time)
    {
        this.GraduateSessionID = m.GraduateSessionID;
        this.GraduateYearHebrew = m.GraduateYearHebrew;
        this.GraduateYearNumber = m.GraduateYearNumber;
        this.CreationTime = m.CreationTime;
        this.spCreationTime = m.spCreationTime;
        return this;
    }
}

public partial class Log
{
    public Log Update(ref Log m, DateTime time)
    {
        this.LogID = m.LogID;
        this.LogDate = m.LogDate;
        this.LogMessage = m.LogMessage;
        this.LogType = m.LogType;
        this.spLogDate = m.spLogDate;
        return this;
    }
}

public partial class New
{
    public New Update(ref New m, DateTime time)
    {
        this.NewsID = m.NewsID;
        this.NewsContentLink = m.NewsContentLink;
        this.NewsContentOriginal = m.NewsContentOriginal;
        this.NewsCreationDate = m.NewsCreationDate;
        this.NewsLastUpdate = time;
        this.NewsPlace = m.NewsPlace;
        this.spNewsCreationDate = m.spNewsCreationDate;
        this.spNewsLastUpdate = m.spNewsLastUpdate;
        return this;
    }
}
