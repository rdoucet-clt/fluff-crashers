using System.Collections.Generic;
using System.Linq;
using ElevatorController.Interfaces;

namespace ElevatorController.Domain
{
    public class FloorEventsHandler : IFloorEventsHandler
    {
        private List<Embarkable> _embarkable;

        public FloorEventsHandler(List<Embarkable> embarkable)
        {
            _embarkable = embarkable;
        }

        public void OnElevatorArrived(IElevator elevator)
        {
            if(!_embarkable.Any()) return;

            var direction = elevator.CurrentDirection == Direction.None 
                                ? _embarkable.First().Direction
                                : elevator.CurrentDirection;
            
            foreach (var embarkable in _embarkable.ToArray())
            {
                if (embarkable.Direction != direction) continue;

                try 
                {
                    elevator.Embark(embarkable);
                }
                catch(ElevatorFullException)
                {
                }

                _embarkable.Remove(embarkable);
            }
        }
    }
}