using UnityEngine;
using UnityEngine.EventSystems;

public class HexCellSelector : MonoBehaviour
{
    [SerializeField] private Transform _gridParent = null;
    [SerializeField] private HexMesh _hexMesh = null;

    private IPathFinder _pathFinder = null;
    private IMapSettings _mapSettings = null;
    private IMap _map = null;
    private HexMapGenerator hexMapGenerator;
    private ICell _searchFromCell = null;
    private ICell _searchToCell = null;

    private void Start()
    {
        _mapSettings = GameInstances.Instance.MapSettings;
        _pathFinder = GameInstances.Instance.PathFinder;
        _map = GameInstances.Instance.Map;
        hexMapGenerator = new HexMapGenerator(_map, _hexMesh, _gridParent);
        hexMapGenerator.CreateMap();
    }

    private void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            bool tryFrind = false;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _searchFromCell = UpdateSelection(_searchFromCell, _mapSettings.ColorSelectionFrom);
                tryFrind = true;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _searchToCell = UpdateSelection(_searchToCell, _mapSettings.ColorSelectionTo);
                tryFrind = true;
            }


            if (tryFrind && _searchFromCell != null && _searchToCell != null && _searchFromCell != _searchToCell)
            {
                if (_map != null || _map.Cells.Count > 0)
                {
                    _pathFinder.FindPathOnMap(_searchFromCell, _searchToCell, _map);
                }
            }            
        }     
    }

    private ICell UpdateSelection(ICell target, Color color)
    {
        ICell current = Select(RayCastFromPointer());
        if (current != null)
        {
            if (target != null)
            {
                target.DisableHighlight();
            }
            target = current;
            target.EnableHighlight(color);
        }
        else if(target != null)
        { 
            target.DisableHighlight();
            target = null;
        }
        return target;
    }

    private Vector3 RayCastFromPointer()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(inputRay, out RaycastHit hit))
        {
            return hit.point;
        }
        return default;
    }

    private ICell Select(Vector3 point)
    {
        return _map.GetCell(point);
    }
}
