using UnityEngine;
using UnityEngine.UI;

public class UIManage : MonoBehaviour
{
    [SerializeField] private Button _quit = null;
    [SerializeField] private Button _rebuildMap = null;
    [SerializeField] private HexCellSelector _hexCellSelector = null;
    [SerializeField] private Transform _gridParent = null;
    [SerializeField] private HexMesh _hexMesh = null;
    private IMap _map = null;
    private HexMapGenerator hexMapGenerator;


    private void Start()
    {
        _quit.onClick.AddListener(OnQuitClick);
        _rebuildMap.onClick.AddListener(OnRebuildMapClick);

        _map = GameInstances.Instance.Map;
        hexMapGenerator = new HexMapGenerator(_map, _hexMesh, _gridParent);
        hexMapGenerator.CreateMap();
    }

    private void OnQuitClick()
    {
        _quit.onClick.RemoveListener(OnQuitClick);
        _rebuildMap.onClick.RemoveListener(OnRebuildMapClick);
        Application.Quit();
    }

    private void OnRebuildMapClick()
    {
        _hexCellSelector.Reset();
        hexMapGenerator.CreateMap();
    }
}
