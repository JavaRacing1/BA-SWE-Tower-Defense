using Godot;
using System;

public abstract partial class MeleeEnemy : Enemy
{
	protected Node _currentTower;

	public Node CurrentTower
	{
		get { return _currentTower; }
		set { _currentTower = value; }
	}

    public virtual void OnTowerEnteredBody(Node2D tower)
    {

	}
	public override void Action()
	{

	}

    protected Defender SelectClosestTarget(Area2D _attackRangeArea)
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
        return (Defender)closestTarget;
    }

    public virtual void Attack(Defender closestTarget, int damage)
	{
		closestTarget.Health -= damage;
        //GD.Print("Angriff auf: " + closestTarget.Name + " HP: " + closestTarget.Health);
	}





}
