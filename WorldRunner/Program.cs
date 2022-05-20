using ElevatorController.Domain;
using ElevatorController.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace WorldRunner
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var numberOfFloors = 10;
            var floorHandlers = Enumerable.Range(0, numberOfFloors)
            .Select(c=> new Floor(c, numberOfFloors)).ToDictionary(c=>c.FloorNumber, c=>(IFloorEventsHandler)c);

            var elevatorController = new ElevatorBuilder()
                .CreateElevatorFloors(floorHandlers)
                .CreateElevators(4)
                .Build();

            foreach(var floor in floorHandlers.Values)
            {
                ((Floor)floor).SetContorller(elevatorController);
            }

            ((Floor)floorHandlers[2]).AddEmbarkable(4, 57);
            ((Floor)floorHandlers[2]).AddEmbarkable(1, 37);
            ((Floor)floorHandlers[5]).AddEmbarkable(2, 37);
            ((Floor)floorHandlers[1]).AddEmbarkable(3, 55);



            Console.WriteLine("Completed");

        }
    }

    public class Floor : IFloorEventsHandler
    {
        public List<Embarkable> _embarkables = new();
        private IElevatorController _elevatorController;
        private int _maxFloorNumber;

        public IFloorEventsHandler Handler {get;}
        public int FloorNumber { get ;set;}

        public Floor(int floorNumber, int maxFloorNumber)
        {
            FloorNumber = floorNumber;
            _maxFloorNumber = maxFloorNumber;
            FloorNumber = floorNumber;
        }

        public void AddEmbarkable(int toFloorNumber, int kilos)
        {
            _embarkables.Add(new Embarkable {
                fromFloor = FloorNumber,
                ToFloor = toFloorNumber,
                Kilos = kilos
            });
            _elevatorController.RequestElevator(FloorNumber,  FloorNumber > toFloorNumber ? Direction.Down : Direction.Up);
        }

        public void SetContorller(IElevatorController elevatorController)
        {
            _elevatorController = elevatorController;   
        }

        public void OnElevatorArrived(IElevator elevator)
        {
            if(!_embarkables.Any()) return;

            var direction = elevator.CurrentDirection == Direction.None 
                                ? _embarkables.First().Direction
                                : elevator.CurrentDirection;
            
            foreach (var embarkable in _embarkables.ToArray())
            {
                if (embarkable.Direction != direction) continue;

                try 
                {
                    elevator.Embark(embarkable);
                    Console.WriteLine("This person is going to " + embarkable.ToFloor);
                }
                catch(ElevatorFullException)
                {
                }

                _embarkables.Remove(embarkable);
            }

            elevator.Move();
        }
    }
}