using System;
using System.Net;
using VaccineFinder.Vaccine;

namespace VaccineFinder.Notification
{
    class TelegramNotifier : INotification
    {
        const string botToken = "1695612041:AAFscyqTbWjSZZcAkFA_GJvXZPUbdLRPHxs";
        const string chatid = "-1001272859899";
        const string urlstring = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
        const string msgFormatter = "{3}, {0} {1} {2}, {4}, Dose 1 Avail - {5}";
        string prevDose1Msg = string.Empty;

        //Test Message
        //https://api.telegram.org/bot1695612041:AAFscyqTbWjSZZcAkFA_GJvXZPUbdLRPHxs/sendMessage?chat_id=-1001272859899&text=Test

        public void Notify(Center c, int availability1Count)
        {
            if (c == null)
                return;


           string dose1msg = CreateMessage(c, availability1Count);
            if (prevDose1Msg == dose1msg)
                return;


            string retval = string.Empty;
            string url = string.Format(urlstring, botToken, chatid,dose1msg);
            try
            {
                using (var webClient = new WebClient())
                {
                    retval = webClient.DownloadString(url);
                    prevDose1Msg = dose1msg;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to Post to Telegram:", e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private string CreateMessage(Center c, int availabilityCount)
        {
            return string.Format(msgFormatter, c.name, c.address, c.pincode, c.sessions[0].date, 
                c.sessions[0].vaccine, availabilityCount);
               
        }
    }
}
