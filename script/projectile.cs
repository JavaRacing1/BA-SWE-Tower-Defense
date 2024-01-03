using Godot;
using System;

public partial class projectile : Node2D
{
	protected virtual void MoveProjectile(float ShotSpeed)
	{
        Vector2 movement = new(ShotSpeed, 0);
        Translate(movement);
    }
}
