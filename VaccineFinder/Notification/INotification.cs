using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineFinder.Vaccine;

namespace VaccineFinder.Notification
{
    interface INotification
    {
        //public INotificationProvider;
         void Notify(Center c, int availabilityCount);

    }
}
