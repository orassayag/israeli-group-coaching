using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class RemoveFromMailList : System.Web.UI.Page
{
    private Logger logger = new Logger();
    private CoachDal coachingDal = new CoachDal();

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void removeYes_Click(object sender, EventArgs e)
    {
        if (Request["RemoveCode"] == null)
        {
            this.logger.Error(new AdminException
            (". Request[\"RemoveCode\"] == null"), MethodBase.GetCurrentMethod().Name);
            this.removeLabel.Text = @"לא הוסרת מרשימת התפוצה עקב בעיות טכניות. אנא פנה ידנית 
                                    בדף 'צור קשר' באתר עם כתובת האימייל איתה נרשמת לרשימת התפוצה";
            return;
        }

        MailList mail = this.coachingDal.GetMailListByRemoveCode((string)Request["RemoveCode"]);
        if (mail == null)
        {
            this.logger.Error(new AdminException
                (". mail == null"), MethodBase.GetCurrentMethod().Name);
            this.removeLabel.Text = @"לא הוסרת מרשימת התפוצה עקב בעיות טכניות. אנא פנה ידנית 
                                    בדף 'צור קשר' באתר עם כתובת האימייל איתה נרשמת לרשימת התפוצה";
            return;
        }

        this.coachingDal.DisableMailList(mail.MailListID);
        this.removeLabel.Text = "הוסרת מרשימת התפוצה בהצלחה";
        this.buttons.Visible = false;
        this.question.Visible = false;
    }

    protected void removeNo_Click(object sender, EventArgs e)
    {
        this.removeLabel.Text = "לא הוסרת מרשימת התפוצה";
        this.buttons.Visible = false;
        this.question.Visible = false;
    }
}
