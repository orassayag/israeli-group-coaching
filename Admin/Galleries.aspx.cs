using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

public partial class Admin_Galleries : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
//        if (!Page.IsPostBack)
//        {
//            this.allArtistsSelector.Items.Add(new ListItem("--Select Action--", "1"));
//            this.allArtistsSelector.Items.Add(new ListItem("Add New Gallery", "2"));
//            this.allArtistsSelector.Items.Add(new ListItem("Remove/Update Gallery", "3"));
//            this.allArtistsSelector.Items.Add(new ListItem("Add New Picture", "4"));
//            this.allArtistsSelector.Items.Add(new ListItem("Remove/Update Picture", "5"));

//            foreach (Gallery p in (IEnumerable<Gallery>)this.Master._PapaDal.GetAll("allArtistsGallery"))
//            {
//                ListItem l = new ListItem(p.GalleryNameHe, "s" + p.GalleryID);
//                this.removeUpdateAllArtistsGallerySelector.Items.Add(l);
//                this.addPicGallerySelector.Items.Add(l);
//            }

//            foreach (AllArtistPic p in (IEnumerable<AllArtistPic>)this.Master._PapaDal.GetAll("allArtistsPic"))
//            {
//                this.removeUpdatePicGallerySelector.Items.Add(new ListItem(p.PicName, "s" + p.PicID));
//            }

//            this.DivSwitcher(1);
//        }
    }

    protected void allArtistsSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
//        if (!this.ValidateFields(1))
//        {
//            return;
//        }

//        this.DivSwitcher(int.Parse(this.allArtistsSelector.SelectedValue));
    }

    protected void addAllArtistsGalleryButton_Click(object sender, EventArgs e)
    {
//        if (this.allArtistsGalleryHiddenUp.Value == "")
//        {
//            this.AddAllArtistsGallery();
//        }
//        else
//        {
//            this.UpdateGallery();
//            this.allArtistsGalleryHiddenUp.Value = "";
//        }
    }

    protected void cancelAllArtistsGalleryButton_Click(object sender, EventArgs e)
    {
//        this.Start();
    }

    protected void removeAllArtistsGalleryButton_Click(object sender, EventArgs e)
    {
//        if (!this.ValidateFields(11))
//        {
//            return;
//        }

//        Gallery p = (Gallery)this.Master._PapaDal.Get("gallery",
//        this.removeUpdateAllArtistsGallerySelector.SelectedValue.Remove(0, 1));
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearGallery();
//            return;
//        }

//        this.allArtistsGalleryHiddenRe.Value = p.GalleryID;
//        this.Notify(this.Master._Notifier.Notify(5, "Red", p.GalleryNameHe));
    }

    protected void updateAllArtistsGalleryButton_Click(object sender, EventArgs e)
    {
//        if (!this.ValidateFields(11))
//        {
//            return;
//        }

//        Gallery p = (Gallery)this.Master._PapaDal.Get("gallery",
//        this.removeUpdateAllArtistsGallerySelector.SelectedValue.Remove(0, 1));
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearGallery();
//            return;
//        }

//        this.allArtistsGalleryHiddenUp.Value = p.GalleryID;
//        this.UpdateGalleryInit();
//        this.DivSwitcher(2);
    }

    protected void addAllArtistsPicGalleryButton_Click(object sender, EventArgs e)
    {
//        if (this.allArtistsPicHiddenUp.Value == "")
//        {
//            this.AddAllArtistsPic();
//        }
//        else
//        {
//            this.UpdatePic();
//            this.allArtistsPicHiddenUp.Value = "";
//        }
    }

    protected void disableAllArtistsPicGallery_Click(object sender, EventArgs e)
    {
//        this.DisablePic();
    }

    protected void enableAllArtistsPicGallery_Click(object sender, EventArgs e)
    {
//        this.EnablePic();
    }

    protected void cancelAddPicGalleryButton_Click(object sender, EventArgs e)
    {
//        this.Start();
    }

    protected void updateAllArtistsPicGallery_Click(object sender, EventArgs e)
    {
//        if (!this.ValidateFields(12))
//        {
//            return;
//        }

//        AllArtistPic p = (AllArtistPic)this.Master._PapaDal.Get("allArtistsPic",
//        this.removeUpdatePicGallerySelector.SelectedValue.Remove(0, 1));
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearGallery();
//            return;
//        }

//        this.allArtistsPicHiddenUp.Value = p.PicID;
//        this.UpdatePicInit();
//        this.DivSwitcher(4);
    }

    protected void removeAllArtistsPicGallery_Click(object sender, EventArgs e)
    {
//        if (!this.ValidateFields(12))
//        {
//            return;
//        }

//        AllArtistPic p = (AllArtistPic)this.Master._PapaDal.Get("allArtistsPic",
//        this.removeUpdatePicGallerySelector.SelectedValue.Remove(0, 1));
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearGallery();
//            return;
//        }

//        this.allArtistsGalleryHiddenRe.Value = p.PicID;
//        this.Notify(this.Master._Notifier.Notify(5, "Red", p.PicID));
    }

    protected void cancelAllArtistsPicGallery_Click(object sender, EventArgs e)
    {
        //this.Start();
    }

//    public bool ValidateFields(int action)
//    {
//        if (action <= 0)
//        {
//            this.Master._Logger.Error(new AdminException(". action <= 0"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            return false;
//        }

//        switch (action)
//        {
//            case 1:
//                if (this.allArtistsSelector.SelectedIndex == 0)
//                {
//                    this.Master._Logger.Error(new AdminException(". this.allArtistsSelector.SelectedIndex == 0"), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(1, "Red", ""));
//                    return false;
//                }
//                break;
//            case 2:
//                if (this.allArtistsPicPlace.Text == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". this.allArtistsPicPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(19, "Red", ""));
//                    return false;
//                }

//                int r = -1;
//                if (!int.TryParse(this.allArtistsPicPlace.Text, out r))
//                {

//                    this.Master._Logger.Error(new AdminException
//                    (". (!int.TryParse(this.allArtistsPicPlace.Text, out r))"), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(20, "Red", ""));
//                    return false;
//                }

//                if (r < 0)
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". (r < 0)"), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(20, "Red", ""));
//                    return false;
//                }

//                if (r > 16)
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". (r > 16)"), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(66, "Red", r.ToString()));
//                    return false;
//                }
//                break;
//            case 3:
//                if (this.removeUpdatePicGallerySelector.SelectedValue == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                            (". this.removeUpdatePicGallerySelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(57, "Red", "All Artists"));
//                    return false;
//                }
//                break;
//            case 4:
//                if (this.allArtistsGalleryHiddenRe.Value == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                        (". this.allArtistsGalleryHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//                    return false;
//                }
//                break;
//            case 5:
//                if (this.allArtistsGalleryHiddenUp.Value == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                        (". this.allArtistsGalleryHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//                    return false;
//                }
//                break;
//            case 6:
//                if (this.allArtistsHidden.Value == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                        (". this.allArtistsHidden.Value == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//                    return false;
//                }
//                break;
//            case 7:
//                if (this.allArtistsPicUpload.Value == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". this.allArtistsPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(3, "Red", this.allArtistsPicUpload.Value));
//                    return false;
//                }
//                break;
//            case 8:
//                if (this.allArtistsPicHiddenRe.Value == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                        (". this.allArtistsPicHiddenRe.Value == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//                    return false;
//                }
//                break;
//            case 9:
//                if (this.allArtistsPicHiddenUp.Value == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                        (". this.allArtistsPicHiddenUp.Value == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//                    return false;
//                }
//                break;
//            case 10:
//                if (this.allArtistsGalleryNameHe.Text == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                        (". this.allArtistsGalleryNameHe.Text == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(24, "Red", ""));
//                    return false;
//                }

//                if (this.allArtistsGalleryNameEn.Text == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                        (". this.allArtistsGalleryNameEn.Text == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(25, "Red", ""));
//                    return false;
//                }

//                if (this.allArtistsGalleryPlace.Text == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". this.allArtistsGalleryPlace.Text == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(63, "Red", ""));
//                    return false;
//                }

//                int i = -1;
//                if (!int.TryParse(this.allArtistsGalleryPlace.Text, out i))
//                {

//                    this.Master._Logger.Error(new AdminException
//                    (". (!int.TryParse(this.allArtistsGalleryPlace.Text, out i))"), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(64, "Red", ""));
//                    return false;
//                }

//                if (i < 0)
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". (i < 0)"), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(64, "Red", ""));
//                    return false;
//                }
//                break;
//            case 11:
//                if (this.removeUpdateAllArtistsGallerySelector.SelectedValue == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". this.removeUpdateAllArtistsGallerySelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Gallery"));
//                    return false;
//                }
//                break;
//            case 12:
//                if (this.removeUpdatePicGallerySelector.SelectedValue == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". this.removeUpdatePicGallerySelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(57, "Red", "Picture"));
//                    return false;
//                }
//                break;
//            case 13:
//                if (this.addPicGallerySelector.SelectedValue == "")
//                {
//                    this.Master._Logger.Error(new AdminException
//                    (". this.addPicGallerySelector.SelectedValue == \"\""), MethodBase.GetCurrentMethod().Name);
//                    this.Notify(this.Master._Notifier.Notify(65, "Red", ""));
//                    return false;
//                }
//                break;
//            default:
//                break;
//        }

//        return true;
//    }

//    public void ClearFields(int action)
//    {
//        if (action <= 0)
//        {
//            this.Master._Logger.Error(new AdminException(". action <= 0"),
//            MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            return;
//        }

//        switch (action)
//        {
//            case 1:
//                foreach (ListItem l in this.allArtistsSelector.Items)
//                {
//                    l.Selected = false;
//                }
//                break;
//            case 2:
//                this.allArtistsPicLastUpdateLabel.Text = "";
//                this.allArtistsPicStatusLabel.Text = "";
//                this.allArtistsGalleryNameHe.Text = "";
//                this.allArtistsGalleryNameEn.Text = "";
//                this.allArtistsGalleryPlace.Text = "";
//                break;
//            case 3:
//                foreach (ListItem l in this.removeUpdateAllArtistsGallerySelector.Items)
//                {
//                    l.Selected = false;
//                }
//                break;
//            case 4:
//                this.allArtistsPicHiddenRe.Value = "";
//                this.allArtistsPicHiddenUp.Value = "";
//                this.allArtistsGalleryHiddenRe.Value = "";
//                this.allArtistsGalleryHiddenUp.Value = "";
//                break;
//            case 5:
//                foreach (ListItem l in this.addPicGallerySelector.Items)
//                {
//                    l.Selected = false;
//                }
//                this.allArtistsPicUploadPic.Src = "";
//                this.allArtistsPicPlace.Text = "";
//                break;
//            case 6:
//                foreach (ListItem l in this.removeUpdatePicGallerySelector.Items)
//                {
//                    l.Selected = false;
//                }

//                break;
//            default:
//                break;
//        }
//    }

//    private void DivSwitcher(int action)
//    {
//        if (action <= 0)
//        {
//            this.Master._Logger.Error(new AdminException
//                 (". action <= 0"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            return;
//        }

//        this.ShowAllArtistsPicUpdateInfo(false);
//        this.ShowGalleryUpdateInfo(false);

//        this.mainAllArtists.Visible = false;
//        this.addAllArtistsGallery.Visible = false;
//        this.removeUpdateAllArtistsGallery.Visible = false;
//        this.addPicToGallery.Visible = false;
//        this.removeUpdatePicGallery.Visible = false;
//        this.allArtistsNotify.Visible = false;

//        this.mainAllArtists.Attributes["class"] = "mailNo";
//        this.addAllArtistsGallery.Attributes["class"] = "mailNo";
//        this.removeUpdateAllArtistsGallery.Attributes["class"] = "mailNo";
//        this.addPicToGallery.Attributes["class"] = "mailNo";
//        this.removeUpdatePicGallery.Attributes["class"] = "mailNo";
//        this.allArtistsNotify.Attributes["class"] = "mailNo";

//        switch (action)
//        {
//            case 1:
//                this.mainAllArtists.Visible = true;
//                this.mainAllArtists.Attributes["class"] = "mailYes";
//                break;
//            case 2:
//                this.addAllArtistsGallery.Visible = true;
//                this.addAllArtistsGallery.Attributes["class"] = "mailYes";

//                if (this.allArtistsGalleryHiddenUp.Value == "")
//                {
//                    this.ShowGalleryUpdateInfo(false);
//                }
//                else
//                {
//                    this.ShowGalleryUpdateInfo(true);
//                }
//                break;
//            case 3:
//                this.removeUpdateAllArtistsGallery.Visible = true;
//                this.removeUpdateAllArtistsGallery.Attributes["class"] = "mailYes";
//                break;
//            case 4:
//                this.addPicToGallery.Visible = true;
//                this.addPicToGallery.Attributes["class"] = "mailYes";

//                if (this.allArtistsPicHiddenUp.Value == "")
//                {
//                    this.ShowAllArtistsFile(true);
//                    this.ShowAllArtistsPicture(false);
//                    this.ShowEnableDisable(false);
//                    this.ShowAllArtistsPicUpdateInfo(false);
//                }
//                else
//                {
//                    this.ShowAllArtistsFile(true);
//                    this.ShowAllArtistsPicture(true);
//                    this.ShowEnableDisable(true);
//                    this.ShowAllArtistsPicUpdateInfo(true);
//                }
//                break;
//            case 5:
//                this.removeUpdatePicGallery.Visible = true;
//                this.removeUpdatePicGallery.Attributes["class"] = "mailYes";
//                break;
//            case 6:
//                this.allArtistsNotify.Visible = true;
//                this.allArtistsNotify.Attributes["class"] = "mailYes";
//                break;
//            default:
//                break;
//        }
//    }

//    private void AddAllArtistsGallery()
//    {
//        if (!this.ValidateFields(10))
//        {
//            this.ClearGallery();
//            return;
//        }

//        if (!this.Master._PapaDal.CheckAvailablePlace
//                ("allArtistsGallery", int.Parse(this.allArtistsGalleryPlace.Text)))
//        {
//            this.Master._Logger.Error(new AdminException
//                (@". (!this.Master._PapaDal.CheckAvailablePlace
//                (""allArtistsGallery"", int.Parse(this.allArtistsGalleryPlace.Text)))"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(21, "Red", this.allArtistsGalleryPlace.Text));
//            this.ClearGallery();
//            return;
//        }


//        try
//        {
//            string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.allArtistsGalleryNameHe.Text);

//            Gallery g = new Gallery
//            {
//                GalleryID = this.Master._PapaDal.GetNextAvailableID("gallery"),
//                GalleryNameEn = this.allArtistsGalleryNameEn.Text,
//                GalleryNameHe = textHe,
//                GalleryType = "allArtistsGallery",
//                LastUpdate = TimeNow.TheTimeNow,
//                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
//                spCreationTime = TimeNow.TheTimeNow.ToShortDateString(),
//                CreationTime = TimeNow.TheTimeNow,
//                GalleryPlace = int.Parse(this.allArtistsGalleryPlace.Text),
//            };

//            this.Master._PapaDal.Add("gallery", g);
//            this.Master._Logger.Log(new AdminException(". " + g.GalleryNameHe + " Was Successfully Added"),
//                            MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(15, "White", g.GalleryNameHe));

//            ListItem l = new ListItem(g.GalleryNameHe, "s" + g.GalleryID);

//            this.addPicGallerySelector.Items.Add(l);
//            this.removeUpdateAllArtistsGallerySelector.Items.Add(l);
//        }
//        catch (Exception e)
//        {
//            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(16, "Red", this.allArtistsGalleryNameHe.Text));
//        }
//    }

//    private void UpdateGalleryInit()
//    {
//        if (!this.ValidateFields(5))
//        {
//            this.ClearGallery();
//            return;
//        }

//        Gallery p = (Gallery)this.Master._PapaDal.Get("gallery", this.allArtistsGalleryHiddenUp.Value);
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearGallery();
//            return;
//        }

//        try
//        {
//            this.galleryLastUpdateLabel.Text = p.spLastUpdate;
//            this.allArtistsGalleryNameHe.Text = p.GalleryNameHe;
//            this.allArtistsGalleryNameEn.Text = p.GalleryNameEn;
//            this.allArtistsGalleryPlace.Text = p.GalleryPlace.ToString();
//            this.ShowGalleryUpdateInfo(true);
//        }
//        catch (Exception e)
//        {
//            this.ClearGallery();
//            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//        }
//    }

//    private void UpdateGallery()
//    {
//        if (!this.ValidateFields(5))
//        {
//            this.ClearGallery();
//            return;
//        }

//        if (!this.ValidateFields(10))
//        {
//            this.ClearGallery();
//            return;
//        }

//        Gallery g = (Gallery)this.Master._PapaDal.Get("gallery", this.allArtistsGalleryHiddenUp.Value);
//        if (g == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". g == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearGallery();
//            return;
//        }

//        if (!this.Master._PapaDal.CheckAvailablePlaceExcept
//                ("allArtistsGallery", g.GalleryPlace, int.Parse(this.allArtistsGalleryPlace.Text)))
//        {
//            this.Master._Logger.Error(new AdminException
//                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept
//           (""allArtistsGallery"", g.GalleryPlace, int.Parse(this.allArtistsGalleryPlace.Text)))"),
//            MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(21, "Red", this.allArtistsGalleryPlace.Text));
//            this.ClearGallery();
//            return;
//        }

//        try
//        {
//            string textHe = this.Master._GlobalFunctions.ConvertToUtf8(this.allArtistsGalleryNameHe.Text);

//            g.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
//            g.LastUpdate = TimeNow.TheTimeNow;
//            g.GalleryNameHe = textHe;
//            g.GalleryNameEn = this.allArtistsGalleryNameEn.Text;
//            g.GalleryPlace = int.Parse(this.allArtistsGalleryPlace.Text);

//            this.Master._PapaDal.Update("gallery", g, TimeNow.TheTimeNow);
//            this.Master._Logger.Log(new AdminException(". " + g.GalleryNameHe +
//                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(17, "White", g.GalleryNameHe));

//            this.removeUpdateAllArtistsGallerySelector.Items.FindByValue
//            ("s" + g.GalleryID).Text = g.GalleryNameHe;
//            this.addPicGallerySelector.Items.FindByValue
//            ("s" + g.GalleryID).Text = g.GalleryNameHe;
//        }
//        catch (Exception e)
//        {
//            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(18, "Red", this.allArtistsGalleryNameHe.Text));
//        }
//    }

//    private void RemoveGallery()
//    {
//        if (!this.ValidateFields(4))
//        {
//            this.ClearGallery();
//            return;
//        }

//        Gallery g = (Gallery)this.Master._PapaDal.Get("gallery", this.allArtistsGalleryHiddenRe.Value);
//        if (g == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". g == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearGallery();
//            return;
//        }

//        try
//        {
//            this.DeletePictures(g.GalleryID);

//            this.Master._PapaDal.Remove("gallery", g.GalleryID);
//            this.Master._Logger.Log(new AdminException(". " + g.GalleryID + " Was Successfully Removed"),
//                MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(12, "White", g.GalleryNameHe));

//            this.removeUpdatePicGallerySelector.Items.Remove
//            (this.removeUpdatePicGallerySelector.Items.FindByValue
//            ("s" + this.allArtistsGalleryHiddenRe.Value));
//            this.addPicGallerySelector.Items.Remove
//            (this.addPicGallerySelector.Items.FindByValue
//            ("s" + this.allArtistsGalleryHiddenRe.Value));
//        }
//        catch (Exception)
//        {
//            try
//            {
//                this.Master._PapaDal.Remove("gallery", g.GalleryID);
//                this.Master._Logger.Log(new AdminException(". " + g.GalleryID + " Was Successfully Removed"),
//                    MethodBase.GetCurrentMethod().Name);
//                this.Notify(this.Master._Notifier.Notify(12, "White", g.GalleryNameHe));

//                this.removeUpdatePicGallerySelector.Items.Remove
//                (this.removeUpdatePicGallerySelector.Items.FindByValue
//                ("s" + this.allArtistsGalleryHiddenRe.Value));
//                this.addPicGallerySelector.Items.Remove
//                (this.addPicGallerySelector.Items.FindByValue
//                ("s" + this.allArtistsGalleryHiddenRe.Value));

//            }
//            catch (Exception e)
//            {
//                this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
//                this.Notify(this.Master._Notifier.Notify(13, "Red", g.GalleryNameHe));
//            }
//        }
//    }

//    private void AddAllArtistsPic()
//    {
//        if (!this.ValidateFields(2))
//        {
//            this.ClearPic();
//            return;
//        }

//        if (!this.ValidateFields(13))
//        {
//            this.ClearPic();
//            return;
//        }

//        if (this.allArtistsPicUpload.Value == "")
//        {
//            this.Master._Logger.Error(new AdminException
//            (". this.allArtistsPicUpload.Value == \"\""), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(37, "Red", "All Artists"));
//            return;
//        }

//        if (!this.Master._GlobalFunctions.ValidatePicEnd(this.allArtistsPicUpload.Value))
//        {
//            this.Master._Logger.Error(new AdminException
//            (". !this.Master._GlobalFunctions.ValidatePicEnd(this.allArtistsPicUpload.Value)"),
//            MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(14, "Red", this.allArtistsPicUpload.Value));
//            this.ClearPic();
//            return;
//        }

//        string galleryID = this.addPicGallerySelector.SelectedValue.Remove(0, 1);

//        if (!this.Master._PapaDal.CheckAvailableGalleriesPicturePlace("allArtistsPic", galleryID,
//            int.Parse(this.allArtistsPicPlace.Text)))
//        {
//            this.Master._Logger.Error(new AdminException
//                (@". (!this.Master._PapaDal.CheckAvailableGalleriesPicturePlace(""allArtistsPic"", proID,
//            int.Parse(this.allArtistsPicPlace.Text)))"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(21, "Red", this.allArtistsPicPlace.Text));
//            this.ClearPic();
//            return;
//        }

//        string ID = this.Master._PapaDal.GetNextAvailableID("allArtistsPic");
//        string fileName = this.allArtistsPicUpload.PostedFile.FileName;
//        string fileNameToSave = "allArtists_id-" + ID + "_" + fileName;
//        string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
//        string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;

//        try
//        {
//            this.allArtistsPicUpload.PostedFile.SaveAs(fullPath);
//        }
//        catch (Exception i)
//        {
//            this.Master._Logger.Error(i, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(36, "Red", this.allArtistsPicUpload.Value));
//            this.ClearPic();
//            return;
//        }

//        try
//        {
//            AllArtistPic p = new AllArtistPic
//            {
//                Active = 2,
//                PicName = fileName,
//                GalleryID = galleryID,
//                PicFullPath = fullPath,
//                PicRelativePath = relativePath,
//                PicID = ID,
//                LastUpdate = TimeNow.TheTimeNow,
//                spLastUpdate = TimeNow.TheTimeNow.ToShortDateString(),
//                UploadTime = TimeNow.TheTimeNow,
//                spUploadTime = TimeNow.TheTimeNow.ToString(),
//                spActive = "Disable",
//                PicPlace = int.Parse(this.allArtistsPicPlace.Text),
//            };

//            this.Master._PapaDal.Add("allArtistsPic", p);
//            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Was Successfully Added"),
//                            MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(15, "White", fileName));

//            this.removeUpdatePicGallerySelector.Items.Add(new ListItem(fileName, "s" + ID));
//        }
//        catch (Exception e)
//        {
//            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(16, "Red", fileName));
//        }
//    }

//    private void UpdatePicInit()
//    {
//        if (!this.ValidateFields(9))
//        {
//            this.ClearPic();
//            return;
//        }

//        AllArtistPic p = (AllArtistPic)this.Master._PapaDal.Get("allArtistsPic", this.allArtistsPicHiddenUp.Value);
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearPic();
//            return;
//        }

//        try
//        {
//            foreach (ListItem l in this.addPicGallerySelector.Items)
//            {
//                l.Selected = false;
//            }

//            this.allArtistsPicPlace.Text = p.PicPlace.ToString();
//            this.allArtistsPicLastUpdateLabel.Text = p.spLastUpdate;
//            this.allArtistsPicStatusLabel.Text = p.spActive;
//            this.addPicGallerySelector.Items.FindByValue("s" + p.GalleryID).Selected = true;
//            this.allArtistsPicUploadPic.Src = p.PicRelativePath;
//            this.ShowAllArtistsPicture(true);
//            this.ShowAllArtistsPicUpdateInfo(true);
//        }
//        catch (Exception e)
//        {
//            this.ClearPic();
//            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//        }
//    }

//    private void UpdatePic()
//    {
//        if (!this.ValidateFields(9))
//        {
//            this.ClearPic();
//            return;
//        }

//        if (!this.ValidateFields(2))
//        {
//            this.ClearPic();
//            return;
//        }

//        AllArtistPic p = (AllArtistPic)this.Master._PapaDal.Get("allArtistsPic", this.allArtistsPicHiddenUp.Value);
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearPic();
//            return;
//        }

//        if (!this.Master._PapaDal.CheckAvailableGalleriesPicturePlaceExcept
//        ("allArtistsPic", p.GalleryID, p.PicPlace, int.Parse(this.allArtistsPicPlace.Text)))
//        {
//            this.Master._Logger.Error(new AdminException
//                (@". (!this.Master._PapaDal.CheckAvailablePlaceExcept
//           (""allArtistsPic"", g.GalleryPlace, int.Parse(this.allArtistsPicPlace.Text)))"),
//            MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(21, "Red", this.allArtistsPicPlace.Text));
//            this.ClearPic();
//            return;
//        }

//        if (this.allArtistsPicUpload.Value != "")
//        {
//            if (!this.Master._GlobalFunctions.ValidatePicEnd(this.allArtistsPicUpload.Value))
//            {
//                this.Master._Logger.Error(new AdminException
//                (". !this.Master._GlobalFunctions.ValidatePicEnd(this.allArtistsPicUpload.Value)"),
//                MethodBase.GetCurrentMethod().Name);
//                this.Notify(this.Master._Notifier.Notify(14, "Red", this.allArtistsPicUpload.Value));
//                this.ClearPic();
//                return;
//            }

//            try
//            {
//                string fileName = this.allArtistsPicUpload.PostedFile.FileName;
//                string fileNameToSave = "allArtists_id-" + p.PicID + "_" + fileName;
//                string fullPath = this.Master._GlobalFunctions.GetFullPath() + fileNameToSave;
//                string relativePath = this.Master._GlobalFunctions.GetRelativePath() + fileNameToSave;


//                if (File.Exists(p.PicFullPath))
//                {
//                    File.Delete(p.PicFullPath);
//                }
//                this.allArtistsPicUpload.PostedFile.SaveAs(fullPath);

//                p.PicName = fileName;
//                p.PicFullPath = fullPath;
//                p.PicRelativePath = relativePath;
//            }
//            catch (Exception h)
//            {
//                this.Master._Logger.Error(h, MethodBase.GetCurrentMethod().Name);
//                this.Notify(this.Master._Notifier.Notify(36, "Red", this.allArtistsPicUpload.Value));
//                this.ClearPic();
//                return;
//            }
//        }

//        try
//        {
//            p.GalleryID = this.addPicGallerySelector.SelectedValue.Remove(0, 1);
//            p.spLastUpdate = TimeNow.TheTimeNow.ToShortDateString();
//            p.LastUpdate = TimeNow.TheTimeNow;
//            p.PicPlace = int.Parse(this.allArtistsPicPlace.Text);

//            this.Master._PapaDal.Update("allArtistsPic", p, TimeNow.TheTimeNow);
//            this.Master._Logger.Log(new AdminException(". " + p.PicID +
//                    " Was Successfully Updated"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(17, "White", p.PicName));

//            this.removeUpdatePicGallerySelector.Items.FindByValue
//            ("s" + p.PicID).Text = p.PicName;
//        }
//        catch (Exception e)
//        {
//            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(18, "Red", p.PicName));
//        }
//    }

//    private void ShowEnableDisable(bool visible)
//    {
//        this.disableAllArtistsPicGallery.Visible = visible;
//        this.enableAllArtistsPicGallery.Visible = visible;
//    }

//    private void ShowAllArtistsFile(bool visible)
//    {
//        this.allArtistsPicUpload.Visible = visible;
//    }

//    private void ShowAllArtistsPicture(bool visible)
//    {
//        this.allArtistsPicUploadPic.Visible = visible;
//    }

//    private void RemovePic()
//    {
//        if (!this.ValidateFields(4))
//        {
//            this.ClearPic();
//            return;
//        }

//        AllArtistPic p = (AllArtistPic)this.Master._PapaDal.Get("allArtistsPic", this.allArtistsPicHiddenRe.Value);
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearPic();
//            return;
//        }

//        try
//        {
//            if (File.Exists(p.PicFullPath))
//            {
//                File.Delete(p.PicFullPath);
//            }

//            this.Master._PapaDal.Remove("allArtistsPic", p.PicID);
//            this.Master._Logger.Log(new AdminException(". " + p.PicName + " Was Successfully Removed"),
//                MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(12, "White", p.PicName));

//            this.removeUpdatePicGallerySelector.Items.Remove
//            (this.removeUpdatePicGallerySelector.Items.FindByValue
//            ("s" + p.PicID));
//        }
//        catch (Exception e)
//        {
//            this.Master._Logger.Log(e, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(13, "Red", p.PicName));
//        }
//    }

//    private void EnablePic()
//    {
//        if (!this.ValidateFields(9))
//        {
//            this.ClearPic();
//            return;
//        }

//        AllArtistPic p = (AllArtistPic)this.Master._PapaDal.Get("allArtistsPic", this.allArtistsPicHiddenUp.Value);
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearPic();
//            return;
//        }

//        if (p.Active == 1)
//        {
//            this.Master._Logger.Warn(new AdminException
//            (". " + this.allArtistsPicHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(9, "Red", p.PicName));
//            this.ClearFields(3);
//            return;
//        }

//        if (!this.Master._PapaDal.CheckAllArtistsGalleryStatus(p.GalleryID))
//        {
//            this.Master._Logger.Error(new AdminException
//            (@". (!this.Master._PapaDal.CheckAllArtistsGalleryStatus(p.GalleryID))"),
//                MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(52, "Red", ""));
//            this.ClearFields(5);
//        }

//        try
//        {
//            this.Master._PapaDal.Enable("allArtistsPic", p.PicID);
//            this.Master._Logger.Log(new AdminException(". " + p.PicID +
//                " Has Been Successfully Enabled"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(6, "White", p.PicName));
//        }
//        catch (Exception f)
//        {
//            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(10, "Red", p.PicName));
//        }
//    }

//    private void DisablePic()
//    {
//        if (!this.ValidateFields(9))
//        {
//            this.ClearPic();
//            return;
//        }

//        AllArtistPic p = (AllArtistPic)this.Master._PapaDal.Get("allArtistsPic", this.allArtistsPicHiddenUp.Value);
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//                        (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            this.ClearPic();
//            return;
//        }

//        if (p.Active == 2)
//        {
//            this.Master._Logger.Warn(new AdminException
//            (". " + this.allArtistsPicHiddenUp.Value + " Is Already Disabled"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(8, "Red", p.PicName));
//            this.ClearPic();
//            return;
//        }

//        try
//        {
//            this.Master._PapaDal.Disable("allArtistsPic", p.PicName);
//            this.Master._Logger.Log(new AdminException(". " + p.PicID + " Has Been Successfully Disabled"),
//            MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(7, "White", p.PicName));
//        }
//        catch (Exception f)
//        {
//            this.Master._Logger.Log(f, MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(11, "Red", p.PicName));
//        }
//    }

    protected void okBut_Click(object sender, EventArgs e)
    {
//        if (!this.ValidateFields(6))
//        {
//            return;
//        }

        //this.AfterOk(this.allArtistsHidden.Value);
    }

    protected void cancelBut_Click(object sender, EventArgs e)
    {
//        this.Start();
    }

//    private void ClearGallery()
//    {
//        this.ClearFields(2);
//        this.ClearFields(3);
//        this.ClearFields(4);
//    }

//    private void ClearPic()
//    {
//        this.ClearFields(4);
//        this.ClearFields(5);
//        this.ClearFields(6);
//    }

//    public void ShowGalleryUpdateInfo(bool visible)
//    {
//        this.galleryUpdateInfo.Visible = visible;
//        if (visible)
//        {
//            this.galleryUpdateInfo.Attributes["class"] = "mailYes";
//        }
//        else
//        {
//            this.galleryUpdateInfo.Attributes["class"] = "mailNo";
//        }
//    }

//    public void ShowAllArtistsPicUpdateInfo(bool visible)
//    {
//        this.allArtistsPicUpdateInfo.Visible = visible;
//        if (visible)
//        {
//            this.allArtistsPicUpdateInfo.Attributes["class"] = "mailYes";
//        }
//        else
//        {
//            this.allArtistsPicUpdateInfo.Attributes["class"] = "mailNo";
//        }
//    }

//    private void Start()
//    {
//        this.ClearFields(1);
//        this.ClearFields(2);
//        this.ClearFields(3);
//        this.ClearFields(4);
//        this.ClearFields(5);
//        this.ClearFields(6);

//        this.ShowAllArtistsPicture(false);
//        this.ShowAllArtistsFile(false);
//        this.ShowEnableDisable(false);
//        this.ShowGalleryUpdateInfo(false);
//        this.ShowAllArtistsPicUpdateInfo(false);

//        this.DivSwitcher(1);
//    }

//    private void DeletePictures(string galleryID)
//    {
//        if (galleryID == "" || galleryID == null)
//        {
//            this.Master._Logger.Error(new AdminException
//            (". galleryID == \"\" || galleryID == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            return;
//        }

//        Gallery p = (Gallery)this.Master._PapaDal.Get("gallery", galleryID);
//        if (p == null)
//        {
//            this.Master._Logger.Error(new AdminException
//            (". p == null"), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            return;
//        }

//        foreach (AllArtistPic d in this.Master._PapaDal.GetAllAllArtistsPictures(p.GalleryID))
//        {
//            this.removeUpdatePicGallerySelector.Items.Remove
//            (this.removeUpdatePicGallerySelector.Items.FindByValue("s" + d.PicID));
//        }
//    }

//    private void AfterOk(string selector)
//    {
//        if (selector == "")
//        {
//            this.Master._Logger.Error(new AdminException
//                 (". selector == \"\""), MethodBase.GetCurrentMethod().Name);
//            this.Notify(this.Master._Notifier.Notify(0, "Red", ""));
//            return;
//        }

//        switch (selector)
//        {
//            case "0":
//                this.Master.Exit();
//                break;
//            case "1":
//                //select an action
//                break;
//            case "3":
//            //no file was sent
//            case "4":
//                //no pics to remove or update
//                break;
//            case "5":
//                if (this.allArtistsGalleryHiddenRe.Value == "")
//                {
//                    this.RemovePic();
//                }
//                else
//                {
//                    this.RemoveGallery();
//                }
//                break;
//            case "17":
//            case "18":
//                //update text
//                break;
//            case "7":
//            case "8":
//            case "11":
//                //disable pic
//                break;
//            case "6":
//            case "9":
//            case "10":
//                //enable pic
//                break;
//            case "12":
//            case "13":
//                //remove pic
//                break;
//            case "15":
//            case "16":
//                //add pic
//                break;
//            case "19":
//                //enter pic place
//                break;
//            case "20":
//                //invalid pic place
//                break;
//            case "21":
//                //pic place not available
//                break;
//            default:
//                break;
//        }

//        if (selector != "5")
//        {
//            this.Start();
//            this.allArtistsHidden.Value = "";
//        }
//    }

//    private void Notify(string[] message)
//    {
//        this.cancelBut.Visible = false;
//        try
//        {
//            if (message == null || message.Count() != 3)
//            {
//                throw new AdminException(". message == null || message.Count() != 3");
//            }

//            if (message[0] == "" || message[0] == null ||
//                message[1] == "" || message[1] == null ||
//                message[2] == "" || message[2] == null)
//            {
//                throw new AdminException(@". message[0] == """" || message[0] == null || message[1] == """" ||
//                                            message[1] == null || message[2] == """" || message[2] == null");
//            }

//            if (message[1] == "Red")
//            {
//                this.notifyLabel.ForeColor = Color.Red;
//            }
//            if (message[1] == "White")
//            {
//                this.notifyLabel.ForeColor = Color.White;
//            }

//            this.notifyLabel.Text = message[0];
//            this.allArtistsHidden.Value = message[2];

//            switch (message[2])
//            {
//                case "5":
//                    this.cancelBut.Visible = true;
//                    break;
//                default:
//                    break;
//            }
//        }
//        catch (Exception e)
//        {
//            this.Master._Logger.Error(e, MethodBase.GetCurrentMethod().Name);
//            this.notifyLabel.ForeColor = Color.Red;
//            this.notifyLabel.Text = "Oops! Something Wrong Has Happened, Please Try Again Or/And contact The Administrator";
//        }
//        finally
//        {
//            this.DivSwitcher(6);
//        }
    //}
}
