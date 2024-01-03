using Godot;

public partial class PauseMenu : Control
{
    public override void _Ready()
    {
        Hide();
    }

    private void OnExitButtonPressed()
    {
        ConfirmationPopup confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
		confirmationPopup.Init("Möchtest du das Level wirklich beenden?", "Level beenden");
		confirmationPopup.Confirmed += () => 
        {
            GetTree().ChangeSceneToFile("res://scene/ui/MainMenu.tscn");
            GameLevel gameLevel = GetNode<GameLevel>("/root/Level");
            if (gameLevel != null)
            {
                gameLevel.QueueFree();
            }
            GetTree().Paused = false;
        };
		AddChild(confirmationPopup);
    }

    private void OnCloseButtonPressed()
    {
        Hide();
        GetTree().Paused = false;
    }

    private void OnSettingsButtonPressed()
    {
        OptionMenu optionMenu = (OptionMenu)GD.Load<PackedScene>("res://scene//ui//OptionMenu.tscn").Instantiate();
		AddChild(optionMenu);
    }
}