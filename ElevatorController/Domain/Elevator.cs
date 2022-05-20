
using System;
using ElevatorController.Interfaces;

namespace ElevatorController.Domain
{
    public class Elevator : IElevator
    {
        public Direction CurrentDirection { get; set; }
        public int CurrentCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public ElevatorState CurrentState { get; set; } = ElevatorState.Idle;
        public int CurrentFloor { get; set; }
        public int TargetFloor { get; set; }
        public int MaxFloor { get; set; }

        public Elevator()
        {
            MaxCapacity = 600;
            MaxFloor = 10;
            CurrentFloor = 1;
        }

        public Elevator(int maxCapacity, int floors, int startingFloor)
        {
            MaxCapacity = maxCapacity;
            MaxFloor = floors;
            CurrentFloor = startingFloor;

            if (CurrentFloor > MaxFloor || CurrentFloor < 0)
            {
                throw new Exception("Invalid starting floor for elevator!");
            }
        }

        public void DisEmbark(IEmbarkable embarkable)
        {
            CurrentCapacity -= embarkable.Kilos;
            if (CurrentFloor == TargetFloor)
            {
                CurrentDirection = Direction.None;
                CurrentState = ElevatorState.Idle;
            }
        }

        public void Embark(IEmbarkable embarkable)
        {
            if (CurrentCapacity + embarkable.Kilos > MaxCapacity)
            {
                throw new ElevatorFullException();
            }

            CurrentCapacity += embarkable.Kilos;
        }

        public void RequestFloor(int floorNumber)
        {
            if (floorNumber > MaxFloor || floorNumber < 0)
            {
                throw new Exception("Invalid request floor!");
            }

            if (CurrentDirection == Direction.None && floorNumber != CurrentFloor)
            {
                TargetFloor = floorNumber;
                CurrentState = ElevatorState.Moving;
                CurrentDirection = (TargetFloor > CurrentFloor) ? Direction.Up : Direction.Down;
            }
        }
    }
}