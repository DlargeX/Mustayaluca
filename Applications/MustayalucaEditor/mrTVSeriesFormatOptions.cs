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
  public partial class TVSereisFormatOptions : Form
  {


    public TVSereisFormatOptions()
    {
      InitializeComponent();
    }

    public bool mrSeriesEpisodeFormat
    {
      get { return cbMRSeriesEpisodeFormat.Checked; }
      set { cbMRSeriesEpisodeFormat.Checked = value; }
    }

    public bool mrTitleLast
    {
      get { return cbTitleSwap.Checked; }
      set { cbTitleSwap.Checked = value; }
    }

    public bool mrDisableFadeLabels
    {
      get { return cbDisableFadeLabels.Checked; }
      set { cbDisableFadeLabels.Checked = value; }
    }

    public string mrSeriesFont
    {
      get 
      {
        if (cboxMRSeriesFont.Text.StartsWith("font5"))
          return "font5";
        else
          return cboxMRSeriesFont.Text; 
      }

      set
      {
        if (value.StartsWith("font5"))
          cboxMRSeriesFont.SelectedIndex = cboxMRSeriesFont.Items.IndexOf("font5");
        else
          cboxMRSeriesFont.SelectedIndex = cboxMRSeriesFont.Items.IndexOf("font5");
      }
    }

    public string mrEpisodeFont
    {
      get
      {
        if (cboxMREpisodeFont.Text.StartsWith("font8"))
          return "font8";
        else
          return cboxMREpisodeFont.Text;
      }

      set
      {
        if (value.StartsWith("font8"))
          cboxMREpisodeFont.SelectedIndex = cboxMREpisodeFont.Items.IndexOf("font8");
        else
          cboxMREpisodeFont.SelectedIndex = cboxMREpisodeFont.Items.IndexOf("font8");
      }

    }

    private void btSaveAndExit_Click(object sender, EventArgs e)
    {
      this.Hide();
    }
  }
}
