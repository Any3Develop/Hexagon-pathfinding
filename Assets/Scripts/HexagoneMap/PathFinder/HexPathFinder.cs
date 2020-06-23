using System.Collections.Generic;
using UnityEngine;

public partial class HexPathFinder : IPathFinder
{
    private IMapSettings _hexMapSettings = null;
    private HexCellPriorityQueue _searchFrontier = new HexCellPriorityQueue();

    public List<ICell> FindPathOnMap(ICell fromCell, ICell toCell, IMap map)
    {
        if(_hexMapSettings == null)
        {
            _hexMapSettings = GameInstances.Instance.MapSettings;
        }

        List<ICell> path = new List<ICell>();
        _searchFrontier.Clear();

        for (int i = 0; i < map.Cells.Count; i++)
        {
            map.Cells[i].Distance = int.MaxValue;
            map.Cells[i].DisableHighlight();
            map.Cells[i].UpdateLabel(string.Empty);
        }
        fromCell.EnableHighlight(_hexMapSettings.ColorSelectionFrom);
        toCell.EnableHighlight(_hexMapSettings.ColorSelectionTo);

        fromCell.Distance = 0;
        _searchFrontier.Enqueue(fromCell);

        while (_searchFrontier.Count > 0)
        {
            ICell current = _searchFrontier.Dequeue();

            if (current == toCell)
            {
                current = current.PathFrom;
                path.Add(toCell);
                path.Add(toCell.PathFrom);
                while (current != fromCell)
                {
                    current.EnableHighlight(_hexMapSettings.ColorPath);
                    current = current.PathFrom;
                    path.Add(current);     
                };

                int i = 0;
                path.Reverse();
                foreach (var item in path)
                {
                    item.UpdateLabel(i++ + "");
                }
                
                break;
            }

            for (HexDirection direction = HexDirection.NE; direction <= HexDirection.NW; direction++)
            {
                ICell neighbor = current.GetNeighbor(direction);
                if (neighbor == null)
                {
                    continue;
                }
                if (!neighbor.IsWalkable)
                {
                    continue;
                }

                int distance = current.Distance;
                if (current.HasRoadThroughEdge(direction))
                {
                    distance += 1;
                }
                else
                {
                    distance += 10;
                }
                if (neighbor.Distance == int.MaxValue)
                {
                    neighbor.Distance = distance;
                    neighbor.PathFrom = current;
                    neighbor.SearchHeuristic =
                    neighbor.Coordinates.DistanceTo(toCell.Coordinates);
                    _searchFrontier.Enqueue(neighbor);
                }
                else if (distance < neighbor.Distance)
                {
                    int oldPriority = neighbor.SearchPriority;
                    neighbor.Distance = distance;
                    neighbor.PathFrom = current;
                    _searchFrontier.Change(neighbor, oldPriority);
                }
            }
        }
        return map.Cells;
    }

}

