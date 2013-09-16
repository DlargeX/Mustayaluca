using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MustayalucaEditor;

namespace MustayalucaEdit
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      formMustayalucaEditor appStart = new formMustayalucaEditor();
      if (args.Length != 0)
      {
        if (args[0].ToLower().Contains("regenerateonly"))
          appStart.reGenterateMenu();
      }
      else
        appStart.ShowPlugin();
    }
  }
}