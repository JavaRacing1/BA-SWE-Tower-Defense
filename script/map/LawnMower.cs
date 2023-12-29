using Godot;
using System;
using System.Transactions;

public partial class LawnMower : CharacterBody2D
{
	private Area2D _HitboxArea;
	private bool _activated;

    public LawnMower() 
    { 
        
    }
	public override void _Ready()
	{
		_HitboxArea = GetNode<Area2D>("HitboxArea");
	}

	public override void _Process(double delta)
	{
		if(_activated)
		{
			Move();
			DestroyEnemyUnits();
		}
	}

    private void Move()
	{
		Vector2 movement = new(5, 0);
		Translate(movement);
		if (Position.X > 1200)
			QueueFree();
	}

	private void DestroyEnemyUnits()
	{
        foreach (Node2D body in _HitboxArea.GetOverlappingAreas())
        {
            if (body.GetParent() is Enemy && body.Name == "HitboxArea")
            {
                Enemy target = (Enemy)body.GetParent();
				target.Health -= 1000;
				//Ondefeated Methode ware auch moeglich, ist aber private und so nicht erreichbar
            }
        }
    }

	public bool ActivateLawnMover
	{
		get
		{
			return _activated;
		}
		set
		{
			_activated = true;
		}
	}
}
