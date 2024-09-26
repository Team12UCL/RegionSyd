using RegionSyd.Models;
using System.Collections.ObjectModel;

public class Trip
{
    public int TripId { get; set; }
    public int AmbulanceId { get; set; }
    public int TaskId { get; set; }


    public Trip( int ambulanceId, int taskId)
    {
        
        AmbulanceId = ambulanceId;
        TaskId = taskId;

    }

    public Trip(int tripId, int ambulanceId, int taskId)
    {
        TripId = tripId;
        AmbulanceId = ambulanceId;
        TaskId = taskId;
        
    }

    // Metode til at finde alle "Status=Available" ambulancer
    // Spørgsmål:
    // metoden returnerer en observable collection til visning i UI i stedet for en list? Konversionen kan også ske i viewmodel
    // i stedet for parameteret "strings" filtreres der efter dato og region som sættes i UI?
    // Hvad angiver status på ambulancer? Er det sat ud fra opgave-kriterier eller bare om ambulancen kører i dag?
    //public ObservableCollection<Ambulance> FindAvailableAmbulances(DateTime taskTime, Region taskRegion)
    //{
    //    // en liste over alle ambulancer skal hentes fra et repository eller persistens?
    //    List<Ambulance> availableAmbulances = GetAmbulancesFromRepository().Where(a => a.Status == "Available").ToList();
    //    // en liste der fyldes med ambulancer, der matcher filtre
    //    List<Ambulance> matchingAmbulances = FindMatchingAmbulances(taskTime, taskRegion, availableAmbulances);

    //    // hvis der ikke findes nogen ambulancer med returkørsler, så returneres alle ambulancer, ellers returneres de ambulancer, der har returkørsler
    //    // listen skal evt. merges med listen over alle ambulancer, hvis man ikke vil bruge de matchende ambulancer
    //    if (matchingAmbulances.Count() != 0)
    //    {
    //        Console.WriteLine("Der er ambulancer med matchende returkørsler tilgængelige. Returner matches.");
    //        ObservableCollection<Ambulance> ambulances = new ObservableCollection<Ambulance>(matchingAmbulances);
    //        return ambulances;
    //    }
    //    else
    //    {
    //        Console.WriteLine("Der er ingen ambulancer med matchende returkørsler tilgængelige. Returnerer alle ambulancer.");
    //        ObservableCollection<Ambulance> ambulances = new ObservableCollection<Ambulance>(availableAmbulances);
    //        return ambulances;
    //    }
    //}

    //// Ekstra metode til at filtrere ambulancer efter region og tidspunkt
    //// Spørgsmål:
    //// Skal ambulancerne allerede have en liste med tildelte opgaver? Så vi ved, hvor de er henne hvornår og kan matche dem derfra?
    //// Hvordan anslås regionen ud fra destinationen på en Task?
    //private static List<Ambulance> FindMatchingAmbulances(DateTime taskTime, Region taskRegion, List<Ambulance> availableAmbulances, TimeSpan? maxAllowedDelay = null)
    //{
    //    List<Ambulance> matchingAmbulances = new List<Ambulance>();
    //    // servicemålet: transport senest 3 timer efter behandling (kan ændres via UI i særtilfælde?)
    //    maxAllowedDelay ??= TimeSpan.FromHours(3);

    //    // ekstra tjek for at finde ambulancer med matchende returkørsler
    //    foreach (var ambulance in availableAmbulances)
    //    {
    //        // AssignedTasks er de opgaver, ambulancerne allerede har fået. Hvis de har en opgave i samme region, der skal afleveres inden for 3 timer, så er de matchende
    //        foreach (var task in ambulance.AssignedTasks)
    //        {
    //            // udregn tidsforskellen mellem taskTime og tidspunktet hvor ambulancen fuldfører en opgave i regionen (skal helst være under 3 timer)
    //            TimeSpan timeDifference = task.DropoffTime - taskTime; // find differencen mellem de to tidspunkter på dagen for at få en TimeSpan (der skal være under 3 timer)

    //            // Tjek om dropoff-tiden er inden for 3 timer efter taskTime og om dropff-regionen er den samme region som opgaven er i
    //            if (task.Region == taskRegion && timeDifference <= maxAllowedDelay && timeDifference >= TimeSpan.Zero) // TimeSpan.Zero kan også sættes til et stykke tid, så ambulancer, der ankommer inden opgaven godtages
    //            {
    //                matchingAmbulances.Add(ambulance); // tilføj ambulancen til listen over tilgængelige
    //                break; // gå til næste ambulance for at undgå duplikater
    //            }
    //        }
    //    }

    //    return matchingAmbulances;
    //}

    //// Kommer ambulanceId og taskId fra selected items i kollektionerne i UI eller noget filter eller søgning?
    //public void AssignTaskToAmbulance(Task taskId, Ambulance ambulanceId)
    //{
    //    // lav et tjek om ambulancen kan tage opgaven før der kan tildeles? Evt. ud fra ders andre opgaver?
    //    ambulanceId.AssignedTasks.Add(taskId);
    //}
}