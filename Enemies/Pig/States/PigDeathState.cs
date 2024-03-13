using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PigDeathState : PigBaseState
{
    public PigDeathState(PigController pig, string animName) : base (pig, animName) 
    {

    }

    public override void Enter()
    {
        base.Enter();
        DropItems();
        //pig.gameObject.SetActive(false);
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

    private void DeathParticle() {

    }

    private void DropItems() {
        foreach (var items in pig.stats.itemDrops) {
            pig.Instantiate(items, pig.stats.dropForce, pig.stats.torque);
        }
    }
}
