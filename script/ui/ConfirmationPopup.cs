using Godot;

public partial class ConfirmationPopup : Window
{
	[Signal]
	public delegate void ConfirmedEventHandler();

	public void Init(string message, string boxTitle)
	{
		//Titel der MsgBox festlegen
		Title = boxTitle;

		//angezeigten Text festlegen
		Label textLabel = GetNode<Label>("TextureRect/TextLabel");
		textLabel.Text = message;
	}

	private void OnConfirmButtonPressed()
	{
		EmitSignal(SignalName.Confirmed);
	}

	private void OnCancelButtonPressed()
	{
		QueueFree();
	}

	private void OnCloseRequested()
	{
		QueueFree();
	}

}
