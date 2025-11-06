using System;

namespace Lift
{
    public class Floor1State : IFloorState
    {
        public int FloorNumber => 1;

        public string GetFloorName()
        {
            return "Ground Floor";
        }

        public void OnArrival()
        {
            System.Diagnostics.Debug.WriteLine("State Pattern: Arrived at Floor 1");
        }

        public void OnDeparture()
        {
            System.Diagnostics.Debug.WriteLine("State Pattern: Leaving Floor 1");
        }
    }
}