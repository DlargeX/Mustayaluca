using System;
using System.Collections;
using MediaPortal.GUI.Library;


namespace MustayalucaConfig
{
  public class VideoOptionsGUI : GUIWindow
  {
    private static readonly logger smcLog = logger.GetInstance();

    #region Skin Connection

    private enum GUIControls
    {
      //VideoMinOSD = 2,
      VideoBackground = 3
    }

    //[SkinControl((int)GUIControls.VideoMinOSD)] protected GUICheckButton cmc_MinOSD = null;
    [SkinControl((int)GUIControls.VideoBackground)] protected GUICheckButton toggleVideoBackground = null;    

    #endregion

    #region Public Properties

    //public static bool FullVideoOSD { get; set; }
    public static bool DisableVideoBackground { get; set; }

    #endregion

    #region Public methods

    public static void SetProperties()
    {
      //smcLog.WriteLog("MustayalucaConfig: Setting #Mustayaluca.fullVideoOSD = " + FullVideoOSD.ToString().ToLower(), LogLevel.Debug);
      //GUIPropertyManager.SetProperty("#Mustayaluca.fullVideoOSD", FullVideoOSD.ToString().ToLower());
    }

    #endregion

    #region Base overrides

    public override int GetID
    {
      get
      {
        return (int)MustayalucaConfig.SMPScreenID.SMPVideoSettings;
      }
      set
      {
      }
    }

    public override bool Init()
    {
      return Load(GUIGraphicsContext.Skin + @"\MustayalucaConfig_video.xml");
    }

    protected override void OnPageLoad()
    {
      settings.Load(settings.cXMLSectionVideo);  
      //cmc_MinOSD.Selected = !FullVideoOSD;
      //cmc_MinOSD.Label = Translation.MinVideoOSD;
      toggleVideoBackground.Selected = DisableVideoBackground;
      toggleVideoBackground.Label = Translation.DisableVideoBackground;
    }

    protected override void OnPageDestroy(int new_windowId)
    {
      //FullVideoOSD = !cmc_MinOSD.Selected;
      DisableVideoBackground = toggleVideoBackground.Selected;
      SetProperties();
      settings.Save(settings.cXMLSectionVideo);
    }
    #endregion
  }
}
