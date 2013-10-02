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
    public string localxml = string.Empty;
    string onLeft;
    string onRight;
    int subMenuXPos = 960;

    string bhSubMenuWriterH()
    {
      localxml = string.Empty;
      level1LateralBladeVisible = "control.isvisible(";
      level2LateralBladeVisible = "control.isvisible(";
      foreach (menuItem menItem in menuItems)
      {
        if (menItem.subMenuLevel1.Count > 0)
        {
          level1LateralBladeVisible += menItem.subMenuLevel1ID.ToString() + ")|control.isvisible(";

          writeSubMenuLevel1H(menItem);
        }
        if (menItem.subMenuLevel2.Count > 0)
        {
          level2LateralBladeVisible += (menItem.subMenuLevel1ID + 100).ToString() + ")|control.isvisible(";
          writeSubMenuLevel2H(menItem);
        }
      }

      localxml += "<!--             End of Submenu Code            -->";
      return localxml;
    }


    string writeSubMenuLevel1H(formMustayalucaEditor.menuItem parentMenu)
    {
      string dummyFocusControls = "Control.HasFocus(";
 
      for (int i = 0; i < parentMenu.subMenuLevel1.Count; i++)
      {
        if (i < (parentMenu.subMenuLevel1.Count - 1))
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")|Control.HasFocus(";
        else
          if (parentMenu.subMenuLevel2.Count == 0)
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")";
          else
            dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")|Control.HasFocus(";
      }

      localxml +=   "<control>" +
                      "<type>group</type>" +
                      "<description>Group Element Sub Menu 1</description>" +
                      "<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssText - 3).ToString() + "</posY>" +
                      "<posX>" + (subMenuXPos - ((240 * parentMenu.subMenuLevel1.Count)/2)).ToString() + "</posX>" +
                      "<width>1800</width>" +
                      "<height>41</height>" +
                      "<dimColor>ffffffff</dimColor>" +
                      "<layout>StackLayout(0,Horizontal)</layout>" +
                      "<animation effect=\"slide\" end=\"0,500\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>" +
                      "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"250\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>";

      for (int j = 0; j < parentMenu.subMenuLevel1.Count; j++)
      {
        onLeft = (parentMenu.subMenuLevel1ID + (j)).ToString();
        onRight = (parentMenu.subMenuLevel1ID + (j + 2)).ToString();
        if (j == 0)
        {
          onLeft = (parentMenu.subMenuLevel1ID + parentMenu.subMenuLevel1.Count).ToString();
          onRight = (parentMenu.subMenuLevel1ID + (j + 2)).ToString();
        }

        if (j == (parentMenu.subMenuLevel1.Count - 1))
        {
          onLeft = (parentMenu.subMenuLevel1ID + j).ToString();
          onRight = (parentMenu.subMenuLevel1ID + 1).ToString();
        }



        localxml += "<control>" +
                "<description>SUB ITEM " + j.ToString() + "</description>" +
                "<type>button</type>" +
                "<id>" + (parentMenu.subMenuLevel1ID + (j + 1)).ToString() + "</id>" +
                "<label>" + parentMenu.subMenuLevel1[j].displayName + "</label>" +
                "<width>240</width>" +
                "<height>41</height>" +
                "<font>font2</font>" +
                "<textalign>center</textalign>" +
                "<textvalign>middle</textvalign>" +
                "<textureFocus>submenu_bar_focus_alt.png</textureFocus>"; 

        if (parentMenu.subMenuLevel1[j].hyperlink == "196299")
          localxml += "<action>99</action>";
        else if (parentMenu.subMenuLevel1[j].hyperlink == "196297")
          localxml += "<action>97</action>";
        else if (parentMenu.subMenuLevel1[j].hyperlink == "196250")
          localxml += "<action>196250</action>";
        else
          if (parentMenu.subMenuLevel1[j].hyperlink == formMustayalucaEditor.musicSkinID && parentMenu.subMenuLevel1[j].hyperlinkParameter != "false")
            localxml += "<hyperlink>504</hyperlink>";
          else
            localxml += "<hyperlink>" + parentMenu.subMenuLevel1[j].hyperlink + "</hyperlink>";

        //Plugin Parameter handling
        if (parentMenu.subMenuLevel1[j].hyperlinkParameter != "false")
        {
          switch (parentMenu.subMenuLevel1[j].hyperlink)
          {
            case onlineVideosSkinID:
              localxml += "<hyperlinkParameter>site:" + parentMenu.subMenuLevel1[j].hyperlinkParameter + "|return:" + parentMenu.subMenuLevel1[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              break;
            default:
              localxml += "<hyperlinkParameter>" + parentMenu.subMenuLevel1[j].hyperlinkParameter + "</hyperlinkParameter>";
              break;
          }
        }

        localxml += "<onup>" + (parentMenu.id + 900).ToString() + "</onup>" +
                    "<ondown>" + (parentMenu.subMenuLevel1ID + (j + 1)).ToString() + "</ondown>" +
                    "<onright>" + onRight + "</onright>" +
                    "<onleft>" + onLeft + "</onleft>" +
                    "<visible>" + subMenuControl1(parentMenu) + ")</visible>" +
                  "</control>";
      }
      localxml += "</control>";

      return localxml;
    }


    //
    // Level 2
    //
    string writeSubMenuLevel2H(formMustayalucaEditor.menuItem parentMenu)
    {
      string dummyFocusControls = "Control.HasFocus(";

      for (int i = 0; i < parentMenu.subMenuLevel2.Count; i++)
      {
        if (i < (parentMenu.subMenuLevel2.Count - 1))
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")|Control.HasFocus(";
        else
          dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")";
      }


      localxml += "<control>" +
                      "<type>group</type>" +
                      "<description>Group Element Sub Menu 2</description>" +
                      "<posY>" + (int.Parse(txtMenuPos.Text) + basicHomeValues.offsetRssText + 41 - 3).ToString() + "</posY>" +
                      "<posX>" + (subMenuXPos - ((240 * parentMenu.subMenuLevel2.Count) / 2)).ToString() + "</posX>" +
                      "<width>1800</width>" +
                      "<height>41</height>" +
                      "<dimColor>ffffffff</dimColor>" +
                      "<layout>StackLayout(0,Horizontal)</layout>" +
                      "<animation effect=\"slide\" end=\"0,500\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">windowclose</animation>" +
                      "<animation effect=\"fade\" start=\"0\" end=\"100\" delay=\"250\" time=\"250\" acceleration=\"-0.1\" reversible=\"false\">visible</animation>";

      for (int j = 0; j < parentMenu.subMenuLevel2.Count; j++)
      {
        onLeft = (parentMenu.subMenuLevel1ID + (j + 100)).ToString();
        onRight = (parentMenu.subMenuLevel1ID + (j + 102)).ToString();
        if (j == 0)
        {
          onLeft = (parentMenu.subMenuLevel1ID + parentMenu.subMenuLevel2.Count).ToString();
          onRight = (parentMenu.subMenuLevel1ID + (j + 102)).ToString();
        }

        if (j == (parentMenu.subMenuLevel2.Count - 1))
        {
          onLeft = (parentMenu.subMenuLevel1ID + 100 + j).ToString();
          onRight = (parentMenu.subMenuLevel1ID + 101).ToString();
        }

        //localxml += "<control>" +
        //        "<description>SUB ITEM " + j.ToString() + "</description>" +
        //        "<type>button</type>" +
        //        "<id>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</id>" +
        //        "<label>" + parentMenu.subMenuLevel2[j].displayName + "</label>" +
        //        "<width>246</width>";

        localxml += "<control>" +
        "<description>SUB ITEM " + j.ToString() + "</description>" +
        "<type>button</type>" +
        "<id>" + (parentMenu.subMenuLevel1ID + (j + 1)).ToString() + "</id>" +
        "<label>" + parentMenu.subMenuLevel2[j].displayName + "</label>" +
        "<width>240</width>" +
        "<height>41</height>" +
        "<font>font2</font>" +
        "<textalign>center</textalign>" +
        "<textvalign>middle</textvalign>" +
        "<textureFocus>submenu_bar_focus_alt.png</textureFocus>"; 


        if (parentMenu.subMenuLevel2[j].hyperlink == "196299")
          localxml += "<action>99</action>";
        else if (parentMenu.subMenuLevel2[j].hyperlink == "196297")
          localxml += "<action>97</action>";
        else if (parentMenu.subMenuLevel2[j].hyperlink == "196250")
          localxml += "<action>196250</action>";
        else
          if (parentMenu.subMenuLevel2[j].hyperlink == formMustayalucaEditor.musicSkinID && parentMenu.subMenuLevel2[j].hyperlinkParameter != "false")
            localxml += "<hyperlink>504</hyperlink>";
          else
            localxml += "<hyperlink>" + parentMenu.subMenuLevel2[j].hyperlink + "</hyperlink>";

        //Plugin Parameter handling
        if (parentMenu.subMenuLevel2[j].hyperlinkParameter != "false")
        {
          switch (parentMenu.subMenuLevel2[j].hyperlink)
          {
            case onlineVideosSkinID:
              localxml += "<hyperlinkParameter>site:" + parentMenu.subMenuLevel2[j].hyperlinkParameter + "|return:" + parentMenu.subMenuLevel2[j].hyperlinkParameterOption + "</hyperlinkParameter>";
              break;
            default:
              localxml += "<hyperlinkParameter>" + parentMenu.subMenuLevel2[j].hyperlinkParameter + "</hyperlinkParameter>";
              break;
          }
        }

        //localxml += "<onleft>" + (parentMenu.subMenuLevel1ID + 1).ToString() + "</onleft>" +
        //         "<onright>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</onright>" +
        //         "<ondown>" + onRight + "</ondown>" +
        //         "<onup>" + onLeft + "</onup>" +
        //         "<visible allowhiddenfocus=\"true\">control.isvisible(" + (parentMenu.subMenuLevel1ID + 100).ToString() + ")</visible>" +
        //       "</control>";

        localxml += "<onup>" + (parentMenu.id + 900).ToString() + "</onup>" +
                    "<ondown>" + (parentMenu.subMenuLevel1ID + (j + 101)).ToString() + "</ondown>" +
                    "<onright>" + onRight + "</onright>" +
                    "<onleft>" + onLeft + "</onleft>" +
                    "<visible>" + subMenuControl2(parentMenu) + ")</visible>" +
                  "</control>";

      }
      localxml += "</control>";
      return localxml;
    }


    string writeSubMenuDummyControl1()
    {
      string dummyFocusControls = "Control.HasFocus(";
      localxml = "<!-- Dummy Controls for Sub Menu 1 Visibility -->";
      foreach (menuItem parentMenu in menuItems)
      {
        if (parentMenu.subMenuLevel1.Count > 0)
        {
          dummyFocusControls = "Control.HasFocus(";
          for (int i = 0; i < parentMenu.subMenuLevel1.Count; i++)
          {
            if (i < (parentMenu.subMenuLevel1.Count - 1))
              dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")|Control.HasFocus(";
            else
              if (parentMenu.subMenuLevel2.Count == 0)
                dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")";
              else
                dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 1)).ToString() + ")";
          }

          localxml += "<control>" +
                          "<description>dummy for lateral blade Sub Menu 1 visibility</description>" +
                          "<type>label</type>" +
                          "<id>" + (parentMenu.subMenuLevel1ID).ToString() + "</id>" +
                          "<label></label>" +
                          "<visible>" + dummyFocusControls + "</visible>" +
                        "</control>";
        }
      }
      return localxml;
    }

    string writeSubMenuDummyControl2()
    {
      string dummyFocusControls = "Control.HasFocus(";
      localxml = "<!-- Dummy Controls for Sub Menu 2 Visibility -->";
      foreach (menuItem parentMenu in menuItems)
      {
        if (parentMenu.subMenuLevel2.Count > 0)
        {
          dummyFocusControls = "Control.HasFocus(";
          for (int i = 0; i < parentMenu.subMenuLevel2.Count; i++)
          {
            if (i < (parentMenu.subMenuLevel2.Count - 1))
              dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")|Control.HasFocus(";
            else
              if (parentMenu.subMenuLevel2.Count == 0)
                dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")";
              else
                dummyFocusControls += (parentMenu.subMenuLevel1ID + (i + 101)).ToString() + ")";
          }

          localxml += "<control>" +
                          "<description>dummy for lateral blade Sub menu 2 visibility</description>" +
                          "<type>label</type>" +
                          "<id>" + (parentMenu.subMenuLevel1ID + 100).ToString() + "</id>" +
                          "<label></label>" +
                          "<visible>" + dummyFocusControls + "</visible>" +
                        "</control>";
        }
      }
      return localxml;
    }


  }
}
