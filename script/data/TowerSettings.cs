using Godot;
using Godot.Collections;

public partial class TowerSettings : RefCounted
{

    public TowerSettings(Dictionary<string, Variant> towerDict)
    {
        Name = (string)towerDict["name"];
        Cost = (int)towerDict["cost"];
        UpgradeNames = (string[])towerDict["upgrade_names"];
        UpgradeCosts = (int[])towerDict["upgrade_costs"];
        IsRangedTower = (bool)towerDict["ranged"];
    }

    /// <summary>
    /// The name of the tower
    /// </summary>
    public string Name
    {
        get;
    }

    /// <summary>
    /// The placement cost of the tower
    /// </summary>
    public int Cost
    {
        get;
    }

    /// <summary>
    /// The names of all tower upgrades
    /// </summary>
    public string[] UpgradeNames
    {
        get;
    }

    /// <summary>
    /// The costs of all tower upgrades
    /// </summary>
    public int[] UpgradeCosts
    {
        get;
    }


    /// <summary>
    /// True if the tower shoots projectiles
    /// </summary>
    public bool IsRangedTower
    {
        get;
    }
}
