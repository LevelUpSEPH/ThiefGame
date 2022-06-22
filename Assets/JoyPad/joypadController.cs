using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class joypadController : MonoBehaviour , IDragHandler , IPointerDownHandler, IPointerUpHandler
{
    public RawImage joystickBg;
    public RawImage joystickThumb;
    public Vector2 jsPos;
    void Start()
    {
        joystickBg = joystickBg.GetComponent<RawImage>();
        joystickThumb = joystickThumb.GetComponent<RawImage>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDrag(PointerEventData point)
    {
       if( RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBg.rectTransform, point.position, point.pressEventCamera, out jsPos))
        {
            jsPos.x = jsPos.x / joystickBg.rectTransform.sizeDelta.x;
            jsPos.y = jsPos.y / joystickBg.rectTransform.sizeDelta.y;
            if (jsPos.magnitude > 1.0f)
                jsPos = jsPos.normalized;
            joystickThumb.rectTransform.anchoredPosition = new Vector2(jsPos.x * joystickBg.rectTransform.sizeDelta.x/ 2, jsPos.y * joystickBg.rectTransform.sizeDelta.y / 2);
        }
    }
    public void OnPointerDown(PointerEventData point)
    {
        OnDrag(point);
    }
    public void OnPointerUp(PointerEventData point)
    {
        jsPos = Vector2.zero;
        joystickThumb.rectTransform.anchoredPosition = Vector2.zero;
    }
    public float horizontalInput()
    {
        if (jsPos.x != 0)
            return jsPos.x;
        else
            return Input.GetAxis("Horizontal");
    }
    public float verticalInput()
    {
        if (jsPos.y != 0)
            return jsPos.y;
        else
            return Input.GetAxis("Vertical");
    }
}