using System;
using System.Collections.Generic;

namespace Lift
{
 
    public class FloorStateFactory
    {
        private static Dictionary<int, IFloorState> states = new Dictionary<int, IFloorState>
        {
            { 1, new Floor1State() },
            { 2, new Floor2State() }
            
        };

        public static IFloorState GetState(int floorNumber)
        {
            if (states.ContainsKey(floorNumber))
                return states[floorNumber];

            throw new ArgumentException($"Floor {floorNumber} does not exist");
        }

        public static bool IsValidFloor(int floorNumber)
        {
            return states.ContainsKey(floorNumber);
        }
    }
}