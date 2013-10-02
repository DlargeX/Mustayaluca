using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;


namespace MustayalucaConfig
{
  partial class ConfigurationForm : Form
  {
    #region Variables
    // Private Variables
    private DateTimePicker timePicker;
    SkinInfo skInfo = new SkinInfo();

    // Protected Variables
    // Public Variables
    #endregion

    #region Public methods

    public ConfigurationForm()
    {
      InitializeComponent();

      //settings.Load(settings.cXMLSectionUpdate);
      settings.Load(settings.cXMLSectionMisc);
      settings.Load(settings.cXMLSectionVideo);
      settings.Load(settings.cXMLSectionTV);  

      releaseVersion.Text = String.Format("Version: {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString());
      DateTime buildDate = getLinkerTimeStamp(Assembly.GetExecutingAssembly().Location);
      compileTime.Text += " " + buildDate.ToString() + " GMT";

      timePicker = new DateTimePicker();
      timePicker.Format = DateTimePickerFormat.Time;
      timePicker.ShowUpDown = true;
      timePicker.Location = new Point(309, 85);
      timePicker.Width = 100;
      //CheckUpdate.Controls.Add(timePicker);
    }

    #endregion

    #region Private methods

    // Save settings to file
    void btSave_Click(object sender, EventArgs e)
    {
      // get current settings and save to file
      //MustayalucaConfig.checkOnStart = cbCheckOnStart.Checked;
      //MustayalucaConfig.checkForUpdateAt = cbCheckForUpdateAt.Checked;
      //MustayalucaConfig.checkInterval = comboCheckInterval.SelectedIndex;
      //MustayalucaConfig.checkTime = timePicker.Value;
      //MustayalucaConfig.nextUpdateCheck = MustayalucaConfig.nextCheckAt.ToString();
      //MustayalucaConfig.patchUtilityRunUnattended = cbRunUnattended.Checked;
      //MustayalucaConfig.patchUtilityRestartMP = cbRestartMP.Checked;      
      
     // settings.Save(settings.cXMLSectionUpdate);
      settings.Save(settings.cXMLSectionVideo);
      settings.Save(settings.cXMLSectionMisc);
      settings.Save(settings.cXMLSectionTV);
      
      this.Close();
    }

    void btCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    // Load settings from xml
    void ConfigurationForm_Load(object sender, EventArgs e)
    { 
      //cbRunUnattended.Checked = MustayalucaConfig.patchUtilityRunUnattended;
      //cbRestartMP.Checked = MustayalucaConfig.patchUtilityRestartMP;


      //cbCheckOnStart.Checked = MustayalucaConfig.checkOnStart;
      //cbCheckForUpdateAt.Checked = MustayalucaConfig.checkForUpdateAt;
      //if (MustayalucaConfig.checkForUpdateAt)
      //{
      //  cbCheckForUpdateAt.Checked = MustayalucaConfig.checkForUpdateAt;
      //  comboCheckInterval.SelectedIndex = MustayalucaConfig.checkInterval;
      //  timePicker.Value = MustayalucaConfig.checkTime;
      //}
    }

    private static DateTime getLinkerTimeStamp(string filePath)
    {
      const int PeHeaderOffset = 60;
      const int LinkerTimestampOffset = 8;

      byte[] b = new byte[2047];
      using (System.IO.Stream s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
      {
        s.Read(b, 0, 2047);
      }
      int secondsSince1970 = BitConverter.ToInt32(b, BitConverter.ToInt32(b, PeHeaderOffset) + LinkerTimestampOffset);
      return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(secondsSince1970);
    }

    void btCheckForUpdate_Click(object sender, EventArgs e)
    {
      //updateCheck.patchList.Clear();
      //if (updateCheck.updateAvailable(true))
      //  updateFound.displayDetail(true);
      //else
      //  MessageBox.Show(Translation.NoUpdatesAvailable);
    }

    #endregion

  }
}