using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Maui.Storage; // For Preferences

namespace MDFitJourney.Services
{
    public class RoutineService
    {
        private static readonly Lazy<RoutineService> _instance = new(() => new RoutineService());
        public static RoutineService Instance => _instance.Value;

        private readonly Dictionary<string, RoutineDay> routines = new()
        {
            { "Sunday", new RoutineDay() },
            { "Monday", new RoutineDay() },
            { "Tuesday", new RoutineDay() },
            { "Wednesday", new RoutineDay() },
            { "Thursday", new RoutineDay() },
            { "Friday", new RoutineDay() },
            { "Saturday", new RoutineDay() }
        };

        private RoutineService()
        {
            LoadFromPreferences();
        }

        public RoutineDay GetRoutineForDay(string day)
        {
            if (routines.TryGetValue(day, out var routine))
                return routine;
            return new RoutineDay();
        }

        public void SetRoutineForDay(string day, RoutineDay routine)
        {
            if (routines.ContainsKey(day))
            {
                routines[day] = routine;
                SaveToPreferences();
            }
        }

        public void SetRoutineTitle(string day, string title)
        {
            if (routines.ContainsKey(day))
                routines[day].Title = title;
            SaveToPreferences();
        }

        public void AddExercise(string day, Exercise exercise)
        {
            if (routines.ContainsKey(day))
                routines[day].Exercises.Add(exercise);
            SaveToPreferences();
        }

        public Dictionary<string, RoutineDay> GetAllRoutines()
        {
            return new Dictionary<string, RoutineDay>(routines);
        }

        public void SaveToPreferences()
        {
            var json = JsonSerializer.Serialize(routines);
            Preferences.Set("UserRoutines", json);
        }

        public void LoadFromPreferences()
        {
            if (Preferences.ContainsKey("UserRoutines"))
            {
                var json = Preferences.Get("UserRoutines", "");
                var loaded = JsonSerializer.Deserialize<Dictionary<string, RoutineDay>>(json);
                if (loaded != null)
                {
                    foreach (var kv in loaded)
                        routines[kv.Key] = kv.Value;
                }
            }
        }
    }

    public class RoutineDay
    {
        public string Title { get; set; } = "Not set";
        public List<Exercise> Exercises { get; set; } = new();
    }

    public class Exercise
    {
        public string Name { get; set; }
        public string Sets { get; set; }
        public string Reps { get; set; }
        public string Weight { get; set; }
    }
}
