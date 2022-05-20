using System;
using ElevatorController.Interfaces;

namespace ElevatorController.Domain
{
    public class Embarkable : IEmbarkable
    {
        public int Kilos { get; set;}
        public int fromFloor { get; set;}
        public int ToFloor { get; set; }

        public int Passengers => throw new NotImplementedException();

        public Direction Direction {
            get 
            {
                if (fromFloor > ToFloor) {
                    return Direction.Down;
            }

                return Direction.Up;
            }
        }
    }
}
