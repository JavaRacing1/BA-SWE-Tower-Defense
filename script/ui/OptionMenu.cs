using Godot;
using System;

public partial class OptionMenu : Window
{
	private AudioStreamPlayer _audioPlayer;
	private Timer _volumeTimer;
	private Label _currentVolumeLabel;
	private PlayerData _playerData;
	private int _currentVolume;
	private double _testSoundLength;

	public override void _Ready()
	{
		_audioPlayer = GetNode<AudioStreamPlayer>("TestSoundPlayer");
		_testSoundLength = _audioPlayer.Stream.GetLength();
		_volumeTimer = GetNode<Timer>("VolumeTimer");
		_currentVolumeLabel = GetNode<Label>("CurrentVolumeLabel");
		_playerData = GetNode<PlayerData>("/root/PlayerData");

		_playerData.Load();
		HScrollBar volumeScrollbar = GetNode<HScrollBar>("VolumeScrollbar");
		_currentVolume = _playerData.Volume;
		volumeScrollbar.SetValueNoSignal(_currentVolume);
		_currentVolumeLabel.Text = Convert.ToString(_currentVolume);
	}

	private void OnVolumeScrollbarValueChanged(double value)
	{
		_currentVolume = (int)value;
		_currentVolumeLabel.Text = Convert.ToString(_currentVolume);

		if (!_audioPlayer.Playing && _volumeTimer.TimeLeft == 0)
		{
			_audioPlayer.VolumeDb = Mathf.LinearToDb((float)value / 100);
			_audioPlayer.Playing = true;

			_volumeTimer.Start(_testSoundLength * 2);
		}
	}

	private void OnCloseRequested()
	{
		_playerData.Volume = _currentVolume;
		_playerData.Save();

		QueueFree();
	}

	private void OnCloseButtonPressed()
	{
		OnCloseRequested();
	}

}


