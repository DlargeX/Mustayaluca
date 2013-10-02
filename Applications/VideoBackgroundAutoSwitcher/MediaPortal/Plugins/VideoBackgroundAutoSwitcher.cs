namespace MediaPortal.Plugins
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using MediaPortal.GUI.Library;
    using MediaPortal.Player;
    using MediaPortal.Util;
    
    public class VideoBackgroundAutoSwitcher : IPluginReceiver, IPlugin, ISetupForm
    {
        private OnActionHandler handler;

        public string Author()
        {
            return "Leo";
        }

        public bool CanEnable()
        {
            return true;
        }

        public bool DefaultEnabled()
        {
            return true;
        }

        public string Description()
        {
            return "Plugin to automatically activate video background mode when a video is played and returning to GUI";
        }

        public bool GetHome(out string buttonText, out string buttonImage, out string buttonImageFocused, out string hoverImage)
        {
            string str;
            hoverImage = str = "";
            buttonImageFocused = str;
            buttonText = buttonImage = str;
            return false;
        }

        public int GetWindowId()
        {
            return 0;
        }

        public bool HasSetup()
        {
            return false;
        }

        private void OnAction(MediaPortal.GUI.Library.Action action)
        {
            if (((action.wID == MediaPortal.GUI.Library.Action.ActionType.ACTION_SHOW_GUI) || (action.wID == MediaPortal.GUI.Library.Action.ActionType.ACTION_SHOW_FULLSCREEN)) && GUIGraphicsContext.Vmr9Active)
            {
                GUIGraphicsContext.ShowBackground = false;
                GUIGraphicsContext.Overlay = false;
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
                GUIGraphicsContext.ShowBackground = false;
                GUIGraphicsContext.Overlay = false;
            }
        }

        public string PluginName()
        {
            return "VideoBackgroundAutoSwitcher";
        }

        public void ShowPlugin()
        {
        }

        public void Start()
        {
            Log.Info("VideoBackgroundAutoSwitcher : Plugin started", new object[0]);
            this.handler = new OnActionHandler(this.OnAction);
            GUIWindowManager.OnNewAction += this.handler;
            g_Player.PlayBackStarted += new g_Player.StartedHandler(this.OnPlayBackStarted);
            g_Player.PlayBackEnded += new g_Player.EndedHandler(this.OnPlayBackEnded);
        }

        public void Stop()
        {
            GUIWindowManager.OnNewAction -= this.handler;
        }

        public bool WndProc(ref Message msg)
        {
            return false;
        }
    }
}

