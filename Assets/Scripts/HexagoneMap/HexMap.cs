using System.Collections.Generic;
using UnityEngine;

public class HexMap : IMap
{
    private IMapSettings _settings = GameInstances.Instance.MapSettings;

    public List<ICell> Cells { get;} = new List<ICell>();

    public void Add(ICell cell)
    {
        if (Cells.Contains(cell))
        {
            Debug.LogError(this + " : [SetCell] alredy exist");
            return;
        }
        Cells.Add(cell);
    }

    public ICell GetCell(Vector3 position)
    {
        var coordinates = HexCoordinates.FromPosition(position);
        int z = coordinates.Z;
        if (z < 0 || z >= _settings.CellCountZ)
        {
            return null;
        }
        int x = coordinates.X + z / 2;
        if (x < 0 || x >= _settings.CellCountX)
        {
            return null;
        }
        return Cells[x + z * _settings.CellCountX];
    }

    public void Remove(ICell cell)
    {
        if (!Cells.Contains(cell))
        {
            Debug.LogError(this + " : [RemoveCell] does not cintains");
            return;
        }
        cell.UnInitialize();
        Cells.Remove(cell);
    }

    public void Clear()
    {
        foreach (var item in Cells)
        {
            item.UnInitialize();
        }
        Cells.Clear();
    }
}
