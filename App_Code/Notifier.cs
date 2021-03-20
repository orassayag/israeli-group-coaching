using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

/// <summary>
/// Summary description for Notifier
/// </summary>
public class Notifier
{
    public Notifier() { }

    /// <summary>
    /// This method update the status of the result of the submit button
    /// </summary>
    /// <param name="lbl"></param>
    public string[] Notify(int option, string color, string valueName)
    {
        string message = "";

        //select the notify to present the user
        switch (option)
        {
            case 1:
                message = "Are You Sure You Want To Delete " + valueName + " ?";
                break;
            case 2:
                message = "Page " + valueName + " Was Successfully Removed";
                break;
            case 3:
                message = "Please Select An Action";
                break;
            case 4:
                string[] content = valueName.Split(',');

                message = "Page " + content[0] + " Was Successfully Added<BR>Page Link: http://www.malliere.com/ContactPage.aspx?pageID=" + content[1] + "<BR>" +
                "Full Page Link: " + "http://www.malliere.com/FullPage.aspx?pageID=" + content[1] + "&type=content";
                break;
            case 5:
                message = "Please Enter Mail To Send Admin's UserID And Password";
                break;
            case 6:
                message = "Failed To Send An E-Mail To " + valueName + ".\n\rPlease Check The Mail Address, And Try Again.\n\rIf The Problem Continues, Please contact The Administrator";
                break;
            case 7:
                message = "A Recover Admin Password And UserID E-Mail To " + valueName + " Was Successfully Sent";
                break;
            case 8:
                message = "Values " + valueName + " Already Exists, Please Select Different Name Or/And Password";
                break;
            case 9:
                message = valueName + " Was Successfully Added";
                break;
            case 10:
                message = valueName + " Failed To Be Added";
                break;
            case 11:
                message = "Page Already Exist";
                break;
            case 12:
                message = "Failed To Add Page " + valueName;
                break;
            case 13:
                message = "Failed To Update Page " + valueName;
                break;
            case 14:
                message = "Please Enter Page's Title";
                break;
            case 15:
                message = "Please Enter Page's Description";
                break;
            case 16:
                message = "Please Enter Page's Key Words";
                break;
            case 17:
                message = "Please Enter Page's Content, The Page Is Empty";
                break;
            case 18:
                message = "Please Select Page's Language";
                break;
            case 19:
                message = "Please Enter Password";
                break;
            case 20:
                message = "Please Re-Enter Password";
                break;
            case 21:
                message = "Passwords Don't Match";
                break;
            case 22:
                message = "Please Enter User ID";
                break;
            case 23:
                message = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
                break;
            case 24:
                message = "You Can't Remove " + valueName + ". You Must Have At Least 1 Active Admin";
                break;
            case 25:
                message = "Failed To Update " + valueName;
                break;
            case 26:
                message = valueName + " Was Successfully Added";
                break;
            case 27:
                message = valueName + " Was Successfully Updated";
                break;
            case 28:
                message = valueName + " Was Successfully Removed";
                break;
            case 29:
                message = "Page " + valueName + " Was Successfully Updated";
                break;
            case 30:
                message = "Please Enter Content Button Title";
                break;
            case 31:
                message = "Content Button Title " + valueName + " Already Exists, Please Select Other Button Title";
                break;
            case 32:
                message = valueName + " Was Successfully Enabled";
                break;
            case 33:
                message = valueName + " Was Successfully Disabled";
                break;
            case 34:
                message = valueName + " Already Disabled";
                break;
            case 35:
                message = valueName + " Already Enabled";
                break;
            case 36:
                message = "Mail " + valueName + " Does Not Exist In Mail List";
                break;
            case 37:
                message = "Are You Sure You Wish To Remove " + valueName + " ?";
                break;
            case 38:
                message = "Failed To Remove " + valueName;
                break;
            case 39:
                message = "Failed To Update " + valueName;
                break;
            case 40:
                message = "Mail " + valueName + " Already Exist In Mail List";
                break;
            case 41:
                message = "Failed To Add " + valueName;
                break;
            case 42:
                message = "Failed To Enable " + valueName;
                break;
            case 43:
                message = "Failed To Disable " + valueName;
                break;
            case 44:
                message = "Please Enter Mail's Subject";
                break;
            case 45:
                message = "Please Enter Mail's Body Message";
                break;
            case 46:
                message = valueName;
                break;
            case 47:
                message = "Please Enter From Join Date";
                break;
            case 48:
                message = "Please Enter To Join Date";
                break;
            case 49:
                message = "Illegal Date " + valueName;
                break;
            case 50:
                message = "Please Enter Session's Hebrew Year";
                break;
            case 51:
                string[] details = valueName.Split(',');

                message = string.Format("Date {0} Can't Be Bigger Then {1}", details[0], details[1]);
                break;
            case 52:
                message = "Please Select Session's Year";
                break;
            case 53:
                message = "Please Select A Session";
                break;
            case 54:
                message = "Please Enter The Graduate's Name";
                break;
            case 55:
                message = "Please Select A Graduate";
                break;
            case 56:
                message = "No Logs File To View";
                break;
            case 57:
                message = "Please Select Log File To Read";
                break;
            case 58:
                message = "Failed To Read Log " + valueName;
                break;
            case 59:
                message = "Please Select Admin";
                break;
            case 60:
                message = "Are You Sure You Want To Delete All Log Files?";
                break;
            case 61:
                message = "All Log Files Has Been Successfully Removed";
                break;
            case 62:
                message = "Please Select Page To Remove/Update";
                break;
            case 63:
                message = "Please Select News Place In Order";
                break;
            case 64:
                message = "Please Enter News Message";
                break;
            case 65:
                message = "Please Enter Page Place In The Menu";
                break;
            case 66:
                message = "Page's Place Is Not Available. Please Select Other Place";
                break;
            case 67:
                message = "Invalid Page's Place";
                break;
            case 68:
                message = "Please Enter Page's Url";
                break;
            case 69:
                message = "Please Select Page Type";
                break;
            case 70:
                message = "Page Type " + valueName + " Already Exists, Select Other Type Or Change The Type Of The Existing One And Try Again";
                break;
            case 71:
                message = "Please Select News To Remove/Update";
                break;
            case 72:
                string[] article = valueName.Split(',');

                string link = "http://www.malliere.com/ArticlePage.aspx?pageID=" + article[1];

                message = "Article " + article[0] + " Was Successfully Added<BR>Article Link:" + link +
                          "<BR>Article Full Page: " + "http://www.malliere.com/FullPage.aspx?pageID=" + article[1] + "&type=article";
                break;
            case 74:
                message = "Session's Place Is Not Available. Please Select Other Place";
                break;
            case 75:
                message = "Please Enter New's Name In Menu";
                break;
            default:
                break;
        }

        if (message == "")
        {
            throw new AdminException("message == \"\" at " + MethodBase.GetCurrentMethod().Name);
        }

        return new string[] { message, color, option.ToString() };
    }
}
