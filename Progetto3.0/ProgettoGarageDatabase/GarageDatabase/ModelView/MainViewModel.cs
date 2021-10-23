using GarageDatabase.Classi;
using GarageDatabase.View;

namespace GarageDatabase.ModelView
{ 
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand GestioneViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public GestioneView GestioneVM { get; set; }

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            GestioneVM = new GestioneView();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            GestioneViewCommand = new RelayCommand(o =>
            {
                CurrentView = GestioneVM;
            });
        }
    }
}
