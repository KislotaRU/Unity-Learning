using System;
using UnityEngine;

public class CollectingState : State
{
    private readonly Unit _unit;
    private readonly CollectingConfiguration _configuration;

    private Item _item;

    public event Action Collected;
    
    public CollectingState(Unit unit, CollectingConfiguration configuration)
    {
        _unit = unit;
        _configuration = configuration;
    }

    public void SetTarget(Item item) =>
        _item = item;

    public override void Enter()
    {
        Item item = null;

        Collider[] colliders = Physics.OverlapSphere(_unit.Hand.transform.position, _configuration.CaptureRadius, _configuration.TargetMask);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Item temporaryItem) == false)
                continue;

            if (temporaryItem.GetType() == _item.GetType())
            {
                item = temporaryItem;
                break;
            }
        }

        if (item != null)
        {
            item.transform.SetParent(_unit.Hand);
            item.transform.position = _unit.Hand.position;

            item.HandleCollect();
        }

        _unit.StateMachine.SetState<IdleState>(UnitStateType.Idle, null);

        Collected?.Invoke();
    }
}