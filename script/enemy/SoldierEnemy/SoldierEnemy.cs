using Godot;
using System;

public partial class SoldierEnemy : MeleeEnemy
{
    AnimatedSprite2D _soldierEnemy;
    Area2D _attackRangeArea, _hitboxArea;
    CollisionShape2D _hitbox;
    Timer _attackTimer, _regenerationTimer;
    private bool _isRegenerating = false;
    private bool _hasRegenerated = false;

    public SoldierEnemy()
    {
        //TODO: Change values
        _delay = 15;
        _animationDelay = 1;
        _actionAnimation = "idle";

        EnemyName = "SoldierEnemy";
        WalkSpeed = 0.6f;
        Health = 7;
    }

    public override void _Ready()
    {
        _soldierEnemy = GetNode<AnimatedSprite2D>("SoldierEnemy");
        _soldierEnemy.Play("walking");
        _soldierEnemy.AnimationLooped += OnAnimationLooped;
        _attackRangeArea = GetNode<Area2D>("AttackRangeArea");
        _hitboxArea = GetNode<Area2D>("HitboxArea");
        _hitbox = _hitboxArea.GetChild<CollisionShape2D>(0);

        _attackTimer = GetNode<Timer>("AttackTimer");
        _regenerationTimer = GetNode<Timer>("RegenerationTimer");
    }

    public override void _Process(double delta)
    {
        getStatuseffectDamage();

        if (!CanAttack() && !EnemyDefeated && !_isRegenerating)
        {
            MoveEnemy(WalkSpeed);
        }

        if (Health <= 0 && !EnemyDefeated && !_hasRegenerated)
        {
            Regenerate();
        }
        else if(Health <= 0 && !EnemyDefeated && _hasRegenerated)
        {
            OnEnemyDefeated();
        }
    }

    private bool CanAttack()
    {
        if (_attackTimer.IsStopped() && !EnemyDefeated && !_isRegenerating)
        {
            Defender closestTarget = SelectClosestTarget(_attackRangeArea);
            if (closestTarget != null && !closestTarget.ImmuneToDamage)
            {
                WalkSpeed = 0;
                _attackTimer.Start();
                _soldierEnemy.Play("attacking");
                Attack(closestTarget, 1);
                return true;
            }
            else
            {
                if (!EnemyDefeated && !_isRegenerating)
                {
                    WalkSpeed = 0.6f;
                    _soldierEnemy.Play("walking");
                }
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void Regenerate()
    {
        _hasRegenerated = true;
        _isRegenerating = true;
        ImmuneToDamage = true;
        Health = 10;
        _soldierEnemy.Play("regenerate");
        _hitboxArea.CollisionLayer = 0;
        _hitboxArea.CollisionMask = 0;
        GD.Print("regeneration started, HP: " + Health);
    }

    private void EndRegeneration()
    {
        _isRegenerating = false;
        ImmuneToDamage = false;
        _soldierEnemy.Play("walking");
        WalkSpeed = 1f;

        _hitboxArea.CollisionLayer = 1;
        _hitboxArea.CollisionMask = 1;
    }

    private void OnEnemyDefeated()
    {
        WalkSpeed = 0;
        EnemyDefeated = true;
        _hitboxArea.QueueFree();
        _soldierEnemy.Play("death");
    }

    private void OnAnimationLooped()
    {
        if (_soldierEnemy.Animation == "death")
        {
            Destroy();
        }
        else if(_soldierEnemy.Animation == "regenerate")
        {
            EndRegeneration();
        }
    }
}
