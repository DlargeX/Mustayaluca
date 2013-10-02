using System;
using System.Collections;
using MediaPortal.GUI.Library;
using System.Windows.Forms;

namespace MustayalucaConfig
{
  public class SkinInfoGUI : GUIWindow
  {
    private static readonly logger smcLog = logger.GetInstance();

    #region Skin Connection

    private enum GUIControls
    {
      exitScreen  = 2,
      versionLabel = 3,
      actualScreenRes = 4
    }

    [SkinControl((int)GUIControls.exitScreen)] protected GUIButtonControl skinInfo_exit = null;
    [SkinControl((int)GUIControls.versionLabel)] protected GUILabelControl version_label = null;
    [SkinControl((int)GUIControls.actualScreenRes)] protected GUILabelControl screen_res = null;


    #endregion

    #region Public Properties



    #endregion

    #region Public methods

    public SkinInfoGUI()
    {
    }


    #endregion

    #region Base overrides

    public override int GetID
    {
      get
      {
        return (int)MustayalucaConfig.SMPScreenID.SMPSkinInfo;
      }
      set
      {
      }
    }

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\MySkinInfo.xml");
    }

    protected override void OnPageLoad()
    {
      System.Reflection.Assembly oAssembly = System.Reflection.Assembly.GetExecutingAssembly();
      System.Diagnostics.FileVersionInfo oFileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(oAssembly.Location);

      string version = oFileVersionInfo.FileVersion;

      GUILabelControl.SetControlLabel(GetID, (int)GUIControls.versionLabel, version);
      GUILabelControl.SetControlLabel(GetID, (int)GUIControls.actualScreenRes, "(" + Screen.PrimaryScreen.Bounds.Width.ToString() + "x" + Screen.PrimaryScreen.Bounds.Height.ToString() + ")");
    }

    protected override void OnPageDestroy(int new_windowId)
    {

    }
    #endregion
  }
}
