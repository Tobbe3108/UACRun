using System;
using System.Windows;
using Persistence;
using UACRun.UserControls;

namespace UACRun.Windows
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    private const int ToDisplay = 5;
    
    public MainWindow()
    {
      var sqlLiteRepository = new SqlLiteCustomerRepository();
      InitializeComponent();

      sqlLiteRepository.UpdateDatabase();
      
      var apps = sqlLiteRepository.GetAllApps() ?? throw new ArgumentNullException($"_sqlLiteRepository.GetAllApps()");
      
      var height = 0;
      for (var index = 0; index < apps.Count; index++)
      {
        if (index >= ToDisplay)
        {
          SearchPanelScrollViewer.MaxHeight = height;
        }
        height += 50;
      }

      foreach (var app in apps)
      {
        SearchItemPanel.Children.Add(new SearchResult(app));
      }
    }

    private void AddNewFavoriteButton_Click(object sender, RoutedEventArgs e)
    {

    }
  }
}