using System.Collections.ObjectModel;
using System.Linq;
using YouTubeLoader.Extensions;
using YouTubeLoader.Interfaces;

namespace YouTubeLoader.ViewModels
{
    public class NavigationVm : Bindable
    {
        public NavigationVm()
        {
            PageViewModels = new ObservableCollection<IPageViewModel>
            {
                new DownloaderVm(),
                new SettingsVm()
            };

            if (PageViewModels.Count > 0)
                SelectedPageViewModel = PageViewModels[0];
        }

        private ObservableCollection<IPageViewModel> _pageViewModels;
        public ObservableCollection<IPageViewModel> PageViewModels
        {
            get { return _pageViewModels; }
            set { SetField(ref _pageViewModels, value); }
        }

        private IPageViewModel _selectedPageViewModel;
        public IPageViewModel SelectedPageViewModel
        {
            get { return _selectedPageViewModel; }
            set { SetField(ref _selectedPageViewModel, value); }
        }

        private RelayCommand _commandChangePageViewModel;
        public RelayCommand CommandChangePageViewModel
        {
            get
            {
                return _commandChangePageViewModel ??
                       (_commandChangePageViewModel = new RelayCommand(p => Execute_ChangePageViewModel(p as IPageViewModel), p => true));
            }
        }

        private void Execute_ChangePageViewModel(IPageViewModel pageViewModel)
        {
            if (SelectedPageViewModel.Name == pageViewModel.Name)
                return;

            SelectedPageViewModel = PageViewModels.First(x => x == pageViewModel);
        }
    }
}
