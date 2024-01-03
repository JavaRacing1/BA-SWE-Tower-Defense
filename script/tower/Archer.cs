using Godot;
using System;

public partial class Archer : RangeDefender
{
    private Enemy _targetEnemy;
    public Archer()
    {
        //TODO: Change values and add action animation
        _delay = 2;
        _animationDelay = 1;
        _actionAnimation = "idle";
        Health = 5;
        _damage = 2;
        _ArrowVelocity = 5;
    }

    public override void Action()
    {

    }

    public override void _Ready()
    {
        _AttackArea = GetNode<Area2D>("AttackArea");
        _AttackTimer = GetNode<Timer>("AttackTimer");
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _HitboxArea = GetNode<Area2D>("HitboxArea");
        _AttackTimer.WaitTime = _delay; ;
        _animatedSprite.Play(_actionAnimation);
        _animatedSprite.AnimationLooped += OnAnimationLooped;
    }

    private Enemy SelectTarget()
    {
        Enemy closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Node2D body in _AttackArea.GetOverlappingAreas())
        {
            if (body.Name == "HitboxArea")
            {
                Node2D parent = (Node2D)body.GetParent();
                if (parent is Enemy)
                {
                    float distance = Position.DistanceTo(parent.Position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = parent as Enemy;
                    }
                }
            }
        }
        return closestTarget;
    }

    private bool CanAttack()
    {
        if (_AttackTimer.IsStopped())
        {
            _targetEnemy = SelectTarget();
            if (_targetEnemy != null)
                return true;
            else
                return false;
        }
        else
            return false;

    }

    public override void _Process(double delta)
    {
        if (!DefenderDefeated)
        {
            if (CanAttack())
            {
                _animatedSprite.Play("attack");
                SpawnArrow();
                _AttackTimer.Start();
            }
            else if (_targetEnemy == null)
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

    private void SpawnArrow()
    {
        arrowProjectile arrow = (arrowProjectile)GD.Load<PackedScene>("res://scene/tower/arrowProjectile.tscn").Instantiate();
        arrow.Init(_targetEnemy.Position, _targetEnemy, _ArrowVelocity);
        arrow.hitTarget += ArrowHit;
        //arrow.Position = new Vector2(Position.X+100,Position.Y+40);
        arrow.TopLevel = true;
        arrow.Position = new Vector2(GlobalPosition.X+80,GlobalPosition.Y+55);
        AddChild(arrow);
    }

    private void ArrowHit()
    {
        Attack(_targetEnemy, _damage);
        _targetEnemy.AddStatusEffect("burn");//eigentlich erst bei Upgrade
    }
}