using Godot;

public partial class MainMenu : Node
{
	private void OnExitButtonPressed()
	{
		ConfirmationPopup confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
		confirmationPopup.Init("Willst du wirklich das Spiel verlassen?", "Spiel verlassen");
		confirmationPopup.Confirmed += () => GetTree().Quit();
		AddChild(confirmationPopup);
	}

	public void OnSettingsButtonPressed()
	{
		OptionMenu optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
		AddChild(optionMenu);
	}

	public void OnLevelSelectionButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://scene/ui/LevelSelectionMenu.tscn");
	}
}

