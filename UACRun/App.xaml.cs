using System.Windows;

namespace UACRun
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App
  {
    public SqlLiteCustomerRepository SqlLiteCustomerRepository { get; set; }
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);
    }
  }
}