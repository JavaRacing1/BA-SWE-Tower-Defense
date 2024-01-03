using Godot;
using System.Collections.Generic;

public partial class TowerSelectionMenu : Node
{
    private PlayerData _playerData;
    private GridContainer _availableTowersContainer;
    private GridContainer _selectedTowersContainer;
    private Button _startLevelButton;
    private Label _availableTowerNumberDisplay;

    private string _levelNumber;
    private int _selectedTowerCount = 0;
    private SortedSet<string> _selectedTowers;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
        _selectedTowers = new SortedSet<string>(new TowerComparer(towerConfig));

        _playerData = GetNode<PlayerData>("/root/PlayerData");
        _availableTowersContainer = GetNode<GridContainer>("AvailableTowersContainer");
        _selectedTowersContainer = GetNode<GridContainer>("SelectedTowersContainer");
        _startLevelButton = GetNode<Button>("StartLevelButton");
        _availableTowerNumberDisplay = GetNode <Label> ("DisplayAvailableNumber");

        //TODO: Remove hardcoded value
        _levelNumber = "One";
        _startLevelButton.Disabled = true;
        _availableTowerNumberDisplay.Text = "Noch 4 Türme auswählbar";
        _playerData.Load();

        CreateTowerButtons();
    }

    private void OnGoBackButtonPressed()
    {
        ConfirmationPopup confirmationPopup = (ConfirmationPopup)GD.Load<PackedScene>("res://scene//ui//ConfirmationPopup.tscn").Instantiate();
        confirmationPopup.Init("Willst du wirklich zur Levelauswahl zurückgehen?", "Zur Levelauswahl zurückgehen");
        confirmationPopup.Confirmed += () => GetTree().ChangeSceneToFile("res://scene/ui/LevelSelectionMenu.tscn");
        AddChild(confirmationPopup);
    }

    private void OnTowerButtonPressed(Button clickedButton)
    { 
        CheckStartLevelButton();

        if (_selectedTowers.Contains(clickedButton.Text))
        {
            _selectedTowers.Remove(clickedButton.Text);
            _selectedTowerCount--;
            _selectedTowersContainer.GetNode(clickedButton.Text).QueueFree();
        }
        else if (_selectedTowerCount < 4)
        {
            _selectedTowers.Add(clickedButton.Text);
            _selectedTowerCount++;
            Label towerLabel = new();
            towerLabel.Text = towerLabel.Name = clickedButton.Text;
            _selectedTowersContainer.AddChild(towerLabel);
        }

        int towerLeftCount = 4 - _selectedTowerCount;
        string towerText = towerLeftCount == 1 ? "Turm" : "Türme";
        _availableTowerNumberDisplay.Text = towerLeftCount > 0 ? $"Noch {towerLeftCount} {towerText} auswählbar" : "Kein Turm mehr auswählbar";

        CheckStartLevelButton();
    }

    private void OnStartLevelButtonPressed()
    {
        PackedScene levelScene = GD.Load<PackedScene>($"res://scene/map/level/Level{_levelNumber}.tscn");
        GameLevel gameLevel = (GameLevel)levelScene.Instantiate();
        GetTree().Root.AddChild(gameLevel);
        gameLevel.FillTowerContainer(_selectedTowers);
        QueueFree();
    }

    private void CreateTowerButtons()
    {
        foreach (string tower in _playerData.UnlockedTowers)
        {
            Button towerButton = new();
            towerButton.Text = towerButton.Name = tower;
            towerButton.Pressed += () => OnTowerButtonPressed(towerButton);
            _availableTowersContainer.AddChild(towerButton);
        }
    }

    private void CheckStartLevelButton()
    {
        _startLevelButton.Disabled = _selectedTowerCount == 0 || _selectedTowerCount > 4;
    }
}
