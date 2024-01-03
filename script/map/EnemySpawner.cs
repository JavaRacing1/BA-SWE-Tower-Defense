using Godot;
using System;

public partial class EnemySpawner : Node
{
    private Timer _spawnTimer;
    private (float, string)[] _spawnTimes;
    private Enemy _enemy;
    private int _currentSpawnIndex = 0;
    private bool finished = false;

    public bool Finished
    {
        get { return finished; }
    }

    public EnemySpawner(int levelNr)
    {
        switch (levelNr)
        {
            case 1:
                _spawnTimes = new (float, string)[] { (10, "SoldierEnemy"), (10, "SoldierEnemy"), (25, "KnightEnemy"), (20, "KnightEnemy"), (20, "BanditEnemy"), (1, "BanditEnemy"), (2, "SoldierEnemy"), (1, "SoldierEnemy"), (1, "SoldierEnemy"),(1, "PyroEnemy"),(1, "PyroEnemy"),(1, "PyroEnemy"), (20, "GiantEnemy"), (5,"") };
                break;
            default: break;
        }

        _spawnTimer = new Timer
        {
            Name = "SpawnTimer"
        };
        AddChild(_spawnTimer);
        _spawnTimer.WaitTime = 10.0f;
        _spawnTimer.OneShot = false;
        _spawnTimer.Timeout += OnSpawnTimerTimeout;
    }

    public void SpawnTimerStart()
    {
        _spawnTimer.Start();
    }

    private void OnSpawnTimerTimeout()
    {
        if (_currentSpawnIndex < _spawnTimes.Length)
        {
            SpawnEnemy(_spawnTimes[_currentSpawnIndex].Item2);
            _currentSpawnIndex++;

            if (_currentSpawnIndex < _spawnTimes.Length)
            {
                _spawnTimer.WaitTime = _spawnTimes[_currentSpawnIndex - 1].Item1;
            }
        }
        else
        {
            _spawnTimer.Stop();
            finished = true;
        }
    }

    private void SpawnEnemy(string enemyType)
    {
        int random = GD.RandRange(0, 4);
        int lane = random + 1;

        switch (enemyType)
        {
            case "KnightEnemy":
                _enemy = (KnightEnemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}/{enemyType}.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) - 20);
                AddChild(_enemy);
                GD.Print(enemyType + " spawnt auf Lane " + lane);
                break;
            case "BanditEnemy":
                _enemy = (BanditEnemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}/{enemyType}.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) + 10);
                AddChild(_enemy);
                GD.Print(enemyType + " spawnt auf Lane " + lane);
                break;
            case "GiantEnemy":
                _enemy = (GiantEnemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}/{enemyType}.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) - 28);
                AddChild(_enemy);
                GD.Print(enemyType + " spawnt auf Lane " + lane);
                break;
            case "SoldierEnemy":
                _enemy = (SoldierEnemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}/{enemyType}.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) +50);
                AddChild(_enemy);
                GD.Print(enemyType + " spawnt auf Lane " + lane);
                break;
            case "PyroEnemy":
                _enemy = (PyroEnemy)GD.Load<PackedScene>($"res://scene/enemy/{enemyType}/{enemyType}.tscn").Instantiate();
                _enemy.Position = new Vector2(1080, (random * 125) + 35);
                AddChild(_enemy);
                GD.Print(enemyType + " spawnt auf Lane " + lane);
                break;
            default: break;
        }
    }
}