using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private TouchManager gameManager;

    private bool isClicked = false;

    private void Awake()
    {

        gameManager = GameObject.FindObjectOfType<TouchManager>();

    }

    private void Update()
    {

        if (isClicked)
        {

            gameManager.setUiWasUsedThisFrame(true);

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
