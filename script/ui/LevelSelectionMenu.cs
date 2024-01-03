using Godot;

public partial class LevelSelectionMenu : Node
{

    public void OnSettingsButtonPressed()
    {
        OptionMenu optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
        AddChild(optionMenu);
    }

	public void OnGoBackButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
	}

	public void OnSelectLevel1ButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/ui/TowerSelectionMenu.tscn");
	}
}
