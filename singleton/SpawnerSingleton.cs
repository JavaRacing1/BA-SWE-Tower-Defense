using Godot;
using System;

public partial class SpawnerSingleton : Node
{
	private static EnemySpawner _enemySpawner;

	public override void _Ready()
	{
		_enemySpawner = GD.Load<PackedScene>("res://script/enemy/EnemySpawner.cs").Instantiate() as EnemySpawner;
		AddChild(_enemySpawner);
    }

	public static EnemySpawner GetEnemySpawner()
	{
		return _enemySpawner;
	}
}
