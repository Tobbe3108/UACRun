using System.Windows.Input;

namespace UACRun.UserControls
{
  /// <summary>
  /// Interaction logic for FavoriteButton.xaml
  /// </summary>
  public partial class FavoriteButton
  {
    public App App { get; set; }
    
    public FavoriteButton(App app)
    {
      InitializeComponent();
      App = app;
    }

    private void AppImage_MouseDown(object sender, MouseButtonEventArgs e)
    {

    }
  }
}
