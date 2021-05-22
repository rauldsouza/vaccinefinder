using System;
using System.Collections.Generic;
using System.Timers;

namespace VaccineFinder
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        static List<int> districts = new List<int> { 294, 265, 276 };
        static VaccineFinderAPI api = new VaccineFinderAPI();

        static void Main(string[] args)
        {
           
            //All Districts of Bangalore
           
            SetTimer();
            Console.WriteLine("\nPress the Enter key to abort Vaccine Finder...\n");
            Console.WriteLine("Vaccine Finder Started - {0:HH:mm:ss.fff}", DateTime.Now.ToString());
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();
            Console.WriteLine("Terminating Application");
      
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(30000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            api.FindAppointments(districts);
        }
    }
}
