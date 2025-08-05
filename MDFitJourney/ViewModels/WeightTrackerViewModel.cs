using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Microcharts;
using SkiaSharp;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace MDFitJourney.ViewModels
{
    public partial class WeightEntry : ObservableObject
    {
        [ObservableProperty]
        private DateTime date;

        [ObservableProperty]
        private double weight;
    }

    public partial class WeightTrackerViewModel : ObservableObject
    {
        [ObservableProperty]
        private string currentWeight;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;

        public ObservableCollection<WeightEntry> WeightEntries { get; set; } = new ObservableCollection<WeightEntry>();

        private Chart _weightChart;
        public Chart WeightChart
        {
            get => _weightChart;
            private set => SetProperty(ref _weightChart, value);
        }

        public WeightTrackerViewModel()
        {
            // Optional: Add initial data to test the chart
            WeightEntries.Add(new WeightEntry { Date = DateTime.Today, Weight = 179 });

            UpdateChart();
        }

        [RelayCommand]
        private async Task SaveWeight()
        {
            if (double.TryParse(CurrentWeight, out double weightValue))
            {
                var existingEntry = WeightEntries.FirstOrDefault(e => e.Date.Date == SelectedDate.Date);

                if (existingEntry != null)
                {
                    existingEntry.Weight = weightValue;
                }
                else
                {
                    WeightEntries.Add(new WeightEntry
                    {
                        Date = SelectedDate.Date,
                        Weight = weightValue
                    });
                }

                // Sort and update the collection
                var sortedEntries = WeightEntries.OrderBy(e => e.Date).ToList();
                WeightEntries.Clear();
                foreach (var entry in sortedEntries)
                    WeightEntries.Add(entry);

                CurrentWeight = string.Empty;
                SelectedDate = DateTime.Today;

                UpdateChart();

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

        private async void UpdateChart()
        {
            WeightChart = null;
            OnPropertyChanged(nameof(WeightChart));

            await Task.Delay(50); // Small delay

            if (WeightEntries == null || WeightEntries.Count == 0)
            {
                WeightChart = new LineChart
                {
                    Entries = new List<ChartEntry>(),
                    BackgroundColor = SKColors.Transparent
                };
                return;
            }

            var entries = WeightEntries
                .Where(e => e != null && e.Weight > 0)
                .OrderBy(e => e.Date)
                .Select(e => new ChartEntry((float)e.Weight)
                {
                    Label = e.Date.ToString("MM/dd"),
                    ValueLabel = e.Weight.ToString("F1"),
                    Color = SKColor.Parse("#bbdf32"),
                    ValueLabelColor = SKColors.White,
                    TextColor = SKColors.White
                }).ToList();

            WeightChart = new LineChart
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 4,
                PointMode = PointMode.Circle,
                PointSize = 6,
                LabelTextSize = 20,
                BackgroundColor = SKColors.Transparent
            };
        }
    }

}
