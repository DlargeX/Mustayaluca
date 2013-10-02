
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
    public const string cXMLSectionTV = "TVConfig";
    public const string cXMLSectionMisc = "MiscConfig";
    public const string cXMLSectionVideo = "VideoConfig";
    
    private const string cXMLSectionEditorOptions = "Mustayaluca Options";
    private const string cXMLSectionEditorItems = "Mustayaluca Menu Items";
    #endregion

    #region XML Configuration Strings
    public const string cXMLSettingTVGuideSize = "tvGuideSize";	
/*     public const string cXMLSettingTVMiniGuideSize = "tvMiniGuideSize"; */

    public const string cXMLSettingDisableVideoBackground = "videoDisableVideoBackground";

    public const string cXMLSettingMiscTextColor = "miscTextColor";
    public const string cXMLSettingMiscTextColor2 = "miscTextColor2";
    public const string cXMLSettingMiscTextColor3 = "miscTextColor3";
    public const string cXMLSettingMiscWatchedColor = "miscWatchedColor";
    public const string cXMLSettingMiscRemoteColor = "miscRemoteColor";
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

          #region TV
          case settings.cXMLSectionTV:
            TVConfig.TVGuideRowSize = (TVConfig.TVGuideRows)xmlreader.GetValueAsInt(section, settings.cXMLSettingTVGuideSize, 10);
            //TVConfig.TVMiniGuideRowSize = (TVConfig.TVMiniGuideRows)xmlreader.GetValueAsInt(section, settings.cXMLSettingTVMiniGuideSize, 7);
            break;
          #endregion

          #region Video
          case settings.cXMLSectionVideo:
            VideoOptionsGUI.DisableVideoBackground = xmlreader.GetValueAsInt(section, settings.cXMLSettingDisableVideoBackground, 1) == 1;
            break;
          #endregion

          #region Misc
          case settings.cXMLSectionMisc:
            MiscConfigGUI.TextColor = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscTextColor, "FFFFFF");
            MiscConfigGUI.TextColor2 = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscTextColor2, "FFFFFF");
            MiscConfigGUI.TextColor3 = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscTextColor3, "FFFFFF");
            MiscConfigGUI.WatchedColor = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscWatchedColor, "a0ff75");
            MiscConfigGUI.RemoteColor = xmlreader.GetValueAsString(section, settings.cXMLSettingMiscRemoteColor, "f7c3a8");
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

          #region TV
          case settings.cXMLSectionTV:
            xmlwriter.SetValue(section, settings.cXMLSettingTVGuideSize, (int)TVConfig.TVGuideRowSize);
            //xmlwriter.SetValue(section, settings.cXMLSettingTVMiniGuideSize, (int)TVConfig.TVMiniGuideRowSize);
            break;
          #endregion

          #region Video
          case settings.cXMLSectionVideo:       
            xmlwriter.SetValue(section, settings.cXMLSettingDisableVideoBackground, VideoOptionsGUI.DisableVideoBackground ? 1 : 0);          
            break;
          #endregion

          #region Misc
          case settings.cXMLSectionMisc:
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
