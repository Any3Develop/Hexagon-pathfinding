using UnityEngine;

public class HexMapGenerator
{
    private HexMesh _hexMesh = null;
    private HexCellViewFactory _hexCellViewFactory;
    private HexCellPresenterFactory _hexCellPresenterFactory;
    private IMapSettings _settings = GameInstances.Instance.MapSettings;
    private IMap _map = null;

    public HexMapGenerator(IMap map, HexMesh hexMesh, Transform cellParent)
    {
        _map = map;
        _hexMesh = hexMesh;
        _hexCellViewFactory = new HexCellViewFactory(_settings.CellViewPrefab, cellParent);
        _hexCellPresenterFactory = new HexCellPresenterFactory();
    }
    public void CreateMap()
    {
        _map.Clear();
        for (int z = 0, i = 0; z < _settings.CellCountZ; z++)
        {
            for (int x = 0; x < _settings.CellCountX; x++)
            {
                CreateCell(x, z, i++);
            }
        }
        UpdateMesh();
    }

    private void UpdateMesh()
    {
        _hexMesh.Triangulate(_map.Cells.ToArray());
    }
    private void CreateCell(int x, int z, int i)
    {
        Vector3 position;

        position.x = (x + z * 0.5f - z / 2) * (HexMetrices.InnerRadius * 2f);
        position.y = z * (HexMetrices.OuterRadius * 1.5f);
        position.z = 0f;

        ICell cell = _hexCellPresenterFactory.Create();
        cell.Initialize(_hexCellViewFactory.Create());
        cell.UiPosition = position;
        cell.Coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.UpdateLabel(cell.Distance.ToString());
        cell.IsWalkable = Random.value > 0.2f ? true : false;
        if(!cell.IsWalkable)
        {
            cell.CellColor = _settings.BlockedCellColor;
        }
        else
        {
            cell.CellColor = _settings.DefaultCellColor;
        }

        bool makeRoad = Random.value > 0.7f && cell.IsWalkable ? true : false;
        if(makeRoad)
        {
            cell.CellColor = _settings.RoadCellColor;
            var directions = System.Enum.GetValues(typeof(HexDirection)) as HexDirection[];
            foreach (var item in directions)
            {
                cell.AddRoad(item);
            }
        }
        if (x > 0)
        {
            cell.SetNeighbor(HexDirection.W, _map.Cells[i - 1]);
        }
        if (z > 0)
        {
            if ((z & 1) == 0)
            {
                cell.SetNeighbor(HexDirection.SE, _map.Cells[i - _settings.CellCountX]);
                if (x > 0)
                {
                    cell.SetNeighbor(HexDirection.SW, _map.Cells[i - _settings.CellCountX - 1]);
                }
            }
            else
            {
                cell.SetNeighbor(HexDirection.SW, _map.Cells[i - _settings.CellCountX]);
                if (x < _settings.CellCountX - 1)
                {
                    cell.SetNeighbor(HexDirection.SE, _map.Cells[i - _settings.CellCountX + 1]);
                }
            }
        }
        _map.Add(cell);
    }
}
