namespace ElevatorController.Interfaces
{
    public interface IElevator
    {
        Direction CurrentDirection { get; }
        int CurrentCapacity { get; }
        int MaxCapacity { get; set; }
        ElevatorState CurrentState { get; set; }
        int CurrentFloor { get; set; }
        void RequestFloor(int floorNumber);
        void Embark(IEmbarkable embarkable);
        void DisEmbark(IEmbarkable embarkable);
    }
}
