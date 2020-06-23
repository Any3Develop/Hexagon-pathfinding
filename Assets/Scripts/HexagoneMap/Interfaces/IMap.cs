using System.Collections.Generic;
using UnityEngine;

public interface IMap
{
    List<ICell> Cells { get; }
    ICell GetCell(Vector3 position);
    void Add(ICell cell);
    void Remove(ICell cell);
    void Clear();
}