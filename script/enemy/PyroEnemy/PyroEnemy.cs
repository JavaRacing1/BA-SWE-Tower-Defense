using Godot;
using System;

public partial class PyroEnemy : RangedEnemy
{
    private Defender _targetDefender;
    AnimatedSprite2D _pyroEnemy;
    Area2D _attackRangeArea, _hitboxArea;
    Timer _attackTimer;

    public PyroEnemy()
    {
        //TODO: Change values and add action animation
        _delay = 2;
        _animationDelay = 1;
        _actionAnimation = "idle";
        Health = 6;
        WalkSpeed = 0.4f;
        _arrowVelocity = 5;
    }

    public override void _Ready()
    {
        _attackRangeArea = GetNode<Area2D>("AttackRangeArea");
        _attackTimer = GetNode<Timer>("AttackTimer");
        _pyroEnemy = GetNode<AnimatedSprite2D>("PyroEnemy");
        _hitboxArea = GetNode<Area2D>("HitboxArea");
        _pyroEnemy.Play("walking");
        _pyroEnemy.AnimationLooped += OnAnimationLooped;
    }

    private Defender SelectTarget()
    {
        Defender closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Node2D body in _attackRangeArea.GetOverlappingAreas())
        {
            if (body.Name == "HitboxArea")
            {
                Node2D parent = (Node2D)body.GetParent();
                if (parent is Defender)
                {
                    float distance = Position.DistanceTo(parent.Position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = parent as Defender;
                    }
                }
            }
        } 
        return closestTarget;
    }

    private bool CanAttack()
    {
        if (_attackTimer.IsStopped())
        {
            _targetDefender = SelectTarget();
            if (_targetDefender != null)
            {
                WalkSpeed = 0;
                return true;
            }
            else
            {
                WalkSpeed = 0.4f;
                return false;
            }
        }
        else
            return false;

    }

    public override void _Process(double delta)
    {
        if (!EnemyDefeated)
        {
            if (CanAttack())
            {
                _pyroEnemy.Play("attacking");
                SpawnFireball();
                _attackTimer.Start();
            }
            else if (_targetDefender == null)
            {
                _pyroEnemy.Play("walking");
                MoveEnemy(WalkSpeed);
            }

            if (Health <= 0)
            {
                OnEnemyDefeated();
            }
        }
    }

    private void OnEnemyDefeated()
    {
        WalkSpeed = 0;
        EnemyDefeated = true;
        _hitboxArea.QueueFree();
        _pyroEnemy.Play("death");
    }

    private void OnAnimationLooped()
    {
        if (_pyroEnemy.Animation == "death")
        {
            Destroy();
        }
    }

    private void SpawnFireball()
    {
        FireballProjectile fireball = (FireballProjectile)GD.Load<PackedScene>("res://scene/enemy/PyroEnemy/FireballProjectile.tscn").Instantiate();
        fireball.Init(_targetDefender.Position, _targetDefender, _arrowVelocity);
        fireball.hitTarget += FireballHit;
        fireball.TopLevel = true;
        fireball.Position = new Vector2(GlobalPosition.X-20, GlobalPosition.Y+10);
        AddChild(fireball);
    }

    private void FireballHit()
    {
        Attack(_targetDefender, 2);
    }
}
