namespace ElevatorController.Interfaces
{
    public interface IElevatorController
    {
        void RequestElevator(int fromFloor, Direction direction);
        
    }
}