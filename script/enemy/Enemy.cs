using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using TowerDefense;

namespace TowerDefense
{
    public struct Statuseffects
    {
        public string name;
        public Timer DamageTimer;
		public Timer DelayTimer;
        public int damage;

        public bool Contains(string name)
        {
            if (this.name == name)
                return true;
            else return false;
        }

        public Statuseffects(string name, int Timelength, int damage)
        {
            this.name = name;

            DamageTimer = new Timer();
			DamageTimer.OneShot = true;
			DamageTimer.WaitTime = Timelength;

			DelayTimer=new Timer(); ;
			DelayTimer.OneShot = true;
			DelayTimer.WaitTime = 1.5f;

			this.damage = damage;
        }
    }
}

public abstract partial class Enemy : GameEntity
{
	protected bool _enemyDefeated = false;
	protected bool _enemyCrossedLastField = false;
	protected float _walkSpeed;
	protected string _name;
	protected List<Statuseffects> _statusEffects = new List<Statuseffects>();

	public bool EnemyDefeated
	{
		get { return _enemyDefeated; }
		set { _enemyDefeated = value;}
	}

	public bool EnemyCrossedLastLane
	{
		get { return _enemyCrossedLastField; }
		set { _enemyCrossedLastField = value;}
	}

	public float WalkSpeed
	{
		get { return _walkSpeed; }
		set { _walkSpeed = value; }
	}

	public string EnemyName
	{
		get { return _name; }
		set { _name = value; }
	}

	public virtual void Init()
	{

	}
	
	public virtual void AddStatusEffect(string effect)
	{
		bool contained=false;

		foreach(Statuseffects statuseffect in _statusEffects)
		{
			if(statuseffect.Contains(effect))
			{
				contained= true;
			}
		}

		switch (effect)
		{
			case "burn":
				{ 
                    if (!contained)
					{
                        Statuseffects stat = new Statuseffects("burn", 10, 1);
						AddChild(stat.DamageTimer);
						AddChild(stat.DelayTimer);

						stat.DamageTimer.Start();
						stat.DelayTimer.Start();

                        _statusEffects.Add(stat);
					}
					else
					{
						_statusEffects.First(x => x.name == effect).DamageTimer.Start();
                    }
					break;
				}
		}
	}

    public override void Destroy()
    {
        QueueFree();
    }

	public virtual void getStatuseffectDamage()
	{
		foreach(Statuseffects effect in _statusEffects)
		{
			if(!effect.DamageTimer.IsStopped() && effect.DelayTimer.IsStopped())
			{
				Health -= effect.damage;
				//GD.Print(EnemyName + " HP: " + Health);
				effect.DelayTimer.Start();
			}
		}
	}

    protected void MoveEnemy(float WalkSpeed)
    {
        Vector2 movement = new(-WalkSpeed, 0);
        Translate(movement);
    }
}
