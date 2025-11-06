using System;

namespace Lift
{
  
    public class FloorStateContext
    {
        private IFloorState currentState;

        public FloorStateContext()
        {
           
            currentState = FloorStateFactory.GetState(1);
        }

        public void ChangeFloor(int floorNumber)
        {
            currentState.OnDeparture();
            currentState = FloorStateFactory.GetState(floorNumber);
            currentState.OnArrival();
        }

        public IFloorState GetCurrentState()
        {
            return currentState;
        }

        public string GetCurrentFloorName()
        {
            return currentState.GetFloorName();
        }
    }
}