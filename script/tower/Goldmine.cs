using Godot;

public partial class Goldmine : Defender
{
    [Signal]
    public delegate void MoneyGeneratedEventHandler(int moneyAmount);

    private int _moneyPerCycle = 250;

    public Goldmine()
    {
        _delay = 15;
        _animationDelay = 1;

        //TODO: Add action animation
        _actionAnimation = "idle";
        Health = 5;
    }

    public override void _Ready()
    {
        base._Ready();
        _actionTimer.Start(_delay);
        _animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _animatedSprite.Play(_actionAnimation);
    }

    public override void _Process(double delta)
    {
        if (Health <= 0)
        {
            Destroy();
        }
    }

    public override void Action()
    {
        EmitSignal(SignalName.MoneyGenerated, _moneyPerCycle);
    }
}
