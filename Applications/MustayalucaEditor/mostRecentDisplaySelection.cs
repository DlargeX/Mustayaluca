using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MustayalucaEditor
{
  public partial class mostRecentDisplaySelection : Form
  {
    public mostRecentDisplaySelection()
    {
      InitializeComponent();

      rbSubOff.Checked = true;
      rbSubTVSeries.Checked = false;
      rbSubMovies.Checked = false;
      rbSubMusic.Checked = false;
      rbSubPictures.Checked = false;
      rbSubTV.Checked = false;
      rbSubFreeDriveSpace.Checked = false;
      rbSubPowerControl.Checked = false;
      rbSubSleepControl.Checked = false;
      rbSubStocks.Checked = false;
      rbSubHtpcInfo.Checked = false;
      rbSubUpdateControl.Checked = false;

    }

    public void setEnableState(formMustayalucaEditor.displayMostRecent overlayType, bool state)
    {
        switch (overlayType)
        {
            case formMustayalucaEditor.displayMostRecent.tvSeries:
                rbSubTVSeries.Enabled = state;
                rbSubTVSeries.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.movies:
                rbSubMovies.Enabled = state;
                rbSubMovies.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.music:
                rbSubMusic.Enabled = state;
                rbSubMusic.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.pictures:
                rbSubPictures.Enabled = state;
                rbSubPictures.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.recordedTV:
                rbSubTV.Enabled = state;
                rbSubTV.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.freeDriveSpace:
                rbSubFreeDriveSpace.Enabled = state;
                rbSubFreeDriveSpace.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.powerControl:
                rbSubPowerControl.Enabled = state;
                rbSubPowerControl.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.sleepControl:
                rbSubSleepControl.Enabled = state;
                rbSubSleepControl.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.stocks:
                rbSubStocks.Enabled = state;
                rbSubStocks.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.htpcInfo:
                rbSubHtpcInfo.Enabled = state;
                rbSubHtpcInfo.Checked = state;
                break;
            case formMustayalucaEditor.displayMostRecent.updateControl:
                rbSubUpdateControl.Enabled = state;
                rbSubUpdateControl.Checked = state;
                break;
            default:
                rbSubOff.Checked = true;
                break;
        }
    }

    public bool disableMusicRB 
    {
      set
      {
        rbSubMusic.Enabled = value; ;
      }
    }

    public bool disableRecordedTVRB
    {
      set
      {
        rbSubTV.Enabled = value; ;
      }
    }

    public formMustayalucaEditor.displayMostRecent mrToDisplay
    {
      get { return selectedMostRecent(); }
      set { setMostRecent(value); }
    }

    private formMustayalucaEditor.displayMostRecent selectedMostRecent()
    {
      if (rbSubTVSeries.Checked)
        return formMustayalucaEditor.displayMostRecent.tvSeries;

      if (rbSubMovies.Checked)
        return formMustayalucaEditor.displayMostRecent.movies;

      if (rbSubMusic.Checked)
        return formMustayalucaEditor.displayMostRecent.music;

      if (rbSubPictures.Checked)
        return formMustayalucaEditor.displayMostRecent.pictures;

      if (rbSubTV.Checked)
        return formMustayalucaEditor.displayMostRecent.recordedTV;

      if (rbSubFreeDriveSpace.Checked)
          return formMustayalucaEditor.displayMostRecent.freeDriveSpace;

      if (rbSubPowerControl.Checked)
          return formMustayalucaEditor.displayMostRecent.powerControl;

      if (rbSubSleepControl.Checked)
          return formMustayalucaEditor.displayMostRecent.sleepControl;

      if (rbSubStocks.Checked)
          return formMustayalucaEditor.displayMostRecent.stocks;

      if (rbSubHtpcInfo.Checked)
          return formMustayalucaEditor.displayMostRecent.htpcInfo;

      if (rbSubUpdateControl.Checked)
          return formMustayalucaEditor.displayMostRecent.updateControl;

      return formMustayalucaEditor.displayMostRecent.off;
    
    }
    private void setMostRecent(formMustayalucaEditor.displayMostRecent rbSetting)
    {
        switch (rbSetting)
        {
            case formMustayalucaEditor.displayMostRecent.off:
                rbSubOff.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.tvSeries:
                rbSubTVSeries.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.movies:
                rbSubMovies.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.music:
                rbSubMusic.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.pictures:
                rbSubPictures.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.recordedTV:
                rbSubTV.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.freeDriveSpace:
                rbSubFreeDriveSpace.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.powerControl:
                rbSubPowerControl.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.sleepControl:
                rbSubSleepControl.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.stocks:
                rbSubStocks.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.htpcInfo:
                rbSubHtpcInfo.Checked = true;
                break;
            case formMustayalucaEditor.displayMostRecent.updateControl:
                rbSubUpdateControl.Checked = true;
                break;
        }

    }

    private void btSaveAndClose_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

  }
}
