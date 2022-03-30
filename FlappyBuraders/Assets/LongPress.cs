using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum buttonMode {
        OneLeft,
        OneRight,
        TwoLeft,
        TwoRight
    }
    [SerializeField]
    private buttonMode _buttonMode;
    public BuraderMove buraderMove;
    void Start() {
        onLongClick.AddListener(Move);
        onClick.AddListener(Break);
        onRelease.AddListener(Release);
    }
    private void Move() {
        switch (_buttonMode) {
            case buttonMode.OneLeft:
                buraderMove.MoveLeftBtn();
                break;
            case buttonMode.OneRight:
                buraderMove.MoveRightBtn();
                break;
            case buttonMode.TwoLeft:
                buraderMove.MoveLeftBtn();
                break;
            case buttonMode.TwoRight:
                buraderMove.MoveRightBtn();
                break;
        }
    }
    private void Break() {
        buraderMove.ThisBreak();
    }
    private void Release() {
        buraderMove.ThisRelease();
    }
    private bool isPressed;

    public void OnPointerDown(PointerEventData eventData) {
        // if (onClick != null) {
        //     onClick.Invoke(); 
        // }
        Break();
        isPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData) {
        isPressed = false;
        // if (onRelease != null) {
        //     onRelease.Invoke(); 
        // }
        Release();
    }

    public UnityEvent onLongClick;
    public UnityEvent onClick;
    public UnityEvent onRelease;
    // Update is called once per frame
    void Update()
    {
        if (isPressed) {
            // if (onLongClick != null) {
            //     onLongClick.Invoke();  
            // }
            Move();
        }
        
    }
}
