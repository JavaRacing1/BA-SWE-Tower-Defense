using Godot;
using System;

public abstract partial class Enemy : Node
{
	protected bool _enemyDefeated = false;
	protected bool _enemyCrossedLastField = false;
	protected float _walkSpeed;
	protected string _name;
	//protected  _statusEffects;

	public abstract void Init();

	public float WalkSpeed
	{ 
		get 
		{ 
			return _walkSpeed; 
		}
		set 
		{ 
			_walkSpeed = value; 
		}
	}

	public string EnemyName
	{
		get { return _name; }
		set { _name = value; }
	}

	protected void Destroy()
	{

	}
}
