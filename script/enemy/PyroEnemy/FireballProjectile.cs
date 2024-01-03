using Godot;
using System;

public partial class FireballProjectile : projectile
{
    [Signal]
    public delegate void hitTargetEventHandler();

    private Vector2 _targetPosition;
    private Defender _target;
    private float _velocity;
    private bool _projectileHit = false;
    private Area2D _hitboxArea;
    private AnimatedSprite2D _fireballProjectile;

    public void Init(Vector2 targetPosition, Defender target, float velocity)
    {
        _targetPosition = targetPosition;
        _target = target;
        _velocity = velocity;
        Visible = true;
    }
    public override void _Ready()
    {
        _fireballProjectile = GetNode<AnimatedSprite2D>("FireballProjectile");
        _fireballProjectile.Play("fireball");
        _fireballProjectile.AnimationLooped += OnAnimationLooped;
        _hitboxArea = GetNode<Area2D>("HitboxArea");
    }

    public override void _Process(double delta)
    {
        MoveProjectile(-_velocity);//TODO: Pfeil auf Bahnen fliegen lassen

        if (CheckTarget() && !_projectileHit)
        {
            _projectileHit = true;
            EmitSignal(SignalName.hitTarget);
            _velocity = 0;
            _fireballProjectile.Play("explosion");

        }

        if (Position.X < _targetPosition.X - 150)//Falls Gegner schon besiegt wurde
        {
            _fireballProjectile.Play("explosion");
        }
    }

    private bool CheckTarget()
    {
        foreach (Node2D body in _hitboxArea.GetOverlappingAreas())
        {
            if (body.Name == "HitboxArea")
            {
                Node2D parent = (Node2D)body.GetParent();
                if (parent as Defender == _target)
                {
                    return true;
                }
            }
        }
        return false;
    }	

    private void OnAnimationLooped()
    {
        if(_fireballProjectile.Animation == "explosion")
        {
            QueueFree();
        }
    }
}



