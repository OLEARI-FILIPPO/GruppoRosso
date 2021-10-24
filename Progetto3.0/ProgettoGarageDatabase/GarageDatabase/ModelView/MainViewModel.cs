using GarageDatabase.Classi;
using GarageDatabase.View;

namespace GarageDatabase.ModelView
{ 
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand GestioneViewCommand { get; set; }
        public RelayCommand InputCommand { get; set; }

        public RelayCommand StoricoViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }
        public StoricoViewModel StoricoVM { get; set; }
        public InputViewModel InputVM { get; set; }
        public GestioneViewModel GestioneVM { get; set; }

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            GestioneVM = new GestioneViewModel();
            StoricoVM = new StoricoViewModel();

            InputVM = new InputViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            StoricoViewCommand = new RelayCommand(o =>
            {
                CurrentView = StoricoVM;
            });

            InputCommand = new RelayCommand(o =>
            {
                CurrentView = InputVM ;
            });


            GestioneViewCommand = new RelayCommand(o =>
            {
                CurrentView = GestioneVM;
            });
        }
    }
}
