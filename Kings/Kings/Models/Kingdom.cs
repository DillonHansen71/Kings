using System;

namespace Kings.Models.ManageViewModels
{
    public class Kingdom
    {
        public int ID { get; set; }
        public int KingID { get; set; }
        public int Citizen { get; set; }
        public int Gold { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
