using ElevatorController.Interfaces;
public interface IRule
{
    bool Check(IElevator elevator);
}

public class CapacityRule : IRule
{
    public bool Check(IElevator elevator)
    {
        return elevator.CurrentCapacity < elevator.MaxCapacity;
    }
}

public class ElevateStateRule : IRule
{
    public bool Check(IElevator elevator)
    {
        return elevator.CurrentState == ElevatorState.Idle;
    }
}