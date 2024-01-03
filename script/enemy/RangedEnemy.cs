using Godot;
using System;

public partial class RangedEnemy : Enemy
{
    protected float _arrowVelocity = 5;

    public override void Action()
    {

    }

    protected void Attack(Defender target, int damage)
    {
        target.Health -= damage;
        GD.Print(target.Name + " HP: " + target.Health);
    }
}
