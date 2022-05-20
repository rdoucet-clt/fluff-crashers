using System.Collections.Generic;

namespace ElevatorController.Interfaces
{
    public interface IElevatorBuilder
    {
        IElevatorBuilder CreateElevatorFloors(Dictionary<int, IFloorEventsHandler> floorEventHandlers);
        IElevatorBuilder CreateElevators(int numberOfElevators = 4);
        IElevatorController Build();
    }
}