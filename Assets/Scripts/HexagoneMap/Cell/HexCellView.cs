using UnityEngine;
using UnityEngine.UI;

public class HexCellView : MonoBehaviour, ICellView
{
    public RectTransform SelfRectTransform => _selfRectTransform;
    [SerializeField] private RectTransform _selfRectTransform = null;

    public Color CellColor { get => _cellColor; }
    [SerializeField] private Color _cellColor = Color.white;

    [SerializeField] Image _highlight = null;
    [SerializeField] Text _label = null;

    public void SetCellColor(Color cellColor)
    {
        _cellColor = cellColor;
    }

    public void DisableHighlight()
    {
        if(!_highlight)
        {
            Debug.LogError(this + " : [DisableHighlight] Image component is null");
            return;
        }
        _highlight.enabled = false;
    }

    public void EnableHighlight(Color color)
    {
        if (!_highlight)
        {
            Debug.LogError(this + " : [EnableHighlight] Image component is null");
            return;
        }
        _highlight.enabled = true;
        _highlight.color = color;
    }

    public void SetString(string text)
    {
        if (!_label)
        {
            Debug.LogError(this + " : [SetString] Text component is null");
            return;
        }
        _label.text = text;
    }

    public void DestroyCell()
    {
        if (!_selfRectTransform)
        {
            Debug.LogError(this + " : [SetString] SelfParent is null");
            return;
        }
        Destroy(_selfRectTransform.gameObject);
    }
}

public class HexCellViewFactory : IFactory<ICellView>
{
    private readonly GameObject _prefab;
    private readonly Transform _parent;
    public HexCellViewFactory(GameObject prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;
    }

    public ICellView Create()
    {
        if (!_prefab || !_parent)
        {
            Debug.LogError(this + " : [ICellView Create] arguments is null  prefab : " + _prefab + " , parent : " + _parent);
            return default;
        }
        return Object.Instantiate(_prefab, _parent).GetComponent<ICellView>();
    }
}

