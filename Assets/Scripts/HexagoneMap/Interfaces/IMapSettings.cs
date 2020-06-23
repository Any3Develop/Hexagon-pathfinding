using UnityEngine;

public interface IMapSettings
{
    int CellCountX { get; }
    int CellCountZ { get; }
    GameObject CellViewPrefab { get; }
    Color ColorPath { get; }
    Color ColorSelectionFrom { get; }
    Color ColorSelectionTo { get; }
    Color DefaultCellColor { get; }
    Color BlockedCellColor { get; }
    Color RoadCellColor { get; }
}