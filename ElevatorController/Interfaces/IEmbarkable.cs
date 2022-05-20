namespace ElevatorController.Interfaces
{
    public interface IEmbarkable
    {
        int ToFloor { get; }
        public int Kilos { get; }
        public Direction Direction{ get; }
    }
}