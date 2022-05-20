using System.Collections.Generic;
using System.Linq;
using ElevatorController.Interfaces;

namespace ElevatorController.Domain
{
    public class ElevatorController : IElevatorController
    {
        private Dictionary<int, IFloorEventsHandler> _floorEventHandlers;
        private IEnumerable<IElevator> _elevators;
        private IList<IRule> _requestRules;

        public ElevatorController(Dictionary<int, IFloorEventsHandler> floorEventHandlers, IEnumerable<IElevator> elevators)
        {
            _floorEventHandlers = floorEventHandlers;
            _elevators = elevators;
            _requestRules = new List<IRule> {
                new CapacityRule(),
                new ElevateStateRule()
            };
        }

        public void RequestElevator(int fromFloor, Direction direction)
        {
            _floorEventHandlers[fromFloor].OnElevatorArrived(GetClosestReadyElevator(fromFloor, direction));
        }

        private IElevator GetClosestReadyElevator(int fromFloor, Direction direction)
        {
            var readyElevators = _elevators.Where(e => _requestRules.All(r => r.Check(e))).ToList();

            switch (direction) {
                case Direction.Up:
                    return readyElevators.OrderByDescending(e => e.CurrentFloor)
                        .Where(e => e.CurrentFloor <= fromFloor).First();
                case Direction.Down:
                    return readyElevators.OrderBy(e => e.CurrentFloor)
                        .Where(e => e.CurrentFloor >= fromFloor).First();
            }

            throw new System.Exception("Error getting closest ready elevator.");
        }
    }
}