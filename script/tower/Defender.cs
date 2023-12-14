using TowerDefense;

public abstract partial class Defender : GameEntity
{
	protected string _name;

	public void Init(string towerName)
	{
		_name = towerName;
	}

	public int GetTowerCost()
	{
		TowerConfig towerConfig = GetNode<TowerConfig>("/root/TowerConfig");
		TowerSettings towerSettings = towerConfig.GetTowerSettingsByName(_name);
		return towerSettings.Cost;
	}

    public override void Destroy()
    {
        MapField field = (MapField) GetParent();
		field.Tower = null;
		QueueFree();
    }
}