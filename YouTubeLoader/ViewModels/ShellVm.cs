using System.Windows;
using YouTubeLoader.Extensions;

namespace YouTubeLoader.ViewModels
{
    public class ShellVm
    {
        public ShellVm()
        {
            
        }

        // Commands
        private RelayCommand _commandShutdownApp;
        public RelayCommand CommandShutdownApp
        {
            get
            {
                return _commandShutdownApp ??
                       (_commandShutdownApp = new RelayCommand(Execute_ShutdownApp, p => true));
            }
        }

        private static void Execute_ShutdownApp(object obj)
        {
            Application.Current.MainWindow.Close();
        }
    }
}
