using RegionSyd.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace RegionSyd.ViewModels
{
    // Suggestion: en klasse til at generere automatiske forslag til matches mellem ambulancer og opgaver
    // automatiske forslag til matches skal findes ud fra en valgt opgave og en ambulances distance fra opgaven
    // det skal evt. passe med filtre i UI, så disponenten kun ser forslag, der er relevante
    // matches bedømmes ud fra de opgaver, ambulancerne allerede har fået, eller ud fra deres nuværende lokation?
    public class AutomaticMatchesViewModel : INotifyPropertyChanged
    {
        public Ambulance Ambulance { get; set; }
        public Models.Task Task { get; set; }
        public double Distance { get; set; }
        public string suggestionSummary { get; set; }
        public string savingsSummary { get; set; }
        public double SavedHours { get; set; }
        public double SavedDistance { get; set; }

        // en liste til de automatiske forslag
        public ObservableCollection<AutomaticMatchesViewModel> Suggestions { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        // Constructor med parametre til at initialisere forslag med real data
        public AutomaticMatchesViewModel(Ambulance ambulance, Models.Task task)
        {
            Ambulance = ambulance;
            Task = task;
            Distance = GenerateRandomDistance();
            SavedHours = GenerateRandomHours();
            SavedDistance = GenerateRandomDistance();
            suggestionSummary = $"Forslag: Ambulance {ambulance.AmbulanceId} match med opgave {task.TaskId}";
            savingsSummary = $"Distance: {Distance} km (Besparelse: {SavedHours}t, {SavedDistance} km)";
        }

        // Metoder til at generere tilfældige distancer og tidsbesparelser til hvert forslag     
        private double GenerateRandomDistance()
        {
            var random = new Random();
            return random.Next(1, 201); // Hvert forslag får en tilfældig distance til opgaven mellem 1 og 200 km
        }

        private double GenerateRandomHours()
        {
            var random = new Random();
            return random.Next(1, 6); // Hvert forslag får en tilfældig tidsbesparelse mellem 1 og 6 timer
        }

        // Parameterløs constructor til at generere dummydata (når SuggestionViewModel initialiseres uden reel data (dvs. uden parametre), bruges det her dummydata)
        // Alt herunder kan slettes når der er rigtige data at arbejde med
        // Ved sletning: Husk at lave sortering af Suggestions-listen; det kan evt. gøres i properties til listen, men det fungerer vist ikke direkte på observable collection?
        public AutomaticMatchesViewModel()
        {
            // DummyData: tre ambulancer
            var ambulance1 = new Ambulance(11, "Available");
            var task1 = new Models.Task { TaskId = 021 };

            var ambulance2 = new Ambulance(32, "Available");
            var task2 = new Models.Task { TaskId = 53 };

            var ambulance3 = new Ambulance(27, "Available");
            var task3 = new Models.Task { TaskId = 14 };

            Suggestions = new ObservableCollection<AutomaticMatchesViewModel>(
                new List<AutomaticMatchesViewModel>
                {
                    new AutomaticMatchesViewModel(ambulance1, task1),
                    new AutomaticMatchesViewModel(ambulance2, task2),
                    new AutomaticMatchesViewModel(ambulance3, task3)
                }.OrderBy(s => s.Distance)
            );      
        }
    }
}
