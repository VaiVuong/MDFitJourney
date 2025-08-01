using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;

namespace MDFitJourney.ViewModels
{
    public class WeightEntry
    {
        public DateTime Date { get; set; }
        public double Weight { get; set; }
    }


    public partial class WeightTrackerViewModel : ObservableObject
    {

        [ObservableProperty]
        private string currentWeight;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;


        public ObservableCollection<WeightEntry> WeightEntries { get; set; } = new ObservableCollection<WeightEntry>();

        public WeightTrackerViewModel()
        {
            
        }


        [RelayCommand]
        private void SaveWeight()
        {
            
            if (double.TryParse(CurrentWeight, out double weightValue))
            {
                
                var existingEntry = WeightEntries.FirstOrDefault(e => e.Date.Date == SelectedDate.Date);

                if (existingEntry != null)
                {
                    
                    existingEntry.Weight = weightValue;
                    
                    
                    var temp = WeightEntries.ToList();
                    WeightEntries.Clear();
                    foreach (var entry in temp.OrderBy(e => e.Date))
                    {
                        WeightEntries.Add(entry);
                    }
                }
                else
                {
                    
                    var newEntry = new WeightEntry
                    {
                        Date = SelectedDate.Date,
                        Weight = weightValue
                    };
                    
                    
                    WeightEntries.Add(newEntry);
                    var temp = WeightEntries.ToList();
                    WeightEntries.Clear();
                    foreach (var entry in temp.OrderBy(e => e.Date))
                    {
                        WeightEntries.Add(entry);
                    }
                }
                
                
                CurrentWeight = string.Empty;
                SelectedDate = DateTime.Today;
            }
            else
            {
                Console.WriteLine("Invalid weight format. Please enter a number.");
            }
        }
        
     
        [RelayCommand]
        private void EditWeight(WeightEntry entry)
        {
            if (entry != null)
            {
                SelectedDate = entry.Date;
                CurrentWeight = entry.Weight.ToString();
            }
        }
    }
}