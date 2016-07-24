using YouTubeLoader.Extensions;
using YouTubeLoader.Interfaces;

namespace YouTubeLoader.ViewModels
{
    public class SettingsVm : Bindable, IPageViewModel
    {
        public SettingsVm()
        {
            
        }

        public string Name => nameof(SettingsVm);


    }
}
