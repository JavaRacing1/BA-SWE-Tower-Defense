using Godot;
using System;

public partial class GiantEnemy : MeleeEnemy
{
    AnimatedSprite2D _giantEnemy;
    Area2D _attackRangeArea, _hitboxArea;
    Timer _attackTimer;

    public GiantEnemy()
    {
        //TODO: Change values
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";

        EnemyName = "GiantEnemy";
        WalkSpeed = 0.3f;
        Health = 60;
    }

    public override void _Ready()
    {
        _giantEnemy = GetNode<AnimatedSprite2D>("GiantEnemy");
        _giantEnemy.Play("walking");
        _giantEnemy.AnimationLooped += OnAnimationLooped;
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

        if (Health <= 0 && !EnemyDefeated)
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
                _giantEnemy.Play("attacking");
                Attack(closestTarget, 10);
                return true;
            }
            else
            {
                if (!EnemyDefeated)
                {
                    WalkSpeed = 0.3f;
                    _giantEnemy.Play("walking");
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
        _giantEnemy.Play("death");

        //spawnt 2 Grunts beim Tod

        for(int i = 0; i<2; i++)
        {
            GruntEnemy gruntEnemy = (GruntEnemy)GD.Load<PackedScene>("res://scene/enemy/GruntEnemy/GruntEnemy.tscn").Instantiate();
            gruntEnemy.GlobalPosition = GlobalPosition - new Vector2(25*i +100, -40);
            GetParent().AddChild(gruntEnemy);
        }
    }

    private void OnAnimationLooped()
    {
        if (_giantEnemy.Animation == "death")
        {
            Destroy();
        }
    }
}
