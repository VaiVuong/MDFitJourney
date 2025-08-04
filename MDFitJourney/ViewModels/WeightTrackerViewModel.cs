using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Generic;
using Microcharts;
using SkiaSharp;

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

        [ObservableProperty]
        private Chart chart;

        public WeightTrackerViewModel()
        {
            UpdateChart();
        }

        private void UpdateChart()
        {
            var sortedEntries = WeightEntries.OrderBy(e => e.Date).ToList();
            var chartEntries = new List<ChartEntry>();

            foreach (var entry in sortedEntries)
            {
                chartEntries.Add(new ChartEntry((float)entry.Weight)
                {
                    Label = entry.Date.ToString("MM/dd"),
                    ValueLabel = entry.Weight.ToString(),
                    Color = SKColor.Parse("#bbdf32")
                });
            }

            
            Chart = new LineChart
            {
                Entries = chartEntries,
                LineSize = 3,
                PointSize = 10,
                LabelColor = SKColors.White,
                PointMode = PointMode.Circle,
                BackgroundColor = SKColors.Transparent
            };
        }

        public void SaveWeight()
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
                    var newEntry = new WeightEntry
                    {
                        Date = SelectedDate.Date,
                        Weight = weightValue
                    };
                    WeightEntries.Add(newEntry);
                }

                var sortedEntries = WeightEntries.OrderBy(e => e.Date).ToList();
                WeightEntries.Clear();
                foreach (var entry in sortedEntries)
                {
                    WeightEntries.Add(entry);
                }

               
                UpdateChart();

                CurrentWeight = string.Empty;
                SelectedDate = DateTime.Today;
            }
            else
            {
                Console.WriteLine("Invalid weight format. Please enter a number.");
            }
        }
    }
}