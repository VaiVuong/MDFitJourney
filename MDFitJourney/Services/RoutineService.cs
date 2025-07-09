using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDFitJourney.Services
{
    public class RoutineService
    {
        private static readonly Lazy<RoutineService> _instance = new(() => new RoutineService());
        public static RoutineService Instance => _instance.Value;

        private readonly Dictionary<string, string> routines = new()
        {
            { "Sunday", "Not set" },
            { "Monday", "Not set" },
            { "Tuesday", "Not set" },
            { "Wednesday", "Not set" },
            { "Thursday", "Not set" },
            { "Friday", "Not set" },
            { "Saturday", "Not set" }
        };

        public string GetRoutineForDay(string day)
        {
            if (routines.TryGetValue(day, out var routine))
                return routine;
            return "Not set";
        }

        public void SetRoutineForDay(string day, string routine)
        {
            routines[day] = routine;
        }

        public Dictionary<string, string> GetAllRoutines()
        {
            return new Dictionary<string, string>(routines);
        }
    }
}

