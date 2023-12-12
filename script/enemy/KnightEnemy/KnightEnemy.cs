using Godot;
using System;

public partial class KnightEnemy : MeleeEnemy
{
	AnimatedSprite2D _knightEnemy;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

    public override void Init()
    {
		EnemyName = "KnightEnemy";
    }
}
