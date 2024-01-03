using Godot;
using System.Collections.Generic;


namespace TowerDefense
{
	public enum FieldType
	{
		Normal
	}

	public partial class MapField : Control
	{
		[Signal]
		public delegate void DefenderPlacedEventHandler(int cost);

		private static Dictionary<FieldType, Texture2D> _fieldTextureCache = new();
		private int _fieldNr;
		private Sprite2D _sprite;

        public Defender Tower { get; set; }

        public void Init(FieldType fieldType, int fieldNumber)
		{
			_sprite = GetNode<Sprite2D>("Background");
			_fieldNr = fieldNumber;

			if (!_fieldTextureCache.ContainsKey(fieldType))
			{
				_sprite.Texture = GD.Load<Texture2D>($"res://assets/texture/field/{fieldType}.png");
				_fieldTextureCache.Add(fieldType, _sprite.Texture);
			}
			else
			{
				_sprite.Texture = _fieldTextureCache[fieldType];
			}
		}

		public override bool _CanDropData(Vector2 atPosition, Variant data)
		{
			return Tower == null && (string)data != "";
		}

		public override void _DropData(Vector2 atPosition, Variant data)
		{
			string towerName = (string)data;

			Tower = (Defender)GD.Load<PackedScene>($"res://scene/tower/{towerName}.tscn").Instantiate();
			Tower.Init(towerName);
			Tower.TopLevel = true;
			Tower.Position = GlobalPosition;

			if (towerName == "goldmine")
			{
				GameLevel level = (GameLevel)GetParent().GetParent();
				((Goldmine)Tower).MoneyGenerated += level.AddMoney;
			}

			AddChild(Tower);
			EmitSignal(SignalName.DefenderPlaced, Tower.GetTowerCost());
		}
	}


}