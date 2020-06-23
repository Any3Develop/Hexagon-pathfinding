using UnityEngine;

public interface ICellView
{
    Color CellColor { get; }
    RectTransform SelfRectTransform { get; }

    void DisableHighlight();
    void EnableHighlight(Color color);
    void SetCellColor(Color cellColor);
    void SetString(string text);
    void DestroyCell();
}