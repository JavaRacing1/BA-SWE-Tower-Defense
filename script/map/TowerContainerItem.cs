using Godot;
using System.Collections.Generic;

public partial class TowerContainerItem : Control
{
    private static Dictionary<string, Texture2D> _iconTextureCache = new();
    private static Dictionary<string, Texture2D> _backgroundTextureCache = new();
    private string _towerName;
    private int _towerCost;
    private TextureRect _towerBackground;
    private bool _buyable = false;

    /// <summary>
    /// Initializes the item with the tower which should be displayed
    /// </summary>
    /// <param name="towerName">The name of the tower</param>
    /// <param name="towerCost">The cost of the tower</param>
    public void Init(string towerName, int towerCost)
    {
        _towerName = towerName;
        _towerCost = towerCost;

        _towerBackground = GetNode<TextureRect>("ItemBackground");

        Label towerLabel = _towerBackground.GetNode<Label>("TowerCostLabel");
        towerLabel.Text = $"{towerCost}$";

        if (!_iconTextureCache.ContainsKey(towerName))
        {
            Texture2D texture = GD.Load<Texture2D>($"res://assets/texture/tower/icon/{towerName}.png");
            _iconTextureCache.Add(towerName, texture);
        }
    }

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (_buyable)
        {
            SetDragPreview(CreateDragPreview());
            return _towerName;
        }

        return "";
    }

    private Control CreateDragPreview()
    {
        TextureRect previewNode = new TextureRect
        {
            Size = new Vector2(64, 64),
            ExpandMode = TextureRect.ExpandModeEnum.FitWidth,
            Texture = _iconTextureCache[_towerName]
        };
        //previewNode.PivotOffset = new Vector2(50,50);TODO: MAuszeiger mittig vom Symbol
        return previewNode;
    }

    public void UpdateItemStatus(int currentMoney)
    {
        _buyable = currentMoney >= _towerCost;
        string textureName = _buyable ? _towerName : _towerName + "_disabled";

        if (!_backgroundTextureCache.ContainsKey(textureName))
        {
            Texture2D texture = GD.Load<Texture2D>($"res://assets/texture/tower/background/{textureName}.png");
            _backgroundTextureCache.Add(textureName, texture);
            _towerBackground.Texture = texture;
        }
        else
        {
            _towerBackground.Texture = _backgroundTextureCache[textureName];
        }
    }
}
