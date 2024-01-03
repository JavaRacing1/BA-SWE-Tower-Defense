using Godot;
using System;

public partial class arrowProjectile : projectile
{
    [Signal]
    public delegate void hitTargetEventHandler();

	private Vector2 _targetPosition;
	private Enemy _target;
	private float _velocity;
    private Area2D _HitboxArea;
	private AnimatedSprite2D _animatedSprite;

	public void Init(Vector2 targetPosition, Enemy target, float velocity)
	{
		_targetPosition = targetPosition;
		_target = target;
		_velocity = velocity;
		Visible = true;
    }
	public override void _Ready()
	{
        _animatedSprite = GetNode<AnimatedSprite2D>("Arrow");
		_animatedSprite.Play("arrow");
		_HitboxArea = GetNode<Area2D>("HitboxArea");
    }

	public override void _Process(double delta)
	{
		MoveProjectile(_velocity);//TODO: Pfeil auf Bahnen fliegen lassen

		if(CheckTarget())
		{
			EmitSignal(SignalName.hitTarget);
            QueueFree();
        }

		if(Position.X > _targetPosition.X + 150)//Falls Gegner schon besiegt wurde
		{
			QueueFree();//schoener machen, er soll dann auf den Boden fliegen und verschwinden, nicht einfach so
		}
	}

	private bool CheckTarget()
	{
        foreach (Node2D body in _HitboxArea.GetOverlappingAreas())
        {
            if (body.Name == "HitboxArea")
            {
                Node2D parent = (Node2D)body.GetParent();
                if (parent as Enemy ==_target)
                {
					return true;
                }
            }
        }
		return false;
    }
}
