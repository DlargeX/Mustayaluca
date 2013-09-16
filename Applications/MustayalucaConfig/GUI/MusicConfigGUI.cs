//using System;
//using System.Collections;
//using MediaPortal.GUI.Library;
//using Action = MediaPortal.GUI.Library.Action;
//using System.IO;
//using SMPCheckSum;


//namespace MustayalucaConfig
//{
//  public class MusicOptionsGUI : GUIWindow
//  {
//    private static readonly logger smcLog = logger.GetInstance();

//    #region Skin Connection

//    private enum GUIControls
//    {
//      MusicCDCover = 2,          // Music Options
//      MusicGFXeq = 3,            // Music Options

//    }

//    [SkinControl((int)GUIControls.MusicCDCover)] protected GUIToggleButtonControl MusicCDCover = null;
//    [SkinControl((int)GUIControls.MusicGFXeq)] protected GUIToggleButtonControl MusicGFXeq = null;

//    #endregion

//    #region Constructor

//    public MusicOptionsGUI()
//    {
//    }

//    #endregion

//    #region Public Properties
//    public static bool cdCoverOnly { get; set; }
//    public static bool showEqGraphic { get; set; }
//    #endregion

//    #region Public methods
    
//    public static void SetProperties()
//    { 
//      smcLog.WriteLog("MustayalucaConfig: Setting #Mustayaluca.cdCoverOnly = " + cdCoverOnly.ToString().ToLower(), LogLevel.Debug);
//      GUIPropertyManager.SetProperty("#Mustayaluca.cdCoverOnly", cdCoverOnly.ToString().ToLower());

//      smcLog.WriteLog("MustayalucaConfig: Setting #Mustayaluca.showEqGraphic = " + showEqGraphic.ToString().ToLower(), LogLevel.Debug);
//      GUIPropertyManager.SetProperty("#Mustayaluca.showEqGraphic", showEqGraphic.ToString().ToLower());
//    }


//    #endregion

//    #region Base overrides

//    public override bool Init()
//    {
//      return Load(GUIGraphicsContext.Skin + @"\MustayalucaConfig_music.xml");

//    }

//    public override int GetID
//    {
//      get
//      {
//        return (int)MustayalucaConfig.SMPScreenID.SMPMusicSettings;
//      }
//      set { }
//    }


//    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
//    {
//      switch (controlId)
//      {
//        case (int)GUIControls.MusicCDCover:
//          cdCoverOnly = MusicCDCover.Selected;
//          break;
//        case (int)GUIControls.MusicGFXeq:
//          showEqGraphic = MusicGFXeq.Selected;
//          break;
//      }
//      base.OnClicked(controlId, control, actionType);
//    }

//    protected override void OnPageLoad()
//    {
//      settings.Load(settings.cXMLSectionMusic);
//      MusicGFXeq.Selected = showEqGraphic;
//      MusicCDCover.Selected = cdCoverOnly;

//      // Load Translations
//      MusicCDCover.Label = Translation.CDCover;
//      MusicGFXeq.Label = Translation.ShowEQ;

//    }

//    protected override void OnPageDestroy(int new_windowId)
//    {      
//      smcLog.WriteLog(string.Format("MustayalucaConfig: Settings.Save({0})", settings.cXMLSectionMusic), LogLevel.Info);

//      showEqGraphic = MusicGFXeq.Selected;
//      cdCoverOnly = MusicCDCover.Selected;

//      settings.Save(settings.cXMLSectionMusic);
//      SetProperties();

//      GUIWindowManager.OnResize();
//    }
//    #endregion
//  }
//}
