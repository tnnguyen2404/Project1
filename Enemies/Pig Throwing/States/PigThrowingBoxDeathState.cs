using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigThrowingBoxDeathState : PigThrowingBoxBaseState
{
    public PigThrowingBoxDeathState(PigThrowingBoxController pigThrowing, string animName) : base (pigThrowing, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
        DropItems();
        pigThrowing.gameObject.SetActive(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Exit()
    {
        base.Exit();
    }

    private void DropItems() {
        foreach (var item in pigThrowing.stats.itemDrops) {
            pigThrowing.InstantiateItemDrop(item, pigThrowing.stats.dropForce, pigThrowing.stats.torque);
        }
    }
}
