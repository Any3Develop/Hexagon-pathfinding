using UnityEngine;

public interface ICell
{
    Vector3 UiPosition { get; set; }
    Vector3 LocalPosition { get; set; }

    Color CellColor { get; set; }
    HexCoordinates Coordinates { get; set; }
    int Distance { get; set; }
    int Elevation { get; set; }
    bool IsWalkable { get; set; }
    ICell NextWithSamePriority { get; set; }
    ICell PathFrom { get; set; }
    int SearchHeuristic { get; set; }
    int SearchPriority { get; }

    void Initialize(ICellView view);
    void UnInitialize();

    HexEdgeType GetEdgeType(ICell otherCell);
    ICell GetNeighbor(HexDirection direction);
    void AddRoad(HexDirection direction);
    void RemoveRoad(HexDirection direction);
    bool HasRoadThroughEdge(HexDirection direction);
    void SetNeighbor(HexDirection direction, ICell cell); 
    void DisableHighlight();
    void EnableHighlight(Color color);
    void UpdateLabel(string text);
}