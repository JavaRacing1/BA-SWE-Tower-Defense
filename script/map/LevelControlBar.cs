using Godot;
using System;

public partial class LevelControlBar : Control
{
	private Label _moneyLabel;
	private Button _startButton;
	private VBoxContainer _towerItemContainer;

	[Signal]
	public delegate void StartButtonPressedEventHandler(Button startButton);

	[Signal]
	public delegate void PauseButtonPressedEventHandler();

	public override void _Ready()
	{
		_moneyLabel = GetNode<Label>("ControlBar/VBoxContainer/MoneyContainer/CenterContainer/HBoxContainer/MoneyLabel");
		_startButton = GetNode<Button>("ControlBar/VBoxContainer/ButtonContainer/HBoxContainer/StartButton");
		_towerItemContainer = GetNode<VBoxContainer>("ControlBar/VBoxContainer/TowerContainer/TowerItemContainer");
	}

	/// <summary>
	/// Updates the money display
	/// </summary>
	/// <param name="money">The money amount which should be displayed</param>
	public void DisplayMoney(int money)
	{
		_moneyLabel.Text = Convert.ToString(money);
	}

	/// <summary>
	/// Adds a tower item to the inventory
	/// </summary>
	/// <param name="item">The item added to the inventory</param>
	public void AddTowerButton(TowerContainerItem item)
	{
		_towerItemContainer.AddChild(item);
	}

	public void OnStartButtonPressed()
	{
		EmitSignal(SignalName.StartButtonPressed, _startButton);
	}

	public void OnPauseButtonPressed()
	{
		EmitSignal(SignalName.PauseButtonPressed);
	}

}
