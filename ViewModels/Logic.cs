using RegionSyd.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionSyd.ViewModels
{
    // Suggestion: en klasse til at generere automatiske forslag til matches mellem ambulancer og opgaver
    // automatiske forslag til matches skal findes ud fra en valgt opgave og en ambulances distance fra opgaven
    // ELLER? skal de generes ud fra alle opgaver eller kun opgaven som disponenten p.t. har valgt?
    // det skal evt. passe med filtre i UI, så disponenten kun ser forslag, der er relevante
    // matches bedømmes ud fra de opgaver, ambulancerne allerede har fået, eller ud fra deres nuværende lokation?
    public class Suggestion : INotifyPropertyChanged
    {
        public Ambulance Ambulance { get; set; }
        public Models.Task Task { get; set; }
        public double Distance { get; set; }
        public string suggestionSummary { get; set; }
        public string savingsSummary { get; set; }
        public double SavedHours { get; set; }
        public double SavedDistance { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Suggestion(Ambulance ambulance, Models.Task task)
        {
            Ambulance = ambulance;
            Distance = GenerateRandomDistance();
            SavedHours = GenerateRandomHours();
            SavedDistance = GenerateRandomDistance();
            suggestionSummary = $"Forslag: Ambulance {ambulance.AmbulanceId} match med opgave {task.TaskId}";
            savingsSummary = $"Distance: {Distance} km (Besparelse: {SavedHours}t, {SavedDistance} km)"; // kunne evt. udregnes ud fra det næstbedste forslag?
        }

        // metoder til at generere tilfældige distancer og besparelser til hvert forslag hver gang programmet kører
        private double GenerateRandomDistance()
        {
            var random = new Random();
            return random.Next(1, 201); // hvert forslag får en tilfældig distance til opgaven mellem 1 og 200 km
        }
        private double GenerateRandomHours()
        {
            var random = new Random();
            return random.Next(1, 6); // hvert forslag får en tilfældig tidsbesparelse mellem 1 og 6 timer hver gang programmet kører
        }
    }

    public class DummyData : INotifyPropertyChanged
    {
        // en liste over automatiske forslag
        public ObservableCollection<Suggestion> Suggestions { get; set; }

        public DummyData()
        {
            // DummyData: tre ambulancer
            var ambulance1 = new Ambulance(11, "Available");
            var task1 = new Models.Task { TaskId = 021 };

            var ambulance2 = new Ambulance(32, "Available");
            var task2 = new Models.Task { TaskId = 53 };

            var ambulance3 = new Ambulance(27, "Available");
            var task3 = new Models.Task { TaskId = 14 };

            // DummyData: en liste med tre automatiske forslag
            var suggestions = new List<Suggestion>
            {
                new Suggestion(
                    ambulance1,
                    task1
                ),
                new Suggestion(
                    ambulance2,
                    task2
                ),
                new Suggestion(
                    ambulance3,
                    task3
                )
            };

            // forslagene sorteres efter distance (nærmeste først)
            Suggestions = new ObservableCollection<Suggestion>(suggestions.OrderBy(s => s.Distance));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
