using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

public partial class TowerConfig : Node
{
	private const string _filePath = "res://config/towers.json";
	private readonly System.Collections.Generic.Dictionary<string, TowerSettings> _loadedTowers = new();

	/// <summary>
	/// A Dictionary, which contains all configured tower names with their corresponding settings
	/// </summary>
	public System.Collections.Generic.Dictionary<string, TowerSettings> LoadedTowers
	{
		get
		{
			return _loadedTowers;
		}
	}

	public override void _Ready()
	{
		Load();
	}

	/// <summary>
	/// Returns the TowerSettings for the tower name
	/// </summary>
	/// <param name="towerName">The name of the tower type</param>
	/// <returns>The TowerSettings of the tower type</returns>
	public TowerSettings GetTowerSettingsByName(string towerName)
	{
		return _loadedTowers.GetValueOrDefault(towerName, null);
	}

	/// <summary>
	/// Loads the config file and fills the LoadedTowers collection
	/// </summary>
	/// <exception cref="System.IO.FileLoadException">When the config file could not be accessed</exception>
	/// <exception cref="JsonException">When the config file contains invalid JSON</exception>
	private void Load()
	{
		if (!FileAccess.FileExists(_filePath))
		{
			return;
		}

		using FileAccess towerFile = FileAccess.Open(_filePath, FileAccess.ModeFlags.Read);
		if (towerFile == null)
		{
			Error fileError = FileAccess.GetOpenError();
			GD.PrintErr($"File Error: {FileAccess.GetOpenError()}");
			throw new System.IO.FileLoadException($"File load exception: {fileError}");
		}

		StringBuilder fileStringBuilder = new StringBuilder();
		while (towerFile.GetPosition() < towerFile.GetLength())
		{
			fileStringBuilder.Append(towerFile.GetLine().StripEdges());
		}

		var json = new Json();
		Error parseResult = json.Parse(fileStringBuilder.ToString());
		if (parseResult != Error.Ok)
		{
			string errorMsg = $"JSON Parse Error: {json.GetErrorMessage()} in {fileStringBuilder} at line {json.GetErrorLine()}";
			GD.Print(errorMsg);
			throw new JsonException(errorMsg);
		}

		var towerDict = new Godot.Collections.Dictionary<string, Variant>((Dictionary)json.Data);
		Array<Godot.Collections.Dictionary<string, Variant>> towers = (Array<Godot.Collections.Dictionary<string, Variant>>)towerDict["towers"];

		foreach (Godot.Collections.Dictionary<string, Variant> tower in towers)
		{
			string towerName = (string)tower["name"];
			var towerSettings = new TowerSettings(tower);
			_loadedTowers.Add(towerName, towerSettings);
		}
	}
}
