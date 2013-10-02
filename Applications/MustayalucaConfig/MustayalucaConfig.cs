using System;
using System.Collections.Generic;
using System.Reflection;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using Action = MediaPortal.GUI.Library.Action;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using MediaPortal.Dialogs;
using Microsoft.Win32;
using Cornerstone.Tools;
using Cornerstone.Database;
using Cornerstone.Database.Tables;
using MediaPortal.Plugins.MovingPictures;
using MediaPortal.Plugins.MovingPictures.Database;
using MediaPortal.Plugins.MovingPictures.MainUI;
using SMPCheckSum;
using MediaPortal.Player;
using WindowPlugins.GUITVSeries;
using TVSeriesHelper = WindowPlugins.GUITVSeries.Helper;

namespace MustayalucaConfig
{
  [PluginIcons("MustayalucaConfig.Resources.SMPSettings.png", "MustayalucaConfig.Resources.SMPSettingsDisabled.png")]
  public class MustayalucaConfig : GUIWindow, ISetupForm
  {
    #region Enums
    public enum PendingChanges
    {
      ClearCache
    }
    #endregion

    #region Variables

    public int mrImageToDisplay = 3;
    public string isImageFanart = null;
    public int isImageCount = 1;
    bool VideoBackgroundAutoSwitcher_installed = false;
    public string[] mostTVSeriesRecents = new string[3];
    public string[] mostTVSeriesRecentsWatched = new string[3];
    public string[] mostMovPicsRecents = new string[3];
    public string[] mostMovPicsRecentsWatched = new string[3];    
    
    internal static settings smpSettings = new settings();
    SkinInfo skInfo = new SkinInfo();
    
    private static readonly logger smcLog = logger.GetInstance();

    #endregion

    #region public properties
        
    //public static bool checkOnStart { get; set; }
    //public static bool checkForUpdateAt { get; set; }
    //public static bool updateAvailable { get; set; }
    //public static bool manualInstallNeeded { get; set; }
    //public static int checkInterval { get; set; }
    public static double hours { get; set; }
    //public static string nextUpdateCheck { get; set; }
    //public static string theRevisions { get; set; }
    //public static DateTime checkTime { get; set; }
    //public static bool patchUtilityRunUnattended { get; set; }
    //public static bool patchUtilityRestartMP { get; set; }
    //public static bool patchAppliedLastRun { get; set; }
    public static bool MinimiseOnExit { get; set; }
    //public static System.Threading.Timer updateChkTimer;

    public static List<PendingChanges> PendingRestartChanges = new List<PendingChanges>();

    #endregion

    #region Private Properties

    private static Regex _isNumber = new Regex(@"^\d+$");

    #endregion

    #region Skin Connection

    public enum SkinControlIDs
    {      
      MusicScreens = 40,      // Skin Settings Menu
      VideoScreens = 41,      // Skin Settings Menu
      MiscScreens = 42,       // Skin Settings Menu
      SkinUpdate = 43,        // Skin Settings Menu
      TVSeriesConfig = 44,    // TVSeries Configuration
      MovPicsConfig = 45,     // MovingPictures Configuration
      TVConfig = 46,           // TV Configuration
      SkinInfoScreen = 47     // Skin Information Screen
    }

    public enum SMPScreenID
    {
      SMPMenu = 1196200,
      SMPMusicSettings = 1196201,
      SMPVideoSettings = 1196202,
      SMPMiscSettings = 1196203,
      SMPSkinUpdate = 1196204,
      SMPTVSeriesConfig = 1196205,
      SMPMovPicsConfig = 1196206,
      SMPTVConfig = 1196207,
      SMPSkinInfo = 1196208
    }

    private enum MediaPortalWindows
    {      
      Home = 0,
      HomePlugins = 34,
      BasicHome = 35
    }

    [SkinControl((int)SkinControlIDs.MusicScreens)]
    protected GUIButtonControl btMusicScreens = null;
    [SkinControl((int)SkinControlIDs.VideoScreens)]
    protected GUIButtonControl btVideoScreens = null;
    [SkinControl((int)SkinControlIDs.MiscScreens)]
    protected GUIButtonControl btMiscScreens = null;
    [SkinControl((int)SkinControlIDs.SkinUpdate)]
    protected GUIButtonControl btSkinUpdate = null;
    [SkinControl((int)SkinControlIDs.TVSeriesConfig)]
    protected GUIButtonControl btTVSeriesConfig = null;
    [SkinControl((int)SkinControlIDs.MovPicsConfig)]
    protected GUIButtonControl btMovPicsConfig = null;
    [SkinControl((int)SkinControlIDs.TVConfig)]
    protected GUIButtonControl btTVConfig = null;
    [SkinControl((int)SkinControlIDs.SkinInfoScreen)]
    protected GUIButtonControl btSkinInfo = null;
    #endregion

    #region Base overrides

    public override bool Init()
    {
      logger.LogFile = Path.Combine(Config.GetFolder(Config.Dir.Log), "MustayalucaConfig.log");
      logger.LogError = smpSettings.logLevelError;
      logger.LogWarning = smpSettings.logLevelWarning;
      logger.LogDebug = smpSettings.logLevelDebug;

      smcLog.WriteLog(string.Format("MustayalucaConfig Plugin {0} starting.", Assembly.GetExecutingAssembly().GetName().Version), LogLevel.Info);

      // Check if the skin is Mustayaluca and bail if not.
      // note: user may have multiple Mustayaluca skins for testing
      if (!GUIGraphicsContext.Skin.ToLowerInvariant().Contains("mustayaluca"))
      {
        smcLog.WriteLog("Not Running Mustayaluca Skin, Exiting...", LogLevel.Info);        
      }
      else
      {
        InitMustayaluca();
        smcLog.WriteLog("Mustayaluca Init Complete", LogLevel.Info);
      }
      
      // remember current skin, so we can reload settings if we need to
      settings.PreviousSkin = settings.CurrentSkin;
      GUIWindowManager.OnDeActivateWindow +=new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnDeActivateWindow);

      return Load(GUIGraphicsContext.Skin + @"\MustayalucaConfig.xml");
    }

    protected override void OnPageLoad()
    {      
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.MusicScreens, Translation.MusicMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.VideoScreens, Translation.VideoMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.SkinUpdate, Translation.SkinUpdate);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.MiscScreens, Translation.MiscMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.TVSeriesConfig, Translation.TVSeriesMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.MovPicsConfig, Translation.MovingPicturesMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.TVConfig, Translation.TVMenu);
      GUIControl.SetControlLabel(GetID, (int)SkinControlIDs.SkinInfoScreen, Translation.SkinInfoText);
    }

    protected override void OnPageDestroy(int new_windowId)
    {      
      smcLog.WriteLog("Shutdown of Mustayalucaconfig complete", LogLevel.Info);
    }

    protected override void OnClicked(int controlId, GUIControl control, Action.ActionType actionType)
    {
      #region Activate Setting GUI Windows
      if (control == btMusicScreens)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPMusicSettings);
      }

      if (control == btVideoScreens)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPVideoSettings);
      }

      if (control == btTVSeriesConfig)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPTVSeriesConfig);
      }

      if (control == btMovPicsConfig)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPMovPicsConfig);
      }

      if (control == btTVConfig)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPTVConfig);
      }

      if (control == btMiscScreens)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPMiscSettings);
      }

      if (control == btSkinUpdate)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPSkinUpdate);
      }

      if (control == btSkinInfo)
      {
        GUIWindowManager.ActivateWindow((int)SMPScreenID.SMPSkinInfo);
      }

      #endregion

      // Pass it on
      base.OnClicked(controlId, control, actionType);
    }

    #endregion

    #region Constructor

    public MustayalucaConfig()
    {
      GetID = GetWindowId();
    }

    #endregion

    #region Private Methods
    private void InitMustayaluca()
    {
      #region Init Translations
      InitTranslations();
      #endregion

      #region Load Settings
      // we need to set properties on startup, load corresponding sections
      //settings.Load(settings.cXMLSectionUpdate);
      settings.Load(settings.cXMLSectionMisc);
      settings.Load(settings.cXMLSectionVideo);
      settings.Load(settings.cXMLSectionTV);      
      #endregion

      #region PostPatchInstall
      //if (MustayalucaConfig.patchAppliedLastRun)
      //{
      //  smcLog.WriteLog("Patch Applied, updating settings...", LogLevel.Info);
      //  PostPatchUpdate.UpdateSettings();
      //  smcLog.WriteLog("Settings Updated", LogLevel.Info);

      //  MustayalucaConfig.patchAppliedLastRun = false;
      //  settings.Save(settings.cXMLSectionUpdate);

      //  // Refresh GUI
      //  //smcLog.WriteLog("Refreshing GUI", LogLevel.Info);
      //  //GUIWindowManager.OnResize();
      //}
      #endregion

      #region Init Updates
      // Should we check for an update on startup, if so check and set skin properties 
      // to control the visibility of the icons indicating and update is available?
      //if (MustayalucaConfig.checkOnStart)
      //{
      //    if (updateCheck.updateAvailable(false))
      //    {
      //        MustayalucaConfig.updateAvailable = true;

      //        // This property controls the visibility of the large update icon on the home screens
      //        SetProperty("#Mustayaluca.UpdateAvailable", "true");

      //        // This property controls the visibility or the indicator icon displyed next to the clock on the home screens
      //        SetProperty("#Mustayaluca.ShowUpdateInd", "true");

      //        // This property controls the visibility or the indicator icon that is displayed next to the skin item in the settings screens
      //        SetProperty("#Mustayaluca.ShowSettingsUpdateInd", "true");
      //    }
      //}

      // Should we check for an update at a specific time
      //if (MustayalucaConfig.checkForUpdateAt)
      //{
      //  DateTime stored;
      //  // Setup the callback
      //  TimerCallback updateCallback = new TimerCallback(checkUpdateOnTimer);

      //  // Compare the date strored (if any) with the current time
      //  try
      //  {
      //    stored = Convert.ToDateTime(MustayalucaConfig.nextUpdateCheck);
      //  }
      //  catch
      //  {
      //    stored = DateTime.Now;
      //  }

      //  if (stored.CompareTo(DateTime.Now) > 0)
      //  {
      //    //We have a save date for an update check that is in the future, we will use that.....
      //    TimeSpan setStored = stored - DateTime.Now;
      //    smcLog.WriteLog("Stored Timer Set for : " + setStored.ToString(), LogLevel.Debug);
      //    updateChkTimer = new System.Threading.Timer(updateCallback, null, setStored, TimeSpan.FromHours(MustayalucaConfig.hours));
      //  }
      //  else
      //  {
      //    updateChkTimer = new System.Threading.Timer(updateCallback, null, nextCheckAt, TimeSpan.FromHours(MustayalucaConfig.hours));

      //    // The line below is for testing, comment the above line and uncomment this - the update check will fire 30sec after startup
      //    // updateChkTimer = new Timer(updateCallback, null, 30000, 60000);
      //  }
      //}
      #endregion

      #region Init Misc
      //MusicOptionsGUI.SetProperties();
      MiscConfigGUI.SetProperties();
      VideoOptionsGUI.SetProperties();

      if (skInfo.minimiseMPOnExit.ToLower() == "yes")
        MinimiseOnExit = true;

      if (Screen.PrimaryScreen.Bounds.Width == 1920 && Screen.PrimaryScreen.Bounds.Height == 1080)
      {
        smcLog.WriteLog("Set #Mustayaluca.FullHD = true", LogLevel.Debug);
        MustayalucaConfig.SetProperty("#Mustayaluca.FullHD", "true");
      }
      else
      {
        smcLog.WriteLog("Set #Mustayaluca.FullHD = false", LogLevel.Debug);
        MustayalucaConfig.SetProperty("#Mustayaluca.FullHD", "false");
      }

      // Set Artist Path Property used in music overlays
      string artistPath = Path.Combine(Config.GetFolder(Config.Dir.Thumbs), @"Music\Artists");
      smcLog.WriteLog(string.Format("Set #Mustayaluca.ArtistPath = {0}", artistPath), LogLevel.Info);
      MustayalucaConfig.SetProperty("#Mustayaluca.ArtistPath", artistPath);
      #endregion

      #region Init Events
      GUIWindowManager.OnActivateWindow += new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
      GUIGraphicsContext.OnVideoWindowChanged += new VideoWindowChangedHandler(GUIGraphicsContext_OnVideoWindowChanged);
      GUIGraphicsContext.OnNewAction += new OnActionHandler(smpAction);
      SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);

      if (Helper.IsAssemblyAvailable("VideoBackgroundAutoSwitcher", new Version(1, 0, 1, 0)))
      {
        VideoBackgroundAutoSwitcher_installed = true;
        smcLog.WriteLog("VideoBackgroundAutoSwitcher is Installed and enabled, this is not neceassary as the functionality is built into the skin configuration plugin.", LogLevel.Info);
        smcLog.WriteLog("The internal functionality will be disabled - the VideoBackgroundAutoSwitcher can be uninstalled if required", LogLevel.Info);

      }
      else
      {
        smcLog.WriteLog("VideoBackgroundAutoSwitcher : Plugin code started", LogLevel.Info);
        g_Player.PlayBackStarted += new g_Player.StartedHandler(this.OnPlayBackStarted);
        g_Player.PlayBackEnded += new g_Player.EndedHandler(this.OnPlayBackEnded);
      }

      #endregion      
    }

    private void InitTranslations()
    {
      Translation.Init();

      // Set all fixed translation properties 
      // these have defaults defined in the code and are used internally
      try
      {
        foreach (string name in Translation.Strings.Keys)
        {
          if (!string.IsNullOrEmpty(Translation.Strings[name]))
          {
            smcLog.WriteLog("#Mustayaluca." + name + ": " + Translation.Strings[name], LogLevel.Debug);
            SetProperty("#Mustayaluca." + name, Translation.Strings[name]);
          }
        }
      }
      catch (Exception ex)
      {
        smcLog.WriteLog(string.Format("MustayalucaConfig: Translation Exception: {0}", ex.Message), LogLevel.Error);
      }

      // Set user defined property, this is defined in the langauge file as the full 
      // property name including the the # i.e #Mustayaluca.MyDefinedProperty
      foreach (string propName in Translation.FixedTranslations.Keys)
      {
        if (!string.IsNullOrEmpty(propName))
        {
          string propValue;
          Translation.FixedTranslations.TryGetValue(propName, out propValue);
          if (IsInteger(propValue))
          {
            smcLog.WriteLog(string.Format("Converting MediaPortal translation '{0}' to Mustayaluca translation -> {1}", propValue, propName), LogLevel.Debug);
            SetProperty(propName, GUILocalizeStrings.Get(int.Parse(propValue)));
          }
          else
          {
            smcLog.WriteLog(propName + ": " + propValue, LogLevel.Debug);
            SetProperty(propName, propValue);
          }
        }
      }
    }

    private void DeInitMustayaluca()
    {
      smcLog.WriteLog("De-Initializing Mustayaluca");

      GUIWindowManager.OnActivateWindow -= new GUIWindowManager.WindowActivationHandler(GUIWindowManager_OnActivateWindow);
      GUIGraphicsContext.OnVideoWindowChanged -= new VideoWindowChangedHandler(GUIGraphicsContext_OnVideoWindowChanged);
      GUIGraphicsContext.OnNewAction -= new OnActionHandler(smpAction);
      SystemEvents.PowerModeChanged -= new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged);

      if (!VideoBackgroundAutoSwitcher_installed)
      {
        g_Player.PlayBackStarted -= new g_Player.StartedHandler(this.OnPlayBackStarted);
        g_Player.PlayBackEnded -= new g_Player.EndedHandler(this.OnPlayBackEnded);
      }
    }

    void ShowRestartMessage()
    {
      GUIDialogYesNo dlg = (GUIDialogYesNo)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_YES_NO);
      dlg.Reset();
      dlg.SetHeading(Translation.MediaPortalRestart);      
      dlg.SetLine(1, Translation.MediaPortalRestartMessage);      
      dlg.DoModal(GUIWindowManager.ActiveWindow);
      if (dlg.IsConfirmed)
      {
        RestartMediaPortal();
      }
    }

    bool IsInteger(string theValue)
    {
      if (string.IsNullOrEmpty(theValue)) return false;
      Match m = _isNumber.Match(theValue);
      return m.Success;
    }

    #endregion

    #region Public methods

    internal static void SetProperty(string property, string value)
    {
      if (property == null)
        return;

      //// If the value is empty always add a space
      //// otherwise the property will keep 
      //// displaying it's previous value
      if (String.IsNullOrEmpty(value))
        value = " ";

      GUIPropertyManager.SetProperty(property, value);
    }

    //public static DateTime NextAt(TimeSpan time)
    //{
    //  DateTime now = DateTime.Now;
    //  DateTime result = now.Date + time;

    //  return (now <= result) ? result : result.AddDays(1);
    //}

    //public void checkUpdateOnTimer(object source)
    //{
    //  smcLog.WriteLog("Update timer fired - checking for update", LogLevel.Debug);
    //  if (updateCheck.updateAvailable(false))
    //  {
    //    MustayalucaConfig.updateAvailable = true;

    //    // This property controls the visibility of the large update icon on the home screens
    //    MustayalucaConfig.SetProperty("#Mustayaluca.UpdateAvailable", "true");

    //    // This property controls the visibility or the indicator icon displyed next to the clock on the home screens
    //    MustayalucaConfig.SetProperty("#Mustayaluca.ShowUpdateInd", "true");

    //    // This property controls the visibility or the indicator icon that is displayed next to the skin item in the settings screens
    //    MustayalucaConfig.SetProperty("#Mustayaluca.ShowSettingsUpdateInd", "true");
    //  }
    //  else
    //    MustayalucaConfig.updateAvailable = false;
    //}

    //public static TimeSpan nextCheckAt
    //{
    //  get
    //  {
    //    switch (MustayalucaConfig.checkInterval)
    //    {
    //      case 1:
    //        MustayalucaConfig.hours = 24 * 7;
    //        break;
    //      case 2:
    //        MustayalucaConfig.hours = 24 * 14;
    //        break;
    //      case 3:
    //        MustayalucaConfig.hours = 24 * 28;
    //        break;
    //    }
    //    TimeSpan hoursToAdd = TimeSpan.FromHours(MustayalucaConfig.hours);

    //    DateTime nextCheck = NextAt(new TimeSpan(MustayalucaConfig.checkTime.Hour, 0, 0).Add(hoursToAdd));
    //    smcLog.WriteLog("Next update check at : " + nextCheck.ToLongDateString() + ":" + nextCheck.ToLongTimeString(), LogLevel.Info);
    //    MustayalucaConfig.nextUpdateCheck = nextCheck.ToString();
    //    settings.Save(settings.cXMLSectionUpdate);

    //    TimeSpan setTimerFor = (nextCheck - DateTime.Now).Add(hoursToAdd);
    //    smcLog.WriteLog("Timer Set for : " + setTimerFor.ToString(), LogLevel.Info);

    //    return setTimerFor;
    //  }
    //}

    //public static void checkAndCopy(string destinationPath)
    //{
    //  String[] Files;
    //  Files = Directory.GetFileSystemEntries(destinationPath);
    //  foreach (string patchdir in Files)
    //  {
    //    if (patchdir.ToLower().Contains("language"))
    //      copyDirectory(Path.Combine(destinationPath, "language"), SkinInfo.mpPaths.langBasePath);
    //    if (patchdir.ToLower().Contains("skin"))
    //      copyDirectory(Path.Combine(destinationPath, "skin"), SkinInfo.mpPaths.skinBasePath);
    //    if (patchdir.ToLower().Contains("thumbs"))
    //      copyDirectory(Path.Combine(destinationPath, "thumbs"), SkinInfo.mpPaths.thumbsPath);
    //    if (patchdir.ToLower().Contains("database"))
    //      copyDirectory(Path.Combine(destinationPath, "database"), SkinInfo.mpPaths.databasePath);
    //  }
    //}

    //public static void copyDirectory(string patchSource, string patchDestination)
    //{
    //  String[] patchFiles;
    //  CheckSum checkSum = new CheckSum();

    //  if (patchDestination[patchDestination.Length - 1] != Path.DirectorySeparatorChar)
    //    patchDestination += Path.DirectorySeparatorChar;

    //  if (!Directory.Exists(patchDestination))
    //    Directory.CreateDirectory(patchDestination);

    //  patchFiles = Directory.GetFileSystemEntries(patchSource);

    //  foreach (string Element in patchFiles)
    //  {
    //    if (Directory.Exists(Element))
    //      copyDirectory(Element, patchDestination + Path.GetFileName(Element));
    //    else
    //    {
    //      if (Path.GetExtension(patchDestination + Path.GetFileName(Element)).ToLower() == ".xml")        // checkSum on xmlrpc files only
    //        if (checkSum.Compare(patchDestination + Path.GetFileName(Element)))                         // Only copy if checksum exists and matches
    //        {
    //          File.Copy(Element, patchDestination + Path.GetFileName(Element), true);
    //          smcLog.WriteLog("Patching: Copy >" + Element + " to " + patchDestination + Path.GetFileName(Element), LogLevel.Debug);
    //        }
    //    }
    //  }
    //}

    public static void RunPendingRestartChanges()
    {
      if (PendingRestartChanges.Count > 0)
      {
        foreach (var pendingChange in PendingRestartChanges)
        {
          switch (pendingChange)
          {
            case PendingChanges.ClearCache:
              try
              {
                smcLog.WriteLog("Clearing skin cache");
                string path = Path.Combine(SkinInfo.mpPaths.cacheBasePath, "Mustayaluca");
                if (Directory.Exists(path)) Directory.Delete(path, true);
              }
              catch (Exception ex)
              {
                smcLog.WriteLog("Error deleting skin cache: {0}", LogLevel.Error, ex.Message);
              }
              break;
          }
        }
        PendingRestartChanges.Clear();
      }
    }

    public static void RestartMediaPortal()
    {
      string restartExe = Path.Combine(SkinInfo.mpPaths.sMPbaseDir, "SMPMediaPortalRestart.exe");
      ProcessStartInfo processStart = new ProcessStartInfo(restartExe);
      processStart.Arguments = @"/restartmp ";
      processStart.Arguments += smpSettings.mpSetAsFullScreen ? "true" : "false";
      processStart.Arguments += " \"" + Path.Combine(Path.Combine(SkinInfo.mpPaths.Mustayalucapath, "Media"), "splashscreen.png") + "\"";

      smcLog.WriteLog("SMPMediaPortalRestart Parameter: " + processStart.Arguments, LogLevel.Info);

      processStart.WorkingDirectory = Path.GetDirectoryName(restartExe);
      System.Diagnostics.Process.Start(processStart);
      if (smpSettings.mpSetAsFullScreen)
        Thread.Sleep(2000);

      RunPendingRestartChanges();
      
      if (MinimiseOnExit)
        Environment.Exit(0);
      else
      {
        Action exitAction = new Action(Action.ActionType.ACTION_EXIT, 0, 0);
        GUIGraphicsContext.OnAction(exitAction);
      }
    }

    public static void ShowRestartMessage(int windowID, string windowName)
    {
      GUIDialogYesNo dlg = (GUIDialogYesNo)GUIWindowManager.GetWindow((int)GUIWindow.Window.WINDOW_DIALOG_YES_NO);
      dlg.Reset();
      dlg.SetHeading(windowName);
      dlg.SetLine(1, Translation.ConfigRequiresRestartLine1);
      dlg.SetLine(2, Translation.ConfigRequiresRestartLine2);
      dlg.SetLine(3, Translation.ConfigRequiresRestartLine3);
      dlg.DoModal(windowID);

      if (dlg.IsConfirmed)
      {
        RestartMediaPortal();
      }
    }
    
    /// <summary>
    /// Shows Virtual Keyboard
    /// </summary>
    /// <param name="input">Inital Text in Keyboard</param>
    /// <param name="output">Response from User</param>
    /// <returns>true if value has changed</returns>
    public static bool ShowKeyboard(string input, out string output)
    {
      output = input;

      try
      {
        VirtualKeyboard keyboard = (VirtualKeyboard)GUIWindowManager.GetWindow((int)Window.WINDOW_VIRTUAL_KEYBOARD);
        if (null == keyboard)
          return false;

        keyboard.Reset();
        keyboard.Text = input;        
        keyboard.DoModal(GUIWindowManager.ActiveWindow);

        if (keyboard.IsConfirmed)
        {
          output = keyboard.Text;          
          return !string.Equals(input, output, StringComparison.InvariantCultureIgnoreCase);
        }
        else
          return false;
      }
      catch (Exception ex)
      {
        smcLog.WriteLog("Error showing virtual keyboard " + ex.Message, LogLevel.Info);
        return false;
      }
    }

    #endregion

    #region Event Handlers
    private void GUIWindowManager_OnDeActivateWindow(int windowID)
    {
      // Settings/General window
      if (windowID == (int)Window.WINDOW_SETTINGS_SKIN)
      {
        // check if skin changed
        if (settings.CurrentSkin != settings.PreviousSkin && settings.CurrentSkin.EndsWith("Mustayaluca"))
        {
          smcLog.WriteLog("Skin Changed to Mustayaluca from GUI, initializing plugin.", LogLevel.Info);
          InitMustayaluca();          
          settings.PreviousSkin = settings.CurrentSkin;
        }
        else if (settings.CurrentSkin != settings.PreviousSkin && settings.PreviousSkin.EndsWith("Mustayaluca"))
        {
          smcLog.WriteLog("Mustayaluca is no longer default skin, de-initializing plugin.", LogLevel.Info);
          DeInitMustayaluca();
          settings.PreviousSkin = settings.CurrentSkin;
        }

        //check if language changed
        if (settings.CurrentLanguage != settings.PreviousLanguage)
        {
          smcLog.WriteLog(string.Format("Language Changed to '{0}' from GUI, initializing translations.", settings.CurrentLanguage), LogLevel.Info);
          InitTranslations();
        }

      }
    }

    public void smpAction(Action Action)
    {
      switch (Action.wID)
      {
        case Action.ActionType.REMOTE_1:
        case Action.ActionType.REMOTE_2:
        case Action.ActionType.REMOTE_3:
        case Action.ActionType.ACTION_PLAY:
        case Action.ActionType.ACTION_MUSIC_PLAY:          
          break;
          
        case (Action.ActionType)196250:
          smcLog.WriteLog("Restarting MediaPortal");
          ShowRestartMessage();
          break;

        case Action.ActionType.ACTION_SHUTDOWN:
        case Action.ActionType.ACTION_EXIT:
          smcLog.WriteLog("Exiting MediaPortal");
          RunPendingRestartChanges();
          break;
      }
      if (!VideoBackgroundAutoSwitcher_installed)
      {
        if (((Action.wID == MediaPortal.GUI.Library.Action.ActionType.ACTION_SHOW_GUI) || (Action.wID == MediaPortal.GUI.Library.Action.ActionType.ACTION_SHOW_FULLSCREEN)) && GUIGraphicsContext.Vmr9Active)
        {
            if (VideoOptionsGUI.DisableVideoBackground)
            {
                GUIGraphicsContext.ShowBackground = true;
                GUIGraphicsContext.Overlay = true;
            }
            else
            {
                GUIGraphicsContext.ShowBackground = false;
                GUIGraphicsContext.Overlay = false;
            }
        }
      }

    }

    private void GUIGraphicsContext_OnVideoWindowChanged()
    {
      if (GUIWindowManager.ActiveWindow == (int)MediaPortalWindows.Home)
        return;

      //smcLog.WriteLog("MustayalucaConfig: OnVideoWindowsChanged Event Called", LogLevel.Debug);

      if (GUIGraphicsContext.ShowBackground == true)
      {
        //smcLog.WriteLog("#Mustayaluca.VideoWallpaper = false", LogLevel.Debug);
        GUIPropertyManager.SetProperty("#Mustayaluca.VideoWallpaper", "false");
      }
      else
      {
        //smcLog.WriteLog("#Mustayaluca.VideoWallpaper = true", LogLevel.Debug);
        GUIPropertyManager.SetProperty("#Mustayaluca.VideoWallpaper", "true");
      }
    }

    private void GUIWindowManager_OnActivateWindow(int windowID)
    {
      if (GUIWindowManager.ActiveWindow == (int)MediaPortalWindows.Home)
        return;

      //if (MustayalucaConfig.updateAvailable)
      //{
      //  // If we move off the basichome or Std menu turn off the the Update Available property so the main fade icon
      //  // is not displayed again
      //  // When leave the menu also turn off the update indicator displyed next to the clock as this may cause issues in other screens. 
      //  if (windowID != (int)MediaPortalWindows.BasicHome && windowID != (int)MediaPortalWindows.Home)
      //  {
      //    SetProperty("#Mustayaluca.UpdateAvailable", "false");
      //    SetProperty("#Mustayaluca.ShowUpdateInd", "false");
      //  }
      //  else
      //    SetProperty("#Mustayaluca.ShowUpdateInd", "true");
      //}
      //else
      {
        //smcLog.WriteLog("MustayalucaConfig: Setting all Update Indicator Values to false", LogLevel.Debug);
        SetProperty("#Mustayaluca.UpdateAvailable", "false");
        SetProperty("#Mustayaluca.ShowUpdateInd", "false");
        SetProperty("#Mustayaluca.ShowSettingsUpdateInd", "false");
      }
    }

    void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
    {
      if (e.Mode == PowerModes.Resume)
      {
        smcLog.WriteLog("Mustayaluca is Resuming from Standby", LogLevel.Info);
        
        // check for updates if enabled
        //if (MustayalucaConfig.checkOnStart)
        //{
        //  if (updateCheck.updateAvailable(false))
        //  {
        //    MustayalucaConfig.updateAvailable = true;

        //    // This property controls the visibility of the large update icon on the home screens
        //    SetProperty("#Mustayaluca.UpdateAvailable", "true");

        //    // This property controls the visibility or the indicator icon displyed next to the clock on the home screens
        //    SetProperty("#Mustayaluca.ShowUpdateInd", "true");

        //    // This property controls the visibility or the indicator icon that is displayed next to the skin item in the settings screens
        //    SetProperty("#Mustayaluca.ShowSettingsUpdateInd", "true");
        //  }
        //}
      }
      else if (e.Mode == PowerModes.Suspend)
      {
        smcLog.WriteLog("Mustayaluca is entering Standby", LogLevel.Info);
      }
    }

    private void OnPlayBackEnded(g_Player.MediaType type, string filename)
    {
      if ((type.Equals(g_Player.MediaType.Video) || type.Equals(g_Player.MediaType.TV)) || type.Equals(g_Player.MediaType.Recording))
      {
        GUIGraphicsContext.ShowBackground = true;
        GUIGraphicsContext.Overlay = true;
      }
    }

    private void OnPlayBackStarted(g_Player.MediaType type, string filename)
    {
      if ((type.Equals(g_Player.MediaType.Video) || type.Equals(g_Player.MediaType.TV)) || type.Equals(g_Player.MediaType.Recording))
      {
          if (VideoOptionsGUI.DisableVideoBackground)
          {
              GUIGraphicsContext.ShowBackground = true;
              GUIGraphicsContext.Overlay = true;
          }
          else
          {
              GUIGraphicsContext.ShowBackground = false;
              GUIGraphicsContext.Overlay = false;
          }
      }
    }



    #endregion

    #region ISetupForm Members
    /// <summary>
    /// With GetID it will be an window-plugin / otherwise a process-plugin
    /// Enter the id number here again
    /// </summary>
    public override int GetID
    {
      get { return GetWindowId(); }
      set { }
    }

    /// <summary>
    /// Returns the name of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the name of the plugin which is shown in the plugin menu</returns>
    public string PluginName()
    {
      return "Mustayaluca Configuration";
    }

    /// <summary>
    /// Returns the description of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the description of the plugin which is shown in the plugin menu</returns>
    public string Description()
    {
      return "Mustayaluca configuration control";
    }

    /// <summary>
    /// Returns the author of the plugin which is shown in the plugin menu
    /// </summary>
    /// <returns>the author of the plugin which is shown in the plugin menu</returns>
    public string Author()
    {
      return "Mustayaluca Team";
    }

    /// <summary>
    /// Indicates whether plugin can be enabled/disabled
    /// </summary>
    public void ShowPlugin()
    {
      ConfigurationForm configurationForm = new ConfigurationForm();
      configurationForm.ShowDialog();
    }

    /// <summary>
    /// Indicates whether plugin can be enabled/disabled
    /// </summary>
    /// <returns>true if the plugin can be enabled/disabled</returns>
    public bool CanEnable()
    {
      return false;
    }

    /// <summary>
    /// Get Windows-ID
    /// </summary>
    /// <returns>unique id for this plugin</returns>
    public int GetWindowId()
    {
      // WindowID of windowplugin belonging to this setup
      // enter your own unique code
      return (int)SMPScreenID.SMPMenu;
    }

    /// <summary>
    /// Indicates if plugin is enabled by default
    /// </summary>
    /// <returns>true if this plugin is enabled by default</returns>
    public bool DefaultEnabled()
    {
      return true;
    }

    /// <summary>
    /// indicates if a plugin has its own setup screen
    /// </summary>
    /// <returns>true if the plugin has its own setup screen</returns>
    public bool HasSetup()
    {
      return false;
    }

    /// <summary>
    /// no Home for this plugin, return false
    /// </summary>
    /// <param name="strButtonText"></param>
    /// <param name="strButtonImage"></param>
    /// <param name="strButtonImageFocus"></param>
    /// <param name="strPictureImage"></param>
    /// <returns></returns>
    public bool GetHome(out string strButtonText, out string strButtonImage,
                        out string strButtonImageFocus, out string strPictureImage)
    {
      strButtonText = String.Empty;
      strButtonImage = String.Empty;
      strButtonImageFocus = String.Empty;
      strPictureImage = String.Empty;
      return false;
    }
    #endregion
  }

}