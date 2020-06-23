using UnityEngine;

[CreateAssetMenu(fileName = "HexMapSettings", menuName = "Configs/HexMapSettings")]
public class HexMapSettings : ScriptableObject, IMapSettings
{
    public Color ColorSelectionFrom => _colorSelectionFrom;
    [SerializeField] private Color _colorSelectionFrom = Color.white;

    public Color ColorSelectionTo => _colorSelectionTo;
    [SerializeField] private Color _colorSelectionTo = Color.white;

    public Color ColorPath => _colorPath;
    [SerializeField] private Color _colorPath = Color.black;

    public Color DefaultCellColor => _defaultCellColor;
    [SerializeField] private Color _defaultCellColor = Color.white;

    public Color BlockedCellColor => _blockedCellColor;
    [SerializeField] private Color _blockedCellColor = Color.gray;

    public Color RoadCellColor => _roadCellColor;
    [SerializeField] private Color _roadCellColor = Color.gray / 2;

    public GameObject CellViewPrefab => _cellViewPrefab;
    [SerializeField] private GameObject _cellViewPrefab = null;

    public int CellCountX => _cellCountX;
    [SerializeField] private int _cellCountX = 6;

    public int CellCountZ => _cellCountZ;
    [SerializeField] private int _cellCountZ = 6;
}
