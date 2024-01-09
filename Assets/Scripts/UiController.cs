using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private TouchManager touchManager;

    private bool isClicked = false;

    private void Awake()
    {

        touchManager = GameObject.FindObjectOfType<TouchManager>();

    }

    private void Update()
    {

        if (isClicked)
        {

            touchManager.setUiWasUsedThisFrame(true);

            return;

        }

    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {

        isClicked = true;

    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {

        isClicked = false;

    }

}
