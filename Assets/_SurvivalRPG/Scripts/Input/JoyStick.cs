using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform _joyStickHandle;
    private Vector3 _joyStickHandleOgPosition;
    [SerializeField]
    private float _joyStickHandleMaxDistance;

    public Vector2 Input => (_joyStickHandle.position - _joyStickHandleOgPosition) / _joyStickHandleMaxDistance;

    private void Start()
    {
        _joyStickHandleOgPosition = _joyStickHandle.position;
    }

    public void OnBeginDrag(PointerEventData eventData) { }

    public void OnDrag(PointerEventData eventData)
    {
        _joyStickHandle.position = eventData.position;
        ClampJoyStickHandle();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _joyStickHandle.position = _joyStickHandleOgPosition;
    }

    private void ClampJoyStickHandle()
    {
        if (Vector3.Distance(_joyStickHandle.position, _joyStickHandleOgPosition) > _joyStickHandleMaxDistance)
        {
            _joyStickHandle.position = _joyStickHandleOgPosition + (_joyStickHandle.position - _joyStickHandleOgPosition).normalized * _joyStickHandleMaxDistance;
        }
    }
}
