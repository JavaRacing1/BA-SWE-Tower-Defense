using Godot;
using System;
using System.Diagnostics;

public partial class KnightEnemy : MeleeEnemy
{
	AnimatedSprite2D _knightEnemy;
	Area2D _attackRangeArea, _hitboxArea;
	Timer _attackTimer;

	public KnightEnemy()
	{
		//TODO: Change values
		_delay = 15;
		_animationDelay = 1;
		_actionAnimation = "idle";

		EnemyName = "KnightEnemy";
		WalkSpeed = 0.5f;
		Health = 25;
	}

	public override void _Ready()
	{
		_knightEnemy = GetNode<AnimatedSprite2D>("KnightEnemy");
		_knightEnemy.Play("walking");
        _knightEnemy.AnimationLooped += OnAnimationLooped;
        _attackRangeArea = GetNode<Area2D>("AttackRangeArea");
        _hitboxArea = GetNode<Area2D>("HitboxArea");

        _attackTimer = GetNode<Timer>("AttackTimer");
	}

	public override void _Process(double delta)
	{
		getStatuseffectDamage();

		if (!CanAttack() && !EnemyDefeated)
		{
			MoveEnemy(WalkSpeed);
		}

		if(Health <=0 && !EnemyDefeated) 
		{
			OnEnemyDefeated();
		}
	}

	private bool CanAttack()
	{
		if (_attackTimer.IsStopped() && !EnemyDefeated)
		{
			Defender closestTarget = SelectClosestTarget(_attackRangeArea);
			if (closestTarget != null && !closestTarget.ImmuneToDamage)
			{
				WalkSpeed = 0;
				_attackTimer.Start();
				_knightEnemy.Play("attacking");
				Attack(closestTarget, 1);
				return true;
			}
			else
			{
				if(!EnemyDefeated)
				{
					WalkSpeed = 0.5f;
					_knightEnemy.Play("walking");
				}
				return false;
			}
		}
		else
		{
			return false;
		}
	}

    private void OnEnemyDefeated()
    {
        WalkSpeed = 0;
        EnemyDefeated = true;
        _hitboxArea.QueueFree();
        _knightEnemy.Play("death");
    }
    private void OnAnimationLooped()
    {
        if (_knightEnemy.Animation == "death")
        {
            Destroy();
        }
    }
}
