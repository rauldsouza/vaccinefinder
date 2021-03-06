using System;
using System.Net;
using VaccineFinder.Vaccine;

namespace VaccineFinder.Notification
{
    class TelegramNotifier : INotification
    {
        //TODO : Move to Prop file
        const string botToken = "1695612041:AAFscyqTbWjSZZcAkFA_GJvXZPUbdLRPHxs";
        const string chatid = "-1001272859899";
        const string urlstring = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
        const string msgFormatter = "{3}, {0} {1} {2}, {4}, Avail - {5}";

        public void Notify(Center c)
        {
            if (c == null)
                return;

           string retval = string.Empty;
           string url = string.Format(urlstring, botToken, chatid,CreateMessage(c));
            try
            {
                using (var webClient = new WebClient())
                {
                    retval = webClient.DownloadString(url);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Unable to Post to Telegram:", e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        private string CreateMessage(Center c)
        {
            return string.Format(msgFormatter, c.name, c.address, c.pincode, c.sessions[0].date, 
                c.sessions[0].vaccine, c.sessions[0].available_capacity_dose1);
               
        }
    }
}
