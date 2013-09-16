
using System;
using System.Collections.Generic;
using System.Globalization;
using MediaPortal.Configuration;
using MediaPortal.GUI.Library;
using MediaPortal.Util;
using System.Xml;
using System.IO;

namespace MustayalucaConfig
{
  class settings
  {
    #region Private Variables
    private static readonly logger smcLog = logger.GetInstance();
    #endregion

    #region XML Configuration Sections
    public const string cXMLSectionTVSeries = "TVSeriesConfig";
    public const string cXMLSectionMovingPictures = "MovingPicturesConfig";
    public const string cXMLSectionTV = "TVConfig";
    public const string cXMLSectionMusic = "MusicConfig";
    public const string cXMLSectionMisc = "MiscConfig";
    public const string cXMLSectionVideo = "VideoConfig";
    public const string cXMLSectionUpdate = "UpdateConfig";
    
    private const string cXMLSectionEditorOptions = "Mustayaluca Options";
    private const string cXMLSectionEditorItems = "Mustayaluca Menu Items";

    private const string cXMLMostRecentTVSeriesVal = "tvSeries";
    private const string cXMLMostRecentMoviesVal = "movies";

    private const string cXMLMostRecentMenuItems = "menuitem{0}showMostRecent";
    private const string cXMLMenuItemControlID = "menuitem{0}id";

    #endregion

    #region XML Configuration Strings
    public const string cXMLSettingUpdateCheckOnStart = "updateCheckOnStart";
    public const string cXMLSettingUpdateCheckForUpdateAt = "updateCheckForUpdateAt";
    public const string cXMLSettingUpdateCheckInterval = "updateCheckInterval";
    public const string cXMLSettingUpdateCheckTime = "updateCheckTime";
    public const string cXMLSettingUpdateRunPatchUtilityUnattended = "updateRunPatchUtilityUnattended";
    public const string cXMLSettingUpdateNextUpdateCheck = "updateNextUpdateCheck";
    public const string cXMLSettingUpdatePatchUtilityRestartMP = "updatePatchUtilityRestartMP";
    public const string cXMLSettingUpdatePatchAppliedLastRun = "updatePatchAppliedLastRun";

    public const string cXMLSettingMusicCdCoverOnly = "musicCdCoverOnly";
    public const string cXMLSettingMusicShowEqGraphic = "musicShowEqGraphic";
    public const string cXMLSettingNowPlayingStyle = "musicNowPlayingStyle";

    public const string cXMLSettingTVSeriesDefaultStyle = "tvseriesDefaultStyle";
    public const string cXMLSettingTVSeriesWideBannerMod = "tvseriesWideBannerMod";
    public const string cXMLSettingTVSeriesShowTotalEpisodeCounts = "tvseriesShowTotalEpisodeCounts";
    public const string cXMLSettingTVSeriesShowIconsInListView = "tvseriesShowIconsInListView";

    public const string cXMLSettingMovingPicturesDefaultStyle = "movingpicturesDefaultStyle";
    public const string cXMLSettingMovingPicturesThumbMod = "movingpicturesThumbMod";

    public const string cXMLSettingTVGuideSize = "tvGuideSize";
    public const string cXMLSettingTVMiniGuideSize = "tvMiniGuideSize";
    public const string cXMLSettingTVEnableRandomTVSeriesFanart = "tvEnableRandomTVSeriesFanart";

    public const string cXMLSettingVideoFullVideoOSD = "videoFullVideoOSD";
    public const string cXMLSettingDisableVideoBackground = "videoDisableVideoBackground";

    public const string cXMLSettingMiscShowHiddenMenuImage = "miscShowHiddenMenuImage";
    public const string cXMLSettingMiscShowRoundedCovers = "miscShowRoundedCovers";
    public const string cXMLSettingMiscShowIconsInArtwork = "miscShowIconsInArtwork";
    public const string cXMLSettingMiscEnablePlayMostRecents = "miscEnablePlayMostRecents";
    public const string cXMLSettingMiscFilterWatchedInRecentlyAdded = "MiscFilterWatchedInRecentlyAdded";
    public const string cXMLSettingMiscMostRecentFanartTimerInt = "miscMostRecentFanartTimerInt";
    public const string cXMLSettingMiscUseLargeFonts = "miscUseLargeFonts";
    public const string cXMLSettingMiscUnfocusedAlphaListItems = "miscUnfocusedAlphaListItems";
    public const string cXMLSettingMiscUnfocusedAlphaThumbs = "miscUnfocusedAlphaThumbs";
    public const string cXMLSettingMiscTextColor = "miscTextColor";
    public const string cXMLSettingMiscTextColor2 = "miscTextColor2";
    public const string cXMLSettingMiscTextColor3 = "miscTextColor3";
    public const string cXMLSettingMiscWatchedColor = "miscWatchedColor";
    public const string cXMLSettingMiscRemoteColor = "miscRemoteColor";

    public const string cXMLSettingMovPicsRecentAdded = "movPicsMostRecent";
    public const string cXMLSettingMovPicsRecentWatched = "mrMovPicsWatched";
    public const string cXMLSettingTVSeriesRecentAdded = "tvSeriesMostRecent";
    public const string cXMLSettingTVSeriesRecentWatched = "mrTVSeriesWatched";
    #endregion

    #region Public Properties

    public bool logLevelError
    {
      get
      {
        if (_logLevel() >= 0)
          return true;
        else
          return false;
      }
    }

    public bool logLevelWarning
    {
      get
      {
        if (_logLevel() >= 1)
          return true;
        else
          return false;
      }
    }

    public bool logLevelDebug
    {
      get
      {
        if (_logLevel() == 3)
          return true;
        else
          return false;
      }
    }

    public bool isVisEnabled 
    {
      get
      {
        return _isVisEnabled();
      }
    }

    public bool mrSeasonEpisodeStyle2
    {
      get
      {
        return _mrSeasonEpisodeStyle2();
      }
    }

    public bool mpSetAsFullScreen
    {
      get
      {
        return _mpSetAsFullScreen();
      }
    }

    public static string CurrentSkin { get { return GUIGraphicsContext.Skin; } }
    public static string PreviousSkin { get; set; }

    public static string CurrentLanguage
    {
      get
      {
        string language = string.Empty;
        try
        {
          language = GUILocalizeStrings.GetCultureName(GUILocalizeStrings.CurrentLanguage());
        }
        catch (Exception)
        {
          language = CultureInfo.CurrentUICulture.Name;
        }
        return language;
      }
    }
    public static string PreviousLanguage { get; set; }

    #endregion

    #region Public methods   

    public static void Load(string section)
    {
      smcLog.WriteLog(string.Format("MustayalucaConfig: Settings.Load({0})", section), LogLevel.Info);
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MustayalucaConfig.xml")))
      {
        // Read user settings from configuration file
        switch (section)
        {
          #region Update
          case settings.cXMLSectionUpdate:           
            //if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdateCheckOnStart, 1) != 0)
            //  MustayalucaConfig.checkOnStart = true;
            //if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdateCheckForUpdateAt, 1) != 0)
            //  MustayalucaConfig.checkForUpdateAt = true;
            //if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdateRunPatchUtilityUnattended, 1) != 0)
            //  MustayalucaConfig.patchUtilityRunUnattended = true;
            //if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdatePatchUtilityRestartMP, 1) != 0)
            //  MustayalucaConfig.patchUtilityRestartMP = true;
            //if (xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdatePatchAppliedLastRun, 0) != 0)
            //  MustayalucaConfig.patchAppliedLastRun = true;

            //if (MustayalucaConfig.checkForUpdateAt)
            //{
            //  string checkTime = xmlreader.GetValueAsString(section, settings.cXMLSettingUpdateCheckTime, "");

            //  if (string.IsNullOrEmpty(checkTime))
            //  {
            //    MustayalucaConfig.checkTime = DateTime.Parse("03:00");
            //    MustayalucaConfig.checkInterval = 1;
            //  }
            //  else
            //  {
            //    DateTime dtCheckTime = new DateTime();
            //    if (!DateTime.TryParse(checkTime, CultureInfo.CurrentCulture, DateTimeStyles.None, out dtCheckTime))
            //      dtCheckTime = DateTime.Now;
            //    MustayalucaConfig.checkTime = dtCheckTime;

            //    MustayalucaConfig.nextUpdateCheck = xmlreader.GetValueAsString(section, settings.cXMLSettingUpdateNextUpdateCheck, "");
            //    MustayalucaConfig.checkInterval = xmlreader.GetValueAsInt(section, settings.cXMLSettingUpdateCheckInterval, 1);
            //  }
            //}
            break;
          #endregion

          #region Music
          case settings.cXMLSectionMusic:
            //if (xmlreader.GetValueAsInt(section, settings.cXMLSettingMusicCdCoverOnly, 1) != 0)
            //  MusicOptionsGUI.cdCoverOnly = true;
            //if (xmlreader.GetValueAsInt(section, settings.cXMLSettingMusicShowEqGraphic, 1) != 0)
            //  MusicOptionsGUI.showEqGraphic = true;
            break;
          #endregion

          #region TVSeries
          case settings.cXMLSectionTVSeries:
            //TVSeriesConfig.IsDefaultStyle = xmlreader.GetValueAsInt(section, settings.cXMLSettingTVSeriesDefaultStyle, 1) == 1;
            //TVSeriesConfig.WideBannerMod = (TVSeriesConfig.WideBanners)xmlreader.GetValueAsInt(section, settings.cXMLSettingTVSeriesWideBannerMod, 0);
            break;
          #endregion

          #region MovingPictures
          case settings.cXMLSectionMovingPictures:
            //MovingPicturesConfig.IsDefaultStyle = xmlreader.GetValueAsInt(section, settings.cXMLSettingMovingPicturesDefaultStyle, 1) == 1;
            //MovingPicturesConfig.ThumbnailMod = (MovingPicturesConfig.Thumbnails)xmlreader.GetValueAsInt(section, settings.cXMLSettingMovingPicturesThumbMod, 0);
            break;
          #endregion

          #region TV
          case settings.cXMLSectionTV:
            TVConfig.TVGuideRowSize = (TVConfig.TVGuideRows)xmlreader.GetValueAsInt(section, settings.cXMLSettingTVGuideSize, 10);
            //TVConfig.TVMiniGuideRowSize = (TVConfig.TVMiniGuideRows)xmlreader.GetValueAsInt(section, settings.cXMLSettingTVMiniGuideSize, 7);
            //TVConfig.EnableRandomTVSeriesFanart = xmlreader.GetValueAsInt(section, settings.cXMLSettingTVEnableRandomTVSeriesFanart, 0) == 1;
            break;
          #endregion

          #region Video
          case settings.cXMLSectionVideo:
            //VideoOptionsGUI.FullVideoOSD = xmlreader.GetValueAsInt(section, settings.cXMLSettingVideoFullVideoOSD, 1) == 1;
            VideoOptionsGUI.DisableVideoBackground = xmlreader.GetValueAsInt(section, settings.cXMLSettingDisableVideoBackground, 1) == 1;
            break;
          #endregion

          #region Misc
          case settings.cXMLSectionMisc:
            MiscConfigGUI.ShowHiddenMenuImage = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscShowHiddenMenuImage, 1) == 1;
            MiscConfigGUI.ShowRoundedImages = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscShowRoundedCovers, 1) == 1;
            MiscConfigGUI.ShowIconsInArtwork = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscShowIconsInArtwork, 1) == 1;
            MiscConfigGUI.EnablePlayMostRecents = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscEnablePlayMostRecents, 1) == 1;
            MiscConfigGUI.FilterWatchedInRecentlyAdded = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscFilterWatchedInRecentlyAdded, 0) == 1;
            MiscConfigGUI.TextColor = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscTextColor, "FFFFFF");
            MiscConfigGUI.TextColor2 = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscTextColor2, "FFFFFF");
            MiscConfigGUI.TextColor3 = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscTextColor3, "FFFFFF");
            MiscConfigGUI.WatchedColor = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscWatchedColor, "a0ff75");
            MiscConfigGUI.RemoteColor = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscRemoteColor, "f7c3a8");
            MiscConfigGUI.UnfocusedAlphaListItems = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscUnfocusedAlphaListItems, 100);
            MiscConfigGUI.UnfocusedAlphaThumbs = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscUnfocusedAlphaThumbs, 100);
            MiscConfigGUI.MostRecentFanartTimerInt = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscMostRecentFanartTimerInt, 7);
            MiscConfigGUI.UseLargeFonts = xmlreader.GetValueAsInt(section, settings.cXMLSettingMiscUseLargeFonts, 0) == 1;
            break;
          #endregion

        }
      }      
    }   

    public static void Save(string section)
    {
      smcLog.WriteLog(string.Format("Mustayaluca: Settings.Save({0})", section), LogLevel.Info);
      using (MediaPortal.Profile.Settings xmlwriter = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MustayalucaConfig.xml")))
      {
        switch (section)
        {
          #region Update
          case settings.cXMLSectionUpdate:
            //xmlwriter.SetValue(section, settings.cXMLSettingUpdateCheckOnStart, MustayalucaConfig.checkOnStart ? 1 : 0);
            //xmlwriter.SetValue(section, settings.cXMLSettingUpdateCheckForUpdateAt, MustayalucaConfig.checkForUpdateAt ? 1 : 0);
            //xmlwriter.SetValue(section, settings.cXMLSettingUpdateCheckInterval, MustayalucaConfig.checkInterval);
            //xmlwriter.SetValue(section, settings.cXMLSettingUpdateCheckTime, MustayalucaConfig.checkTime.ToShortTimeString());
            //xmlwriter.SetValue(section, settings.cXMLSettingUpdateNextUpdateCheck, MustayalucaConfig.nextUpdateCheck);
            //xmlwriter.SetValue(section, settings.cXMLSettingUpdateRunPatchUtilityUnattended, MustayalucaConfig.patchUtilityRunUnattended ? 1 : 0);
            //xmlwriter.SetValue(section, settings.cXMLSettingUpdatePatchUtilityRestartMP, MustayalucaConfig.patchUtilityRestartMP ? 1 : 0);
            //xmlwriter.SetValue(section, settings.cXMLSettingUpdatePatchAppliedLastRun, MustayalucaConfig.patchAppliedLastRun ? 1 : 0);
            break;
          #endregion

          #region Music
          case settings.cXMLSectionMusic:
            //xmlwriter.SetValue(section, settings.cXMLSettingMusicCdCoverOnly, MusicOptionsGUI.cdCoverOnly ? 1 : 0);
            //xmlwriter.SetValue(section, settings.cXMLSettingMusicShowEqGraphic, MusicOptionsGUI.showEqGraphic ? 1 : 0);
            break;
          #endregion

          #region TVSeries
          case cXMLSectionTVSeries:
            //xmlwriter.SetValue(section, settings.cXMLSettingTVSeriesDefaultStyle, TVSeriesConfig.IsDefaultStyle ? 1 : 0);
            //xmlwriter.SetValue(section, settings.cXMLSettingTVSeriesWideBannerMod, (int)TVSeriesConfig.WideBannerMod);
            break;
          #endregion

          #region MovingPictures
          case cXMLSectionMovingPictures:
            //xmlwriter.SetValue(section, settings.cXMLSettingMovingPicturesDefaultStyle, MovingPicturesConfig.IsDefaultStyle ? 1 : 0);
            //xmlwriter.SetValue(section, settings.cXMLSettingMovingPicturesThumbMod, (int)MovingPicturesConfig.ThumbnailMod);
            break;
          #endregion

          #region TV
          case settings.cXMLSectionTV:
            xmlwriter.SetValue(section, settings.cXMLSettingTVGuideSize, (int)TVConfig.TVGuideRowSize);
            //xmlwriter.SetValue(section, settings.cXMLSettingTVMiniGuideSize, (int)TVConfig.TVMiniGuideRowSize);
            //xmlwriter.SetValue(section, settings.cXMLSettingTVEnableRandomTVSeriesFanart, TVConfig.EnableRandomTVSeriesFanart ? 1 : 0);
            break;
          #endregion

          #region Video
          case settings.cXMLSectionVideo:
            //xmlwriter.SetValue(section, settings.cXMLSettingVideoFullVideoOSD, VideoOptionsGUI.FullVideoOSD ? 1 : 0);          
            xmlwriter.SetValue(section, settings.cXMLSettingDisableVideoBackground, VideoOptionsGUI.DisableVideoBackground ? 1 : 0);          
            break;
          #endregion

          #region Misc
          case settings.cXMLSectionMisc:
            xmlwriter.SetValue(section, settings.cXMLSettingMiscShowHiddenMenuImage, MiscConfigGUI.ShowHiddenMenuImage ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscShowRoundedCovers, MiscConfigGUI.ShowRoundedImages ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscShowIconsInArtwork, MiscConfigGUI.ShowIconsInArtwork ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscEnablePlayMostRecents, MiscConfigGUI.EnablePlayMostRecents ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscFilterWatchedInRecentlyAdded, MiscConfigGUI.FilterWatchedInRecentlyAdded ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscMostRecentFanartTimerInt, MiscConfigGUI.MostRecentFanartTimerInt);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscUnfocusedAlphaListItems, MiscConfigGUI.UnfocusedAlphaListItems);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscUnfocusedAlphaThumbs, MiscConfigGUI.UnfocusedAlphaThumbs);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscUseLargeFonts, MiscConfigGUI.UseLargeFonts ? 1 : 0);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscTextColor, MiscConfigGUI.TextColor);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscTextColor2, MiscConfigGUI.TextColor2);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscTextColor3, MiscConfigGUI.TextColor3);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscRemoteColor, MiscConfigGUI.RemoteColor);
            xmlwriter.SetValue(section, settings.cXMLSettingMiscWatchedColor, MiscConfigGUI.WatchedColor);
            break;
          #endregion

        }
        MediaPortal.Profile.Settings.SaveCache();
      }
    }

    #endregion

    #region Private Methods

    bool _isVisEnabled()
    {
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        return xmlreader.GetValueAsBool("musicmisc", "showVisInNowPlaying", false);
      }
    }

    bool _mpSetAsFullScreen()
    {
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        return xmlreader.GetValueAsBool("general", "startfullscreen", false);
      }
    }

    int _logLevel()
    {
      using (MediaPortal.Profile.Settings xmlreader = new MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "MediaPortal.xml")))
      {
        return xmlreader.GetValueAsInt("general", "loglevel", 0);
      }
    }

    bool _mrSeasonEpisodeStyle2()
    {
      string optionsTag = "Mustayaluca Options";
      XmlDocument doc = new XmlDocument();

      if (openmustayalucamenuprofile(doc))
      {

        XmlNodeList nodelist = doc.DocumentElement.SelectNodes("/profile/skin");
        try
        {
          return bool.Parse(readEntryValue(optionsTag, "mrSeriesEpisodeFormat", nodelist));
        }
        catch
        {
          smcLog.WriteLog("MustayalucaConfig: Option mrSeriesEpisodeFormat not present",LogLevel.Error);
          return false;
        }
      }
      return false;
    }

    bool _timerRequired()
    {
      string optionsTag = "Mustayaluca Options";
      XmlDocument doc = new XmlDocument();

      if (openmustayalucamenuprofile(doc))
      {

        XmlNodeList nodelist = doc.DocumentElement.SelectNodes("/profile/skin");
        try
        {
          return bool.Parse(readEntryValue(optionsTag, "mostRecentCycleFanart", nodelist));
        }
        catch
        {
          smcLog.WriteLog("MustayalucaConfig: Option mostRecentCycleFanart not present",LogLevel.Error);
          return false;
        }
      }
      return false;
    }

    bool openmustayalucamenuprofile(XmlDocument doc)
    {
      string mustayalucamenuprofile = SkinInfo.mpPaths.configBasePath + "mustayalucamenuprofile.xml";
      //
      // Open the usermenu settings file - NOTE: need to check for it in correct location, if not found look in skin dir for default version
      //
      if (!File.Exists(mustayalucamenuprofile))
      {
        // Ok, so no mustayalucamenuprofile.xml exists, this is most likely because this is a new skin install and this is the first time
        // the editor has been run and the file has not yet been created in the default location.
        // Check for and load the default version supplied with the skin
        mustayalucamenuprofile = SkinInfo.mpPaths.Mustayalucapath + "mustayalucamenuprofile.xml";
        if (!File.Exists(mustayalucamenuprofile))
        {
          //ok, so now really in trouble, throw an error to the user and bailout!
          smcLog.WriteLog("Can't find mustayalucamenuprofile.xml \r\r" + SkinInfo.mpPaths.configBasePath + "mustayalucamenuprofile.xml",LogLevel.Error);
          return false;
        }
      }

      try
      {
        doc.Load(mustayalucamenuprofile);
        return true;
      }
      catch (Exception e)
      {
        smcLog.WriteLog("Exception while loading mustayalucamenuprofile.xml\n\n" + e.Message,LogLevel.Error);
        return false;
      }
    }

    private string readEntryValue(string section, string elementName, XmlNodeList unodeList)
    {
      string entryValue;

      foreach (XmlNode node in unodeList)
      {
        XmlNode innerNode = node.Attributes.GetNamedItem("name");
        if (innerNode.InnerText == "Mustayaluca")
        {
          XmlNodeList skinNodeList = node.SelectNodes("section");
          foreach (XmlNode skinNode in skinNodeList)
          {
            XmlNode skinNodeSection = skinNode.Attributes.GetNamedItem("name");
            if (skinNodeSection.InnerText == section)
            {
              XmlNode path = skinNode.SelectSingleNode("entry[@name=\"" + elementName + "\"]");
              if (path != null)
              {
                entryValue = path.InnerText;
                return entryValue;
              }
            }
          }
        }
      }
      return "false";
    }

    #endregion

  }
}
