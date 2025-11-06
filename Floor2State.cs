using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lift
{
    
    public class Floor2State : IFloorState
    {
        public int FloorNumber => 2;

        public string GetFloorName()
        {
            return "Second Floor";
        }

        public void OnArrival()
        {
            System.Diagnostics.Debug.WriteLine("State Pattern: Arrived at Floor 2");
        }

        public void OnDeparture()
        {
            System.Diagnostics.Debug.WriteLine("State Pattern: Leaving Floor 2");
        }
    }
}