namespace MustayalucaEditor
{
  public partial class formMustayalucaEditor
  {
    public int baseXPosAdded;
    public int baseYPosAdded;
    public int baseXPosWatched;
    public int baseYPosWatched;

    #region Main

    void generateMostRecentOverlay(chosenMenuStyle menuStyle, isOverlayType isOverlay, int xPosAdded, int yPosAdded, int xPosWatched, int yPosWatched)
    {
      baseXPosAdded = xPosAdded;
      baseYPosAdded = yPosAdded;
      baseXPosWatched = xPosWatched;
      baseYPosWatched = yPosWatched;

      switch (isOverlay)
      {
        case isOverlayType.TVSeries:
          MostRecentTVSeriesSummary();
          break;
        case isOverlayType.MovPics:
          MostRecentMovingPicturesSummary();
          break;
        case isOverlayType.Music:
          LastAddedMusicSummary();
          break;
        case isOverlayType.Pictures:
          MostRecentlyAddedPictures();
          writeXMLFile("basichome.recentlyadded.Pictures.Summary.xml");
          break;
        case isOverlayType.RecordedTV:
          MostRecentRecordedTVSummary();
          writeXMLFile("basichome.recentlyadded.RecordedTV.Summary.xml");
          break;
        case isOverlayType.freeDriveSpace:
          driveFreeSpaceOverlay();
          writeXMLFile("basichome.FreeDriveSpace.Overlay.xml");
          break;
        case isOverlayType.sleepControl:
          sleepControlOverlay();
          writeXMLFile("basichome.SleepControl.Overlay.xml");
          break;
        case isOverlayType.stocks:
          stocksOverlay();
          writeXMLFile("basichome.Stocks.Overlay.xml");
          break;
        case isOverlayType.powerControl:
          powerControlOverlay();
          writeXMLFile("basichome.PowerControl.Overlay.xml");
          break;
        case isOverlayType.htpcInfo:
          htpcInfoOverlay();
          writeXMLFile("basichome.HTPCInfo.Overlay.xml");
          break;
        case isOverlayType.updateControl:
          updateControlOverlay();
          writeXMLFile("basichome.UpdateControl.Overlay.xml");
          break;
        default:
          break;
      }
    }

    #endregion

    #region TVSeries Horizontal Summary

    void MostRecentTVSeriesSummary()
    {
      buildTVSeriesSummaryFile(formMustayalucaEditor.mrTVSeriesSummStyle);

      if (formMustayalucaEditor.mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.plot)
        writeXMLFile("basichome.recentlyadded.tvseries.HSum1.xml");

      if (formMustayalucaEditor.mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
        writeXMLFile("basichome.recentlyadded.tvseries.HSum2.xml");
    }

    #endregion

    #region TVSeries Summary Builder

    void buildTVSeriesSummaryFile(mostRecentTVSeriesSummaryStyle sumStyle)
    {

    #region Summary Style (Fanart)

      if (sumStyle == mostRecentTVSeriesSummaryStyle.fanart)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: Recently Added Series (Fanart)</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "|control.hasfocus(91919994)|control.hasfocus(91919995)|control.hasfocus(91919996)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "<control>" +
                      "<description>Recent Series 1 Background</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                      "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                      "<width>254</width>" +
                      "<height>203</height>" +
                      "<texture>recently_added_series.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 1 Fanart</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvseries.latest1.fanart</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 1 Overlay</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                      "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                      "<width>254</width>" +
                      "<height>203</height>" +
                      "<texture>recently_added_series_shine.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 1 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<label>#latestMediaHandler.tvseries.latest1.serieName</label>" +
                      "<font>font5</font>" +
                      "<textcolor>ffcdcccc</textcolor>" +
                      "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                      "<scrollWrapString> | </scrollWrapString>" +
                      "<visible>!control.hasfocus(91919994)</visible>" +
                      "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                      "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 1 Info</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<label>S#latestMediaHandler.tvseries.latest1.seasonIndexE#latestMediaHandler.tvseries.latest1.episodeIndex - #latestMediaHandler.tvseries.latest1.episodeName</label>" +
                      "<font>font5</font>" +
                      "<textcolor>ffcdcccc</textcolor>" +
                      "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                      "<scrollWrapString> | </scrollWrapString>" +
                      "<visible>control.hasfocus(91919994)</visible>" +
                      "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                      "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 1 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919994</id>" +
                      "<posX>" + (baseXPosAdded + 70).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                      "<width>113</width>" +
                      "<height>113</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml +=  "<onup>91919994</onup>";
            else
              xml +=  "<onup>" + (tvSeriesMenuID + 600).ToString() + "01</onup>";

            xml +=    "<ondown>" + (tvSeriesMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919995</onright>" +
                      "<onleft>91919996</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 2 Background</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 257).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                      "<width>254</width>" +
                      "<height>203</height>" +
                      "<texture>recently_added_series.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 2 Fanart</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 282).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvseries.latest2.fanart</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 2 Overlay</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 254).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                      "<width>254</width>" +
                      "<height>203</height>" +
                      "<texture>recently_added_series_shine.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 2 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 283).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<label>#latestMediaHandler.tvseries.latest2.serieName</label>" +
                      "<font>font5</font>" +
                      "<textcolor>ffcdcccc</textcolor>" +
                      "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                      "<scrollWrapString> | </scrollWrapString>" +
                      "<visible>!control.hasfocus(91919995)</visible>" +
                      "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                      "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 2 Info</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 283).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<label>S#latestMediaHandler.tvseries.latest1.seasonIndexE#latestMediaHandler.tvseries.latest1.episodeIndex - #latestMediaHandler.tvseries.latest1.episodeName</label>" +
                      "<font>font5</font>" +
                      "<textcolor>ffcdcccc</textcolor>" +
                      "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                      "<scrollWrapString> | </scrollWrapString>" +
                      "<visible>control.hasfocus(91919995)</visible>" +
                      "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                      "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Series 2 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919995</id>" +
                      "<posX>" + (baseXPosAdded + 324).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                      "<width>113</width>" +
                      "<height>113</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml +=  "<onup>91919995</onup>";
            else
              xml +=  "<onup>" + (tvSeriesMenuID + 600).ToString() + "01</onup>";

            xml += "<ondown>" + (tvSeriesMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919996</onright>" +
                    "<onleft>91919994</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Series 3 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 508).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Series 3 Fanart</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 536).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>#latestMediaHandler.tvseries.latest3.fanart</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Series 3 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 508).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Series 3 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 537).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.tvseries.latest3.serieName</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>!control.hasfocus(91919996)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Series 3 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 537).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>S#latestMediaHandler.tvseries.latest3.seasonIndexE#latestMediaHandler.tvseries.latest3.episodeIndex	 - #latestMediaHandler.tvseries.latest3.episodeName</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919996)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Series 3 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919996</id>" +
                    "<posX>" + (baseXPosAdded + 578).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>113</width>" +
                    "<height>113</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml +=  "<onup>91919996</onup>";
            else
              xml +=  "<onup>" + (tvSeriesMenuID + 600).ToString() + "01</onup>";

              xml +=  "<ondown>" + (tvSeriesMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919994</onright>" +
                      "<onleft>91919995</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

    #endregion

    #region Summary Style (Plot)


      if (sumStyle == mostRecentTVSeriesSummaryStyle.plot)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                  "<description>GROUP: Recently Added Episodes (Plot)</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                   "<visible>" + mostRecentVisibleControls(isOverlayType.TVSeries) + "|control.hasfocus(91919994)|control.hasfocus(91919995)|control.hasfocus(91919996)</visible>" +
                   "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                   "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "<!-- Background -->" +
                    "<control>" +
                      "<description>Recent Episodes BG</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded).ToString() + "</posY>" +
                      "<width>680</width>" +
                      "<height>298</height>" +
                      "<texture>recently_added_episode_background.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Episodes Seperator</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 199).ToString() + "</posY>" +
                      "<width>620</width>" +
                      "<height>2</height>" +
                      "<texture>recent_added_seperator.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Episodes 1 Fanart</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvseries.latest1.fanart</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Episodes 2 Fanart</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 240).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvseries.latest2.fanart</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Episodes 3 Fanart</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 450).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvseries.latest3.fanart</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<!-- Info when no episode thumb button has focus -->" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recently Added Episodes Label</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 172).ToString() + "</posY>" +
                      "<width>620</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#SkinTranslation.Translations.recentlyAdded.Episodes.Label</label>" +
                      "<visible>!control.hasfocus(91919994)+!control.hasfocus(91919995)+!control.hasfocus(91919996)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Episodes 1 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 205).ToString() + "</posY>" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>(S#latestMediaHandler.tvseries.latest1.seasonIndexE#latestMediaHandler.tvseries.latest1.episodeIndex) #latestMediaHandler.tvseries.latest1.serieName - #latestMediaHandler.tvseries.latest1.episodeName</label>" +
                      "<visible>!control.hasfocus(91919994)+!control.hasfocus(91919995)+!control.hasfocus(91919996)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Episodes 2 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 227).ToString() + "</posY>" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>(S#latestMediaHandler.tvseries.latest2.seasonIndexE#latestMediaHandler.tvseries.latest2.episodeIndex) #latestMediaHandler.tvseries.latest2.serieName - #latestMediaHandler.tvseries.latest2.episodeName</label>" +
                      "<visible>!control.hasfocus(91919994)+!control.hasfocus(91919995)+!control.hasfocus(91919996)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Episodes 3 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 249).ToString() + "</posY>" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>(S#latestMediaHandler.tvseries.latest3.seasonIndexE#latestMediaHandler.tvseries.latest3.episodeIndex) #latestMediaHandler.tvseries.latest3.serieName - #latestMediaHandler.tvseries.latest3.episodeName</label>" +
                      "<visible>!control.hasfocus(91919994)+!control.hasfocus(91919995)+!control.hasfocus(91919996)</visible>" +
                    "</control>" +
                    "<!-- Info when recent episode 1 button has focus -->" +
                    "<control>" +
                      "<description>Recent Episodes 1 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvseries.latest1.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Episodes 1 Title</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 149).ToString() + "</posY>" +
                      "<width>620</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.tvseries.latest1.serieName</label>" +
                      "<visible>control.hasfocus(91919994)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Episodes 1 Details</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 174).ToString() + "</posY>" +
                      "<width>620</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>S#latestMediaHandler.tvseries.latest1.seasonIndexE#latestMediaHandler.tvseries.latest1.episodeIndex - #latestMediaHandler.tvseries.latest1.episodeName</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<visible>control.hasfocus(91919994)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Episodes 1 Plot</description>" +
                      "<type>textboxscrollup</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 204).ToString() + "</posY>" +
                      "<width>620</width>" +
                      "<height>65</height>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.tvseries.latest1.plot</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<seperator>---------------------------------------------------------------------------------------------------------</seperator>" +
                      "<visible>control.hasfocus(91919994)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Episodes 1 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919994</id>" +
                      "<posX>" + (baseXPosAdded + 74).ToString() + "</posX>" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                      "<width>112</width>" +
                      "<height>112</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml +=  "<onup>91919994</onup>";
            else
              xml +=  "<onup>" + (tvSeriesMenuID + 600).ToString() + "01</onup>";

            xml += "<ondown>" + (tvSeriesMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919995</onright>" +
                    "<onleft>91919996</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<!-- Info when recent episode 2 button has focus -->" +
                  "<control>" +
                    "<description>Recent Episodes 2 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 240).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>#latestMediaHandler.tvseries.latest2.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control Style=\"NoShadow\">" +
                    "<description>Recent Episodes 2 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 149).ToString() + "</posY>" +
                    "<width>620</width>" +
                    "<font>font17</font>" +
                    "<textcolor>ff474747</textcolor>" +
                    "<label>#latestMediaHandler.tvseries.latest2.serieName</label>" +
                    "<visible>control.hasfocus(91919995)</visible>" +
                  "</control>" +
                  "<control Style=\"NoShadow\">" +
                    "<description>Recent Episodes 2 Details</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 174).ToString() + "</posY>" +
                    "<width>620</width>" +
                    "<font>font5</font>" +
                    "<textcolor>ff7a7a7a</textcolor>" +
                    "<label>S#latestMediaHandler.tvseries.latest2.seasonIndexE#latestMediaHandler.tvseries.latest2.episodeIndex - #latestMediaHandler.tvseries.latest2.episodeName</label>" +
                    "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                    "<visible>control.hasfocus(91919995)</visible>" +
                  "</control>" +
                  "<control Style=\"NoShadow\">" +
                    "<description>Recent Episodes 2 Plot</description>" +
                    "<type>textboxscrollup</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 204).ToString() + "</posY>" +
                    "<width>620</width>" +
                    "<height>65</height>" +
                    "<font>font5</font>" +
                    "<textcolor>ff7a7a7a</textcolor>" +
                    "<label>#latestMediaHandler.tvseries.latest2.plot</label>" +
                    "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                    "<seperator>---------------------------------------------------------------------------------------------------------</seperator>" +
                    "<visible>control.hasfocus(91919995)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Episodes 2 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919995</id>" +
                    "<posX>" + (baseXPosAdded + 284).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                    "<width>112</width>" +
                    "<height>112</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml +=  "<onup>91919995</onup>";
            else
              xml +=  "<onup>" + (tvSeriesMenuID + 600).ToString() + "01</onup>";

            xml += "<ondown>" + (tvSeriesMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919996</onright>" +
                    "<onleft>91919994</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<!-- Info when recent episode 3 button has focus -->" +
                  "<control>" +
                    "<description>Recent Episodes 3 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 450).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>#latestMediaHandler.tvseries.latest3.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control Style=\"NoShadow\">" +
                    "<description>Recent Episodes 3 Title</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 149).ToString() + "</posY>" +
                    "<width>620</width>" +
                    "<font>font17</font>" +
                    "<textcolor>ff474747</textcolor>" +
                    "<label>#latestMediaHandler.tvseries.latest3.serieName</label>" +
                    "<visible>control.hasfocus(91919996)</visible>" +
                  "</control>" +
                  "<control Style=\"NoShadow\">" +
                    "<description>Recent Episodes 3 Details</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 174).ToString() + "</posY>" +
                    "<width>620</width>" +
                    "<font>font5</font>" +
                    "<textcolor>ff7a7a7a</textcolor>" +
                    "<label>S#latestMediaHandler.tvseries.latest3.seasonIndexE#latestMediaHandler.tvseries.latest3.episodeIndex - #latestMediaHandler.tvseries.latest3.episodeName</label>" +
                    "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                    "<visible>control.hasfocus(91919996)</visible>" +
                  "</control>" +
                  "<control Style=\"NoShadow\">" +
                    "<description>Recent Episodes 3 Plot</description>" +
                    "<type>textboxscrollup</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 204).ToString() + "</posY>" +
                    "<width>620</width>" +
                    "<height>65</height>" +
                    "<font>font5</font>" +
                    "<textcolor>ff7a7a7a</textcolor>" +
                    "<label>#latestMediaHandler.tvseries.latest3.plot</label>" +
                    "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                    "<seperator>---------------------------------------------------------------------------------------------------------</seperator>" +
                    "<visible>control.hasfocus(91919996)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Episodes 3 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919996</id>" +
                    "<posX>" + (baseXPosAdded + 484).ToString() + "</posX>" +
                    "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>" +
                    "<width>112</width>" +
                    "<height>112</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
             if (cbDisableExitMenu.Checked)
              xml +=  "<onup>91919996</onup>";
            else
              xml +=  "<onup>" + (tvSeriesMenuID + 600).ToString() + "01</onup>";

              xml +=  "<ondown>" + (tvSeriesMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919994</onright>" +
                      "<onleft>91919995</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

      #endregion

    }

    #endregion

    #region Music Last 3 Added Summary

    void LastAddedMusicSummary()
    {
      buildMostRecentMusicSummary(formMustayalucaEditor.mrMusicStyle);

      if (formMustayalucaEditor.mrMusicStyle == musicMostRecentStyle.style1)
        writeXMLFile("basichome.recentlyadded.Music.Summary1.xml");

      if (formMustayalucaEditor.mrMusicStyle == musicMostRecentStyle.style2)
        writeXMLFile("basichome.recentlyadded.Music.Summary2.xml");
    }

    #endregion

    #region MovingPictures Horizontal Summary

    void MostRecentMovingPicturesSummary()
    {
      buildMovingPicturesSummaryFile(formMustayalucaEditor.mrMovPicsSummStyle);

      if (formMustayalucaEditor.mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.plot)
        writeXMLFile("basichome.recentlyadded.movpics.HSum1.xml");

      if (formMustayalucaEditor.mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
        writeXMLFile("basichome.recentlyadded.movpics.HSum2.xml");

    }

    #endregion

    #region MovingPictures Summary Builder

    void buildMovingPicturesSummaryFile(mostRecentMovPicsSummaryStyle sumStyle)
    {

      #region Summary Styles (Fanart)

      if (sumStyle == mostRecentMovPicsSummaryStyle.fanart)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: Recently Added Movies</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "|control.hasfocus(91919991)|control.hasfocus(91919992)|control.hasfocus(91919993)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "<control>" +
                    "<description>Recent movies 1 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>290</height>" +
                    "<texture>recently_added_movies.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 1 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 28).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 28).ToString() + "</posY>\n" +
                    "<width>136</width>" +
                    "<height>200</height>" +
                    "<texture>#latestMediaHandler.movingpicture.latest1.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 1 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>290</height>" +
                    "<texture>recently_added_movies_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 1 Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 44).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 231).ToString() + "</posY>\n" +
                    "<width>104</width>" +
                    "<height>22</height>" +
                    "<texture>logos\\rating\\#latestMediaHandler.movingpicture.latest1.rating.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<visible>!control.hasfocus(91919991)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 1 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 27).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 231).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.movingpicture.latest1.year - #latestMediaHandler.movingpicture.latest1.classification\r#latestMediaHandler.movingpicture.latest1.genre\r#latestMediaHandler.movingpicture.latest1.runtime</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919991)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 1 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919991</id>" +
                    "<posX>" + (baseXPosAdded + 28).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 60).ToString() + "</posY>\n" +
                    "<width>136</width>" +
                    "<height>136</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
          if (cbDisableExitMenu.Checked)
            xml +=  "<onup>91919991</onup>";
          else
            xml +=  "<onup>" + (MovingPicturesMenuID + 600).ToString() + "01</onup>";

            xml +=  "<ondown>" + (MovingPicturesMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919992</onright>" +
                    "<onleft>91919993</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 2 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 192).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>290</height>" +
                    "<texture>recently_added_movies.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 2 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 218).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 28).ToString() + "</posY>\n" +
                    "<width>136</width>" +
                    "<height>200</height>" +
                    "<texture>#latestMediaHandler.movingpicture.latest2.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 2 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 192).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>290</height>" +
                    "<texture>recently_added_movies_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 2 Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 236).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 231).ToString() + "</posY>\n" +
                    "<width>104</width>" +
                    "<height>22</height>" +
                    "<texture>logos\\rating\\#latestMediaHandler.movingpicture.latest2.rating.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<visible>!control.hasfocus(91919992)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 2 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 219).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 231).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.movingpicture.latest2.year - #latestMediaHandler.movingpicture.latest2.classification\r#latestMediaHandler.movingpicture.latest2.genre\r#latestMediaHandler.movingpicture.latest2.runtime</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919992)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 2 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919992</id>" +
                    "<posX>" + (baseXPosAdded + 218).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 60).ToString() + "</posY>\n" +
                    "<width>136</width>" +
                    "<height>136</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
          if (cbDisableExitMenu.Checked)
            xml +=  "<onup>91919992</onup>";
          else
            xml +=  "<onup>" + (MovingPicturesMenuID + 600).ToString() + "01</onup>";

            xml +=  "<ondown>" + (MovingPicturesMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919993</onright>" +
                    "<onleft>91919991</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 3 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 384).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>290</height>" +
                    "<texture>recently_added_movies.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 3 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 410).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 28).ToString() + "</posY>\n" +
                    "<width>136</width>" +
                    "<height>200</height>" +
                    "<texture>#latestMediaHandler.movingpicture.latest3.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 3 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 384).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>290</height>" +
                    "<texture>recently_added_movies_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 3 Rating</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 428).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 231).ToString() + "</posY>\n" +
                    "<width>104</width>" +
                    "<height>22</height>" +
                    "<texture>logos\\rating\\#latestMediaHandler.movingpicture.latest3.rating.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<visible>!control.hasfocus(91919993)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 3 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 410).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 231).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.movingpicture.latest3.year - #latestMediaHandler.movingpicture.latest3.classification\r#latestMediaHandler.movingpicture.latest3.genre\r#latestMediaHandler.movingpicture.latest3.runtime</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919993)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent movies 3 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919993</id>" +
                    "<posX>" + (baseXPosAdded + 410).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 60).ToString() + "</posY>\n" +
                    "<width>136</width>" +
                    "<height>136</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
          if (cbDisableExitMenu.Checked)
            xml +=  "<onup>91919993</onup>";
          else
            xml +=  "<onup>" + (MovingPicturesMenuID + 600).ToString() + "01</onup>";

            xml +=  "<ondown>" + (MovingPicturesMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919991</onright>" +
                    "<onleft>91919992</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                "</control>" +
                "</controls>" +
              "</window>";
      }

      #endregion

      #region Summary Styles (Plot)


      if (sumStyle == mostRecentMovPicsSummaryStyle.plot)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: Recently Added Movies</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.MovPics) + "|control.hasfocus(91919991)|control.hasfocus(91919992)|control.hasfocus(91919993)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "<!-- Background -->" +
                    "<control>" +
                      "<description>Recent Movies BG</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                      "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                      "<width>530</width>" +
                      "<height>420</height>" +
                      "<texture>recently_added_poster_background.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Movies Rating BG</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 428).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 250).ToString() + "</posY>\n" +
                      "<width>60</width>" +
                      "<height>22</height>" +
                      "<texture>recent_added_rating.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                      "<visible>control.hasfocus(91919991)|control.hasfocus(91919992)|control.hasfocus(91919993)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Movies Seperator</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 301).ToString() + "</posY>\n" +
                      "<width>448</width>" +
                      "<height>2</height>" +
                      "<texture>recent_added_seperator.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<!-- Info when no movie thumb button has focus -->" +
                      "<control Style=\"NoShadow\">" +
                      "<description>Recently Added Movies Label</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 271).ToString() + "</posY>\n" +
                      "<width>440</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#SkinTranslation.Translations.recentlyAdded.Movies.Label</label>" +
                      "<visible>!control.hasfocus(91919991)+!control.hasfocus(91919992)+!control.hasfocus(91919993)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 1 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 311).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>440</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest1.title (#latestMediaHandler.movingpicture.latest1.year)</label>" +
                      "<visible>!control.hasfocus(91919991)+!control.hasfocus(91919992)+!control.hasfocus(91919993)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 2 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 336).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>440</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest2.title (#latestMediaHandler.movingpicture.latest2.year)</label>" +
                      "<visible>!control.hasfocus(91919991)+!control.hasfocus(91919992)+!control.hasfocus(91919993)</visible>" +
                    "</control>" +
                      "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 3 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 361).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>440</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest3.title (#latestMediaHandler.movingpicture.latest3.year)</label>" +
                      "<visible>!control.hasfocus(91919991)+!control.hasfocus(91919992)+!control.hasfocus(91919993)</visible>" +
                    "</control>" +
                    "<!-- Info when recent movie 1 button has focus -->" +
                    "<control>" +
                      "<description>Recent Movies 1 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 41).ToString() + "</posY>\n" +
                      "<width>136</width>" +
                      "<height>200</height>" +
                      "<texture>#latestMediaHandler.movingpicture.latest1.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 1 Title</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 250).ToString() + "</posY>\n" +
                      "<width>380</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest1.title</label>" +
                      "<visible>control.hasfocus(91919991)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 1 Rating</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 458).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 250).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest1.rating</label>" +
                      "<visible>control.hasfocus(91919991)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 1 Details</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 274).ToString() + "</posY>\n" +
                      "<width>390</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>(#latestMediaHandler.movingpicture.latest1.classification) - #latestMediaHandler.movingpicture.latest1.runtime - #latestMediaHandler.movingpicture.latest1.genre</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<visible>control.hasfocus(91919991)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 1 Plot</description>" +
                      "<type>textboxscrollup</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 306).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<height>85</height>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest1.plot</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<seperator>-----------------------------------------------------------------------------------</seperator>" +
                      "<visible>control.hasfocus(91919991)</visible>" +
                    "</control>" +
                    "<control>" +
                    "<description>Recent movies 1 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919991</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 73).ToString() + "</posY>\n" +
                      "<width>136</width>" +
                      "<height>136</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml +=  "<onup>91919991</onup>";
            else
              xml +=  "<onup>" + (MovingPicturesMenuID + 600).ToString() + "01</onup>";

              xml +=  "<ondown>" + (MovingPicturesMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919992</onright>" +
                      "<onleft>91919993</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                    "<!-- Info when recent movie 2 button has focus -->" +
                    "<control>" +
                      "<description>Recent movies 2 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 197).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 41).ToString() + "</posY>\n" +
                      "<width>136</width>" +
                      "<height>200</height>" +
                      "<texture>#latestMediaHandler.movingpicture.latest2.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 2 Title</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 250).ToString() + "</posY>\n" +
                      "<width>380</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest2.title</label>" +
                      "<visible>control.hasfocus(91919992)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 2 Rating</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 458).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 250).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest2.rating</label>" +
                      "<visible>control.hasfocus(91919992)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 2 Details</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 274).ToString() + "</posY>\n" +
                      "<width>390</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>(#latestMediaHandler.movingpicture.latest2.classification) - #latestMediaHandler.movingpicture.latest2.runtime - #latestMediaHandler.movingpicture.latest2.genre</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<visible>control.hasfocus(91919992)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 2 Plot</description>" +
                      "<type>textboxscrollup</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 306).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<height>85</height>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest2.plot</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<seperator>-----------------------------------------------------------------------------------</seperator>" +
                      "<visible>control.hasfocus(91919992)</visible>" +
                    "</control>" +
                    "<control>" +
                    "<description>Recent movies 2 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919992</id>" +
                      "<posX>" + (baseXPosAdded + 197).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 73).ToString() + "</posY>\n" +
                      "<width>136</width>" +
                      "<height>136</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
              if (cbDisableExitMenu.Checked)
                xml += "<onup>91919992</onup>";
              else
                xml += "<onup>" + (MovingPicturesMenuID + 600).ToString() + "01</onup>";

              xml += "<ondown>" + (MovingPicturesMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919993</onright>" +
                      "<onleft>91919991</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                    "<!-- Info when recent movie 3 button has focus -->" +
                    "<control>" +
                      "<description>Recent movies 3 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 353).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 41).ToString() + "</posY>\n" +
                      "<width>136</width>" +
                      "<height>200</height>" +
                      "<texture>#latestMediaHandler.movingpicture.latest3.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 3 Title</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 250).ToString() + "</posY>\n" +
                      "<width>380</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest3.title</label>" +
                      "<visible>control.hasfocus(91919993)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 3 Rating</description>" +
                      "<type>label</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 458).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 250).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest3.rating</label>" +
                      "<visible>control.hasfocus(91919993)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 3 Details</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 274).ToString() + "</posY>\n" +
                      "<width>390</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>(#latestMediaHandler.movingpicture.latest3.classification) - #latestMediaHandler.movingpicture.latest3.runtime - #latestMediaHandler.movingpicture.latest3.genre</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<visible>control.hasfocus(91919993)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Movies 3 Plot</description>" +
                      "<type>textboxscrollup</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 41).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 306).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<height>85</height>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.movingpicture.latest3.plot</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<seperator>-----------------------------------------------------------------------------------</seperator>" +
                      "<visible>control.hasfocus(91919993)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent movies 3 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919993</id>" +
                      "<posX>" + (baseXPosAdded + 353).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 73).ToString() + "</posY>\n" +
                      "<width>136</width>" +
                      "<height>136</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
              if (cbDisableExitMenu.Checked)
                xml += "<onup>91919993</onup>";
              else
                xml += "<onup>" + (MovingPicturesMenuID + 600).ToString() + "01</onup>";

              xml += "<ondown>" + (MovingPicturesMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919991</onright>" +
                      "<onleft>91919992</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
}

      #endregion
    }

    #endregion

    #region MovingPictures Most Recent Watched

    void mostRecentMoviesWatched()
    {
      string fanartProperty = "#Mustayaluca.recentlyWatched.movie1.fanart";
      string fadelabelControl = "fadelabel";
      string mediaControl = string.Empty;
      string alignTxt = "right";

      string mrMovieTitleFont = movPicsOptions.MovieTitleFont;
      string mrMovieDetailFont = movPicsOptions.MovieDetailFont; bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;
      int xPos = baseXPosWatched + 20; ;

      if (menuStyle == chosenMenuStyle.verticalStyle)
        mediaControl = "![player.hasmedia]+";

      if (movPicsOptions.DisableFadeLabels)
        fadelabelControl = "label";

      xml += "<control>\n" +
                  "<description>GROUP: RecentlyWatched Movie 1</description>\n" +
                  "<type>group</type>\n" +
                  "<dimColor>0xffffffff</dimColor>\n" +
                  "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#Mustayaluca.recentlyWatched.movie1.show,true)</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
                "<description>Movie 1 BG</description>\n" +
                  "<posX>" + baseXPosWatched.ToString() + "</posX>\n" +
                  "<posY>" + baseYPosWatched.ToString() + "</posY>\n" +
                  "<type>image</type>\n" +
                  "<id>0</id>\n" +
                  "<width>306</width>\n" +
                  "<height>320</height>\n" +
                  "<texture>recentsummoverlaybg.png</texture>\n" +
                  "<shouldCache>true</shouldCache>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
                "</control>\n" +
                "<control>\n" +
                  "<description>Header label</description>\n" +
                  "<type>label</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosWatched + 20).ToString() + "</posY>\n" +
                  "<width>258</width>\n" +
                  "<label>#Mustayaluca.RecentlyWatched</label>\n" +
                  "<font>mediastream10tc</font>\n" +
                  "<textcolor>White</textcolor>\n" +
                "</control>      " +
                "<control>\n" +
                  "<description>Movie 1 Title</description>\n" +
                  "<type>" + fadelabelControl + "</type>\n" +
                  "<id>0</id>\n" +
                  "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
                  "<posY>" + (baseYPosWatched + 195).ToString() + "</posY>\n" +
                  "<width>258</width>\n" +
                  "<label>#Mustayaluca.recentlyWatched.movie1.title</label>\n" +
                  "<textcolor>White</textcolor>\n" +
                  "<font>" + mrMovieTitleFont + "</font>" +
                  "<scrollStartDelaySec>20</scrollStartDelaySec>" +
                "</control>\n";

      if (!movPicsOptions.HideRuntime)
      {
        xml += "<control>\n" +
          "<description>Movie 1 Runtime</description>\n" +
          "<type>label</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
          "<width>257</width>\n" +
          "<label>#Mustayaluca.recentlyWatched.movie1.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 204;
      }
      else
      {
        alignTxt = "left";
        xPos -= 5;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 1 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
          "<width>100</width>\n" +
          "<label>#Mustayaluca.recentlyWatched.movie1.certification</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
          "<align>" + alignTxt + "</align>\n" +
          "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
          "<wrapString> |  </wrapString>\n" +
        "</control>";
      }

      if (!movPicsOptions.HideRating)
      {
        if (movPicsOptions.UseTextRating)
        {
          xml += "<control>" +
            "<description>Movie 1 Star Rating Text</description>" +
            "<type>label</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 215).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<align>right</align>" +
            "<label>#Mustayaluca.recentlyWatched.movie1.actualscore/10</label>" +
          "</control>";
        }
        else
        {
          xml += "<control>" +
            "<description>Movie 1 Star Rating Image</description>" +
            "<type>image</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 215).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<texture>star#Mustayaluca.recentlyWatched.movie1.score.png</texture>" +
            "<shouldCache>true</shouldCache>" +
          "</control>";
        }
      }

      xPos = baseXPosWatched + 19;
      xml += "<control>\n" +
              "<description>Movie 1 thumb/fanart</description>\n" +
              "<type>image</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 42).ToString() + "</posY>\n" +
              "<width>268</width>\n" +
              "<height>151</height>\n" +
              "<keepaspectratio>true</keepaspectratio>\n" +
              "<texture>" + fanartProperty + "</texture>\n" +
              "<shouldCache>true</shouldCache>\n" +
            "</control>\n" +
          "</control>\n" +
          "<control>\n" +
            "<description>GROUP: recentlyWatched Movie 2</description>\n" +
            "<type>group</type>\n" +
            "<dimColor>0xffffffff</dimColor>\n" +
            "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#Mustayaluca.recentlyWatched.movie2.show,true)</visible>" +
            "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
            "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
            "<description>Movie 2 Title</description>\n" +
              "<type>" + fadelabelControl + "</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 230).ToString() + "</posY>\n" +
              "<width>258</width>\n" +
              "<label>#Mustayaluca.recentlyWatched.movie2.title</label>\n" +
              "<font>" + mrMovieTitleFont + "</font>" +
              "<textcolor>White</textcolor>\n" +
              "<scrollStartDelaySec>20</scrollStartDelaySec>" +
            "</control>\n";

      if (!movPicsOptions.HideRuntime)
      {
        xml += "<control>\n" +
          "<description>Movie 2 Runtime</description>\n" +
          "<type>label</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
          "<width>257</width>\n" +
          "<label>#Mustayaluca.recentlyWatched.movie2.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 204;
      }
      else
      {
        xPos -= 5;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 2 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
          "<width>100</width>\n" +
          "<label>#Mustayaluca.recentlyWatched.movie2.certification</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
          "<align>" + alignTxt + "</align>\n" +
          "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
          "<wrapString> |  </wrapString>\n" +
        "</control>";
      }

      if (!movPicsOptions.HideRating)
      {
        if (movPicsOptions.UseTextRating)
        {
          xml += "<control>" +
            "<description>Movie 2 Star Rating Text</description>" +
            "<type>label</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<align>right</align>" +
            "<label>#Mustayaluca.recentlyWatched.movie2.actualscore/10</label>" +
          "</control>";
        }
        else
        {
          xml += "<control>" +
            "<description>Movie 2 Star Rating Image</description>" +
            "<type>image</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 251).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<texture>star#Mustayaluca.recentlyWatched.movie2.score.png</texture>" +
            "<shouldCache>true</shouldCache>" +
          "</control>";
        }
      }

      xPos = baseXPosWatched + 19;
      xml += "</control>\n" +
      "<control>\n" +
        "<description>GROUP: RecentlyAdded Movie 3</description>\n" +
        "<type>group</type>\n" +
        "<dimColor>0xffffffff</dimColor>\n" +
        "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.MovPics) + "+string.equals(#Mustayaluca.recentlyWatched.movie3.show,true)</visible>" +
        "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
        "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
          "<type>" + fadelabelControl + "</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 265).ToString() + "</posY>\n" +
          "<width>258</width>\n" +
          "<label>#Mustayaluca.recentlyWatched.movie3.title</label>\n" +
          "<textcolor>White</textcolor>\n" +
          "<font>" + mrMovieTitleFont + "</font>" +
          "<scrollStartDelaySec>20</scrollStartDelaySec>" +
        "</control>\n";


      if (!movPicsOptions.HideRuntime)
      {
        xml += "<control>\n" +
          "<description>Movie 3 Runtime</description>\n" +
          "<type>label</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
          "<width>257</width>\n" +
          "<label>#Mustayaluca.recentlyWatched.movie3.runtime</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
        "</control>";
        xPos = baseXPosWatched + 204;
      }
      else
      {
        xPos -= 5;
      }

      if (!movPicsOptions.HideCertification)
      {
        xml += "<control>\n" +
          "<description>Movie 3 Certification</description>\n" +
          "<type>fadelabel</type>\n" +
          "<id>0</id>\n" +
          "<posX>" + xPos.ToString() + "</posX>\n" +
          "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
          "<width>100</width>\n" +
          "<label>#Mustayaluca.recentlyWatched.movie3.certification</label>\n" +
          "<font>" + mrMovieDetailFont + "</font>" +
          "<textcolor>White</textcolor>\n" +
          "<align>" + alignTxt + "</align>\n" +
          "<scrollStartDelaySec>30</scrollStartDelaySec>\n" +
          "<wrapString> |  </wrapString>\n" +
        "</control>";
      }

      if (!movPicsOptions.HideRating)
      {
        if (movPicsOptions.UseTextRating)
        {
          xml += "<control>" +
            "<description>Movie 3 Star Rating Text</description>" +
            "<type>label</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 284).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<font>" + mrMovieDetailFont + "</font>" +
            "<align>right</align>" +
            "<label>#Mustayaluca.recentlyWatched.movie3.actualscore/10</label>" +
          "</control>";
        }
        else
        {
          xml += "<control>" +
            "<description>Movie 3 Star Rating Image</description>" +
            "<type>image</type>" +
            "<id>0</id>" +
            "<posX>" + (baseXPosWatched + 214).ToString() + "</posX>\n" +
            "<posY>" + (baseYPosWatched + 285).ToString() + "</posY>\n" +
            "<width>70</width>" +
            "<height>13</height>" +
            "<texture>star#Mustayaluca.recentlyWatched.movie3.score.png</texture>" +
            "<shouldCache>true</shouldCache>" +
          "</control>";
        }
      }

      xml += "</control>\n" +
    "</controls>\n" +
  "</window>";
    }

    #endregion

    #region TVSeries Most Recent Watched

    void mostRecentTVSeriesWatched()
    {
      string fanartProperty = "#Mustayaluca.MostRecentImageFanartWatched";
      string fadeLabelControl = "fadelabel";
      string mediaControl = string.Empty;

      string mrSeriesNameFont = tvSeriesOptions.mrSeriesFont;
      string mrEpisodeFont = tvSeriesOptions.mrEpisodeFont;
      bool mrSeriesTitleLast = tvSeriesOptions.mrTitleLast;

      if (menuStyle == chosenMenuStyle.verticalStyle)
        mediaControl = "[!player.hasmedia]+";

      if (tvSeriesOptions.mrDisableFadeLabels)
        fadeLabelControl = "label";

      if (!mostRecentTVSeriesCycleFanart)
        fanartProperty = "#Mustayaluca.recentlyWatched.series1.fanart";

      xml += "<control>\n" +
                "<description>GROUP: RecentlyWatched Series</description>\n" +
                "<type>group</type>\n" +
                "<dimColor>0xffffffff</dimColor>\n" +
                "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#Mustayaluca.recentlyWatched.series1.show,true)</visible>" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
              "<description>Series 1 BG</description>\n" +
              "<posX>" + baseXPosWatched.ToString() + "</posX>\n" +
              "<posY>" + baseYPosWatched.ToString() + "</posY>\n" +
              "<type>image</type>\n" +
              "<id>0</id>\n" +
              "<width>306</width>\n" +
              "<height>320</height>\n" +
              "<texture>recentsummoverlaybg.png</texture>\n" +
              "<shouldCache>true</shouldCache>" +
              "<colordiffuse>EEFFFFFF</colordiffuse>\n" +
            "</control>\n" +
            "<control>\n" +
              "<description>Header label</description>\n" +
              "<type>label</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 20).ToString() + "</posY>\n" +
              "<width>258</width>\n" +
              "<label>#Mustayaluca.RecentlyWatched</label>\n" +
              "<font>mediastream10tc</font>\n" +
              "<textcolor>White</textcolor>\n" +
            "</control>      " +
            "<control>\n" +
              "<description>Series 1 name</description>\n" +
              "<type>" + fadeLabelControl + "</type>\n" +
              "<id>0</id>\n" +
              "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
              "<posY>" + (baseYPosWatched + 195).ToString() + "</posY>\n" +
              "<width>258</width>\n" +
              "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#Mustayaluca.MostRecent.Watched.1.SEFormat - #Mustayaluca.recentlyWatched.series1.title</label>\n";
      else
        xml += "<label>#Mustayaluca.recentlyWatched.series1.title - #Mustayaluca.MostRecent.Watched.1.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 1 title</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 212).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#Mustayaluca.recentlyWatched.series1.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>" +
    "<control>\n" +
      "<description>Series 1 thumb/fanart</description>\n" +
      "<type>image</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 42).ToString() + "</posY>\n" +
      "<width>268</width>\n" +
      "<height>151</height>\n" +
      "<keepaspectratio>true</keepaspectratio>\n" +
      "<texture>" + fanartProperty + "</texture>\n" +
      "<shouldCache>true</shouldCache>\n" +
    "</control>\n" +
  "</control>\n" +
  "<control>\n" +
    "<description>GROUP: RecentlyWatched Series</description>\n" +
    "<type>group</type>\n" +
    "<dimColor>0xffffffff</dimColor>\n" +
    "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#Mustayaluca.recentlyWatched.series2.show,true)</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
      "<description>Series 2 name</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 230).ToString() + "</posY>\n" +
      "<width>258</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#Mustayaluca.MostRecent.Watched.2.SEFormat - #Mustayaluca.recentlyWatched.series2.title</label>\n";
      else
        xml += "<label>#Mustayaluca.recentlyWatched.series2.title - #Mustayaluca.MostRecent.Watched.2.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 2 title</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 247).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#Mustayaluca.recentlyWatched.series2.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
  "</control>\n" +
  "<control>\n" +
    "<description>GROUP: RecentlyWatched Series</description>\n" +
    "<type>group</type>\n" +
    "<dimColor>0xffffffff</dimColor>\n" +
    "<visible>" + mediaControl + mostRecentVisibleControls(isOverlayType.TVSeries) + "+string.equals(#Mustayaluca.recentlyWatched.series3.show,true)</visible>" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "250" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " delay=" + quote + "700" + quote + " time=" + quote + "500" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                  "<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "100" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>\n";
      if (baseXPosWatched > 640)
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
               "<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      else
      {
        xml += "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-300,0" + quote + " time=" + quote + "1500" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Hidden</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-300,0" + quote + " end=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " acceleration=" + quote + "-0.1" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " start=" + quote + "-400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>\n" +
                "<animation effect=" + quote + "slide" + quote + " end=" + quote + "-400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>\n";
      }
      xml += "<control>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 265).ToString() + "</posY>\n" +
      "<width>258</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>";
      if (mrSeriesTitleLast)
        xml += "<label>#Mustayaluca.MostRecent.Watched.3.SEFormat - #Mustayaluca.recentlyWatched.series3.title</label>\n";
      else
        xml += "<label>#Mustayaluca.recentlyWatched.series3.title - #Mustayaluca.MostRecent.Watched.3.SEFormat</label>\n";
      xml += "<font>" + mrSeriesNameFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
    "<control>\n" +
      "<description>Series 3 title</description>\n" +
      "<type>" + fadeLabelControl + "</type>\n" +
      "<id>0</id>\n" +
      "<posX>" + (baseXPosWatched + 19).ToString() + "</posX>\n" +
      "<posY>" + (baseYPosWatched + 282).ToString() + "</posY>\n" +
      "<width>255</width>\n" +
      "<scrollStartDelaySec>30</scrollStartDelaySec>" +
      "<label>#Mustayaluca.recentlyWatched.series3.episodetitle</label>\n" +
      "<font>" + mrEpisodeFont + "</font>\n" +
      "<textcolor>White</textcolor>\n" +
    "</control>\n" +
  "</control>\n" +
  "</controls>\n" +
  "</window>";
    }

    #endregion

    #region Most Recent Music Added

    void buildMostRecentMusicSummary(formMustayalucaEditor.musicMostRecentStyle musicSummaryStyle)
    {
      #region Style 1

      if (musicSummaryStyle == musicMostRecentStyle.style1)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: Recently Added Episodes</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                    "<visible>[" + mostRecentVisibleControls(isOverlayType.Music) + "]|control.hasfocus(91919997)|control.hasfocus(91919998)|control.hasfocus(91919999)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "<!-- Background -->" +
                    "<control>" +
                      "<description>Recent Albums BG</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                      "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                      "<width>524</width>" +
                      "<height>298</height>" +
                      "<texture>recently_added_episode_background.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Albums Seperator</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 197).ToString() + "</posY>\n" +
                      "<width>452</width>" +
                      "<height>2</height>" +
                      "<texture>recent_added_seperator.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<!-- Info when no album thumb button has focus -->" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recently Added Albums Label</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 170).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#SkinTranslation.Translations.recentlyAdded.Albums.Label</label>" +
                      "<visible>!control.hasfocus(91919997)+!control.hasfocus(91919998)+!control.hasfocus(91919999)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 1 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 203).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>450</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.music.latest1.artist - #latestMediaHandler.music.latest1.album</label>" +
                      "<visible>!control.hasfocus(91919997)+!control.hasfocus(91919998)+!control.hasfocus(91919999)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 2 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 225).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>450</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.music.latest2.artist - #latestMediaHandler.music.latest2.album</label>" +
                      "<visible>!control.hasfocus(91919997)+!control.hasfocus(91919998)+!control.hasfocus(91919999)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 3 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>450</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.music.latest3.artist - #latestMediaHandler.music.latest3.album</label>" +
                      "<visible>!control.hasfocus(91919997)+!control.hasfocus(91919998)+!control.hasfocus(91919999)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums Info</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 170).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#SkinTranslation.Translations.album.info.Label</label>" +
                      "<visible>control.hasfocus(91919997)|control.hasfocus(91919998)|control.hasfocus(91919999)</visible>" +
                    "</control>" +
                    "<!-- Info when recent album 1 button has focus -->" +
                    "<control>" +
                      "<description>Recent Albums 1 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 36).ToString() + "</posY>\n" +
                      "<width>125</width>" +
                      "<height>125</height>" +
                      "<texture>#latestMediaHandler.music.latest1.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 1 Artist</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 203).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(171)) #latestMediaHandler.music.latest1.artist</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<visible>control.hasfocus(91919997)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 1 Album</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 225).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(170)) #latestMediaHandler.music.latest1.album</label>" +
                      "<visible>control.hasfocus(91919997)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 1 Genre</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(174)) #latestMediaHandler.music.latest1.genre</label>" +
                      "<visible>control.hasfocus(91919997)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Albums 1 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919997</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 36).ToString() + "</posY>\n" +
                      "<width>125</width>" +
                      "<height>125</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml +=  "<onup>91919997</onup>";
            else
              xml +=  "<onup>" + (MusicMenuID + 600).ToString() + "01</onup>";

            xml +=    "<ondown>" + (MusicMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919998</onright>" +
                      "<onleft>91919999</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                    "<!-- Info when recent album 2 button has focus -->" +
                    "<control>" +
                      "<description>Recent Albums 2 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 200).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 36).ToString() + "</posY>\n" +
                      "<width>125</width>" +
                      "<height>125</height>" +
                      "<texture>#latestMediaHandler.music.latest2.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 2 Artist</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 203).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(171)) #latestMediaHandler.music.latest2.artist</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<visible>control.hasfocus(91919998)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 2 Album</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 225).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(170)) #latestMediaHandler.music.latest2.album</label>" +
                      "<visible>control.hasfocus(91919998)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 2 Genre</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(174)) #latestMediaHandler.music.latest2.genre</label>" +
                      "<visible>control.hasfocus(91919998)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Albums 2 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919998</id>" +
                      "<posX>" + (baseXPosAdded + 200).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 36).ToString() + "</posY>\n" +
                      "<width>125</width>" +
                      "<height>125</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
             if (cbDisableExitMenu.Checked)
               xml += "<onup>91919998</onup>";
             else
               xml += "<onup>" + (MusicMenuID + 600).ToString() + "01</onup>";

               xml += "<ondown>" + (MusicMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919999</onright>" +
                      "<onleft>91919997</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                    "<!-- Info when recent album 3 button has focus -->" +
                    "<control>" +
                      "<description>Recent Albums 3 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 363).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 36).ToString() + "</posY>\n" +
                      "<width>125</width>" +
                      "<height>125</height>" +
                      "<texture>#latestMediaHandler.music.latest3.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 3 Artist</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 203).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(171)) #latestMediaHandler.music.latest3.artist</label>" +
                      "<scrollStartDelaySec>5</scrollStartDelaySec>" +
                      "<visible>control.hasfocus(91919999)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 3 Album</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 225).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(170)) #latestMediaHandler.music.latest3.album</label>" +
                      "<visible>control.hasfocus(91919999)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Albums 3 Genre</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 36).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 247).ToString() + "</posY>\n" +
                      "<width>450</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(174)) #latestMediaHandler.music.latest3.genre</label>" +
                      "<visible>control.hasfocus(91919999)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Albums 3 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919999</id>" +
                      "<posX>" + (baseXPosAdded + 363).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 36).ToString() + "</posY>\n" +
                      "<width>125</width>" +
                      "<height>125</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
                if (cbDisableExitMenu.Checked)
                  xml += "<onup>91919999</onup>";
                else
                  xml += "<onup>" + (MusicMenuID + 600).ToString() + "01</onup>";

                xml += "<ondown>" + (MusicMenuID + 900).ToString() + "</ondown>" +
                       "<onright>91919997</onright>" +
                       "<onleft>91919998</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

      #endregion

      #region Style 2

      if (musicSummaryStyle == musicMostRecentStyle.style2)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: Recently Added Music</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                    "<visible>[" + mostRecentVisibleControls(isOverlayType.Music) + "]|control.hasfocus(91919997)|control.hasfocus(91919998)|control.hasfocus(91919999)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "<control>" +
                    "<description>Recent Music 1 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>230</height>" +
                    "<texture>recently_added_music.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 1 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 26).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<height>140</height>" +
                    "<texture>#latestMediaHandler.music.latest1.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 1 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>230</height>" +
                    "<texture>recently_added_music_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 1 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 27).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 171).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.music.latest1.artist</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>!control.hasfocus(91919997)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 1 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 27).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 171).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.music.latest1.album</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919997)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 1 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919997</id>" +
                    "<posX>" + (baseXPosAdded + 26).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<height>140</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
           if (cbDisableExitMenu.Checked)
             xml += "<onup>91919997</onup>";
           else
             xml += "<onup>" + (MusicMenuID + 600).ToString() + "01</onup>";

             xml += "<ondown>" + (MusicMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919998</onright>" +
                    "<onleft>91919999</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 2 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 192).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>230</height>" +
                    "<texture>recently_added_music.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 2 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 218).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<height>140</height>" +
                    "<texture>#latestMediaHandler.music.latest2.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 2 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 192).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>230</height>" +
                    "<texture>recently_added_music_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 2 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 219).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 171).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.music.latest2.artist</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>!control.hasfocus(91919998)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 2 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 219).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 171).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.music.latest2.album</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919998)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 2 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919998</id>" +
                    "<posX>" + (baseXPosAdded + 218).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<height>140</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml += "<onup>91919998</onup>";
            else
              xml += "<onup>" + (MusicMenuID + 600).ToString() + "01</onup>";

             xml += "<ondown>" + (MusicMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919999</onright>" +
                    "<onleft>91919997</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 3 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 384).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>230</height>" +
                    "<texture>recently_added_music.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 3 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 410).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<height>140</height>" +
                    "<texture>#latestMediaHandler.music.latest3.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 3 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 384).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>192</width>" +
                    "<height>230</height>" +
                    "<texture>recently_added_music_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 3 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 411).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 171).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.music.latest3.artist</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>!control.hasfocus(91919999)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 3 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 411).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 171).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<label>#latestMediaHandler.music.latest3.album</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919999)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Music 3 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919999</id>" +
                    "<posX>" + (baseXPosAdded + 411).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>140</width>" +
                    "<height>140</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
            if (cbDisableExitMenu.Checked)
              xml += "<onup>91919997</onup>";
            else
              xml += "<onup>" + (MusicMenuID + 600).ToString() + "01</onup>";

             xml += "<ondown>" + (MusicMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919997</onright>" +
                    "<onleft>91919998</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

      #endregion
    }

    #endregion

    #region Most Recent RecordedTV

    void MostRecentRecordedTVSummary()
    {
      // Fix layout for now = will add the option later
      mrRecordedTVStyle = recordedTVMostRecentStyle.style1;

      #region Style 1

      if (mrRecordedTVStyle == recordedTVMostRecentStyle.style1)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: Recently Added Recordings (Plot)</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                    "<visible>[" + mostRecentVisibleControls(isOverlayType.RecordedTV) + "]+![string.starts(#latestMediaHandler.tvrecordings.latest1.title,#)|string.equals(#latestMediaHandler.tvrecordings.latest1.title,)]|control.hasfocus(91919984)|control.hasfocus(91919985)|control.hasfocus(91919986)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "<!-- Background -->" +
                    "<control>" +
                      "<description>Recent Recordings BG</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                      "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                      "<width>680</width>" +
                      "<height>298</height>" +
                      "<texture>recently_added_episode_background.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Recordings Seperator</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 199).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<height>2</height>" +
                      "<texture>recent_added_seperator.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<!-- Dummy Images -->" +
                    "<control>" +
                      "<description>Image</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>default_recordings.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                      "<keepaspectratio>yes</keepaspectratio>" +
                      "<zoom>yes</zoom>" +
                    "</control>" +
                    "<control>" +
                      "<description>Image</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 240).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>default_recordings.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                      "<keepaspectratio>yes</keepaspectratio>" +
                      "<zoom>yes</zoom>" +
                    "</control>" +
                    "<control>" +
                      "<description>Image</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 450).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>default_recordings.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                      "<keepaspectratio>yes</keepaspectratio>" +
                      "<zoom>yes</zoom>" +
                    "</control>" +
                    "<!-- Info when no episode thumb button has focus -->" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recently Added Recordings Label</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 172).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#SkinTranslation.Translations.recentlyAdded.Recordings.Label</label>" +
                      "<visible>!control.hasfocus(91919984)+!control.hasfocus(91919985)+!control.hasfocus(91919986)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 1 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 205).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.tvrecordings.latest1.title</label>" +
                      "<visible>!control.hasfocus(91919984)+!control.hasfocus(91919985)+!control.hasfocus(91919986)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 2 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 227).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.tvrecordings.latest2.title</label>" +
                      "<visible>!control.hasfocus(91919984)+!control.hasfocus(91919985)+!control.hasfocus(91919986)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 3 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 249).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.tvrecordings.latest3.title</label>" +
                      "<visible>!control.hasfocus(91919984)+!control.hasfocus(91919985)+!control.hasfocus(91919986)</visible>" +
                    "</control>" +
                    "<!-- Info when recent recording 1 button has focus -->" +
                    "<control>" +
                      "<description>Recent Recordings 1 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvrecordings.latest1.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 1 Title</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 172).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.tvrecordings.latest1.title</label>" +
                      "<visible>control.hasfocus(91919984)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 1 Date</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 205).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#SkinTranslation.Translations.recorded.on.Label: #latestMediaHandler.tvrecordings.latest1.dateAdded</label>" +
                      "<visible>control.hasfocus(91919984)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 1 Genre</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 227).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(174)) #latestMediaHandler.tvrecordings.latest1.genre</label>" +
                      "<visible>control.hasfocus(91919984)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Recordings 1 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919984</id>" +
                      "<posX>" + (baseXPosAdded + 74).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>112</width>" +
                      "<height>112</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
             if (cbDisableExitMenu.Checked)
               xml += "<onup>91919984</onup>";
             else
               xml += "<onup>" + (TVMenuID + 600).ToString() + "01</onup>";

               xml += "<ondown>" + (TVMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919985</onright>" +
                      "<onleft>91919986</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                    "<!-- Info when recent recording 2 button has focus -->" +
                    "<control>" +
                      "<description>Recent Recordings 2 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 240).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvrecordings.latest2.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 2 Title</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 172).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.tvrecordings.latest2.title</label>" +
                      "<visible>control.hasfocus(91919985)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 2 Date</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 205).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#SkinTranslation.Translations.recorded.on.Label: #latestMediaHandler.tvrecordings.latest2.dateAdded</label>" +
                      "<visible>control.hasfocus(91919985)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 2 Genre</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 227).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(174)) #latestMediaHandler.tvrecordings.latest2.genre</label>" +
                      "<visible>control.hasfocus(91919985)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Recordings 2 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919985</id>" +
                      "<posX>" + (baseXPosAdded + 284).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>112</width>" +
                      "<height>112</height>" +
                     "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
             if (cbDisableExitMenu.Checked)
               xml += "<onup>91919985</onup>";
             else
               xml += "<onup>" + (TVMenuID + 600).ToString() + "01</onup>";

               xml += "<ondown>" + (TVMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919986</onright>" +
                      "<onleft>91919984</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                    "<!-- Info when recent recording 3 button has focus -->" +
                    "<control>" +
                      "<description>Recent Recordings 3 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 450).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.tvrecordings.latest3.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 3 Title</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 172).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#latestMediaHandler.tvrecordings.latest3.title</label>" +
                      "<visible>control.hasfocus(91919986)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 3 Date</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 205).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#SkinTranslation.Translations.recorded.on.Label: #latestMediaHandler.tvrecordings.latest3.dateAdded</label>" +
                      "<visible>control.hasfocus(91919986)</visible>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Recordings 3 Genre</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 227).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font5</font>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#(L(174)) #latestMediaHandler.tvrecordings.latest3.genre</label>" +
                      "<visible>control.hasfocus(91919986)</visible>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Recordings 3 Button</description>" +
                      "<type>button</type>" +
                      "<id>91919986</id>" +
                      "<posX>" + (baseXPosAdded + 484).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>112</width>" +
                      "<height>112</height>" +
                      "<textureFocus>recently_added_focus.png</textureFocus>" +
                      "<textureNoFocus>-</textureNoFocus>";
              if (cbDisableExitMenu.Checked)
                xml += "<onup>91919986</onup>";
              else
                xml += "<onup>" + (TVMenuID + 600).ToString() + "01</onup>";

               xml += "<ondown>" + (TVMenuID + 900).ToString() + "</ondown>" +
                      "<onright>91919984</onright>" +
                      "<onleft>91919985</onleft>" +
                      "<label>-</label>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

      #endregion

      #region Style 2

      if (mrRecordedTVStyle == recordedTVMostRecentStyle.style2)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: Recently Added Series</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                    "<visible>[" + mostRecentVisibleControls(isOverlayType.RecordedTV) + "]+![string.starts(#latestMediaHandler.tvrecordings.latest1.title,#)|string.equals(#latestMediaHandler.tvrecordings.latest1.title,)]|control.hasfocus(91919984)|control.hasfocus(91919985)|control.hasfocus(91919986)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "<control>" +
                    "<description>Recorded TV 1 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 1 Dummy Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 27).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>default_recordings.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<keepaspectratio>yes</keepaspectratio>" +
                    "<centered>yes</centered>" +
                    "<zoom>yes</zoom>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 1 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 27).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>#latestMediaHandler.tvrecordings.latest1.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 1 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 1 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 28).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.tvrecordings.latest1.title</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>!control.hasfocus(91919994)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 1 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 28).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.tvrecordings.latest1.dateAdded</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919994)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 1 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919984</id>" +
                    "<posX>" + (baseXPosAdded + 70).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>113</width>" +
                    "<height>113</height>" +
                    "<textureFocus>recently_added_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
           if (cbDisableExitMenu.Checked)
             xml += "<onup>91919984</onup>";
           else
             xml += "<onup>" + (TVMenuID + 600).ToString() + "01</onup>";

             xml += "<ondown>" + (TVMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919985</onright>" +
                    "<onleft>91919986</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 2 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 254).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                    "<description>Recorded TV 2 Dummy Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 282).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>default_recordings.png</texture>" +
                    "<keepaspectratio>yes</keepaspectratio>" +
                    "<centered>yes</centered>" +
                    "<zoom>yes</zoom>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 2 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 282).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>#latestMediaHandler.tvrecordings.latest2.title</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 2 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 254).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 2 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 283).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.tvrecordings.latest2.title</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>!control.hasfocus(91919985)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 2 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 283).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.tvrecordings.latest2.dateAdded</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919985)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 2 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919985</id>" +
                    "<posX>" + (baseXPosAdded + 324).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>113</width>" +
                    "<height>113</height>" +
                    "<textureFocus>now_playing_select_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
           if (cbDisableExitMenu.Checked)
             xml += "<onup>91919985</onup>";
           else
             xml += "<onup>" + (TVMenuID + 600).ToString() + "01</onup>";

             xml += "<ondown>" + (TVMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919986</onright>" +
                    "<onleft>91919984</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 3 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 508).ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 3 Dummy Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 536).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>default_recordings.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<keepaspectratio>yes</keepaspectratio>" +
                    "<centered>yes</centered>" +
                    "<zoom>yes</zoom>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 3 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 536).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<texture>#latestMediaHandler.tvrecordings.latest3.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 3 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 508).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 3 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 537).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.tvrecordings.latest3.title</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>!control.hasfocus(91919986)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 3 Info</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 537).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.tvrecordings.latest3.dateAdded</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                    "<visible>control.hasfocus(91919986)</visible>" +
                    "<animation effect=\"fade\" time=\"150\">visiblechange</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recorded TV 3 Button</description>" +
                    "<type>button</type>" +
                    "<id>91919986</id>" +
                    "<posX>" + (baseXPosAdded + 578).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>113</width>" +
                    "<height>113</height>" +
                    "<textureFocus>now_playing_select_focus.png</textureFocus>" +
                    "<textureNoFocus>-</textureNoFocus>";
           if (cbDisableExitMenu.Checked)
             xml += "<onup>91919986</onup>";
           else
             xml += "<onup>" + (TVMenuID + 600).ToString() + "01</onup>";

             xml += "<ondown>" + (TVMenuID + 900).ToString() + "</ondown>" +
                    "<onright>91919984</onright>" +
                    "<onleft>91919985</onleft>" +
                    "<label>-</label>" +
                  "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";

      #endregion
      }
    }

    #endregion

    #region Most Recent Pictures

    void MostRecentlyAddedPictures()
    {
      // Fix style for now - will add option later
      mrPicturesStyle = picturesMostRecentStyle.style1;

      #region Style 1

      if (mrPicturesStyle == picturesMostRecentStyle.style1)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                    "<description>GROUP: Recently Added Photos</description>" +
                    "<type>group</type>" +
                    "<dimColor>0xffffffff</dimColor>" +
                    "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                    "<visible>" + mostRecentVisibleControls(isOverlayType.Pictures) + "</visible>" +
                    "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                    "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                    "<!-- Background -->" +
                    "<control>" +
                      "<description>Recent Photos BG</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                      "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                      "<width>680</width>" +
                      "<height>298</height>" +
                      "<texture>recently_added_episode_background.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Photos Seperator</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 199).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<height>2</height>" +
                      "<texture>recent_added_seperator.png</texture>" +
                      "<shouldCache>true</shouldCache>" +
                    "</control>" +
                    "<!-- Thumbs -->" +
                    "<control>" +
                      "<description>Recent Photo 1 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.picture.latest1.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                      "<keepaspectratio>yes</keepaspectratio>" +
                      "<zoom>yes</zoom>" +
                      "<zoomfromtop>yes</zoomfromtop>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Photo 2 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 240).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.picture.latest2.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                      "<keepaspectratio>yes</keepaspectratio>" +
                      "<zoom>yes</zoom>" +
                      "<zoomfromtop>yes</zoomfromtop>" +
                    "</control>" +
                    "<control>" +
                      "<description>Recent Photo 3 Thumb</description>" +
                      "<type>image</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 450).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 30).ToString() + "</posY>\n" +
                      "<width>200</width>" +
                      "<height>113</height>" +
                      "<texture>#latestMediaHandler.picture.latest3.thumb</texture>" +
                      "<shouldCache>true</shouldCache>" +
                      "<keepaspectratio>yes</keepaspectratio>" +
                      "<zoom>yes</zoom>" +
                      "<zoomfromtop>yes</zoomfromtop>" +
                    "</control>	  " +
                    "<!-- Title and Names -->" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recently Added Photos Label</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 172).ToString() + "</posY>\n" +
                      "<width>620</width>" +
                      "<font>font17</font>" +
                      "<textcolor>ff474747</textcolor>" +
                      "<label>#SkinTranslation.Translations.recentlyAdded.Pics.Label</label>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Photo 1 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 205).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.picture.latest1.filename - #latestMediaHandler.picture.latest1.dateAdded</label>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Photo 2 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 227).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.picture.latest2.filename - #latestMediaHandler.picture.latest2.dateAdded</label>" +
                    "</control>" +
                    "<control Style=\"NoShadow\">" +
                      "<description>Recent Photo 3 Name</description>" +
                      "<type>fadelabel</type>" +
                      "<id>0</id>" +
                      "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                      "<posY>" + (baseYPosAdded + 249).ToString() + "</posY>\n" +
                      "<font>font5</font>" +
                      "<width>620</width>" +
                      "<textcolor>ff7a7a7a</textcolor>" +
                      "<label>#latestMediaHandler.picture.latest3.filename - #latestMediaHandler.picture.latest3.dateAdded</label>" +
                    "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";
      }

      #endregion

      #region Style 2

      if (mrPicturesStyle == picturesMostRecentStyle.style2)
      {
        xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
              "<window>" +
                "<controls>" +
                  "<control>" +
                  "<description>GROUP: Recently Added Pictures</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<!-- CHANGE the ID value (represented as xxxx) of hasfocus(xxxx) to the value of the ID in basichome you want this to show.If you want it to show on multiple you can chain them like so; control.hasfocus(xxxx) + control.hasfocus(xxxx2) etc. -->" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.Pictures) + "</visible>" +
                  "<animation effect=\"fade\" time=\"150\">WindowOpen</animation>" +
                  "<animation effect=\"fade\" time=\"150\">WindowClose</animation>" +
                  "<control>" +
                    "<description>Recent Pictures 1 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + baseXPosAdded.ToString() + "</posX>\n" +
                    "<posY>" + baseYPosAdded.ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 1 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 27).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<keepaspectratio>yes</keepaspectratio>" +
                    "<zoom>yes</zoom>" +
                    "<zoomfromtop>yes</zoomfromtop>" +
                    "<texture>#latestMediaHandler.picture.latest1.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 1 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 1 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 30).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.picture.latest1.title</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 2 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 254).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 2 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 282).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<keepaspectratio>yes</keepaspectratio>" +
                    "<zoom>yes</zoom>" +
                    "<zoomfromtop>yes</zoomfromtop>" +
                    "<texture>#latestMediaHandler.picture.latest2.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 2 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 254).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 2 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 283).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.picture.latest2.title</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 3 Background</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 508).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 3 Thumb</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 536).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 27).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<height>113</height>" +
                    "<keepaspectratio>yes</keepaspectratio>" +
                    "<zoom>yes</zoom>" +
                    "<zoomfromtop>yes</zoomfromtop>" +
                    "<texture>#latestMediaHandler.picture.latest3.thumb</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 3 Overlay</description>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 508).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded).ToString() + "</posY>\n" +
                    "<width>254</width>" +
                    "<height>203</height>" +
                    "<texture>recently_added_series_shine.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Recent Pictures 3 Name</description>" +
                    "<type>fadelabel</type>" +
                    "<id>0</id>" +
                    "<posX>" + (baseXPosAdded + 537).ToString() + "</posX>\n" +
                    "<posY>" + (baseYPosAdded + 144).ToString() + "</posY>\n" +
                    "<width>200</width>" +
                    "<label>#latestMediaHandler.picture.latest3.title</label>" +
                    "<font>font5</font>" +
                    "<textcolor>ffcdcccc</textcolor>" +
                    "<scrollStartDelaySec>2</scrollStartDelaySec>" +
                    "<scrollWrapString> | </scrollWrapString>" +
                  "</control>" +
                  "</control>" +
                "</controls>" +
              "</window>";

      #endregion
      }
    }

    #endregion

    #region DriveFreeSpace

    void driveFreeSpaceOverlay()
    {
      int yOffset = 10;
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
              "<window>" +
                  "<controls>" +
                      "<control>" +
                          "<description>GROUP: Power Control Plugins Overlay</description>" +
                          "<type>group</type>" +
                          "<dimColor>0xffffffff</dimColor>" +
                           "<visible>" + mostRecentVisibleControls(isOverlayType.freeDriveSpace) + "</visible>" +
                          "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                          "<control>" +
                          "<description>Overlay BG</description>" +
                          "<posX>976</posX>" +
                          "<posY>50</posY>" +
                          "<type>image</type>" +
                          "<id>0</id>" +
                          "<width>306</width>" +
                          "<height>320</height>" +
                          "<texture>recentsummoverlaybg.png</texture>" +
                          "<shouldCache>true</shouldCache>" +
                          "<colordiffuse>EEFFFFFF</colordiffuse>" +
                          "</control>" +
                          "<control>" +
                          "<description>Plugin Name</description>" +
                          "<type>label</type>" +
                          "<id>0</id>" +
                          "<posX>995</posX>" +
                          "<posY>76</posY>" +
                          "<width>258</width>" +
                          "<label>Drive Free Space</label>" +
                          "<font>mediastream10tc</font>" +
                          "<textcolor>White</textcolor>" +
                          "</control>" +
                          "<control>" +
                          "<description>Index Separator</description>" +
                          "<type>label</type>" +
                          "<id>0</id>" +
                          "<posX>995</posX>" +
                          "<posY>80</posY>" +
                          "<width>264</width>" +
                          "<label>____________________________________________________________________________________________________________		</label>" +
                          "<textcolor>ff808080</textcolor>" +
                          "</control>";

      foreach (string driveToDisplay in driveFreeSpaceDrives)
      {

        string driveLetter = driveToDisplay.Substring(0, 1);

        string driveIcon = "FreeDriveSpace_Icon_C.png";
        if (driveLetter.ToLower() != "c")
          driveIcon = "FreeDriveSpace_Icon.png";

        xml += "<!-- *** Drive " + driveLetter + " *** -->" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Image</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>995</posX>" +
                    "<posY>" + (90 + yOffset).ToString() + "</posY>" +
                    "<width>60</width>" +
                    "<height>60</height>" +
                    "<texture>" + driveIcon + "</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>995</posX>" +
                    "<posY>" + (160 + yOffset).ToString() + "</posY>" +
                    "<width>266</width>" +
                    "<height>20</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>985</posX>" +
                    "<posY>" + (161 + yOffset).ToString() + "</posY>" +
                    "<width>266</width>" +
                    "<height>20</height>" +
                    "<texturebg>-</texturebg>" +
                    "<label>#DriveFreeSpace." + driveLetter + ".AvailableSpace.UsedPercentage</label>" +
                    "<lefttexture>osdprogressleft.png</lefttexture>" +
                    "<midtexture>osdprogressmid.png</midtexture>" +
                    "<righttexture>osdprogressright</righttexture>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>Drive " + driveLetter + " Description</description>" +
                    "<type>label</type>" +
                    "<id>1</id>" +
                    "<posX>1060</posX>" +
                    "<posY>" + (100 + yOffset).ToString() + "</posY>" +
                    "<width>198</width>" +
                    "<label>#DriveFreeSpace." + driveLetter + ".AvailableSpace.Data</label>" +
                    "<font>mediastream10</font>" +
                    "<visible>string.contains(#DriveFreeSpace." + driveLetter + ".Enabled,true)</visible>" +
                  "</control>";

        yOffset += 84;
      }

      xml += "</control>" +
"</controls>" +
"</window>";
    }

    #endregion

    #region SleepControl

    void sleepControlOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: Sleep Control Plugins Overlay</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>!string.starts(#SleepControl.Counter,#)+" + mostRecentVisibleControls(isOverlayType.sleepControl) + "</visible>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Overlay BG</description>" +
                  "<posX>976</posX>" +
                  "<posY>50</posY>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>306</width>" +
                  "<height>320</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<shouldCache>true</shouldCache>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>76</posY>" +
                  "<width>258</width>" +
                  "<label>Sleep Control</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>80</posY>" +
                  "<width>264</width>				<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Mode Image</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>1140</posX>" +
                  "<posY>74</posY>" +
                  "<width>20</width>" +
                  "<height>20</height>" +
                  "<texture>#SleepControl.Image</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Counter</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>150</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Counter</label>" +
                  "<font>mediastream28tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Activity</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>200</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Activity</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Mode</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>230</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Method</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control Start</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>260</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.Start</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "<control>" +
                  "<description>Sleep Control End</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>976</posX>" +
                  "<posY>290</posY>" +
                  "<width>306</width>" +
                  "<align>center</align>" +
                  "<label>#SleepControl.End</label>" +
                  "<font>mediastream10tc</font>" +
                "</control>" +
                "</control>" +
              "</controls>" +
            "</window>";

    }
    #endregion

    #region Stocks and Indices

    void stocksOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: Power Control Plugins Overlay</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.stocks) + "</visible>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                "<control>" +
                  "<description>Overlay BG</description>" +
                  "<posX>976</posX>" +
                  "<posY>50</posY>" +
                  "<type>image</type>" +
                  "<id>0</id>" +
                  "<width>306</width>" +
                  "<height>320</height>" +
                  "<texture>recentsummoverlaybg.png</texture>" +
                  "<shouldCache>true</shouldCache>" +
                  "<colordiffuse>EEFFFFFF</colordiffuse>" +
                "</control>" +
                "<!-- *** Indices *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>76</posY>" +
                  "<width>258</width>" +
                  "<label>Indices</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>80</posY>" +
                  "<width>264</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Index 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Index0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>100</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>99</posY>" +
                  "<label>#Stocks.Index0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>99</posY>" +
                  "<label>#Stocks.Index0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index0PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>99</posY>" +
                  "<label>#Stocks.Index0PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Index 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Index1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>120</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>119</posY>" +
                  "<label>#Stocks.Index1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>119</posY>" +
                  "<label>#Stocks.Index1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index1PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>119</posY>" +
                  "<label>#Stocks.Index1PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Index 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Index2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>140</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Index2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>139</posY>" +
                  "<label>#Stocks.Index2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>139</posY>" +
                  "<label>#Stocks.Index2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Index2PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>139</posY>" +
                  "<label>#Stocks.Index2PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stocks *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>165</posY>" +
                  "<width>263</width>" +
                  "<label>Stocks</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>169</posY>" +
                  "<width>264</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Stock 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>189</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>188</posY>" +
                  "<label>#Stocks.Stock0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>188</posY>" +
                  "<label>#Stocks.Stock0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock0PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>188</posY>" +
                  "<label>#Stocks.Stock0PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stock 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>209</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Stock1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Stock1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock1PercentChange</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>209</posY>" +
                  "<label>#Stocks.Stock1PercentChange</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Stock 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Stock2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>229</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Stock2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>100</width>" +
                  "<posX>1014</posX>" +
                  "<posY>228</posY>" +
                  "<label>#Stocks.Stock2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Stock2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1190</posX>" +
                  "<posY>228</posY>" +
                  "<label>#Stocks.Stock2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<control>" +
                    "<description>Stocks.Stock2PercentChange</description>" +
                    "<type>label</type>" +
                    "<id>1</id>" +
                    "<posX>1260</posX>" +
                    "<posY>228</posY>" +
                    "<label>#Stocks.Stock2PercentChange</label>" +
                    "<font>mediastream10c</font>" +
                    "<align>right</align>" +
                "</control>" +
                "<!-- *** Currencies *** -->" +
                "<control>" +
                  "<description>Plugin Name</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>254</posY>" +
                  "<width>258</width>" +
                  "<label>Currencies</label>" +
                  "<font>mediastream10tc</font>" +
                  "<textcolor>White</textcolor>" +
                "</control>" +
                "<control>" +
                  "<description>Index Separator</description>" +
                  "<type>label</type>" +
                  "<id>0</id>" +
                  "<posX>995</posX>" +
                  "<posY>258</posY>" +
                  "<width>264</width>" +
                  "<label>____________________________________________________________________________________________________________</label>" +
                  "<textcolor>ff808080</textcolor>" +
                "</control>" +
                "<!-- *** Currency 0 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency0Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>278</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency0Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency0Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>190</width>" +
                  "<posX>1014</posX>" +
                  "<posY>277</posY>" +
                  "<label>#Stocks.Currency0Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency0Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>277</posY>" +
                  "<label>#Stocks.Currency0Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Currency 1 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency1Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>298</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency1Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency1Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>190</width>" +
                  "<posX>1014</posX>" +
                  "<posY>297</posY>" +
                  "<label>#Stocks.Currency1Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency1Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>297</posY>" +
                  "<label>#Stocks.Currency1Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Currency 2 *** -->" +
                "<control>" +
                  "<description>Stocks.Currency2Indicator</description>" +
                  "<type>image</type>" +
                  "<id>1</id>" +
                  "<posX>995</posX>" +
                  "<posY>318</posY>" +
                  "<width>18</width>" +
                  "<height>18</height>" +
                  "<keepaspectratio>yes</keepaspectratio>" +
                  "<texture>#Stocks.Currency2Indicator</texture>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency2Name</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<width>190</width>" +
                  "<posX>1014</posX>" +
                  "<posY>317</posY>" +
                  "<label>#Stocks.Currency2Name</label>" +
                  "<font>mediastream10c</font>" +
                "</control>" +
                "<control>" +
                  "<description>Stocks.Currency2Last</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>317</posY>" +
                  "<label>#Stocks.Currency2Last</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                "</control>" +
                "<!-- *** Refresh Time *** -->" +
                "<control>" +
                  "<description>Stocks.Time</description>" +
                  "<type>label</type>" +
                  "<id>1</id>" +
                  "<posX>1260</posX>" +
                  "<posY>76</posY>" +
                  "<label>#Stocks.Time</label>" +
                  "<font>mediastream10c</font>" +
                  "<align>right</align>" +
                  "<visible>!string.starts(#Stocks.Time,#)</visible>" +
                "</control>" +
                "</control>" +
              "</controls>" +
            "</window>";
    }

    #endregion

    #region Power Control

    void powerControlOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: Power Control Plugins Overlay</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.powerControl) + "</visible>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                  "<control>" +
                    "<description>Movie 1 BG</description>" +
                    "<posX>976</posX>" +
                    "<posY>50</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>306</width>" +
                    "<height>320</height>" +
                    "<texture>recentsummoverlaybg.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Header label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>995</posX>" +
                    "<posY>76</posY>" +
                    "<width>258</width>" +
                    "<label>Power Control</label>" +
                    "<font>mediastream10tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Index Separator</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>995</posX>" +
                    "<posY>80</posY>" +
                    "<width>264</width>" +
                    "<label>____________________________________________________________________________________________________________</label>" +
                    "<textcolor>ff808080</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Network Device Group 0</description>" +
                    "<type>group</type>" +
                    "<visible>!string.starts(#PowerControl.NetworkDevice0Description,Device)</visible>" +
                    "<!-- *** Network Device 0 *** -->" +
                    "<control>" +
                      "<description>Network Device 0 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>100</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice0TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 0 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1042</posX>" +
                      "<posY>110</posY>" +
                      "<width>198</width>" +
                      "<label>#PowerControl.NetworkDevice0Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 0 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>110</posY>>" +
                      "<texture>#PowerControl.NetworkDevice0AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>Network Device Group 1</description>" +
                    "<type>group</type>" +
                    "<visible>!string.starts(#PowerControl.NetworkDevice1Description,Device)</visible>" +
                    "<!-- *** Network Device 1 *** -->" +
                    "<control>" +
                      "<description>Network Device 1 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>152</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice1TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 1 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1042</posX>" +
                      "<posY>162</posY>" +
                      "<label>#PowerControl.NetworkDevice1Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 1 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>162</posY>>" +
                      "<texture>#PowerControl.NetworkDevice1AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                  "<description>Network Device Group 2</description>" +
                  "<type>group</type>" +
                  "<visible>!string.starts(#PowerControl.NetworkDevice2Description,Device)</visible>" +
                  "<!-- *** Network Device 2 *** -->" +
                    "<control>" +
                      "<description>Network Device 2 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>204</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice2TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                        "<description>Network Device 2 Description</description>" +
                        "<type>label</type>" +
                        "<id>1</id>" +
                        "<posX>1042</posX>" +
                        "<posY>214</posY>" +
                        "<label>#PowerControl.NetworkDevice2Description</label>" +
                        "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 2 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>214</posY>>" +
                      "<texture>#PowerControl.NetworkDevice2AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                    "<description>Network Device Group 3</description>" +
                    "<type>group</type>" +
                    "<visible>!string.starts(#PowerControl.NetworkDevice3Description,Device)</visible>" +
                    "<!-- *** Network Device 3 *** -->" +
                    "<control>" +
                      "<description>Network Device 3 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>256</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice3TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 3 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1042</posX>" +
                      "<posY>266</posY>" +
                      "<label>#PowerControl.NetworkDevice3Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 3 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>266</posY>>" +
                      "<texture>#PowerControl.NetworkDevice3AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                  "<control>" +
                  "<description>Network Device Group 4</description>" +
                  "<type>group</type>" +
                  "<visible>!string.starts(#PowerControl.NetworkDevice4Description,Device)</visible>" +
                    "<!-- *** Network Device 4 *** -->" +
                    "<control>" +
                      "<description>Network Device 4 Image</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>995</posX>" +
                      "<posY>308</posY>" +
                      "<width>40</width>" +
                      "<height>40</height>" +
                      "<texture>#PowerControl.NetworkDevice4TypeImage</texture>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 4 Description</description>" +
                      "<type>label</type>" +
                      "<id>1</id>" +
                      "<posX>1042</posX>" +
                      "<posY>318</posY>" +
                      "<label>#PowerControl.NetworkDevice4Description</label>" +
                      "<font>mediastream10tc</font>" +
                    "</control>" +
                    "<control>" +
                      "<description>Network Device 4 Alive</description>" +
                      "<type>image</type>" +
                      "<id>1</id>" +
                      "<posX>1240</posX>" +
                      "<posY>318</posY>" +
                      "<texture>#PowerControl.NetworkDevice4AliveImage</texture>" +
                      "<width>20</width>" +
                      "<height>20</height>" +
                    "</control>" +
                  "</control>" +
                "</control>" +
              "</controls>" +
            "</window>";
    }

    #endregion

    #region HTPCInfo Overlay

    void htpcInfoOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
            "<window>" +
              "<controls>" +
                "<control>" +
                  "<description>GROUP: Power Control Plugins Overlay</description>" +
                  "<type>group</type>" +
                  "<dimColor>0xffffffff</dimColor>" +
                  "<visible>" + mostRecentVisibleControls(isOverlayType.htpcInfo) + "</visible>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                  "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                  "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                  "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                  "<control>" +
                    "<description>Overlay BG</description>" +
                    "<posX>976</posX>" +
                    "<posY>50</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>306</width>" +
                    "<height>320</height>" +
                    "<texture>recentsummoverlaybg.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                    "<colordiffuse>EEFFFFFF</colordiffuse>" +
                  "</control>" +
                  "<control>" +
                    "<description>Plugin Name</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>995</posX>" +
                    "<posY>76</posY>" +
                    "<width>258</width>" +
                    "<label>HTPC Info</label>" +
                    "<font>mediastream10tc</font>" +
                    "<textcolor>White</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Index Separator</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>995</posX>" +
                    "<posY>80</posY>" +
                    "<width>264</width>" +
                    "<label>____________________________________________________________________________________________________________</label>" +
                    "<textcolor>ff808080</textcolor>" +
                  "</control>" +
                  "<!-- *** CPU Infos *** -->" +
                  "<control>" +
                    "<description>CPU Picture</description>" +
                    "<posX>995</posX>" +
                    "<posY>100</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>80</width>" +
                    "<height>80</height>" +
                    "<texture>HTPC_Info_CPU.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Temp label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>115</posY>" +
                    "<width>170</width>" +
                    "<label>CPU Temp:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Temp value</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>115</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.SensorTemperatureCPU</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.SensorTemperatureCPU,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>135</posY>" +
                    "<width>170</width>" +
                    "<label>CPU Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Processor Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>135</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.ProcessorUsage%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.ProcessorUsage,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1086</posX>" +
                    "<posY>154</posY>" +
                    "<width>176</width>" +
                    "<height>20</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>CPU Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1074</posX>" +
                    "<posY>155</posY>" +
                    "<width>196</width>" +
                    "<height>20</height>" +
                    "<texturebg>-</texturebg>" +
                    "<label>#HTPCInfo.ProcessorUsage</label>" +
                    "<lefttexture>osdprogressleft.png</lefttexture>" +
                    "<midtexture>osdprogressmid.png</midtexture>" +
                    "<righttexture>osdprogressright</righttexture>" +
                    "<visible>!string.starts(#HTPCInfo.ProcessorUsage,#)</visible>" +
                  "</control>" +
                  "<!-- *** RAM Infos *** -->" +
                  "<control>" +
                    "<description>CPU Picture</description>" +
                    "<posX>995</posX>" +
                    "<posY>185</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>80</width>" +
                    "<height>80</height>" +
                    "<texture>HTPC_Info_RAM.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>Free RAM label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>200</posY>" +
                    "<width>170</width>" +
                    "<label>Free RAM:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Free RAM Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>200</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.MemoryAvailable</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.MemoryAvailable,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>220</posY>" +
                    "<width>170</width>" +
                    "<label>RAM Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>Ram Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>220</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.MemoryPercentUsed%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.MemoryPercentUsed,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1086</posX>" +
                    "<posY>239</posY>" +
                    "<width>176</width>" +
                    "<height>20</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>RAM Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1074</posX>" +
                    "<posY>240</posY>" +
                    "<width>196</width>" +
                    "<height>20</height>" +
                    "<texturebg>-</texturebg>" +
                    "<label>#HTPCInfo.MemoryPercentUsed</label>" +
                    "<lefttexture>osdprogressleft.png</lefttexture>" +
                    "<midtexture>osdprogressmid.png</midtexture>" +
                    "<righttexture>osdprogressright</righttexture>" +
                    "<visible>!string.starts(#HTPCInfo.MemoryPercentUsed,#)</visible>" +
                  "</control>" +
                  "<!-- *** GPU Infos *** -->" +
                  "<control>" +
                    "<description>CPU Picture</description>" +
                    "<posX>995</posX>" +
                    "<posY>275</posY>" +
                    "<type>image</type>" +
                    "<id>0</id>" +
                    "<width>80</width>" +
                    "<height>80</height>" +
                    "<texture>HTPC_Info_GPU.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Temp label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>290</posY>" +
                    "<width>170</width>" +
                    "<label>GPU Temp:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Temp value</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>290</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.GPUSensorTemperature0</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.GPUSensorTemperature0,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Usage label</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1090</posX>" +
                    "<posY>310</posY>" +
                    "<width>170</width>" +
                    "<label>GPU Usage:</label>" +
                    "<font>mediastream10</font>" +
                    "<textcolor>white</textcolor>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Usage</description>" +
                    "<type>label</type>" +
                    "<id>0</id>" +
                    "<posX>1255</posX>" +
                    "<posY>310</posY>" +
                    "<width>250</width>" +
                    "<label>#HTPCInfo.GPUSensorUsage0%</label>" +
                    "<font>mediastream10</font>" +
                    "<align>right</align>" +
                    "<visible>!string.starts(#HTPCInfo.GPUSensorUsage0,#)</visible>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Progress BG</description>" +
                    "<type>image</type>" +
                    "<id>1</id>" +
                    "<posX>1086</posX>" +
                    "<posY>329</posY>" +
                    "<width>176</width>" +
                    "<height>20</height>" +
                    "<texture>osdprogressbackv.png</texture>" +
                    "<shouldCache>true</shouldCache>" +
                  "</control>" +
                  "<control>" +
                    "<description>GPU Progress Bar</description>" +
                    "<type>progress</type>" +
                    "<id>20</id>" +
                    "<posX>1074</posX>" +
                    "<posY>330</posY>" +
                    "<width>196</width>" +
                    "<height>20</height>" +
                    "<texturebg>-</texturebg>" +
                    "<label>#HTPCInfo.GPUSensorUsage0</label>" +
                    "<lefttexture>osdprogressleft.png</lefttexture>" +
                    "<midtexture>osdprogressmid.png</midtexture>" +
                    "<righttexture>osdprogressright</righttexture>" +
                    "<visible>!string.starts(#HTPCInfo.GPUSensorUsage0,#)</visible>" +
                  "</control>" +
                "</control>" +
              "</controls>" +
            "</window>";
    }

    #endregion

    #region Update Overlay

    void updateControlOverlay()
    {
      xml = "<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>" +
              "<window>" +
                  "<controls>" +
                      "<control>" +
                          "<description>GROUP: Update Control Plugins Overlay</description>" +
                          "<type>group</type>" +
                          "<dimColor>0xffffffff</dimColor>" +
                          "<visible>" + mostRecentVisibleControls(isOverlayType.updateControl) + "</visible>" +
                          "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"250\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"700\" time=\"500\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"fade\" start=\"0\" end=\"100\" time=\"4000\" reversible=\"false\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"300,0\" time=\"1500\" acceleration=\"-0.1\" reversible=\"false\">Hidden</animation>" +
                          "<animation effect=\"slide\" start=\"300,0\" end=\"0,0\" time=\"1000\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>" +
                          "<animation effect=\"slide\" start=\"400,0\" end=\"0,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowOpen</animation>" +
                          "<animation effect=\"slide\" end=\"400,0\" tween=\"quadratic\" easing=\"in\" time=\" 400\" delay=\"200\">WindowClose</animation>" +
                          "<control>" +
                              "<description>Overlay BG</description>" +
                              "<posX>976</posX>" +
                              "<posY>50</posY>" +
                              "<type>image</type>" +
                              "<id>0</id>" +
                              "<width>306</width>" +
                              "<height>320</height>" +
                              "<texture>recentsummoverlaybg.png</texture>" +
                              "<shouldCache>true</shouldCache>" +
                              "<colordiffuse>EEFFFFFF</colordiffuse>" +
                          "</control>" +
                          "<control>" +
                              "<description>Plugin Name</description>" +
                              "<type>label</type>" +
                              "<id>0</id>" +
                              "<posX>995</posX>" +
                              "<posY>76</posY>" +
                              "<width>258</width>" +
                              "<label>Update Control</label>" +
                              "<font>mediastream10tc</font>" +
                              "<textcolor>White</textcolor>" +
                          "</control>" +
                          "<control>" +
                              "<description>Index Separator</description>" +
                              "<type>label</type>" +
                              "<id>0</id>" +
                              "<posX>995</posX>" +
                              "<posY>80</posY>" +
                              "<width>264</width>" +
                              "<label>____________________________________________________________________________________________________________</label>" +
                              "<textcolor>ff808080</textcolor>" +
                          "</control>" +
                          "<control>" +
                              "<description>Urgency Image</description>" +
                              "<type>image</type>" +
                              "<id>1</id>" +
                              "<posX>995</posX>" +
                              "<posY>115</posY>" +
                              "<width>60</width>" +
                              "<height>60</height>" +
                              "<texture>updatecontrol_empty.png</texture>" +
                              "<shouldCache>true</shouldCache>" +
                              "<visible>string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                              "<description>Urgency Image</description>" +
                              "<type>image</type>" +
                              "<id>1</id>" +
                              "<posX>995</posX>" +
                              "<posY>115</posY>" +
                              "<width>60</width>" +
                              "<height>60</height>" +
                              "<texture>#UpdateControl.AvailableUpdateUrgencyImage</texture>" +
                              "<visible>!string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                            "<description>Text</description>" +
                            "<type>textbox</type>" +
                            "<id>1</id>" +
                            "<posX>1060</posX>" +
                            "<posY>120</posY>" +
                            "<width>198</width>" +
                            "<label>New Updates Available</label>" +
                            "<font>mediastream12tc</font>" +
                            "<visible>!string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>" +
                          "<control>" +
                            "<description>Text</description>" +
                            "<type>textbox</type>" +
                            "<id>1</id>" +
                            "<posX>1060</posX>" +
                            "<posY>120</posY>" +
                            "<width>198</width>" +
                            "<label>No Updates Available</label>" +
                            "<font>mediastream12tc</font>" +
                            "<visible>string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                          "</control>     " +
                          "<control>" +
                            "<type>group</type>" +
                            "<visible>!string.equals(#UpdateControl.AvailableUpdateCount,0)</visible>" +
                            "<control>" +
                            "<description>Text available windows updates</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>190</posY>" +
                            "<width>198</width>" +
                            "<label>Available Updates</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>available windows updates</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>207</posY>" +
                            "<width>198</width>" +
                            "<label>#UpdateControl.AvailableUpdateCount</label>" +
                            "<font>mediastream10</font>          " +
                            "</control>" +
                          "<control>" +
                            "<description>Text available windows updates Size</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>235</posY>" +
                            "<width>198</width>" +
                            "<label>Size</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>available windows updates Size</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>252</posY>" +
                            "<width>198</width>" +
                            "<label>#UpdateControl.AvailableUpdateSize</label>" +
                            "<font>mediastream10</font>          " +
                          "</control>" +
                          "<control>" +
                            "<description>Text Update Time</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>280</posY>" +
                            "<width>198</width>" +
                            "<label>Update Time</label>" +
                            "<font>mediastream10tc</font>" +
                          "</control>" +
                          "<control>" +
                            "<description>Update.Time</description>" +
                            "<type>label</type>" +
                            "<id>1</id>" +
                            "<posX>995</posX>" +
                            "<posY>297</posY>" +
                            "<label>#UpdateControl.UpdateDate</label>" +
                            "<font>mediastream10c</font>" +
                          "</control>" +
                          "</control>" +
                      "</control>" +
                  "</controls>" +
              "</window>";
    }

    #endregion

    #region Private Methods

    string mostRecentVisibleControls(isOverlayType isOverlay)
    {
      string visibleOn = null;
      //
      //Controls to display recent movies overlay
      //
      if (isOverlay == isOverlayType.MovPics)
      {
        foreach (menuItem menItem in menuItems)
        {
          if (menItem.showMostRecent == displayMostRecent.movies)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + menItem.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + menItem.id.ToString() + ")";
          }

          // Check Submenu Level 1
          if (menItem.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < menItem.subMenuLevel1.Count; i++)
            {
              if (menItem.subMenuLevel1[i].showMostRecent == displayMostRecent.movies)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Submenu Level 2
          if (menItem.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < menItem.subMenuLevel2.Count; i++)
            {
              if (menItem.subMenuLevel2[i].showMostRecent == displayMostRecent.movies)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (menItem.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent TVSeries overlay
      //
      if (isOverlay == isOverlayType.TVSeries)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.tvSeries)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.tvSeries)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.tvSeries)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Music overlay
      //
      if (isOverlay == isOverlayType.Music)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.music)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.music)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.music)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Pictures overlay
      //
      if (isOverlay == isOverlayType.Pictures)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.pictures)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.pictures)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.pictures)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent RecordedTV overlay
      //
      if (isOverlay == isOverlayType.RecordedTV)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.recordedTV)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.recordedTV)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.recordedTV)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent SleepContol overlay
      //
      if (isOverlay == isOverlayType.sleepControl)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.sleepControl)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.sleepControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.sleepControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent DriveFreeSpace overlay
      //
      if (isOverlay == isOverlayType.freeDriveSpace)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.freeDriveSpace)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.freeDriveSpace)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.freeDriveSpace)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Stocks and Indices overlay
      //
      if (isOverlay == isOverlayType.stocks)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.stocks)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.stocks)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.stocks)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Power Control overlay
      //
      if (isOverlay == isOverlayType.powerControl)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.powerControl)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.powerControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.powerControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent HTPCInfo overlay
      //
      if (isOverlay == isOverlayType.htpcInfo)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.htpcInfo)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.htpcInfo)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.htpcInfo)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Stocks and Indices overlay
      //
      if (isOverlay == isOverlayType.stocks)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.stocks)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.stocks)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.stocks)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }
      //
      //Controls to display recent Update Control overlay
      //
      if (isOverlay == isOverlayType.updateControl)
      {
        foreach (menuItem item in menuItems)
        {
          if (item.showMostRecent == displayMostRecent.updateControl)
          {
            if (visibleOn == null)
              visibleOn = "[control.isvisible(" + item.id.ToString() + ")";
            else
              visibleOn += "|control.isvisible(" + item.id.ToString() + ")";
          }
          // Check Sunmenu Level 1
          if (item.subMenuLevel1.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel1.Count; i++)
            {
              if (item.subMenuLevel1[i].showMostRecent == displayMostRecent.updateControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 1)).ToString() + ")";
              }
            }
          }
          // Check Sunmenu Level 2
          if (item.subMenuLevel2.Count > 0)
          {
            for (int i = 0; i < item.subMenuLevel2.Count; i++)
            {
              if (item.subMenuLevel2[i].showMostRecent == displayMostRecent.updateControl)
              {
                if (visibleOn == null)
                  visibleOn = "[control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
                else
                  visibleOn += "|control.hasfocus(" + (item.subMenuLevel1ID + (i + 100 + 1)).ToString() + ")";
              }
            }
          }
        }
      }

      if ((isOverlay == isOverlayType.Music ||
           isOverlay == isOverlayType.RecordedTV ||
           isOverlay == isOverlayType.freeDriveSpace ||
           isOverlay == isOverlayType.Pictures ||
           isOverlay == isOverlayType.sleepControl ||
           isOverlay == isOverlayType.stocks ||
           isOverlay == isOverlayType.powerControl ||
           isOverlay == isOverlayType.htpcInfo ||
           isOverlay == isOverlayType.updateControl ||
           isOverlay == isOverlayType.freeDriveSpace) && visibleOn == null)
      {
        visibleOn = "No";
        return visibleOn;
      }

      if (visibleOn != null)
        visibleOn += "]";
      return visibleOn;
    }

    #endregion
  }
}
