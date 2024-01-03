using Godot;
using Godot.Collections;
using System;
using System.Linq;
using System.Text.Json;

public partial class PlayerData : Node
{
    private int _volume = 100;
    private Array<int> _completedLevels = new();
    private Array<string> _unlockedTowers = new(){
        "knight",
        "spearman",
        "wall",
        "goldmine",
        "archer",
        "fire_trap"
    };

    /// <summary>
    /// The volume set by the player
    /// </summary>
    public int Volume
    {
        get
        {
            return _volume;
        }
        set
        {
            _volume = Math.Clamp(value, 0, 100);
        }
    }

    /// <summary>
    /// An array containing the numbers of all completed levels
    /// </summary>
    public Array<int> CompletedLevels
    {
        get
        {
            return _completedLevels;
        }
    }

    /// <summary>
    /// An array containing the names of all unlocked towers
    /// </summary>
    public Array<string> UnlockedTowers
    {
        get
        {
            return _unlockedTowers;
        }
    }

    public override void _Ready()
    {
        if (!FileAccess.FileExists("user://player.dat"))
        {
            Save();
        }
        else
        {
            Load();
        }
    }

    /// <summary>
    /// Adds a level number to the completed level numbers array
    /// </summary>
    /// <param name="levelNr">The level number to be saved as a completed level</param>
    public void AddCompletedLevelNumber(int levelNr)
    {
        _completedLevels.Add(levelNr);
    }

    /// <summary>
    /// Adds a tower type to the unlocked towers array
    /// </summary>
    /// <param name="towerName">The tower name to be saved as an unlocked tower</param>
    public void AddUnlockedTower(string towerName)
    {
        _unlockedTowers.Add(towerName);
    }

    /// <summary>
    /// Loads the saved player data from the local data file
    /// </summary>
    /// <exception cref="System.IO.FileLoadException">When the data file could not be accessed</exception>
    /// <exception cref="JsonException">When the data file contains invalid JSON</exception>
	public void Load()
    {
        if (!FileAccess.FileExists("user://player.dat"))
        {
            return;
        }

        using FileAccess saveFile = FileAccess.Open("user://player.dat", FileAccess.ModeFlags.Read);
        if (saveFile == null)
        {
            Error fileError = FileAccess.GetOpenError();
            GD.PrintErr($"File Error: {FileAccess.GetOpenError()}");
            throw new System.IO.FileLoadException($"File load exception: {fileError}");
        }

        string fileData = saveFile.GetLine();
        var json = new Json();
        Error parseResult = json.Parse(fileData);
        if (parseResult != Error.Ok)
        {
            string errorMsg = $"JSON Parse Error: {json.GetErrorMessage()} in {fileData} at line {json.GetErrorLine()}";
            GD.Print(errorMsg);
            throw new JsonException(errorMsg);
        }

        var dataDict = new Dictionary<string, Variant>((Dictionary)json.Data);

        _volume = (int)dataDict["Volume"];
        _completedLevels = (Array<int>)dataDict["CompletedLevels"];

        Array<string> savedUnlocks = (Array<string>)dataDict["UnlockedTowers"];
        foreach (string unlockedTower in savedUnlocks)
        {
            if (!_unlockedTowers.Contains(unlockedTower))
            {
                _unlockedTowers.Add(unlockedTower);
            }
        }
    }

    /// <summary>
    /// Saves the player data to a local data file
    /// </summary>
    /// <exception cref="System.IO.FileLoadException">When the data file could not be accessed</exception>()
	public void Save()
    {
        var dataDict = new Dictionary<string, Variant>()
        {
            {"Volume", _volume},
            {"CompletedLevels", _completedLevels},
            {"UnlockedTowers", _unlockedTowers}
        };
        string jsonData = Json.Stringify(dataDict);

        using FileAccess saveFile = FileAccess.Open("user://player.dat", FileAccess.ModeFlags.Write);
        if (saveFile == null)
        {
            Error fileError = FileAccess.GetOpenError();
            GD.PrintErr($"File Error: {FileAccess.GetOpenError()}");
            throw new System.IO.FileLoadException($"File load exception: {fileError}");
        }
        saveFile.StoreLine(jsonData);
    }
}
