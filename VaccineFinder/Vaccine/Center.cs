using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineFinder.Vaccine
{
    public class Center
    {
        public int center_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string state_name { get; set; }
        public string district_name { get; set; }
        public string block_name { get; set; }
        public int pincode { get; set; }
        public int lat { get; set; }
        public int @long { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string fee_type { get; set; }
        public List<Session> sessions { get; set; }
        public List<VaccineFee> vaccine_fees { get; set; }
    }
}
