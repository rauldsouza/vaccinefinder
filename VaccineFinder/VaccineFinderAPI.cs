using System.Net;
using System.IO;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VaccineFinder.Vaccine;
using VaccineFinder.Notification;

namespace VaccineFinder
{
    class VaccineFinderAPI
    {
        int count = 0;
        const string URL = "https://cdn-api.co-vin.in/api/v2/admin/location/states";
        const string requestURL = "https://cdn-api.co-vin.in/api/v2/appointment/sessions/calendarByDistrict?district_id={0}&date={1}";

        TelegramNotifier telegramNotifier = null;

        public VaccineFinderAPI()
        {
            telegramNotifier = new TelegramNotifier();
        }

        private string GetDate()
        {
            //TODO Cosider DateLogic
            //CoWin App takes the current Day
            return DateTime.Now.ToString("dd-MM-yyyy");
        }

        public void FindAppointments(List<int> districts)
        {

            if (districts == null || districts.Count == 0)
                return;

            Root rootObj = null;

            string date = GetDate();

            Console.WriteLine("Scanning Iteration {0} and Time-{1} ", ++count, DateTime.Now.ToString());

            foreach (int district in districts)
            {
                if (district < 100)
                    continue;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format(requestURL, district, date));
                request.Method = "GET";
                request.ContentType = "application/json";

                try
                {
                    WebResponse webResponse = request.GetResponse();
                    using (Stream webStream = webResponse.GetResponseStream() ?? Stream.Null)
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        string response = responseReader.ReadToEnd();

                        //TODO - Try filtering with LINQ as opposed to deserialization.
                        rootObj = JsonConvert.DeserializeObject<Root>(response);
                        Console.Out.WriteLine("No. of Centers for district id {0} is {1}", district.ToString(), rootObj.centers.Count.ToString());

                        foreach (Center c in rootObj.centers)
                        {
                            List<Session> found = c.sessions.FindAll(s => s.min_age_limit == 18 && s.available_capacity > 0);
                            if (found != null && found.Count > 0)
                                if (found[0].available_capacity_dose1 > 1)
                                {
                                    telegramNotifier.Notify(c);
                                    /*
                                    Console.Beep();
                                    Console.Beep();*/
                                    Console.WriteLine("Center - {0} with availability - {1}", c.name, found[0].available_capacity_dose1);
                                }
                        }
                    }
                }
                catch (Exception e)
                {

                    Console.Beep();
                    Console.Out.WriteLine("-----------------");
                    Console.Out.WriteLine(e.Message);
                }
            }
        }
    }
}
