using System.Collections.Generic;
using TowerDefense;

public partial class LevelOne : GameLevel
{
	public LevelOne()
	{
		//TODO: Change start money
		_currentMoney = 1000;
	}

	protected override int LevelNumber
	{
		get;
	} = 1;

	//TODO: Configure spawns
	protected override string[] SpawnConfigs
	{
		get;
	} = new string[3] {
		"", "", ""
	};

	protected override FieldType[,] FieldTypes
	{
		get;
	} = new FieldType[5, 10] {
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal },
		{ FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal, FieldType.Normal }
	};

   /* public override void _Ready()//nur fuer mich besser zum debuggen
	{
		SortedSet<string> towerNemes = new SortedSet<string>(new[] { "wall" });
		FillTowerContainer(towerNemes);
	}*/
}
