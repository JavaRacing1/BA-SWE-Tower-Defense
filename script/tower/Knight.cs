using Godot;
using System;
using System.Collections.Generic;

public  partial class Knight : MeleeDefender
{
	private List<Enemy> _attackableEnemiess = new List<Enemy>();

	public Knight()
	{
		//TODO: Change values and add action animation
		_delay = 1.5f;
		_animationDelay = 1;
		_actionAnimation = "idle";
		_damage = 1;
		Health = 30;
	}
	public override void _Ready()
	{
		_AttackArea = GetNode<Area2D>("AttackArea");
		_HitboxArea = GetNode<Area2D>("HitboxArea");
		_AttackTimer = GetNode<Timer>("AttackTimer");
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
		_AttackTimer.WaitTime= _delay; ;
		_animatedSprite.Play(_actionAnimation);
        _animatedSprite.AnimationLooped += OnAnimationLooped;
    }

	private List<Enemy> SelectTargets()
	{
		List<Enemy> EnemyList = new List<Enemy>();

		foreach (Node2D body in _AttackArea.GetOverlappingAreas())
		{
			if (body.Name == "HitboxArea")
			{
				Node2D parent = (Node2D)body.GetParent();
				if (parent is Enemy)
				{
					EnemyList.Add(parent as Enemy);
				}
			}
		}
		return EnemyList;
	}

	private bool CanAttack()
	{
		if (_AttackTimer.IsStopped())
		{
			_attackableEnemiess = SelectTargets();
			if (_attackableEnemiess.Count > 0)
				return true;
			else
				return false;
		}
		else
			return false;

	}
	
	public override void _Process(double delta)
	{
		if(!DefenderDefeated)
		{
            if (CanAttack())
            {
                _animatedSprite.Play("attack");
                foreach (Enemy enemy in _attackableEnemiess)
                {
                    Attack(enemy, _damage);
                    _AttackTimer.Start();
                }
            }
            else if (_attackableEnemiess.Count == 0)
            {
                _animatedSprite.Play("idle");
            }

            if (Health <= 0)
            {
                OnDefenderDefeated();
            }
        }
	}

	private void OnDefenderDefeated()
    {
        _DefenderDefeated = true;
        _HitboxArea.QueueFree();
        _animatedSprite.Play("death");
    }

	private void OnAnimationLooped()
    {
        if (_animatedSprite.Animation == "death")
        {
            Destroy();
        }
    }
}
