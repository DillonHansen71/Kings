using System;

namespace Kings.Models.ManageViewModels
{
    public class Kingdom
    {
       // public Kingdom(int kingID)
      //  {
            //KingID = kingID;
            //Citizen = 1;
            //Gold = 0;
            //LastUpdated = DateTime.UtcNow;
        //}
        public Kingdom()
        {
            
        }
        public int ID { get; set; }
        public int Name { get; set; }
        public int KingID { get; set; }
        public int ResourceID { get; set; }
        public int BuildingID { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
