using Godot;
using System;

public partial class GameEntity : Node
{
    protected float _health;
    protected bool _immuneToDamage;
    protected float _delay;
    protected bool _idleAnimation;
    protected bool _actionAnimation;
    protected float _animationDelay;

    public virtual void InflictDamage()
    {

    }
    public virtual void OnTimerEnd()
    {

    }
    public virtual void Action()
    {

    }
    public virtual void Destroy()
    {
        
        QueueFree();
    }
    public virtual void OnAnimationTimerEnd()
    {

    }
}
