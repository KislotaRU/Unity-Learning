using System.Collections.Generic;

public class UnitStateMachine : StateMachine<UnitStateType>
{
    public UnitStateMachine(Dictionary<UnitStateType, IState> states) : base(states)
    {
        CurrentStateType = UnitStateType.Idle;
        PreviousStateType = UnitStateType.Idle;
    }
}