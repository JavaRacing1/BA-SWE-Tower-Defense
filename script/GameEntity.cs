using Godot;

public abstract partial class GameEntity : Node2D
{
    protected float _delay;
    protected float _animationDelay;
    protected string _idleAnimation = "idle";
    protected string _actionAnimation = "action";
    protected AnimatedSprite2D _sprite;
    protected GameLevel _level;
    protected Timer _actionTimer;
    private Timer _animationTimer;

    public int Health
    {
        get; 
        set;
    }

    public bool ImmuneToDamage {
        get;
        set;
    } = false;

    /// <summary>
    /// The action done by the entity. Gets called when the AnimationTimer ends.
    /// 
    /// Order: ActionTimer => AnimationTimer => Action() => (repeat)
    /// </summary>
    public abstract void Action();

    public abstract void Destroy();

    public override void _Ready()
    {
        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite");
        _sprite.Play(_idleAnimation);

        _actionTimer = GetNode<Timer>("ActionTimer");
        _actionTimer.Timeout += OnTimerEnd;
        _actionTimer.OneShot = true;
        _animationTimer = GetNode<Timer>("AnimationTimer");
        _animationTimer.Timeout += OnAnimationTimerEnd;
        _animationTimer.OneShot = true;

        _level = GetNode<GameLevel>("/root/Level");
    }

    /// <summary>
    /// Inflicts damage to the entity
    /// </summary>
    /// <param name="damage">The damage amount</param>
    public void InflictDamage(int damage)
    {
        if (!ImmuneToDamage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Destroy();
            }
        }
    }

    private void OnTimerEnd()
    {
        _sprite.Play(_actionAnimation);
        _animationTimer.Start(_animationDelay);
    }

    private void OnAnimationTimerEnd()
    {
        if (_level != null && _level.LevelStarted)
        {
            Action();
        }
        _sprite.Play(_idleAnimation);
        _actionTimer.Start(_delay);
    }

}
