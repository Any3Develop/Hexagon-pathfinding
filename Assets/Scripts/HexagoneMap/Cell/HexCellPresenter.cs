using System;
using UnityEngine;

public class HexCellPresenter : ICell
{
    public HexCoordinates Coordinates { get; set; }
    public ICell NextWithSamePriority { get; set; }
    public ICell PathFrom { get; set; }
    public int SearchPriority => Distance + SearchHeuristic;
    public int SearchHeuristic { get; set; }
    public int Distance { get; set; }
    public bool IsWalkable { get; set; } = true;
    public int Elevation
    {
        get => _elevation;
        set
        {
            _elevation = value;
            Vector3 position = _view.SelfRectTransform.localPosition;
            position.y = value * HexMetrices.ElevationStep;
            position.z = _elevation * -HexMetrices.ElevationStep;
            _view.SelfRectTransform.localPosition = position;
        }
    }
    public Color CellColor { get => _view.CellColor; set => _view.SetCellColor(value); }
    public Vector3 UiPosition { get => _view.SelfRectTransform.anchoredPosition3D; set => _view.SelfRectTransform.anchoredPosition3D = value; }
    public Vector3 LocalPosition { get => _view.SelfRectTransform.localPosition; set => _view.SelfRectTransform.localPosition = value; }

    private int _elevation;
    private ICellView _view;
    private ICell[] _neighbors = new ICell[6];
    private bool[] _roads = new bool[6];

    public void Initialize(ICellView view)
    {
        if (view == null)
        {
            Debug.LogError(this + " : [HexCellPresenter] ICellView is null");
            return;
        }
        _view = view;
    }

    public void UnInitialize()
    {
        _view.DestroyCell();
    }

    public ICell GetNeighbor(HexDirection direction)
    {
        return _neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, ICell cell)
    {
        ICell neighbor = _neighbors[(int)direction];
        if (neighbor == null)
        {
            _neighbors[(int)direction] = cell;
            cell.SetNeighbor(direction.Opposite(), this);
        }
    }

    public HexEdgeType GetEdgeType(ICell otherCell)
    {
        return HexMetrices.GetEdgeType(Elevation, otherCell.Elevation);
    }

    public void AddRoad(HexDirection direction)
    {
        _roads[(int)direction] = true;
    }

    public void RemoveRoad(HexDirection direction)
    {
        _roads[(int)direction] = false;
    }

    public bool HasRoadThroughEdge(HexDirection direction)
    {
        return _roads[(int)direction];
    }

    public void DisableHighlight()
    {
        if (_view == null)
        {
            Debug.LogError(this + " : [DisableHighlight] ICellView is null");
            return;
        }
        _view.DisableHighlight();
    }

    public void EnableHighlight(Color color)
    {
        if (_view == null)
        {
            Debug.LogError(this + " : [EnableHighlight] ICellView is null");
            return;
        }
        _view.EnableHighlight(color);
    }

    public void UpdateLabel(string text)
    {
        if (_view == null)
        {
            Debug.LogError(this + " : [UpdateLabel] ICellView is null");
            return;
        }
        _view.SetString(text);
    }
}
public class HexCellPresenterFactory : IFactory<HexCellPresenter>
{
    public HexCellPresenter Create()
    {
        return new HexCellPresenter();
    }
}

