using Godot;
using System;

public abstract partial class MeleeEnemy : Enemy
{
	protected int _currentTower;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public override abstract void Init();
	

	protected void OnTowerEnteredBody()
	{

	}

	protected void Action()
	{

	}

	protected void AttackTower()
	{

	}
}
