
using System;
using System.Collections.Generic;
using ElevatorController.Interfaces;
using System.Collections;
using System.Threading;
using System.Linq;

namespace ElevatorController.Domain
{
    public class Elevator : IElevator
    {
        public Direction CurrentDirection { get; set; }
        public int CurrentCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public ElevatorState CurrentState { get; set; } = ElevatorState.Idle;
        public int CurrentFloor { get; set; }

        public int MaxFloor { get; set; }

        private List<IEmbarkable> Embarkees { get; set; } = new List<IEmbarkable>();

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

        public void Embark(IEmbarkable embarkable)
        {
            if (CurrentCapacity + embarkable.Kilos > MaxCapacity)
            {
                throw new ElevatorFullException();
            }

            Embarkees.Add(embarkable);
            CurrentCapacity += embarkable.Kilos;
            if(CurrentDirection == Direction.None)
                CurrentDirection = embarkable.Direction;
        }

        public void Move()
        {
            switch (CurrentDirection) {
                case Direction.Up:
                    while (this.CurrentFloor <= MaxFloor)
                    {
                        if (!Embarkees.Any()) break;

                        CurrentState = ElevatorState.Moving;
                        CurrentFloor++;
                        Thread.Sleep(500);
                        var floorDisEmbarkees = Embarkees.Where(e => e.ToFloor == CurrentFloor).ToList();
                        if (floorDisEmbarkees.Any())
                        {
                            Console.WriteLine($"{floorDisEmbarkees.Sum(e => e.Kilos)} kilos disembarked on floor {CurrentFloor}");
                        }

                        Embarkees  = Embarkees.Where(e => e.ToFloor != CurrentFloor).ToList();
                    }
                    break;
                case Direction.Down:
                    while (this.CurrentFloor > 0)
                    {
                        if (!Embarkees.Any()) break;

                        CurrentState = ElevatorState.Moving;
                        CurrentFloor--;
                        Thread.Sleep(500);
                        var floorDisEmbarkees = Embarkees.Where(e => e.ToFloor == CurrentFloor).ToList();
                        if (floorDisEmbarkees.Any())
                        {
                            Console.WriteLine($"{floorDisEmbarkees.Sum(e => e.Kilos)} kilos disembarked on floor {CurrentFloor}");
                        }

                        Embarkees  = Embarkees.Where(e => e.ToFloor != CurrentFloor).ToList();
                    }
                    break;
            }


            
        }


    }
}