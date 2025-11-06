
using System;

namespace Lift
{
  
    public interface IFloorState
    {
        int FloorNumber { get; }
        string GetFloorName();
        void OnArrival();
        void OnDeparture();
    }
}
