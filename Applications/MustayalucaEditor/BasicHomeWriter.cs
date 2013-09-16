using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Linq;

namespace MustayalucaEditor
{
  public partial class formMustayalucaEditor
  {
    #region Main Generation Function

    private void generateXML(menuType direction)
    {
      string menuPos;
      string skeletonFile;

      // Sync Submenu ID's with control ID's
      foreach (menuItem item in menuItems)
      {
        if (item.subMenuLevel1.Count > 0)
          menuItems[menuItems.IndexOf(item)].subMenuLevel1ID = (menuItems[menuItems.IndexOf(item)].id - 999) * 10000;
        else
          menuItems[menuItems.IndexOf(item)].subMenuLevel1ID = 0;
      }
      // Save out configuration into mustayalucamenuprofile.xml
      writeMenuProfile(direction);
      bgItems.Clear();

      if (direction == menuType.horizontal)
      {
        menuPos = "menuYPos:" + txtMenuPos.Text;
        skeletonFile = "MustayalucaEditor.xmlFiles.HBasicHomeSkeleton.xml";
      }
      else
      {
        menuPos = "menuXPos:" + txtMenuPos.Text;
        skeletonFile = "MustayalucaEditor.xmlFiles.VBasicHomeSkeleton.xml";
      }
      if (getInfoServiceVersion().CompareTo(isWeatherVersion) >= 0)
        infoServiceDayProperty = "forecast";
      else
        infoServiceDayProperty = "day";

      Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(skeletonFile);
      StreamReader reader = new StreamReader(stream);
      xml = reader.ReadToEnd();

      string acceleration = tbAcceleration.Text;
      string duration = tbDuration.Text;

      string randomFanartGames = randomFanart.fanartGames ? "Yes" : "No";
      string randomFanartMovies = randomFanart.fanartMovies ? "Yes" : "No";
      string randomMoviesScraperFanart = randomFanart.fanartMoviesScraperFanart ? "Yes" : "No";
      string randomFanartMovingPictures = randomFanart.fanartMovingPictures ? "Yes" : "No";
      string randomFanartMusic = randomFanart.fanartMusic ? "Yes" : "No";
      string randomMusicScraperFanart = randomFanart.fanartMusicScraperFanart ? "Yes" : "No";
      string randomFanartPictures = randomFanart.fanartPictures ? "Yes" : "No";
      string randomFanartPlugins = randomFanart.fanartPlugins ? "Yes" : "No";
      string randomFanartTv = randomFanart.fanartTv ? "Yes" : "No";
      string randomFanartTVSeries = randomFanart.fanartTVSeries ? "Yes" : "No";
      string randomScoreCenterFanart = randomFanart.fanartScoreCenter ? "Yes" : "No";

      if (fanartHandlerUsed)
      {
        if (fanartHandlerRelease2)
        {
          xml = xml.Replace("<!-- BEGIN GENERATED DEFINITIONS -->"
                        , "<define>#menuitemFocus:" + focusAlpha.Text + txtFocusColour.Text + "</define>"
                        + "<define>#menuitemNoFocus:" + noFocusAlpha.Text + txtNoFocusColour.Text + "</define>"
                        + "<define>#labelFont:" + cboLabelFont.Text + "</define>"
                        + "<define>#selectedFont:" + cboSelectedFont.Text + "</define>"
                        + "<define>#" + menuPos + "</define>"
                        + "<define>#useRandomGamesUserFanart:" + randomFanartGames + "</define>"
                        + "<define>#useRandomTVSeriesFanart:" + randomFanartTVSeries + "</define>"
                        + "<define>#useRandomPluginsUserFanart:" + randomFanartPlugins + "</define>"
                        + "<define>#useRandomMovingPicturesFanart:" + randomFanartMovingPictures + "</define>"
                        + "<define>#useRandomMusicUserFanart:" + randomFanartMusic + "</define>"
                        + "<define>#useRandomMusicScraperFanart:" + randomMusicScraperFanart + "</define>"
                        + "<define>#useRandomPicturesUserFanart:" + randomFanartPictures + "</define>"
                        + "<define>#useRandomTVUserFanart:" + randomFanartTv + "</define>"
                        + "<define>#useRandomMoviesUserFanart:" + randomFanartMovies + "</define>"
                        + "<define>#useRandomMoviesScraperFanart:" + randomMoviesScraperFanart + "</define>"
                        + "<define>#useRandomScoreCenterUserFanart:" + randomScoreCenterFanart + "</define>");


        }
        else
        {
          xml = xml.Replace("<!-- BEGIN GENERATED DEFINITIONS -->"
                        , "<define>#menuitemFocus:" + focusAlpha.Text + txtFocusColour.Text + "</define>"
                        + "<define>#menuitemNoFocus:" + noFocusAlpha.Text + txtNoFocusColour.Text + "</define>"
                        + "<define>#labelFont:" + cboLabelFont.Text + "</define>"
                        + "<define>#selectedFont:" + cboSelectedFont.Text + "</define>"
                        + "<define>#" + menuPos + "</define>"
                        + "<define>#useRandomGamesFanart:" + randomFanartGames + "</define>"
                        + "<define>#useRandomTVSeriesFanart:" + randomFanartTVSeries + "</define>"
                        + "<define>#useRandomPluginsFanart:" + randomFanartPlugins + "</define>"
                        + "<define>#useRandomMovingPicturesFanart:" + randomFanartMovingPictures + "</define>"
                        + "<define>#useRandomMusicFanart:" + randomFanartMusic + "</define>"
                        + "<define>#useRandomPicturesFanart:" + randomFanartPictures + "</define>"
                        + "<define>#useRandomTVFanart:" + randomFanartTv + "</define>"
                        + "<define>#useRandomMoviesFanart:" + randomFanartMovies + "</define>"
                        + "<define>#useRandomScoreCenterFanart:" + randomScoreCenterFanart + "</define>");
        }
      }
      else
      {
        xml = xml.Replace("<!-- BEGIN GENERATED DEFINITIONS -->"
                        , "<define>#menuitemFocus:" + focusAlpha.Text + txtFocusColour.Text + "</define>"
                        + "<define>#menuitemNoFocus:" + noFocusAlpha.Text + txtNoFocusColour.Text + "</define>"
                        + "<define>#labelFont:" + cboLabelFont.Text + "</define>"
                        + "<define>#selectedFont:" + cboSelectedFont.Text + "</define>"
                        + "<define>#" + menuPos + "</define>");

      }



      // Write out Sub Menu Code
      xml = xml.Replace("<!-- BEGIN SUBMENU DUMMY CONTROLS1 -->", writeSubMenuDummyControl1());
      xml = xml.Replace("<!-- BEGIN SUBMENU DUMMY CONTROLS2 -->", writeSubMenuDummyControl2());
      xml = xml.Replace("<!-- BEGIN GENERATED SUBMENU CODE -->", bhSubMenuWriterH());



      // String will we use to hold the create xml
      StringBuilder rawXML = new StringBuilder();

      // For each defined menu item create the visibility control and the 14 controls for menu item, 2 buttons and 12 label controls
      foreach (menuItem menItem in menuItems)
      {
        fillBackgroundItem(menItem);
        // Check if this menu item is TVSeries or MovingPictures and store the menu ID for use
        // with the InfoService 3 last added items function if a match
        if (menItem.hyperlink == tvseriesSkinID)
          basicHomeValues.tvseriesControl = menItem.id;

        if (menItem.hyperlink == movingPicturesSkinID)
          basicHomeValues.movingPicturesControl = menItem.id;

        if (menItem.hyperlink == musicSkinID)
          basicHomeValues.musicControl = menItem.id;

        if (menItem.hyperlink == tvSkinID)
          basicHomeValues.tvControl = menItem.id;

        // Is this the default Item
        if (menItem.isDefault == true)
          xml = xml.Replace("<!-- BEGIN GENERATED DEFAULTCONTROL CODE-->", "<defaultcontrol>" + (menItem.id + 900).ToString() + "</defaultcontrol>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Dummy label indicating " + menItem.name + " visibility when submen open</description>");
        rawXML.AppendLine("<id>" + menItem.id.ToString() + "</id>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>100</posX>");
        rawXML.AppendLine("<posY>-100</posY>");
        rawXML.AppendLine("<width>500</width>");
        rawXML.AppendLine("<height>0</height>");
        rawXML.AppendLine("<label>-</label>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menItem.id + 700).ToString() + ")|Control.HasFocus(" + (menItem.id + 800).ToString() + ")|Control.HasFocus(" + (menItem.id + 900).ToString() + ")|control.isvisible(" + (menItem.id + 100).ToString() + ")</visible>");
        rawXML.AppendLine("</control>");

        // Write out the menu butons and lables
        for (int i = 0; i < 14; i++)
        {
          writeHorizonalMenu(i, menItem, ref rawXML);

          if (menItem.hyperlink == tvseriesSkinID)
            tvSeriesMenuID = menItem.id;
          else if (menItem.hyperlink == movingPicturesSkinID)
            MovingPicturesMenuID = menItem.id;
          else if (menItem.hyperlink == musicSkinID)
            MusicMenuID = menItem.id;
          else if (menItem.hyperlink == tvSkinID)
            TVMenuID = menItem.id;
        }
      }
      xml = xml.Replace("<!-- BEGIN GENERATED BUTTON CODE-->", rawXML.ToString());


      if (direction != menuType.vertical && subMenuL1Exists && menuStyle != chosenMenuStyle.graphicMenuStyle)
        writeHorizontalSubmenus();

    }

    #endregion

    #region Horizontal SubMenu Controls

    void writeHorizontalSubmenus()
    {

      // Are the Submenus defined, if so we need the additional blade controls
      string tmpXML = string.Empty;
      if (subMenuL1Exists)
      {
        level1LateralBladeVisible = level1LateralBladeVisible.Substring(0, (level1LateralBladeVisible.Length - 19));

        tmpXML = "<control>" +
                  "<description>Level 1 - Lateral blade control item</description>" +
                  "<type>label</type>" +
                  "<id>4242</id>" +
                  "<label></label>" +
                  "<!-- Set 'visible' to YES if you wanna have home labels displayed when lateral blade is active  -->" +
                  "<!-- Set 'visible' to FALSE if you don't wanna have inactive home labels displayed when lateral blade is active  -->" +
                  "<visible>yes</visible>" +
                "</control>" +
                "<control>" +
                  "<description>Lateral blade</description>" +
                  "<type>image</type>" +
                  "<id>11111</id>" +
                  "<posX>0</posX>" +
                  "<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu).ToString() + "</posY>" +
                  "<width>1920</width>" +
                  "<height>41</height>" +
                  "<texture>submenu_bar.png</texture>" +
                  "<visible>" + subMenuControls() +  "</visible>" +
                  "<animation effect=\"slide\" start=\"0,-41\" end=\"0,0\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>" +
                  "<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"100\" acceleration=\"-0.1\" reversible=\"false\">hidden</animation>" +
                  "<animation effect=\"slide\" end=\"0,500\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>" +
        "</control>";
      }


      tmpXML += "<!--             End of Lateral Blade Submenu Code            -->";
      xml = xml.Replace("<!-- BEGIN GENERATED LATERAL MENU CONTROL -->", tmpXML.ToString());

    }

    #endregion

    #region Generate menu Graphics Horizontal

    private void generateMenuGraphicsH()
    {
      StringBuilder rawXML = new StringBuilder();
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Menu Background</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>574</posY>");
      rawXML.AppendLine("<width>1920</width>");
      rawXML.AppendLine("<height>506</height>");
      rawXML.AppendLine("<texture>menu_bar_vignette.png</texture>");
      rawXML.AppendLine("<shouldCache>true</shouldCache>");
      rawXML.AppendLine("</control>");
      
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Menu Background</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetMymenu) + "</posY>");
      rawXML.AppendLine("<width>1920</width>");
      rawXML.AppendLine("<height>" + basicHomeValues.menuHeight + "</height>");
      rawXML.AppendLine("<texture>" + basicHomeValues.mymenu + "</texture>");
      rawXML.AppendLine("<shouldCache>true</shouldCache>");
      rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
      rawXML.AppendLine("</control>");


      if (enableRssfeed.Checked && infoserviceOptions.Enabled)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>RSS Display Bar (No Sub Menu Position)</description>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>" + basicHomeValues.subMenuXpos + "</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu).ToString() + "</posY>");
        rawXML.AppendLine("<width>" + basicHomeValues.subMenuWidth + "</width>");
        rawXML.AppendLine("<height>" + basicHomeValues.subMenuHeight.ToString() + "</height>");
        rawXML.AppendLine("<texture>" + basicHomeValues.mymenu_submenu + "</texture>");
        rawXML.AppendLine("<visible>![" + subMenuControls() + "]</visible>");
        rawXML.AppendLine("<shouldCache>true</shouldCache>");
        rawXML.AppendLine("<animation effect=\"slide\" start=\"0,41\" end=\"0,0\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">visible</animation>");
        rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>RSS Display Bar (Sub Menu Position)</description>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>" + basicHomeValues.subMenuXpos + "</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetSubmenu + 41).ToString() + "</posY>");
        rawXML.AppendLine("<width>" + basicHomeValues.subMenuWidth + "</width>");
        rawXML.AppendLine("<height>" + basicHomeValues.subMenuHeight.ToString() + "</height>");
        rawXML.AppendLine("<texture>" + basicHomeValues.mymenu_submenu + "</texture>");
        rawXML.AppendLine("<shouldCache>true</shouldCache>");
        rawXML.AppendLine("<visible>" + subMenuControls() + "</visible>");
        rawXML.AppendLine("<animation effect=\"slide\" start=\"0,-41\" end=\"0,0\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" time=\"100\" acceleration=\"-0.1\" reversible=\"false\">hidden</animation>");
        rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>");
        rawXML.AppendLine("</control>");

      }
      xml = xml.Replace("<!-- BEGIN GENERATED MENUGRAPHICS CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Weather Vertical

    private void generateWeatherV()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Weather image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>43001</id>");
      rawXML.AppendLine("<posX>987</posX>");
      rawXML.AppendLine("<posY>9</posY>");
      rawXML.AppendLine("<height>53</height>");
      rawXML.AppendLine("<width>55</width>");
      rawXML.AppendLine("<centered>yes</centered>");
      rawXML.AppendLine("<texture>#infoservice.weather.today.img.small.fullpath</texture>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Temperature</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>400</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<posX>1250</posX>");
      rawXML.AppendLine("<posY>22</posY>");
      rawXML.AppendLine("<font>mediastream14c</font>");
      rawXML.AppendLine("<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>condition</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>400</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<posX>1048</posX>");
      rawXML.AppendLine("<posY>15</posY>");
      rawXML.AppendLine("<font>mediastream10tc</font>");
      rawXML.AppendLine("<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "400,0" + quote + " end=" + quote + "0,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "400,0" + quote + " tween=" + quote + "quadratic" + quote + " easing=" + quote + "in" + quote + " time=" + quote + " 400" + quote + " delay=" + quote + "200" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED WEATHER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate RSS Ticker Horizontal

    private void generateRSSTicker()
    {
      StringBuilder rawXML = new StringBuilder();

      switch (rssImage)
      {
        case rssImageType.infoserviceImage:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>RSS Feed image (Default Skin Image with no Sub Menu)</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<id>0</id>");
          rawXML.AppendLine("<width>34</width>");
          rawXML.AppendLine("<height>34</height>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssImage).ToString() + "</posY>");
          rawXML.AppendLine("<posX>1</posX>");
          rawXML.AppendLine("<texture>#infoservice.feed.img</texture>");
          rawXML.AppendLine("<shouldCache>true</shouldCache>");
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+![" + subMenuControls() + "]</visible>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"0,41\" end=\"0,0\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");

          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>RSS Feed image (Default Skin Image with Sub Menu)</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<id>0</id>");
          rawXML.AppendLine("<width>34</width>");
          rawXML.AppendLine("<height>34</height>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssText + 41).ToString() + "</posY>");
          rawXML.AppendLine("<posX>1</posX>");
          rawXML.AppendLine("<texture>#infoservice.feed.img</texture>");
          rawXML.AppendLine("<shouldCache>true</shouldCache>");
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[" + subMenuControls() + "]</visible>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"0,-41\" end=\"0,0\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">visible</animation>");
          rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" time=\" 100\" acceleration=\" -0.1\" reversible=\"false\">hidden</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;

        case rssImageType.skinImage:
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>RSS Feed image (Default Skin Image with no Sub Menu)</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<id>0</id>");
          rawXML.AppendLine("<width>34</width>");
          rawXML.AppendLine("<height>34</height>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssImage).ToString() + "</posY>");
          rawXML.AppendLine("<posX>1</posX>");
          rawXML.AppendLine("<texture>InfoService\\defaultFeedItemAtom.png</texture>");
          rawXML.AppendLine("<shouldCache>true</shouldCache>");
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+![" + subMenuControls() + "]</visible>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"0,41\" end=\"0,0\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");

          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>RSS Feed image (Default Skin Image with Sub Menu)</description>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<id>0</id>");
          rawXML.AppendLine("<width>34</width>");
          rawXML.AppendLine("<height>34</height>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssText + 41).ToString() + "</posY>");
          rawXML.AppendLine("<posX>1</posX>");
          rawXML.AppendLine("<texture>InfoService\\defaultFeedItemAtom.png</texture>");
          rawXML.AppendLine("<shouldCache>true</shouldCache>");
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[" + subMenuControls() + "]</visible>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"0,-41\" end=\"0,0\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">visible</animation>");
          rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" time=\" 100\" acceleration=\" -0.1\" reversible=\"false\">hidden</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">windowclose</animation>");
          rawXML.AppendLine("</control>");
          break;
      }

      rawXML.AppendLine("<control Style=\"NoShadow\">");
      rawXML.AppendLine("<description>RSS Items (No Sub Menu Position)</description>");
      rawXML.AppendLine("<type>fadelabel</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<width>1895</width>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssText + 2).ToString() + "</posY>");
      rawXML.AppendLine("<posX>35</posX>");
      rawXML.AppendLine("<textcolor>ff6d6d6d</textcolor>");
      rawXML.AppendLine("<label>#infoservice.feed.titles</label>");
      rawXML.AppendLine("<wrapString> :: </wrapString>");
      rawXML.AppendLine("<visible>![" + subMenuControls() + "]</visible>");
      rawXML.AppendLine("<animation effect=\"slide\" start=\"0,41\" end=\"0,0\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>");
      rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">windowclose</animation>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control Style=\"NoShadow\">");
      rawXML.AppendLine("<description>RSS Items (Sub Menu Position)</description>");
      rawXML.AppendLine("<type>fadelabel</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<width>1895</width>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssText + 43).ToString() + "</posY>");
      rawXML.AppendLine("<posX>35</posX>");
      rawXML.AppendLine("<textcolor>ff6d6d6d</textcolor>");
      rawXML.AppendLine("<label>#infoservice.feed.titles</label>");
      rawXML.AppendLine("<wrapString> :: </wrapString>");
      rawXML.AppendLine("<visible>" + subMenuControls() + "</visible>");
      rawXML.AppendLine("<animation effect=\"slide\" start=\"0,-41\" end=\"0,0\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" time=\" 100\" acceleration=\" -0.1\" reversible=\"false\">hidden</animation>");
      rawXML.AppendLine("<animation effect=\"slide\" end=\"0,500\" time=\" 250\" acceleration=\" -0.1\" reversible=\"false\">windowclose</animation>");
      rawXML.AppendLine("</control>");


      xml = xml.Replace("<!-- BEGIN GENERATED RSS TICKER CODE-->", rawXML.ToString());

    }

    #endregion

    #region Generate Topbar Vertical Old Style

    private void generateTopBarV1()
    {
      StringBuilder rawXML = new StringBuilder();
      const string quote = "\"";

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Lightening background</description>");
      rawXML.AppendLine("\t\t\t<id>0</id>");
      rawXML.AppendLine("\t\t\t<type>image</type>");
      rawXML.AppendLine("\t\t\t<posx>0</posx>");
      rawXML.AppendLine("\t\t\t<posy>0</posy>");
      rawXML.AppendLine("\t\t\t<width>1920</width>");
      rawXML.AppendLine("\t\t\t<height>1080</height>");
      rawXML.AppendLine("\t\t\t<texture>minimenubg.png</texture>");
      rawXML.AppendLine("\t\t\t<shouldCache>true</shouldCache>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "200" + quote + " end=" + quote + "100" + quote + " time=" + quote + "1000" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>Control.HasFocus(7777)|Control.HasFocus(7888)|Control.HasFocus(7999)|Control.HasFocus(79999)</visible>");      
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Exit Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<posY>435</posY>");
      rawXML.AppendLine("<posX>640</posX>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>#Mustayaluca.MediaPortalExit</label>");
      rawXML.AppendLine("<visible allowhiddenfocus=" + quote + "true" + quote + ">Control.HasFocus(7777)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Restart Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<posY>435</posY>");
      rawXML.AppendLine("<posX>640</posX>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>#Mustayaluca.MediaPortalRestart</label>");
      rawXML.AppendLine("<visible>Control.HasFocus(7888)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Shutdown Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<posY>435</posY>");
      rawXML.AppendLine("<posX>640</posX>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>#Mustayaluca.MediaPortalShutDownMenu</label>");
      rawXML.AppendLine("<visible>Control.HasFocus(7999)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Settings Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<posY>435</posY>");
      rawXML.AppendLine("<posX>640</posX>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>#Mustayaluca.MediaPortalSettings</label>");
      rawXML.AppendLine("<visible>Control.HasFocus(79999)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Shutdown menu hbar</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<posX>385</posX>");
      rawXML.AppendLine("<posY>335</posY>");
      rawXML.AppendLine("<width>511</width>");
      rawXML.AppendLine("<height>1</height>");
      rawXML.AppendLine("<texture>hbar1white.png</texture>");
      rawXML.AppendLine("<visible>Control.HasFocus(7888)|Control.HasFocus(7999)|Control.HasFocus(7777)|Control.HasFocus(79999)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("\t\t\t<description>Exit Button</description>");
      rawXML.AppendLine("\t\t\t<type>button</type>");
      rawXML.AppendLine("\t\t\t<id>7777</id>");
      rawXML.AppendLine("\t\t\t<posX>390</posX>");
      rawXML.AppendLine("\t\t\t<posY>350</posY>");
      rawXML.AppendLine("\t\t\t<onleft>" + (menuItems[basicHomeValues.defaultId].id + 900).ToString() + "</onleft>");
      rawXML.AppendLine("\t\t\t<onright>7888</onright>");
      rawXML.AppendLine("\t\t\t<width>80</width>");
      rawXML.AppendLine("\t\t\t<height>80</height>");
      rawXML.AppendLine("\t\t\t<textureNoFocus>exit_button.png</textureNoFocus>");
      rawXML.AppendLine("\t\t\t<textureFocus>exit_button.png</textureFocus>");
      rawXML.AppendLine("\t\t\t<action>97</action>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
      rawXML.AppendLine("\t\t</control>");
     
      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Restart Button</description>");
      rawXML.AppendLine("\t\t\t<type>button</type>");
      rawXML.AppendLine("\t\t\t<id>7888</id>");
      rawXML.AppendLine("\t\t\t<posX>530</posX>");
      rawXML.AppendLine("\t\t\t<posY>350</posY>");
      rawXML.AppendLine("\t\t\t<onleft>7777</onleft>");
      rawXML.AppendLine("\t\t\t<onright>7999</onright>");
      rawXML.AppendLine("\t\t\t<width>80</width>");
      rawXML.AppendLine("\t\t\t<height>80</height>");
      rawXML.AppendLine("\t\t\t<textureNoFocus>restart_button.png</textureNoFocus>");
      rawXML.AppendLine("\t\t\t<textureFocus>restart_button.png</textureFocus>");
      rawXML.AppendLine("\t\t\t<action>196250</action>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Shutdown Button</description>");
      rawXML.AppendLine("\t\t\t<type>button</type>");
      rawXML.AppendLine("\t\t\t<id>7999</id>");
      rawXML.AppendLine("\t\t\t<posX>670</posX>");
      rawXML.AppendLine("\t\t\t<posY>350</posY>");
      rawXML.AppendLine("\t\t\t<onleft>7888</onleft>");
      rawXML.AppendLine("\t\t\t<onright>79999</onright>");
      rawXML.AppendLine("\t\t\t<width>80</width>");
      rawXML.AppendLine("\t\t\t<height>80</height>");
      rawXML.AppendLine("\t\t\t<textureNoFocus>shutdown_button.png</textureNoFocus>");
      rawXML.AppendLine("\t\t\t<textureFocus>shutdown_button.png</textureFocus>");
      rawXML.AppendLine("\t\t\t<action>99</action>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Settings Button</description>");
      rawXML.AppendLine("\t\t\t<type>button</type>");
      rawXML.AppendLine("\t\t\t<id>79999</id>");
      rawXML.AppendLine("\t\t\t<posX>810</posX>");
      rawXML.AppendLine("\t\t\t<posY>350</posY>");
      rawXML.AppendLine("\t\t\t<onleft>7999</onleft>");
      rawXML.AppendLine("\t\t\t<onright>17</onright>");
      rawXML.AppendLine("\t\t\t<width>80</width>");
      rawXML.AppendLine("\t\t\t<height>80</height>");
      rawXML.AppendLine("\t\t\t<textureNoFocus>settings_button.png</textureNoFocus>");
      rawXML.AppendLine("\t\t\t<textureFocus>settings_button.png</textureFocus>");
      rawXML.AppendLine("\t\t\t<hyperlink>4</hyperlink>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " time=" + quote + "400" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
      rawXML.AppendLine("\t\t</control>");

      rawXML.AppendLine("\t\t<control>");
      rawXML.AppendLine("\t\t\t<description>Icon Fix</description>");
      rawXML.AppendLine("\t\t\t<id>0</id>");
      rawXML.AppendLine("\t\t\t<type>image</type>");
      rawXML.AppendLine("\t\t\t<posx>0</posx>");
      rawXML.AppendLine("\t\t\t<posy>0</posy>");
      rawXML.AppendLine("\t\t\t<width>1920</width>");
      rawXML.AppendLine("\t\t\t<height>1080</height>");
      rawXML.AppendLine("\t\t\t<texture>tv_black.png</texture>");
      rawXML.AppendLine("\t\t\t<animation effect=" + quote + "fade" + quote + " start=" + quote + "200" + quote + " end=" + quote + "100" + quote + " time=" + quote + "1000" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("\t\t\t<visible>!Control.HasFocus(7777)+!Control.HasFocus(7888)+!Control.HasFocus(7999)+!Control.HasFocus(79999)</visible>");
      rawXML.AppendLine("\t\t</control>");    


      xml = xml.Replace("<!-- BEGIN GENERATED TOPBAR CODE OLD STYLE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Topbar Horizontal
    
    //
    // These controls need to be placed before the background controls otherwise 
    // they the hidden animation is not evalulated correctly
    //
    private void generateTopBarHButtons()
    {
      StringBuilder rawXML = new StringBuilder();

      foreach (menuItem menItem in menuItems)
      {
        string tempControls;
        String topMenuId = (menItem.id + 600).ToString();
     
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Dummy label indicating " + menItem.name + " menu visibility</description>");
        rawXML.AppendLine("<id>" + (menItem.id + 100).ToString() + "</id>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>100</posX>");
        rawXML.AppendLine("<posY>-100</posY>");
        rawXML.AppendLine("<width>500</width>");
        rawXML.AppendLine("<height>0</height>");
        rawXML.AppendLine("<label>" + menItem.name + "</label>");

        if (menItem.showMostRecent == displayMostRecent.recordedTV)
          tempControls = "|Control.HasFocus(91919984)|Control.HasFocus(91919985)|Control.HasFocus(91919986)</visible>";
        else
          tempControls = "</visible>";

        rawXML.AppendLine("<visible>Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)|Control.HasFocus(" + topMenuId + "04)" + tempControls);
        rawXML.AppendLine("</control>");
      }
      xml = xml.Replace("<!-- BEGIN GENERATED TOPBAR BUTTONS -->", rawXML.ToString());
    }
    //
    // These controls need to be placed after the background controls so the layer order is correct
    //
    private void generateTopBarH()
    {

      generateTopBarHButtons();

      int twitterHeight = 0;
      basicHomeValues.offsetButtons = (int.Parse(txtMenuPos.Text) - 8);

      StringBuilder rawXML = new StringBuilder();

      foreach (menuItem menItem in menuItems)
      {
        String topMenuId = (menItem.id + 600).ToString();

        rawXML.AppendLine("<!-- /Start Topbar buttons for " + menItem.name + " -->");

        rawXML.AppendLine("<control><description>Topbar buttons " + menItem.name + "</description>");
        rawXML.AppendLine("<type>group</type>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>810</posX>");
        rawXML.AppendLine("<posY>-5</posY>");
        rawXML.AppendLine("<height>73</height>");
        rawXML.AppendLine("<width>300</width>");
        if (useAeonGraphics.Checked)
        {
          rawXML.AppendLine("<texture>3buttonbar-a.png</texture>");
        }
        else
        {
          rawXML.AppendLine("<texture>3buttonbar.png</texture>");
        }
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)|Control.HasFocus(" + topMenuId + "04)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>830</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<texture>exit_button.png</texture>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)|Control.HasFocus(" + topMenuId + "04)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>900</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<texture>restart_button.png</texture>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)|Control.HasFocus(" + topMenuId + "04)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>970</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<texture>shutdown_button.png</texture>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)|Control.HasFocus(" + topMenuId + "04)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>1040</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<texture>settings_button.png</texture>");
        rawXML.AppendLine("<visible>control.isvisible(" + menItem.id + ")+Control.HasFocus(" + topMenuId + "01)|Control.HasFocus(" + topMenuId + "02)|Control.HasFocus(" + topMenuId + "03)|Control.HasFocus(" + topMenuId + "04)</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>BasicHome button button</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "01</id>");
        rawXML.AppendLine("<posX>830</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<textureFocus>exit_button.png</textureFocus>");
        rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("<label>-</label>");
        rawXML.AppendLine("<action>97</action> ");
        rawXML.AppendLine("<onleft>" + topMenuId + "03</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "02</onright>");
        rawXML.AppendLine("<onup>" + topMenuId + "01</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 900).ToString() + "</ondown>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible allowhiddenfocus=\"true\">control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Exit button</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "02</id>");
        rawXML.AppendLine("<posX>900</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<textureFocus>restart_button.png</textureFocus>");
        rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("<label>-</label>");
        rawXML.AppendLine("<action>196250</action>");
        rawXML.AppendLine("<onleft>" + topMenuId + "01</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "03</onright>");
        rawXML.AppendLine("<onup>" + topMenuId + "02</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 900).ToString() + "</ondown>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible allowhiddenfocus=\"true\">control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Shutdown button</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "03</id>");
        rawXML.AppendLine("<posX>970</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<textureFocus>shutdown_button.png</textureFocus>");
        rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("<label>-</label>");
        rawXML.AppendLine("<action>99</action>");
        rawXML.AppendLine("<onleft>" + topMenuId + "02</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "04</onright>");
        rawXML.AppendLine("<onup>" + topMenuId + "03</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 900).ToString() + "</ondown>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible allowhiddenfocus=\"true\">control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");
        rawXML.AppendLine("<animation effect=\"slide\" start=\"0,-" + basicHomeValues.Button3Slide.ToString() + "\" time=\"600\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>Shutdown button</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + topMenuId + "04</id>");
        rawXML.AppendLine("<posX>1040</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetButtons - twitterHeight).ToString() + "</posY>");
        rawXML.AppendLine("<width>50</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<textureFocus>settings_button.png</textureFocus>");
        rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("<label>-</label>");
        rawXML.AppendLine("<hyperlink>4</hyperlink>");
        rawXML.AppendLine("<onleft>" + topMenuId + "03</onleft>");
        rawXML.AppendLine("<onright>" + topMenuId + "01</onright>");
        rawXML.AppendLine("<onup>" + topMenuId + "04</onup>");
        rawXML.AppendLine("<ondown>" + (menItem.id + 900).ToString() + "</ondown>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "1,1" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "1000" + quote + " tween=" + quote + "back" + quote + " ease=" + quote + "out" + quote + ">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "100,100" + quote + " end=" + quote + "125,125" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">focus</animation>");
        rawXML.AppendLine("<animation effect=" + quote + "zoom" + quote + " start=" + quote + "125,125" + quote + " end=" + quote + "100,100" + quote + " center=" + quote + "0,0" + quote + " time=" + quote + "400" + quote + " acceleration=" + quote + "-0.9" + quote + " reversible=" + quote + "false" + quote + ">unfocus</animation>");
        rawXML.AppendLine("<visible allowhiddenfocus=\"true\">control.isvisible(" + menItem.id + ")</visible>");
        rawXML.AppendLine("</control>");
        rawXML.AppendLine("<animation effect=\"slide\" start=\"0,-" + basicHomeValues.Button3Slide.ToString() + "\" time=\"600\" acceleration=\"-0.1\" reversible=\"false\">Visible</animation>");

        rawXML.AppendLine("</control> <!-- /End Topbar buttons for " + menItem.name + " -->");

      }

      xml = xml.Replace("<!-- BEGIN GENERATED TOPBAR CODE -->", rawXML.ToString());
    }

    #endregion

    #region Crowding Fix Horizontal

    private void generateCrowdingFixH()
    {

      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<!--            Crowding Fix                   -->");

      for (int k = 0; k < menuItems.Count; k++)
      {
        string topBarButtons = string.Empty;
        string submenuControl = string.Empty;
        string mostRecentButtons = string.Empty;

        if (menuItems[k].showMostRecent == displayMostRecent.movies)
          mostRecentButtons = "|control.hasfocus(91919991)|control.hasfocus(91919992)|control.hasfocus(91919993)";

        if (menuItems[k].showMostRecent == displayMostRecent.tvSeries)
          mostRecentButtons = "|control.hasfocus(91919994)|control.hasfocus(91919995)|control.hasfocus(91919996)";

        if (menuItems[k].showMostRecent == displayMostRecent.music)
          mostRecentButtons = "|control.hasfocus(91919997)|control.hasfocus(91919998)|control.hasfocus(91919999)";

        if (menuItems[k].showMostRecent == displayMostRecent.recordedTV)
          mostRecentButtons = "|control.hasfocus(91919984)|control.hasfocus(91919985)|control.hasfocus(91919986)";

        int first = k - 2;
        if (first < 0) first += menuItems.Count;

        int second = k - 1;
        if (second < 0) second += menuItems.Count;

        int third = k + 1;
        if (third >= menuItems.Count) third -= menuItems.Count;

        int fourth = k + 2;
        if (fourth >= menuItems.Count) fourth -= menuItems.Count;

        if (menuItems[k].subMenuLevel1.Count > 0)
          submenuControl = "|control.isvisible(" + menuItems[k].subMenuLevel1ID.ToString() + ")";

        topBarButtons = "|control.isvisible(" + (menuItems[k].id + 100).ToString() + ")";

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>home " + menuItems[k].name + "</description>");
        rawXML.AppendLine("<type>button</type>");
        rawXML.AppendLine("<id>" + (menuItems[k].id + 900).ToString() + "</id>");
        rawXML.AppendLine("<posX>0</posX>");
        rawXML.AppendLine("<posY>-30</posY>");
        rawXML.AppendLine("<label>" + menuItems[k].name + "</label>");
        rawXML.AppendLine("<width>540</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<hyperlink>" + menuItems[k].hyperlink + "</hyperlink>");
        if (menuItems[k].hyperlinkParameter != "false")
          rawXML.AppendLine("<hyperlinkParameter>" + menuItems[k].hyperlinkParameter + "</hyperlinkParameter>");
        rawXML.AppendLine("<textureFocus>-</textureFocus>");
        rawXML.AppendLine("<textureNoFocus>-</textureNoFocus>");
        rawXML.AppendLine("<hover>-</hover>");
        rawXML.AppendLine("<onleft>" + (menuItems[second].id + 800).ToString() + "</onleft>");
        rawXML.AppendLine("<onright>" + (menuItems[third].id + 700).ToString() + "</onright>");

        if (menuItems[k].subMenuLevel1.Count > 0)
          rawXML.AppendLine("<ondown>" + (menuItems[k].subMenuLevel1ID + 1).ToString() + "</ondown>");
        else
          rawXML.AppendLine("<ondown>" + (menuItems[k].id + 900).ToString() + "</ondown>");

        if (menuItems[k].showMostRecent == displayMostRecent.movies && cbMostRecentMovPics.Checked)
          rawXML.AppendLine("<onup>91919991</onup>");
        else if (menuItems[k].showMostRecent == displayMostRecent.tvSeries && cbMostRecentTvSeries.Checked)
          rawXML.AppendLine("<onup>91919994</onup>");
        else if (menuItems[k].showMostRecent == displayMostRecent.music && cbEnableRecentMusic.Checked)
          rawXML.AppendLine("<onup>91919997</onup>");
        else if (menuItems[k].showMostRecent == displayMostRecent.recordedTV && cbEnableRecentRecordedTV.Checked)
          rawXML.AppendLine("<onup>91919984</onup>");
        else
        {
          rawXML.AppendLine("<onup>" + (menuItems[k].id + 600).ToString() + "01</onup>");
        }
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " start=" + quote + "0,-100" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.4" + quote + " reversible=" + quote + "false" + quote + ">Visible</animation>");
        rawXML.AppendLine("</control>	");

        // ************ FIRST
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>home " + menuItems[first].name + "</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>30</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
        rawXML.AppendLine("<width>540</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<label>" + menuItems[first].name + "</label>");
        rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
        rawXML.AppendLine("<font>#labelFont</font>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[k].id + 900).ToString() + ")" + submenuControl + topBarButtons + mostRecentButtons + "</visible>");
        rawXML.AppendLine("<animation effect=\"slide\" start=\"-160,0\" end=\"-160,0\" time=\"4\" acceleration=\"-0.0\" reversible=\"false\">WindowOpen</animation><!-- needed to display item at negative offset -->");
        if (!menuItems[k].isDefault)
          rawXML.AppendLine("<animation effect=\"slide\" start=\"-160,0\" end=\"-160,0\" time=\"0\" acceleration=\"-0.0\" reversible=\"false\">Visible</animation><!-- needed to display item at negative offset -->");

        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("</control>");

        // ************** SECOND
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>home " + menuItems[second].name + "</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>270</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
        rawXML.AppendLine("<width>540</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<label>" + menuItems[second].name + "</label>");
        rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
        rawXML.AppendLine("<font>#labelFont</font>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[k].id + 900).ToString() + ")" + submenuControl + topBarButtons + mostRecentButtons + "</visible>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("</control>	");

        // ******** CENTER
        if (cbDropShadow.Checked)
        {
          rawXML.AppendLine("<control>");
          rawXML.AppendLine("<description>home " + menuItems[k].name + "</description>");
          rawXML.AppendLine("<type>label</type>");
          rawXML.AppendLine("<posX>691</posX>");
          rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset + 1) + "</posY>");
          rawXML.AppendLine("<width>540</width>");
          rawXML.AppendLine("<height>50</height>");
          rawXML.AppendLine("<label>" + menuItems[k].name + "</label>");
          rawXML.AppendLine("<textcolor>black</textcolor>");
          rawXML.AppendLine("<font>#selectedFont</font>");
          rawXML.AppendLine("<align>center</align>");
          rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[k].id + 900).ToString() + ")" + submenuControl + topBarButtons + mostRecentButtons + "</visible>");
          rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
          rawXML.AppendLine("</control>	");
        }
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>home " + menuItems[k].name + "</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>690</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
        rawXML.AppendLine("<width>540</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<label>" + menuItems[k].name + "</label>");
        rawXML.AppendLine("<textcolor>#menuitemFocus</textcolor>");
        rawXML.AppendLine("<font>#selectedFont</font>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[k].id + 900).ToString() + ")" + submenuControl + topBarButtons + mostRecentButtons + "</visible>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("</control>	");

        // ******** THIRD
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>home " + menuItems[third].name + "</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>1170</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
        rawXML.AppendLine("<width>540</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<label>" + menuItems[third].name + "</label>");
        rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
        rawXML.AppendLine("<font>#labelFont</font>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[k].id + 900).ToString() + ")" + submenuControl + topBarButtons + mostRecentButtons + "</visible>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("</control>	");

        // *************** FOURTH
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>home " + menuItems[fourth].name + "</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<posX>1650</posX>");
        rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.textYOffset) + "</posY>");
        rawXML.AppendLine("<width>540</width>");
        rawXML.AppendLine("<height>50</height>");
        rawXML.AppendLine("<label>" + menuItems[fourth].name + "</label>");
        rawXML.AppendLine("<textcolor>#menuitemNoFocus</textcolor>");
        rawXML.AppendLine("<font>#labelFont</font>");
        rawXML.AppendLine("<align>center</align>");
        rawXML.AppendLine("<visible>Control.HasFocus(" + (menuItems[k].id + 900).ToString() + ")" + submenuControl + topBarButtons + mostRecentButtons + "</visible>");
        rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,300" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
        rawXML.AppendLine("</control>	");
      }
      xml = xml.Replace("<!-- BEGIN CROWDING FIX CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Item Backgrounds

    private void generateBg(menuType direction)
    {
      StringBuilder rawXML = new StringBuilder();

      foreach (backgroundItem item in bgItems)
      {
        string subMenu = item.subMenuID.ToString();
        //
        // Main controls - these deal with random fanart
        //

        //sort out fanart handler....
        if (item.fanartHandlerEnabled && fanartHandlerRelease2)
        {
          switch (item.fanartPropery.ToLower())
          {
            case "games":
              fhUserDef = ".userdef";
              break;
            case "movie":
              if (item.fhBGSource == fanartSource.Scraper)
                fhUserDef = ".scraper";
              else
                fhUserDef = ".userdef";
              break;
            case "music":
              if (item.fhBGSource == fanartSource.Scraper)
                fhUserDef = ".scraper";
              else
                fhUserDef = ".userdef";
              break;
            case "picture":
              fhUserDef = ".userdef";
              break;
            case "plugins":
              fhUserDef = ".userdef";
              break;
            case "scorecenter":
              fhUserDef = ".userdef";
              break;
            case "tv":
              fhUserDef = ".userdef";
              break;
            default:
              fhUserDef = string.Empty;
              break;
          }
        }

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>" + item.name + " BACKGROUND 1</description>");
        if (item.fanartHandlerEnabled)
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "1</id>");
        else
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "</id>");


        if (weatherBGlink.Checked && item.isWeather)
        {
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>animations\\linkedweather\\#WorldWeather.TodayIconNumber.jpg</texture>");
          rawXML.AppendLine("<shouldCache>true</shouldCache>");
        }
        else
        {
          if (item.fanartHandlerEnabled)
          {
            rawXML.AppendLine("<type>image</type>");
            rawXML.AppendLine("<texture>#fanarthandler." + item.fanartPropery + fhUserDef + ".backdrop1.any</texture>");

          }
          else
          {
            rawXML.AppendLine("<type>image</type>");
            rawXML.AppendLine("<texture>" + item.image + "</texture>");
            rawXML.AppendLine("<shouldCache>true</shouldCache>");
          }
        }

        rawXML.AppendLine("<posx>0</posx>");
        rawXML.AppendLine("<posy>0</posy>");
        rawXML.AppendLine("<width>1920</width>");
        rawXML.AppendLine("<height>1080</height>");

        if (item.fanartHandlerEnabled)
          rawXML.Append("<visible>[");
        else
          rawXML.Append("<visible>");


        for (int i = 0; i < item.ids.Count; i++)
        {
          if (i == 0)
          {
            if (subMenu == "0")
              rawXML.Append("control.isvisible(" + item.ids[i] + ")");
            else
              rawXML.Append("control.isvisible(" + subMenu + ")|control.isvisible(" + item.ids[i] + ")");
          }
          else
            rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
        }

        if (item.fanartHandlerEnabled)
        {
          if (item.EnableMusicNowPlayingFanart)
            rawXML.Append("]+control.isvisible(91919297)+![control.isvisible(91919294)+Player.HasMedia]</visible>");
          else
            rawXML.Append("]+control.isvisible(91919297)</visible>");
        }
        else
          rawXML.Append("</visible>");

        if (validForMPVersion("1.1.5.0"))
          rawXML.AppendLine("<animation effect=\"fade\"  start=\"100\" end=\"0\" time=\"600\" reversible=\"false\">Hidden</animation>");

        rawXML.AppendLine("<animation effect=\"fade\" start=\"30\" end=\"100\" time=\"600\" reversible=\"false\">Visible</animation>");

        if (cbAnimateBackground.Checked)
        {
          rawXML.AppendLine("<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
        }
        rawXML.AppendLine("</control>");

        // Add second background control for random fanart provided 
        if (item.fanartHandlerEnabled)
        {
          rawXML.AppendLine("<control>");

          rawXML.AppendLine("<description>" + item.name + " BACKGROUND 2</description>");
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "2</id>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>#fanarthandler." + item.fanartPropery + fhUserDef + ".backdrop2.any</texture>");
          rawXML.AppendLine("<posx>0</posx>");
          rawXML.AppendLine("<posy>0</posy>");
          rawXML.AppendLine("<width>1920</width>");
          rawXML.AppendLine("<height>1080</height>");
          rawXML.Append("<visible>[");

          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
            {
              if (subMenu == "0")
                rawXML.Append("control.isvisible(" + item.ids[i] + ")");
              else
                rawXML.Append("control.isvisible(" + subMenu + ")|control.isvisible(" + item.ids[i] + ")");
            }
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }

          if (item.EnableMusicNowPlayingFanart)
            rawXML.Append("]+control.isvisible(91919298)+![control.isvisible(91919294)+Player.HasMedia]</visible>");
          else
            rawXML.Append("]+control.isvisible(91919298)</visible>");

          if (validForMPVersion("1.1.5.0"))
            rawXML.AppendLine("<animation effect=\"fade\"  start=\"100\" end=\"0\" time=\"600\" reversible=\"false\">Hidden</animation>");

          rawXML.AppendLine("<animation effect=\"fade\" start=\"30\" end=\"100\" time=\"600\" reversible=\"false\">Visible</animation>");

          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("</control>");
        }

        //
        // Additional two controls that will be added if EnableMusicNowPlayingFanart is true, i.e. for thsi item if music is play display 
        // fanart for the artist.
        //
        if (item.EnableMusicNowPlayingFanart)
        {
          //
          // Control 1
          //
          rawXML.AppendLine("<control>");

          rawXML.AppendLine("<description>" + item.name + " NOW PLAYING BACKGROUND 1</description>");
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "3</id>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>#fanarthandler.music.backdrop1.play</texture>");
          rawXML.AppendLine("<posx>0</posx>");
          rawXML.AppendLine("<posy>0</posy>");
          rawXML.AppendLine("<width>1920</width>");
          rawXML.AppendLine("<height>1080</height>");
          rawXML.Append("<visible>[");
          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
            {
              if (subMenu == "0")
                rawXML.Append("control.isvisible(" + item.ids[i] + ")");
              else
                rawXML.Append("control.isvisible(11111)|control.isvisible(" + item.ids[i] + ")");
            }
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }
          rawXML.Append("]+Player.HasMedia+control.isvisible(91919295)</visible>");
          if (validForMPVersion("1.1.5.0"))
            rawXML.AppendLine("<animation effect=\"fade\"  start=\"100\" end=\"0\" time=\"600\" reversible=\"false\">Hidden</animation>");

          rawXML.AppendLine("<animation effect=\"fade\" start=\"30\" end=\"100\" time=\"600\" reversible=\"false\">Visible</animation>");

          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("</control>");

          //
          // Control 2
          //
          rawXML.AppendLine("<control>");

          rawXML.AppendLine("<description>" + item.name + " NOW PLAYING BACKGROUND 2</description>");
          rawXML.AppendLine("<id>" + (int.Parse(item.ids[0]) + 200).ToString() + "4</id>");
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>#fanarthandler.music.backdrop2.play</texture>");
          rawXML.AppendLine("<posx>0</posx>");
          rawXML.AppendLine("<posy>0</posy>");
          rawXML.AppendLine("<width>1920</width>");
          rawXML.AppendLine("<height>1080</height>");
          rawXML.Append("<visible>[");
          for (int i = 0; i < item.ids.Count; i++)
          {
            if (i == 0)
            {
              if (subMenu == "0")
                rawXML.Append("control.isvisible(" + item.ids[i] + ")");
              else
                rawXML.Append("control.isvisible(11111)|control.isvisible(" + item.ids[i] + ")");
            }
            else
              rawXML.Append("|control.isvisible(" + item.ids[i] + ")");
          }
          rawXML.Append("]+Player.HasMedia+control.isvisible(91919296)</visible>");
          if (validForMPVersion("1.1.5.0"))
            rawXML.AppendLine("<animation effect=\"fade\"  start=\"100\" end=\"0\" time=\"600\" reversible=\"false\">Hidden</animation>");

          rawXML.AppendLine("<animation effect=\"fade\" start=\"30\" end=\"100\" time=\"600\" reversible=\"false\">Visible</animation>");

          if (cbAnimateBackground.Checked)
          {
            rawXML.AppendLine("<animation effect=\"zoom\" start=\"105,105\" end=\"110,110\" time=\"20000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
            rawXML.AppendLine("<animation effect=\"slide\" start=\"0,0\" end=\"-20,-20\" time=\"10000\" tween=\"cubic\" easing=\"inout\" pulse=\"true\" reversible=\"false\" condition=\"true\">Conditional</animation>");
          }
          rawXML.AppendLine("</control>");
        }

      }

      if (cbMostRecentMovPics.Checked)
        rawXML.AppendLine("<import>BasicHome.recentlyaddedMoviesFanart.xml</import>");

      if (cbMostRecentTvSeries.Checked)
        rawXML.AppendLine("<import>BasicHome.recentlyaddedSeriesFanart.xml</import>");

      if (cbEnableRecentMusic.Checked)
        rawXML.AppendLine("<import>BasicHome.recentlyaddedMusicFanart.xml</import>");

      xml = xml.Replace("<!-- BEGIN GENERATED BACKGROUND CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Five Day Weather

    private void GenerateFiveDayWeather()
    {
      if (enableFiveDayWeather.Checked == true)
      {
        foreach (backgroundItem item in bgItems)
        {
          if (item.isWeather)
          {
            basicHomeValues.weatherControl = (int.Parse(item.ids[0]) + 200);
            if (item.fanartHandlerEnabled)
              basicHomeValues.weatherControl = int.Parse((int.Parse(item.ids[0]) + 200).ToString(0 + "1"));

            if (menuStyle == chosenMenuStyle.verticalStyle)
              generateFiveDayWeatherVertical(basicHomeValues.weatherControl);
            else if (weatherStyle == chosenWeatherStyle.bottom)
              generateFiveDayWeatherStyle1(basicHomeValues.weatherControl);
            else if (weatherStyle == chosenWeatherStyle.middle)
              generateFiveDayWeatherStyle2(basicHomeValues.weatherControl);
          }
        }
      }
    }

    #endregion

    #region Generate Five Day Weather Vertical

    private void generateFiveDayWeatherVertical(int weatherId)
    {
      StringBuilder rawXML = new StringBuilder();

      int xPos1 = 660;
      int yPos1 = 70;
      int xPos2 = 455;
      int yPos2 = 415;
      int i = 0;
      int spacing = 250;

      // Create dummy label to be used with basichome nowplaying overlay
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>5-Day Weather Dummy Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>6767</id>");
      rawXML.AppendLine("<posX>-50</posX>");
      rawXML.AppendLine("<posY>-50</posY>");
      rawXML.AppendLine("<label></label>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: Forecast BGs</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");

      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"100\" time=\"400\" reversible=\"false\">Hidden</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"100\" time=\"400\" reversible=\"false\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"100\" time=\"400\" reversible=\"false\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"100\" time=\"400\" reversible=\"false\">WindowClose</animation>");

      // ******************************** Weather Backgrounds **********************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>TODAY BG</description>");
      rawXML.AppendLine("<posX>" + (xPos1 - 200).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 155).ToString() + "</posY>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>6777</id>");
      rawXML.AppendLine("<width>180</width>");
      rawXML.AppendLine("<height>270</height>");
      rawXML.AppendLine("<texture>weather2.png</texture>");
      rawXML.AppendLine("<shouldCache>true</shouldCache>");
      rawXML.AppendLine("</control>");
      for (i = 1; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " BG</description>");
        if (i < 3)
        {
          rawXML.AppendLine("<posX>" + (xPos2 + (spacing * i)).ToString() + "</posX>");
          rawXML.AppendLine("<posY>" + yPos1.ToString() + "</posY>");
        }
        else
        {
          rawXML.AppendLine("<posX>" + (xPos2 + (spacing * (i - 2))).ToString() + "</posX>");
          rawXML.AppendLine("<posY>" + yPos2.ToString() + "</posY>");
        }
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>6777</id>");
        rawXML.AppendLine("<width>180</width>");
        rawXML.AppendLine("<height>270</height>");
        rawXML.AppendLine("<texture>weather2.png</texture>");
        rawXML.AppendLine("</control>");
      }
      rawXML.AppendLine("</control>");

      // ********************************* Weather Icons **************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>TODAY ICON</description>");
      rawXML.AppendLine("<id>0</id>");
      if (WeatherIconsAnimated.Checked)
      {
        rawXML.AppendLine("<type>multiimage</type>");
        rawXML.AppendLine("<imagepath>" + weatherIcon(0) + "</imagepath>");
        rawXML.AppendLine("<timeperimage>33</timeperimage>");
        rawXML.AppendLine("<loop>True</loop>");
      }
      else
      {
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<texture>" + weatherIcon(0) + "</texture>");
      }
      rawXML.AppendLine("<posX>" + (xPos1 - 200).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + ((yPos1 + 155) - 70).ToString() + "</posY>");
      rawXML.AppendLine("<height>180</height>");
      rawXML.AppendLine("<width>180</width>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");

      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"100\" time=\"400\" reversible=\"false\">Hidden</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"20\" end=\"100\" delay=\"100\" time=\"400\" reversible=\"false\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"100\" time=\"400\" reversible=\"false\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"100\" time=\"400\" reversible=\"false\">WindowClose</animation>");

      rawXML.AppendLine("</control>");

      for (i = 1; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("<type>multiimage</type>");
          rawXML.AppendLine("<imagepath>" + weatherIcon(i) + "</imagepath>");
          rawXML.AppendLine("<timeperimage>33</timeperimage>");
          rawXML.AppendLine("<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>" + weatherIcon(i) + "</texture>");
          rawXML.AppendLine("<shouldCache>true</shouldCache>");
        }
        if (i < 3)
        {
          rawXML.AppendLine("<posX>" + (xPos2 + (spacing * i)).ToString() + "</posX>");
          rawXML.AppendLine("<posY>" + (yPos1 - 70).ToString() + "</posY>");
        }
        else
        {
          rawXML.AppendLine("<posX>" + (xPos2 + (spacing * (i - 2))).ToString() + "</posX>");
          rawXML.AppendLine("<posY>" + (yPos2 - 70).ToString() + "</posY>");
        }
        rawXML.AppendLine("<height>180</height>");
        rawXML.AppendLine("<width>180</width>");
        if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
        else
          rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"100\" time=\"400\" reversible=\"false\">Hidden</animation>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"20\" end=\"100\" delay=\"100\" time=\"400\" reversible=\"false\">Visible</animation>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"100\" time=\"400\" reversible=\"false\">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"100\" time=\"400\" reversible=\"false\">WindowClose</animation>");

        rawXML.AppendLine("</control>");
      }
      // ************************************* Weather Text Items *******************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FULL WEATHER DETAILS</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"100\" time=\"400\" reversible=\"false\">Hidden</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"100\" time=\"400\" reversible=\"false\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"100\" time=\"400\" reversible=\"false\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"100\" time=\"400\" reversible=\"false\">WindowClose</animation>");

      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");

      else
        rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      // ************************************* Day 1 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + ((xPos1 - 200) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 245 + 155).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>6030</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + ((xPos1 - 200) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 180 + 155).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather.today.temp</label>");
      rawXML.AppendLine("<font>mediastream28tc</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      if (getInfoServiceVersion().CompareTo(isWeatherVersion) < 0)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY 1 MAX VALUE</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>" + ((xPos1 - 200) + 5).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + (yPos1 + 200 + 155).ToString() + "</posY>");
        rawXML.AppendLine("<align>left</align>");
        rawXML.AppendLine("<label>#infoservice.weather.today.maxtemp</label>");
        rawXML.AppendLine("<font>mediastream12</font>");
        rawXML.AppendLine("<textcolor>white</textcolor>");
        rawXML.AppendLine("</control>");

        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY 1 MIN VALUE</description>");
        rawXML.AppendLine("<type>label</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<posX>" + ((xPos1 - 200) + 175).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + (yPos1 + 200 + 155).ToString() + "</posY>");
        rawXML.AppendLine("<align>right</align>");
        rawXML.AppendLine("<label>#infoservice.weather.today.mintemp</label>");
        rawXML.AppendLine("<font>mediastream12</font>");
        rawXML.AppendLine("<textcolor>white</textcolor>");
        rawXML.AppendLine("</control>");
      }

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + ((xPos1 - 200) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 100 + 155).ToString() + "</posY>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<height>60</height>");
      rawXML.AppendLine("<font>mediastream13</font>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<label>#infoservice.weather.today.condition</label>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 2 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 245).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 175).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.mintemp</label>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 100).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 150).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 3 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 245).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 175).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 100).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 PM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 150).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 4 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 245).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 175).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 100).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 PM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + spacing + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 150).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 5 ****************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + (180 / 2)).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 245).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.weekday</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.maxtemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 175).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 200).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 100).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.day.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 PM WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>" + (xPos2 + (spacing * 2) + 5).ToString() + "</posX>");
      rawXML.AppendLine("<posY>" + (yPos2 + 150).ToString() + "</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<width>170</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.night.condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED FIVE DAY WEATHER CODE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Five Day Weather Horizontal (Style 1)

    private void generateFiveDayWeatherStyle1(int weatherId)
    {
      int i = 0;
      int xPos1 = 120;
      int yPos1 = int.Parse(txtMenuPos.Text) - 354;
      int spacing = 210;

      StringBuilder rawXML = new StringBuilder();

      // Create dummy label to be used with basichome nowplaying overlay
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>5-Day Weather Dummy Label</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>6767</id>");
      rawXML.AppendLine("<posX>-50</posX>");
      rawXML.AppendLine("<posY>-50</posY>");
      rawXML.AppendLine("<label></label>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      rawXML.AppendLine("</control>");

      // Group for Weather Backgrounds
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FIVE DAY WEATHER BACKGROUNDS</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"20\" end=\"100\" delay=\"100\" time=\"400\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"200\" time=\"400\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"200\" time=\"400\">WindowClose</animation>");
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " BG</description>");
        rawXML.AppendLine("<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + yPos1.ToString() + "</posY>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<width>234</width>");
        rawXML.AppendLine("<height>343</height>");
        rawXML.AppendLine("<texture>weather_poster.png</texture>");
        rawXML.AppendLine("<shouldCache>true</shouldCache>");
        rawXML.AppendLine("</control>");

      }
      rawXML.AppendLine("</control>");

      //Group for weather Icons
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FIVE DAY WEATHER ICONS</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"20\" end=\"100\" delay=\"100\" time=\"400\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"200\" time=\"400\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"200\" time=\"400\">WindowClose</animation>");
      if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      // ********************* Weather Icons **************************************
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("<type>multiimage</type>");
          rawXML.AppendLine("<imagepath>" + weatherIcon(i) + "</imagepath>");
          rawXML.AppendLine("<timeperimage>33</timeperimage>");
          rawXML.AppendLine("<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>" + weatherIcon(i) + "</texture>");
        }
        rawXML.AppendLine("<posX>" + (xPos1 + 20 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + (yPos1 - 4).ToString() + "</posY>");
        rawXML.AppendLine("<height>80</height>");
        rawXML.AppendLine("<width>80</width>");
        if (weatherId.ToString().Length == 5)
            rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")]+!control.isvisible(11111)</visible>");
        else
            rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"20\" end=\"100\" delay=\"100\" time=\"400\">Visible</animation>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"200\" time=\"400\">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"200\" time=\"400\">WindowClose</animation>");
        rawXML.AppendLine("</control>");
      }
      rawXML.AppendLine("</control>");

      //Group for weather Text
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FIVE DAY WEATHER TEXT</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"20\" end=\"100\" delay=\"100\" time=\"400\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"200\" time=\"400\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"200\" time=\"400\">WindowClose</animation>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")]+!control.isvisible(11111)</visible>");
      else
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      // **************************************** DAY 1 *********************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>120</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>6030</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>300</posX>");
      rawXML.AppendLine("<posY>615</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.TodayTemperature</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("<type>textbox</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>140</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>140</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<label>#WorldWeather.TodayCondition</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("</control>");
      // **************************************** DAY 2 *****************************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>330</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay0Day</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>510</posX>");
      rawXML.AppendLine("<posY>559</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay0High</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>510</posX>");
      rawXML.AppendLine("<posY>584</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.mintemp</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>350</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay0Low</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>350</posX>");
      rawXML.AppendLine("<posY>654</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay0Condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");
      // **************************************** DAY 3 ***********************************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>540</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay1Day</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>720</posX>");
      rawXML.AppendLine("<posY>559</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay1High</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>720</posX>");
      rawXML.AppendLine("<posY>584</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay1Low</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>560</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay1Condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      // **************************************** DAY 4 ***********************************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>750</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay2Day</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>930</posX>");
      rawXML.AppendLine("<posY>559</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay2High</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>930</posX>");
      rawXML.AppendLine("<posY>584</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay2Low</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>770</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay2Condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      // **************************************** DAY 5 ***********************************************************
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>960</posX>");
      rawXML.AppendLine("<posY>699</posY>");
      rawXML.AppendLine("<width>200</width>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay3Day</label>");
      rawXML.AppendLine("<font>mediastream11tc</font>");
      rawXML.AppendLine("<textcolor>White</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1140</posX>");
      rawXML.AppendLine("<posY>559</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay3High</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1140</posX>");
      rawXML.AppendLine("<posY>584</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay3Low</label>");
      rawXML.AppendLine("<font>mediastream11</font>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>980</posX>");
      rawXML.AppendLine("<posY>634</posY>");
      rawXML.AppendLine("<width>160</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay3Condition</label>");
      rawXML.AppendLine("<font>mediastream10</font>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED FIVE DAY WEATHER CODE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Five Day Weather Horizontal (Style 2)

    private void generateFiveDayWeatherStyle2(int weatherId)
    {
      int i = 0;
      int xPos1 = 234;
      int yPos1 = int.Parse(txtMenuPos.Text) - 560;
      int spacing = 293;
      StringBuilder rawXML = new StringBuilder();

      // Group for Weather Backgrounds
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: Forecast BGs</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
        rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"100\" time=\"400\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"200\" time=\"400\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"200\" time=\"400\">WindowClose</animation>");
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " BG</description>");
        rawXML.AppendLine("<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + yPos1.ToString() + "</posY>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<width>293</width>");
        rawXML.AppendLine("<height>488</height>");
        rawXML.AppendLine("<texture>weather_poster.png</texture>");
        rawXML.AppendLine("<shouldCache>true</shouldCache>");
        rawXML.AppendLine("</control>");     
      }
      rawXML.AppendLine("</control>");

      //Group for weather Icons
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FULL WEATHER ICONS</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"100\" time=\"400\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"200\" time=\"400\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"200\" time=\"400\">WindowClose</animation>");
      if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      // ********************* Weather Icons **************************************
      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " ICON</description>");
        rawXML.AppendLine("<id>0</id>");
        if (WeatherIconsAnimated.Checked)
        {
          rawXML.AppendLine("<type>multiimage</type>");
          rawXML.AppendLine("<imagepath>" + weatherIcon2(i) + "</imagepath>");
          rawXML.AppendLine("<timeperimage>33</timeperimage>");
          rawXML.AppendLine("<loop>True</loop>");
        }
        else
        {
          rawXML.AppendLine("<type>image</type>");
          rawXML.AppendLine("<texture>" + weatherIcon2(i) + "</texture>");
        }
        rawXML.AppendLine("<posX>" + (xPos1 + 50 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + (yPos1 + 50).ToString() + "</posY>");
        rawXML.AppendLine("<height>180</height>");
        rawXML.AppendLine("<width>180</width>");
        if (weatherId.ToString().Length == 5)
            rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
        else
            rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"20\" end=\"100\" delay=\"100\" time=\"400\">Visible</animation>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"200\" time=\"400\">WindowOpen</animation>");
        rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"200\" time=\"400\">WindowClose</animation>");
        rawXML.AppendLine("</control>");
      }


      rawXML.AppendLine("</control>");


      // Group for all text parts
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: FULL WEATHER</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"20\" end=\"100\" delay=\"100\" time=\"400\">Visible</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"200\" time=\"400\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" start=\"100\" end=\"0\" delay=\"200\" time=\"400\">WindowClose</animation>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      if (weatherId.ToString().Length == 5)
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+control.isvisible(" + int.Parse(weatherId.ToString()) + ")|control.isvisible(" + int.Parse((weatherId + 1).ToString()) + ")+!control.isvisible(11111)</visible>");
      else
          rawXML.AppendLine("<visible>plugin.isenabled(World Weather)+[Control.HasFocus(" + (weatherId + 500).ToString() + ")|Control.HasFocus(" + (weatherId + 600).ToString() + ")|control.isvisible(" + weatherId + ")]+!control.isvisible(11111)</visible>");
      // ************************************* Day 1 ****************************************
      rawXML.AppendLine("<control Style=\"DetailTitle\">");
      rawXML.AppendLine("<description>DAY 1 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>280</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 50).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>6030</label>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 TEMP</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>380</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 263).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.TodayTemperature</label>");
      rawXML.AppendLine("<font>font9</font>");
      rawXML.AppendLine("<textcolor>tomato</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>268</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 306).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 1 GENERAL WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>248</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 333).ToString() + "</posY>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<font>font12</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
		  rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
		  rawXML.AppendLine("<seperator></seperator>");
          rawXML.AppendLine("<label>#WorldWeather.TodayCondition</label>");
      rawXML.AppendLine("</control>");
      // ************************************* Day 2 ****************************************
      rawXML.AppendLine("<control Style=\"DetailTitle\">");
      rawXML.AppendLine("<description>DAY 2 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>573</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 50).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay0Day</label>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>557</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 223).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay0High</label>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<textcolor>tomato</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>790</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 223).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay0Low</label>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<textcolor>lightskyblue</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 Sun Image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>552</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 274).ToString() + "</posY>");
      rawXML.AppendLine("<width>24</width>");
      rawXML.AppendLine("<height>24</height>");
      rawXML.AppendLine("<texture>sun.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>580</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 284).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 AM WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>552</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 304).ToString() + "</posY>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay0Condition</label>");
      rawXML.AppendLine("<font>font5</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
      rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
      rawXML.AppendLine("<seperator></seperator>"); 
      rawXML.AppendLine("</control>");

      /*rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 Image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>550</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 354).ToString() + "</posY>");
      rawXML.AppendLine("<width>29</width>");
      rawXML.AppendLine("<height>29</height>");
      rawXML.AppendLine("<texture>moon.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>580</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 364).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 2 PM WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>552</posX>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<posY>" + (yPos1 + 384).ToString() + "</posY>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "2.night.condition</label>");
      rawXML.AppendLine("<font>font5</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
      rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
      rawXML.AppendLine("<seperator></seperator>");
      rawXML.AppendLine("</control>");*/
      // ************************************* Day 3 ****************************************
      rawXML.AppendLine("<control Style=\"DetailTitle\">");
      rawXML.AppendLine("<description>DAY 3 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>866</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 50).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay1Day</label>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>850</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 223).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay1High</label>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<textcolor>tomato</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1083</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 223).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay1Low</label>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<textcolor>lightskyblue</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 Image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>845</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 274).ToString() + "</posY>");
      rawXML.AppendLine("<width>24</width>");
      rawXML.AppendLine("<height>24</height>");
      rawXML.AppendLine("<texture>sun.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>873</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 284).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 AM WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>845</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 304).ToString() + "</posY>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay1Condition</label>");
      rawXML.AppendLine("<font>font5</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
      rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
      rawXML.AppendLine("<seperator></seperator>");
      rawXML.AppendLine("</control>");

      /*rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 Image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>843</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 354).ToString() + "</posY>");
      rawXML.AppendLine("<width>29</width>");
      rawXML.AppendLine("<height>29</height>");
      rawXML.AppendLine("<texture>moon.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>873</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 364).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 3 PM WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>845</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 384).ToString() + "</posY>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "3.night.condition</label>");
      rawXML.AppendLine("<font>font5</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
      rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
      rawXML.AppendLine("<seperator></seperator>");
      rawXML.AppendLine("</control>");*/
      // ************************************* Day 4 ****************************************
      rawXML.AppendLine("<control Style=\"DetailTitle\">");
      rawXML.AppendLine("<description>DAY 4 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1159</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 50).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay2Day</label>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1143</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 223).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay2High</label>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<textcolor>tomato</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1376</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 223).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay2Low</label>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<textcolor>lightskyblue</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 Image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1138</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 274).ToString() + "</posY>");
      rawXML.AppendLine("<width>24</width>");
      rawXML.AppendLine("<height>24</height>");
      rawXML.AppendLine("<texture>sun.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1166</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 284).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 AM WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1138</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 304).ToString() + "</posY>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay2Condition</label>");
      rawXML.AppendLine("<font>font5</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
      rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
      rawXML.AppendLine("<seperator></seperator>");
      rawXML.AppendLine("</control>");

      /*rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 Image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1136</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 354).ToString() + "</posY>");
      rawXML.AppendLine("<width>29</width>");
      rawXML.AppendLine("<height>29</height>");
      rawXML.AppendLine("<texture>moon.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1166</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 364).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 4 PM WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1138</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 384).ToString() + "</posY>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "4.night.condition</label>");
      rawXML.AppendLine("<font>font5</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
      rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
      rawXML.AppendLine("<seperator></seperator>");
      rawXML.AppendLine("</control>");*/
      // ************************************* Day 5 ****************************************
      rawXML.AppendLine("<control Style=\"DetailTitle\">");
      rawXML.AppendLine("<description>DAY 5 LABEL</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1452</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 50).ToString() + "</posY>");
      rawXML.AppendLine("<align>center</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay3Day</label>");
      rawXML.AppendLine("</control>");


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MAX VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1436</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 223).ToString() + "</posY>");
      rawXML.AppendLine("<align>left</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay3High</label>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<textcolor>tomato</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 MIN VALUE</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1669</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 223).ToString() + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay3Low</label>");
      rawXML.AppendLine("<font>font2</font>");
      rawXML.AppendLine("<textcolor>lightskyblue</textcolor>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 Image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1431</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 274).ToString() + "</posY>");
      rawXML.AppendLine("<width>24</width>");
      rawXML.AppendLine("<height>24</height>");
      rawXML.AppendLine("<texture>sun.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1459</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 284).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 AM WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1431</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 304).ToString() + "</posY>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<label>#WorldWeather.ForecastDay3Condition</label>");
      rawXML.AppendLine("<font>font5</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
      rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
      rawXML.AppendLine("<seperator></seperator>");
      rawXML.AppendLine("</control>");

      /*rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 Image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1429</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 354).ToString() + "</posY>");
      rawXML.AppendLine("<width>29</width>");
      rawXML.AppendLine("<height>29</height>");
      rawXML.AppendLine("<texture>moon.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 Seperator</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1459</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 364).ToString() + "</posY>");
      rawXML.AppendLine("<width>250</width>");
      rawXML.AppendLine("<height>2</height>");
      rawXML.AppendLine("<texture>weather_seperator.png</texture>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>DAY 5 PM WEATHER</description>");
      rawXML.AppendLine("<type>textboxscrollup</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1431</posX>");
      rawXML.AppendLine("<posY>" + (yPos1 + 384).ToString() + "</posY>");
      rawXML.AppendLine("<width>243</width>");
      rawXML.AppendLine("<label>#infoservice.weather." + infoServiceDayProperty + "5.night.condition</label>");
      rawXML.AppendLine("<font>font5</font>");
      rawXML.AppendLine("<textalign>center</textalign>");
      rawXML.AppendLine("<textcolor>white</textcolor>");
      rawXML.AppendLine("<scrollStartDelaySec>2</scrollStartDelaySec>");
      rawXML.AppendLine("<spaceBetweenItems>0</spaceBetweenItems>");
      rawXML.AppendLine("<seperator></seperator>");
      rawXML.AppendLine("</control>");*/

      for (i = 0; i < 5; i++)
      {
        rawXML.AppendLine("<control>");
        rawXML.AppendLine("<description>DAY " + i.ToString() + " BG Shine</description>");
        rawXML.AppendLine("<posX>" + (xPos1 + (spacing * i)).ToString() + "</posX>");
        rawXML.AppendLine("<posY>" + yPos1.ToString() + "</posY>");
        rawXML.AppendLine("<type>image</type>");
        rawXML.AppendLine("<id>0</id>");
        rawXML.AppendLine("<width>293</width>");
        rawXML.AppendLine("<height>488</height>");
        rawXML.AppendLine("<texture>weather_poster_shine.png</texture>");
        rawXML.AppendLine("<shouldCache>true</shouldCache>");
        rawXML.AppendLine("</control>");
      }
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED FIVE DAY WEATHER CODE -->", rawXML.ToString());
    }

    #endregion

    #region Generate Weather Summary

    private void generateWeathersummary(int? weatherId)
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>GROUP: WEATHER SUMMARY</description>");
      rawXML.AppendLine("<type>group</type>");
      rawXML.AppendLine("<dimColor>0xffffffff</dimColor>");
      //if (enableFiveDayWeather.Checked && weatherId != null)   // Hide summary only if 5 Day weather summary is enabled
      //  rawXML.AppendLine("<visible>plugin.isenabled(InfoService)+!control.isvisible(" + int.Parse(weatherId.ToString()) + ")</visible>");
      //else
        rawXML.AppendLine("<visible>plugin.isenabled(World Weather)</visible>");

      rawXML.AppendLine("<animation effect=\"fade\" time=\"150\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" time=\"150\">WindowClose</animation>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Weather Summary Bar</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.dateAndWeatherBarYOffset) + "</posY>");
      rawXML.AppendLine("<width>500</width>");
      rawXML.AppendLine("<height>41</height>");
      rawXML.AppendLine("<texture>weather_bar.png</texture>");
      rawXML.AppendLine("</control>");

      //if (WeatherIconsAnimated.Checked)
      //{
      //  rawXML.AppendLine("<control>");
      //  rawXML.AppendLine("<description>Todays weather image (Animated Version)</description>");
      //  rawXML.AppendLine("<type>multiimage</type>");
      //  rawXML.AppendLine("<id>0</id>");
      //  rawXML.AppendLine("<posX>420</posX>");
      //  rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.dateAndWeatherBarYOffset - 4) + "</posY>");
      //  rawXML.AppendLine("<height>50</height>");
      //  rawXML.AppendLine("<width>50</width>");
      //  rawXML.AppendLine("<centered>no</centered>");
      //  rawXML.AppendLine("<texture>-</texture>");
      //  rawXML.AppendLine("<imagepath>" + weatherIcon(0) + "</imagepath>");
      //  rawXML.AppendLine("<timeperimage>33</timeperimage>");
      //  rawXML.AppendLine("<loop>True</loop>");
      //  rawXML.AppendLine("</control>");
      //}
      //else
      //{
      //  rawXML.AppendLine("<control>");
      //  rawXML.AppendLine("<description>Todays weather image (Animated Version)</description>");
      //  rawXML.AppendLine("<type>image</type>");
      //  rawXML.AppendLine("<id>0</id>");
      //  rawXML.AppendLine("<posX>420</posX>");
      //  rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.dateAndWeatherBarYOffset - 4) + "</posY>");
      //  rawXML.AppendLine("<height>50</height>");
      //  rawXML.AppendLine("<width>50</width>");
      //  rawXML.AppendLine("<centered>no</centered>");
      //  rawXML.AppendLine("<texture>" + weatherIcon(0) + "</texture>");
      //  rawXML.AppendLine("</control>");
      //}

      rawXML.AppendLine("<control Style=\"DetailText\">");
      rawXML.AppendLine("<description>Temperature and Weather Condition</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>10</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.dateAndWeatherBarYOffset + 4) + "</posY>"); ;
      rawXML.AppendLine("<label>#WorldWeather.TodayTemperature / #WorldWeather.TodayCondition</label>");
      rawXML.AppendLine("<animation effect=\"fade\" time=\"150\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" time=\"150\">WindowClose</animation>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED WEATHER SUMMARY CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Clock

    private void generateClock()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Clock Summary Bar</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1420</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.dateAndWeatherBarYOffset) + "</posY>");
      rawXML.AppendLine("<width>500</width>");
      rawXML.AppendLine("<height>41</height>");
      rawXML.AppendLine("<texture>time_bar.png</texture>");
      rawXML.AppendLine("<animation effect=\"fade\" time=\"150\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" time=\"150\">WindowClose</animation>");
      rawXML.AppendLine("</control>");      
      
      rawXML.AppendLine("<control Style=\"DetailText\">");
      rawXML.AppendLine("<description>Date</description>");
      rawXML.AppendLine("<type>label</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<posX>1915</posX>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.dateAndWeatherBarYOffset + 4) + "</posY>");
      rawXML.AppendLine("<align>right</align>");
      rawXML.AppendLine("<label>#date / #time</label>");
      rawXML.AppendLine("<animation effect=\"fade\" time=\"150\">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=\"fade\" time=\"150\">WindowClose</animation>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN COMMON TIME CODE-->", rawXML.ToString());
    }

    #endregion

    #region Add Fanart Controls Import

    private void generateFanartControls()
    {
      if (fanartHandlerUsed)
      {
        StringBuilder rawXML = new StringBuilder();
        rawXML.AppendLine("<import>common.fanartcontrols.basichome.xml</import>");
        xml = xml.Replace("<!-- BEGIN FANART HANDLER CONTROLS -->", rawXML.ToString());
      }
    }

    #endregion

    #region Background Loading

    private void generateBackgroundLoading()
    {
      /*StringBuilder rawXML = new StringBuilder();
      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>BACKGROUND LOADING</description>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<posX>0</posX>");
      rawXML.AppendLine("<posY>0</posY>");
      rawXML.AppendLine("<width>1920</width>");
      rawXML.AppendLine("<height>1080</height>");
      rawXML.AppendLine("<texture>LoadingBackdrop.png</texture>");
      rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " start=" + quote + "100" + quote + " end=" + quote + "0" + quote + " time=" + quote + "4000" + quote + " reversible=" + quote + "false" + quote + ">WindowOpen</animation>");
      rawXML.AppendLine("<animation effect=" + quote + "fade" + quote + " start=" + quote + "0" + quote + " end=" + quote + "0" + quote + " time=" + quote + "10" + quote + " reversible=" + quote + "false" + quote + ">WindowClose</animation>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN BACKGROUND LOADING -->", rawXML.ToString());(*/
    }

    #endregion

    #region Generate Fanart Scraper Progress

    private void generatefanartScraper()
    {
      StringBuilder rawXML = new StringBuilder();
      if (menuStyle == chosenMenuStyle.verticalStyle)
        rawXML.AppendLine("<import>common.fanart.scraperv.xml</import>");
      else
        rawXML.AppendLine("<import>common.fanart.scraper.xml</import>");
      xml = xml.Replace("<!-- BEGIN FANART SCRAPER CODE-->", rawXML.ToString());
    }

    #endregion

    #region InfoSerice Most Recent Includes

    void generateMostRecentInclude(isOverlayType overlayType)
    {
      StringBuilder rawXML = new StringBuilder();
      string replaceString = null;

      if (overlayType == isOverlayType.TVSeries)
      {
        replaceString = "<!-- BEGIN MOST RECENT TVSERIES CODE-->";

        if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.plot)
          rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.HSum1.xml</import>");
        else if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
          rawXML.AppendLine("<import>basichome.recentlyadded.tvseries.HSum2.xml</import>");

      }

      if (overlayType == isOverlayType.MovPics)
      {
        replaceString = "<!-- BEGIN MOST RECENT MOVPICS CODE-->";

        if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.plot)
          rawXML.AppendLine("<import>basichome.recentlyadded.movpics.HSum1.xml</import>");
        else if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
          rawXML.AppendLine("<import>basichome.recentlyadded.movpics.HSum2.xml</import>");

      }

      if (helper.pluginEnabled(Helper.Plugins.FanartHandler))
      {
        if (overlayType == isOverlayType.Music)
        {
          replaceString = "<!-- BEGIN MOST RECENT MUSIC CODE-->";
          if (mrMusicStyle == musicMostRecentStyle.style1)
            rawXML.AppendLine("<import>basichome.recentlyadded.Music.Summary1.xml</import>");
          else
            rawXML.AppendLine("<import>basichome.recentlyadded.Music.Summary2.xml</import>");
        }

        if (overlayType == isOverlayType.Pictures)
        {
          replaceString = "<!-- BEGIN MOST RECENT PICTURES CODE-->";
          rawXML.AppendLine("<import>basichome.recentlyadded.Pictures.Summary.xml</import>");
        }

        if (overlayType == isOverlayType.RecordedTV)
        {
          replaceString = "<!-- BEGIN MOST RECENT RECORDEDTV CODE-->";
          rawXML.AppendLine("<import>basichome.recentlyadded.RecordedTV.Summary.xml</import>");
        }
      }

      if (overlayType == isOverlayType.freeDriveSpace)
      {
        replaceString = "<!-- BEGIN FREEDRIVESPACE OVERLAY CODE-->";
        rawXML.AppendLine("<import>basichome.FreeDriveSpace.Overlay.xml</import>");
      }

      if (overlayType == isOverlayType.sleepControl)
      {
        replaceString = "<!-- BEGIN SLEEPCONTROL OVERLAY CODE-->";
        rawXML.AppendLine("<import>basichome.SleepControl.Overlay.xml</import>");
      }

      if (overlayType == isOverlayType.stocks)
      {
        replaceString = "<!-- BEGIN STOCKS OVERLAY CODE-->";
        rawXML.AppendLine("<import>basichome.Stocks.Overlay.xml</import>");
      }

      if (overlayType == isOverlayType.powerControl)
      {
        replaceString = "<!-- BEGIN POWERCONTROL OVERLAY CODE-->";
        rawXML.AppendLine("<import>basichome.PowerControl.Overlay.xml</import>");
      }

      if (overlayType == isOverlayType.htpcInfo)
      {
        replaceString = "<!-- BEGIN HTPCINFO OVERLAY CODE-->";
        rawXML.AppendLine("<import>basichome.HTPCInfo.Overlay.xml</import>");
      }

      if (overlayType == isOverlayType.updateControl)
      {
        replaceString = "<!-- BEGIN UPDATECONTROL OVERLAY CODE-->";
        rawXML.AppendLine("<import>basichome.UpdateControl.Overlay.xml</import>");
      }

      if (!string.IsNullOrEmpty(replaceString))
        xml = xml.Replace(replaceString, rawXML.ToString());
    }

    #endregion

    #region Generate Twitter Horizontal

    private void generateTwitter()
    {
      StringBuilder rawXML = new StringBuilder();


      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Menu Twitter Sub Menu</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>99004</id>");
      rawXML.AppendLine("<posX>280</posX>");
      rawXML.AppendLine("<posY>400</posY>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - basicHomeValues.offsetTwitter).ToString() + "</posY>");
      rawXML.AppendLine("<width>1000</width>");
      rawXML.AppendLine("<height>" + basicHomeValues.subMenuTopHeight + "</height>");
      rawXML.AppendLine("<texture>" + basicHomeValues.mymenu_submenutop + "</texture>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("<wrapString> :: </wrapString>");
      }

      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Twitter image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>0</id>");
      rawXML.AppendLine("<width>28</width>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - (basicHomeValues.offsetTwitterImage - 5)).ToString() + "</posY>");
      rawXML.AppendLine("<posX>330</posX>");
      rawXML.AppendLine("<texture>InfoService\\defaultTwitter.png</texture>");
      rawXML.AppendLine("<keepaspectratio>yes</keepaspectratio>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Twitter Items</description>");
      rawXML.AppendLine("<type>fadelabel</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>1160</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<posY>" + (int.Parse(txtMenuPos.Text) - (basicHomeValues.offsetTwitterImage - 6)).ToString() + "</posY>");
      rawXML.AppendLine("<posX>360</posX>");
      rawXML.AppendLine("<font>mediastream12</font>");
      rawXML.AppendLine("<textcolor>ff000000</textcolor>");
      rawXML.AppendLine("<label>#infoservice.twitter.messages</label>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("<animation effect=" + quote + "slide" + quote + " end=" + quote + "0,320" + quote + " time=" + quote + " 250" + quote + " acceleration=" + quote + " -0.1" + quote + " reversible=" + quote + "false" + quote + ">windowclose</animation>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED TWITTER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Generate Twitter Vertical

    private void generateTwitterV()
    {
      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Menu Twitter Sub Menu</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>99004</id>");
      rawXML.AppendLine("<posX>140</posX>");
      rawXML.AppendLine("<posY>9</posY>");
      rawXML.AppendLine("<width>840</width>");
      rawXML.AppendLine("<height>34</height>");
      rawXML.AppendLine("<texture>" + basicHomeValues.mymenu_submenutop + "</texture>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      if (wrapString.Checked)
      {
        if (useInfoServiceSeparator)
          //rawXML.AppendLine("<wrapString> #infoservice.feed.separator </wrapString>");
          rawXML.AppendLine("<wrapString> :: </wrapString>");
        else
          rawXML.AppendLine("<wrapString> :: </wrapString>");
      }

      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Twitter image</description>");
      rawXML.AppendLine("<type>image</type>");
      rawXML.AppendLine("<id>18</id>");
      rawXML.AppendLine("<width>20</width>");
      rawXML.AppendLine("<posY>16</posY>");
      rawXML.AppendLine("<posX>149</posX>");
      rawXML.AppendLine("<texture>InfoService\\defaultTwitter.png</texture>");
      rawXML.AppendLine("<keepaspectratio>yes</keepaspectratio>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      rawXML.AppendLine("<control>");
      rawXML.AppendLine("<description>Twitter Items</description>");
      rawXML.AppendLine("<type>fadelabel</type>");
      rawXML.AppendLine("<id>1</id>");
      rawXML.AppendLine("<width>775</width>");
      rawXML.AppendLine("<height>50</height>");
      rawXML.AppendLine("<posY>15</posY>");
      rawXML.AppendLine("<posX>175</posX>");
      rawXML.AppendLine("<font>mediastream12tc</font>");
      rawXML.AppendLine("<label>#infoservice.twitter.messages</label>");
      rawXML.AppendLine("<visible>plugin.isenabled(InfoService)</visible>");
      rawXML.AppendLine("</control>");

      xml = xml.Replace("<!-- BEGIN GENERATED TWITTER CODE-->", rawXML.ToString());
    }

    #endregion

    #region Write mustayalucamenuprofile.xml

    private void writeMenuProfile(menuType direction)
    {
      string menuPos;
      string menuOrientation;
      string activeMenuStyle = null;
      string activeWeatherStyle = null;
      string acceleration = tbAcceleration.Text;
      string duration = tbDuration.Text;
      string activeRssImageType = null;
      string targetScreenRes = "SD";
      string tvRecentDisplayType = "full";
      string movPicsDisplayType = "full";
      string mostRecentTVSeriesSummStyle = "fanart";
      string mostRecentMovPicsSummStyle = "fanart";
      string mostRecentMusicSummStyle = "style1";

      string settingDropShadow = cbDropShadow.Checked ? "true" : "false";
      string settingEnableRssfeed = enableRssfeed.Checked ? "true" : "false";
      string settingEnableTwitter = enableTwitter.Checked ? "true" : "false";
      string settingWrapString = wrapString.Checked ? "true" : "false";
      string settingWeatherBGlink = weatherBGlink.Checked ? "true" : "false";
      string settingFiveDayWeatherCheckBox = enableFiveDayWeather.Checked ? "true" : "false";
      string settingSummaryWeatherCheckBox = summaryWeatherCheckBox.Checked ? "true" : "false";
      string settingClearCacheOnGenerate = cboClearCache.Checked ? "true" : "false";
      string settingAnimatedWeather = WeatherIconsAnimated.Checked ? "true" : "false";
      string settingHorizontalContextLabels = horizontalContextLabels.Checked ? "true" : "false";
      string settingFullWeatherSummaryBottom = fullWeatherSummaryBottom.Checked ? "true" : "false";
      string settingFullWeatherSummaryMiddle = fullWeatherSummaryMiddle.Checked ? "true" : "false";
      string disableOnScreenClock = cbDisableClock.Checked ? "true" : "false";
      string hideFanartScrapingtext = cbHideFanartScraper.Checked ? "true" : "false";
      string enableOverlayFanart = cbOverlayFanart.Checked ? "true" : "false";
      string animatedBackground = cbAnimateBackground.Checked ? "true" : "false";
      string tvSeriesMostRecent = cbMostRecentTvSeries.Checked ? "true" : "false";
      string movPicsMostRecent = cbMostRecentMovPics.Checked ? "true" : "false";
      string mrSeriesEpisodeFormat = tvSeriesOptions.mrSeriesEpisodeFormat ? "true" : "false";
      string mrTitleLast = tvSeriesOptions.mrTitleLast ? "true" : "false";
      string settingOldStyleExitButtons = cbExitStyleNew.Checked ? "true" : "false";
      string mrTVSeriesCycleFanart = mostRecentTVSeriesCycleFanart ? "true" : "false";
      string mrMovPicsCycleFanart = mostRecentMovPicsCycleFanart ? "true" : "false";
      string mrMovPicsHideRuntime = movPicsOptions.HideRuntime ? "true" : "false";
      string mrMovPicsHideCertification = movPicsOptions.HideCertification ? "true" : "false";
      string mrMovPicsHideRating = movPicsOptions.HideRating ? "true" : "false";
      string mrMovPicsUseTextRating = movPicsOptions.UseTextRating ? "true" : "false";
      string mrTVSeriesDisableFadeLabel = tvSeriesOptions.mrDisableFadeLabels ? "true" : "false";
      string mrMovPicsDisableFadeLabel = movPicsOptions.DisableFadeLabels ? "true" : "false";
      string mrMusicEnabled = cbEnableRecentMusic.Checked ? "true" : "false";
      string mrPicturesEnabled = cbEnableRecentPictures.Checked ? "true" : "false";
      string mrRecordedTVEnabled = cbEnableRecentRecordedTV.Checked ? "true" : "false";
      string sleepControlEnabled = cbSleepControlOverlay.Checked ? "true" : "false";
      string stocksControlEnabled = cbSocksOverlay.Checked ? "true" : "false";
      string powerControlEnabled = cbPowerControlOverlay.Checked ? "true" : "false";
      string htpcinfoControlEnabled = cbHtpcInfoOverlay.Checked ? "true" : "false";
      string updateControlEnabled = cbUpdateControlOverlay.Checked ? "true" : "false";
      string disableExitMenu = cbDisableExitMenu.Checked ? "true" : "false";

      if (direction == menuType.horizontal)
      {
        menuPos = "menuYPos";
        menuOrientation = "Horizontal";
      }
      else
      {
        menuPos = "menuXPos";
        menuOrientation = "Vertical";
      }

      switch (menuStyle)
      {
        case chosenMenuStyle.verticalStyle:
          activeMenuStyle = "verticalStyle";
          break;
        case chosenMenuStyle.horizontalStandardStyle:
          activeMenuStyle = "horizontalStandardStyle";
          break;
        case chosenMenuStyle.horizontalContextStyle:
          activeMenuStyle = "horizontalContextStyle";
          break;
        case chosenMenuStyle.graphicMenuStyle:
          activeMenuStyle = "graphicMenuStyle";
          break;
      }

      switch (rssImage)
      {
        case rssImageType.infoserviceImage:
          activeRssImageType = "infoservice";
          break;
        case rssImageType.noImage:
          activeRssImageType = "noimage";
          break;
        case rssImageType.skinImage:
          activeRssImageType = "skin";
          break;
      }

      if (weatherStyle == chosenWeatherStyle.bottom)
        activeWeatherStyle = "bottom";
      else if (weatherStyle == chosenWeatherStyle.middle)
        activeWeatherStyle = "middle";

      if (screenres == screenResolutionType.res1920x1080)
        targetScreenRes = "HD";
      else
        targetScreenRes = "SD";

      if (tvSeriesRecentStyle == tvSeriesRecentType.summary)
        tvRecentDisplayType = "summary";
      else
        tvRecentDisplayType = "full";

      if (movPicsRecentStyle == movPicsRecentType.summary)
        movPicsDisplayType = "summary";
      else
        movPicsDisplayType = "full";

      if (mrTVSeriesSummStyle == mostRecentTVSeriesSummaryStyle.fanart)
        mostRecentTVSeriesSummStyle = "fanart";
      else
        mostRecentTVSeriesSummStyle = "plot";

      if (mrMovPicsSummStyle == mostRecentMovPicsSummaryStyle.fanart)
        mostRecentMovPicsSummStyle = "fanart";
      else
        mostRecentMovPicsSummStyle = "plot";

      if (mrMusicStyle == musicMostRecentStyle.style1)
        mostRecentMusicSummStyle = "style1";
      else
        mostRecentMusicSummStyle = "style2";

      driveFreeSpaceList = string.Empty;
      foreach (string drive in driveFreeSpaceDrives)
      {
        driveFreeSpaceList += drive + ",";
      }
      if (driveFreeSpaceList.Length > 0)
        driveFreeSpaceList = driveFreeSpaceList.Substring(0, driveFreeSpaceList.Length - 1);

      xml = ("<profile>\n"
                + "\t<version>" + profileVersion + "</version>\n"
                + "\t<skin name=\"Mustayaluca\">\n"
                + "\t\t<section name=" + quote + "Mustayaluca Options" + quote + ">\n"
                + generateEntry("menustyle", activeMenuStyle, 3, true)
                + generateEntry("weatherstyle", activeWeatherStyle, 3, true)
                + generateEntry("menuitemFocus", focusAlpha.Text + txtFocusColour.Text, 3, true)
                + generateEntry("menuitemNoFocus", noFocusAlpha.Text + txtNoFocusColour.Text, 3, true)
                + generateEntry("labelFont", cboLabelFont.Text, 3, true)
                + generateEntry("selectedFont", cboSelectedFont.Text, 3, true)
                + generateEntry("menuType", menuOrientation, 3, true)
                + generateEntry(menuPos, txtMenuPos.Text, 3, true)
                + generateEntry("acceleration", acceleration, 3, true)
                + generateEntry("duration", duration, 3, true)
                + generateEntry("dropShadow", settingDropShadow, 3, true)
                + generateEntry("enableRssfeed", settingEnableRssfeed, 3, true)
                + generateEntry("enableTwitter", settingEnableTwitter, 3, true)
                + generateEntry("wrapString", settingWrapString, 3, true)
                + generateEntry("weatherBGlink", settingWeatherBGlink, 3, true)
                + generateEntry("fiveDayWeatherCheckBox", settingFiveDayWeatherCheckBox, 3, true)
                + generateEntry("summaryWeatherCheckBox", settingSummaryWeatherCheckBox, 3, true)
                + generateEntry("cboClearCache", settingClearCacheOnGenerate, 3, true)
                + generateEntry("animatedWeather", settingAnimatedWeather, 3, true)
                + generateEntry("horizontalContextLabels", settingHorizontalContextLabels, 3, true)
                + generateEntry("fullWeatherSummaryBottom", settingFullWeatherSummaryBottom, 3, true)
                + generateEntry("fullWeatherSummaryMiddle", settingFullWeatherSummaryMiddle, 3, true)
                + generateEntry("activeRssImageType", activeRssImageType, 3, true)
                + generateEntry("disableOnScreenClock", disableOnScreenClock, 3, true)
                + generateEntry("targetScreenRes", targetScreenRes, 3, true)
                + generateEntry("hideFanartScrapingtext", hideFanartScrapingtext, 3, true)
                + generateEntry("enableOverlayFanart", enableOverlayFanart, 3, true)
                + generateEntry("animatedBackground", animatedBackground, 3, true)
                + generateEntry("tvSeriesMostRecent", tvSeriesMostRecent, 3, true)
                + generateEntry("movPicsMostRecent", movPicsMostRecent, 3, true)
                + generateEntry("tvRecentDisplayType", tvRecentDisplayType, 3, true)
                + generateEntry("movPicsDisplayType", movPicsDisplayType, 3, true)
                + generateEntry("mostRecentTVSeriesSummStyle", mostRecentTVSeriesSummStyle, 3, true)
                + generateEntry("mostRecentMovPicsSummStyle", mostRecentMovPicsSummStyle, 3, true)
                + generateEntry("mostRecentMusicSummStyle", mostRecentMusicSummStyle, 3, true)
                + generateEntry("mrSeriesEpisodeFormat", mrSeriesEpisodeFormat, 3, true)
                + generateEntry("mrTitleLast", mrTitleLast, 3, true)
                + generateEntry("mrEpisodeFont", tvSeriesOptions.mrEpisodeFont, 3, true)
                + generateEntry("mrSeriesFont", tvSeriesOptions.mrSeriesFont, 3, true)
                + generateEntry("settingOldStyleExitButtons", settingOldStyleExitButtons, 3, true)
                + generateEntry("mrTVSeriesCycleFanart", mrTVSeriesCycleFanart, 3, true)
                + generateEntry("mrMovPicsCycleFanart", mrMovPicsCycleFanart, 3, true)
                + generateEntry("mrMovieTitleFont", movPicsOptions.MovieTitleFont, 3, true)
                + generateEntry("mrMovieDetailFont", movPicsOptions.MovieDetailFont, 3, true)
                + generateEntry("mrMovPicsHideRuntime", mrMovPicsHideRuntime, 3, true)
                + generateEntry("mrMovPicsHideCertification", mrMovPicsHideCertification, 3, true)
                + generateEntry("mrMovPicsHideRating", mrMovPicsHideRating, 3, true)
                + generateEntry("mrMovPicsUseTextRating", mrMovPicsUseTextRating, 3, true)
                + generateEntry("mrTVSeriesDisableFadeLabel", mrTVSeriesDisableFadeLabel, 3, true)
                + generateEntry("mrMovPicsDisableFadeLabel", mrMovPicsDisableFadeLabel, 3, true)
                + generateEntry("mrRecordedTVEnabled", mrRecordedTVEnabled, 3, true)
                + generateEntry("mrMusicEnabled", mrMusicEnabled, 3, true)
                + generateEntry("mrPicturesEnabled", mrPicturesEnabled, 3, true)
                + generateEntry("driveFreeSpaceList", driveFreeSpaceList, 3, true)
                + generateEntry("sleepControlEnabled", sleepControlEnabled, 3, true)
                + generateEntry("stocksControlEnabled", stocksControlEnabled, 3, true)
                + generateEntry("powerControlEnabled", powerControlEnabled, 3, true)
                + generateEntry("htpcinfoControlEnabled", htpcinfoControlEnabled, 3, true)
                + generateEntry("updateControlEnabled", updateControlEnabled, 3, true)
                + generateEntry("disableExitMenu", disableExitMenu, 3, true)
                + "\t\t</section>");




      StringBuilder rawXML = new StringBuilder();

      rawXML.AppendLine("\n\t\t<!-- End Of Menu Options -->\n\t\t<section name=" + quote + "Mustayaluca Menu Items" + quote + ">");

      int menuIndex = 0;
      rawXML.AppendLine(generateEntry("count", menuItems.Count.ToString(), 3, false));
      foreach (menuItem menItem in menuItems)
      {
        if (menItem.subMenuLevel1.Count > 0 || menItem.subMenuLevel2.Count > 0)
          menItem.disableBGSharing = true;

        rawXML.AppendLine("\t\t\t<!-- Menu Entry : " + menuIndex.ToString() + " -->");
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "name", menItem.name, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "label", menItem.contextLabel, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "folder", menItem.bgFolder, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "fanartproperty", menItem.fanartProperty, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "fanartSource", menItem.fhBGSource.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "fanarthandlerenabled", menItem.fanartHandlerEnabled.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "enablemusicnowplayingfanart", menItem.EnableMusicNowPlayingFanart.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "hyperlink", menItem.hyperlink, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "hyperlinkParameter", menItem.hyperlinkParameter, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "hyperlinkParameterOption", menItem.hyperlinkParameterOption, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "isdefault", menItem.isDefault.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "isweather", menItem.isWeather.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "id", menItem.id.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "xmlFileName", menItem.xmlFileName, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "buttonTexture", menItem.buttonTexture, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "updatestatus", menItem.updateStatus.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "defaultimage", menItem.defaultImage, 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "disableBGSharing", menItem.disableBGSharing.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "showMostRecent", menItem.showMostRecent.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "submenu1", menItem.subMenuLevel1.Count.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "submenu2", menItem.subMenuLevel2.Count.ToString(), 3, false));
        rawXML.AppendLine(generateEntry("menuitem" + menuIndex.ToString() + "subMenuLevel1ID", menItem.subMenuLevel1ID.ToString(), 3, false));

        if (menItem.subMenuLevel1.Count > 0)
        {
          int subCount = 0;
          subMenuL1Exists = true;
          rawXML.AppendLine("\t\t\t<!-- Menu Entry : " + menuIndex.ToString() + " Sub Level 1 -->");
          foreach (subMenuItem subItem in menItem.subMenuLevel1)
          {
            rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "1subitem" + subCount.ToString() + "displayName", subItem.displayName, 3, false));
            rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "1subitem" + subCount.ToString() + "baseDisplayName", subItem.baseDisplayName, 3, false));
            rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "1subitem" + subCount.ToString() + "xmlFileName", subItem.xmlFileName, 3, false));
            rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "1subitem" + subCount.ToString() + "hyperlink", subItem.hyperlink, 3, false));
            rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "1subitem" + subCount.ToString() + "hyperlinkParameter", subItem.hyperlinkParameter, 3, false));
            rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "1subitem" + subCount.ToString() + "hyperlinkParameterOption", subItem.hyperlinkParameterOption, 3, false));
            rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "1subitem" + subCount.ToString() + "mrDisplay", subItem.showMostRecent.ToString(), 3, false));
            subCount++;
          }

          subCount = 0;
          if (menItem.subMenuLevel2.Count > 0)
          {
            //subMenuL2Exists = true;
            rawXML.AppendLine("\t\t\t<!-- Menu Entry : " + menuIndex.ToString() + " Sub Level 2 -->");
            foreach (subMenuItem subItem in menItem.subMenuLevel2)
            {
              rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "2subitem" + subCount.ToString() + "displayName", subItem.displayName, 3, false));
              rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "2subitem" + subCount.ToString() + "baseDisplayName", subItem.baseDisplayName, 3, false));
              rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "2subitem" + subCount.ToString() + "xmlFileName", subItem.xmlFileName, 3, false));
              rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "2subitem" + subCount.ToString() + "hyperlink", subItem.hyperlink, 3, false));
              rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "2subitem" + subCount.ToString() + "hyperlinkParameter", subItem.hyperlinkParameter, 3, false));
              rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "2subitem" + subCount.ToString() + "hyperlinkParameterOption", subItem.hyperlinkParameterOption, 3, false));
              rawXML.AppendLine(generateEntry("submenu" + menuIndex.ToString() + "2subitem" + subCount.ToString() + "mrDisplay", subItem.showMostRecent.ToString(), 3, false));
              subCount++;
            }
          }
        }

        menuIndex += 1;

        if (menItem.fanartHandlerEnabled)
          fanartHandlerUsed = true;


      }
      rawXML.AppendLine("\t\t</section>");
      rawXML.AppendLine("\t</skin>");
      rawXML.AppendLine("</profile>");

      xml += rawXML.ToString();

      if (System.IO.File.Exists(SkinInfo.mpPaths.configBasePath + "mustayalucamenuprofile.xml"))
        System.IO.File.Copy(SkinInfo.mpPaths.configBasePath + "mustayalucamenuprofile.xml", SkinInfo.mpPaths.configBasePath + "mustayalucamenuprofile.xml.backup." + DateTime.Now.Ticks.ToString());

      if (System.IO.File.Exists(SkinInfo.mpPaths.configBasePath + "mustayalucamenuprofile.xml"))
        System.IO.File.Delete(SkinInfo.mpPaths.configBasePath + "mustayalucamenuprofile.xml");

      StreamWriter writer;
      writer = System.IO.File.CreateText(SkinInfo.mpPaths.configBasePath + "mustayalucamenuprofile.xml");
      writer.Write(xml);
      writer.Close();
    }
    #endregion
  }
}