using UnityEngine;

public class ScrollEvent : MonoBehaviour
{

    private RectTransform _rectTransform;
    private RectTransform[] _rtChildren;
    private float _width, _height;
    private float _childWidth, _childHeight;
    [SerializeField] private float _itemSpacing;
    [SerializeField] private float _horizontalMargin, _verticalMargin;
    [SerializeField] private bool _horizontal, _vertical;

    #region Public Properties
    public float ItemSpacing { get { return _itemSpacing; } }
    public float HorizontalMargin { get { return _horizontalMargin; } }
    public float VerticalMargin { get { return _verticalMargin; } }
    public bool Horizontal { get { return _horizontal; } }
    public bool Vertical { get { return _vertical; } }
    public float Width { get { return _width; } }
    public float Height { get { return _height; } }
    public float ChildWidth { get { return _childWidth; } }
    public float ChildHeight { get { return _childHeight; } }
    #endregion

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _rtChildren = new RectTransform[_rectTransform.childCount];

        SetValues();

        if (_vertical)
            InitializeVertical();
        else
            InitializeHorizontal();
    }
    private void InitializeHorizontal()
    {
        float originX = 0 - (_width * 0.5f);
        float posOffset = _childWidth * 0.5f;
        for (int i = 0; i < _rtChildren.Length; i++)
        {
            Vector2 childPos = _rtChildren[i].localPosition;
            childPos.x = originX + posOffset + i * (_childWidth + _itemSpacing);
            _rtChildren[i].localPosition = childPos;
        }
    }
    private void InitializeVertical()
    {
        float originY = 0 - (_height * 0.5f);
        float posOffset = _childHeight * 0.5f;
        for (int i = 0; i < _rtChildren.Length; i++)
        {
            Vector2 childPos = _rtChildren[i].localPosition;
            childPos.y = originY + posOffset + i * (_childHeight + _itemSpacing);
            _rtChildren[i].localPosition = childPos;
        }
    }
    private void SetValues()
    {
        for (int i = 0; i < _rectTransform.childCount; i++)
        {
            _rtChildren[i] = _rectTransform.GetChild(i) as RectTransform;
        }
        _width = _rectTransform.rect.width - (2 * _horizontalMargin);

        _height = _rectTransform.rect.height - (2 * _verticalMargin);

        _childWidth = _rtChildren[0].rect.width;
        _childHeight = _rtChildren[0].rect.height;

        _horizontal = !_vertical;
    }
}
