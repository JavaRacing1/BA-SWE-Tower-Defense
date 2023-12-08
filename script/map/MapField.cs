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
        public delegate void Defender_placedEventHandler(int cost); 

		private static Dictionary<FieldType, Texture2D> _fieldTextureCache = new();
		private int _fieldNr;
		private Sprite2D _sprite;
		private bool _Towerset;
		private defender _Tower;

		public void Init(FieldType fieldType, int fieldNumber)
		{
			_sprite = GetNode<Sprite2D>("Background");
			_fieldNr = fieldNumber;

			switch (fieldType)
			{
				case FieldType.Normal:
					{
						if(!_fieldTextureCache.ContainsKey(fieldType))
						{
							_sprite.Texture = GD.Load<Texture2D>("res://assets/texture/field/Normal.png");
							_fieldTextureCache.Add(fieldType,_sprite.Texture);
						}
						else 
						{
							_sprite.Texture = _fieldTextureCache[fieldType];
						}
						break;
					}
			}
		}

		public override bool _CanDropData(Vector2 atPosition, Variant data)
		{
			string info = (string)data;
			if(!_Towerset && info!="")
				return true; 
			else
				return false;
		}

		public override void _DropData(Vector2 atPosition, Variant data)
		{
			string towerName=(string) data;

			switch (towerName)
			{
				case "knight":
				{
					_Tower = (knight) GD.Load<PackedScene>($"res://scene/tower/knight/{towerName}.tscn").Instantiate();
					_Tower.Init();
					AddChild(_Tower);
					EmitSignal(SignalName.Defender_placed,_Tower.cost);
					_Towerset=true;
					break;
                }
				case "spearman":
				{
					//hier muss noch was hin
					break;
				}
				default:
				{
						break;
                }
			}

		}


	}


}