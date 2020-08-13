using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Domain.Models;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace Application.Helpers
{
  public static class AppHelper
  {
    public static IEnumerable<App> ReadAllInstalledApps()
    {
      var folderIdAppsFolder = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");
      var appsFolder = (ShellObject) KnownFolderHelper.FromKnownFolderId(folderIdAppsFolder);
      return (from app in (IKnownFolder) appsFolder select new App {Name = app.Name, AppUserModelId = app.ParsingName, Icon = app.Thumbnail.Icon}).ToList();
      
      // var name = app.Name;
      // var appUserModelId = app.ParsingName; // or app.Properties.System.AppUserModel.ID
      // var icon = app.Thumbnail.Bitmap;
      //System.Diagnostics.Process.Start("explorer.exe", @" shell:appsFolder\" + appModelUserID);
    }
  }
}