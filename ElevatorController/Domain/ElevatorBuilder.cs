using ElevatorController.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ElevatorController.Domain
{
    public class ElevatorBuilder : IElevatorBuilder
    {
        private Dictionary<int, IFloorEventsHandler> _floorEventHandlers;
        private IEnumerable<IElevator> _elevators;

        public IElevatorController Build()
        {
            return new ElevatorController(_floorEventHandlers, _elevators);
        }

        public IElevatorBuilder CreateElevatorFloors(Dictionary<int, IFloorEventsHandler> floorEventHandlers)
        {
            _floorEventHandlers = floorEventHandlers;
            return this;
        }


        public IElevatorBuilder CreateElevators(int numberOfElevators = 4)
        {
            _elevators = Enumerable.Range(0, numberOfElevators)
                            .Select(c => new Elevator(600, _floorEventHandlers.Keys.Count(), 1));

            return this;
        }
    }
}