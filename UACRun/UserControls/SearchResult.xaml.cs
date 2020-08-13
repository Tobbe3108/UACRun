using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UACRun.UserControls
{
  /// <summary>
  /// Interaction logic for SearchResult.xaml
  /// </summary>
  public partial class SearchResult
  {
    public Domain.Models.App AppModel { get; set; }
    
    public SearchResult(Domain.Models.App app)
    {
      InitializeComponent();
      AppModel = app;
      
      AppName.Content = AppModel.Name;
      using var icon = AppModel.Icon;
      AppImange.Source =  Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
    }
    
    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {

    }

    private void MainPanel_MouseDown(object sender, MouseButtonEventArgs e)
    {

    }
  }
}
