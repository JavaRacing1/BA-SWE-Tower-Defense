using System;
using System.Collections.Generic;
using Godot;

public partial class TowerComparer : RefCounted, IComparer<string>
{
    private readonly string[] _towerNames;

    public TowerComparer(TowerConfig config)
    {
        _towerNames = new string[config.LoadedTowers.Count];
        config.LoadedTowers.Keys.CopyTo(_towerNames, 0);
    }

    public int Compare(string x, string y)
    {
        int xIndex = Array.IndexOf(_towerNames, x);
        int yIndex = Array.IndexOf(_towerNames, y);
        if (xIndex == -1 || yIndex == -1)
        {
            return -1;
        }
        return xIndex - yIndex;
    }
}