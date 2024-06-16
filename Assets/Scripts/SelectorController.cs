using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SelectorController : MonoBehaviour
{

    private RectTransform rectTransform;

    private float initialPosY;

    private TouchManager touchManager;

    private List<FurnitureSO> furnitures;

    private GameObject selectorPanel;

    private GameObject scrollPanel;

    [SerializeField]
    private GameObject buttonPrefab;


    private void Awake()
    {
        selectorPanel = gameObject;

        rectTransform = GetComponent<RectTransform>();

        initialPosY = rectTransform.anchoredPosition.y;

        touchManager = GameObject.FindObjectOfType<TouchManager>();

        furnitures = touchManager.Furnitures;

        scrollPanel = selectorPanel.GetNamedChild("Grid");
    }

    private void Start()
    {
        foreach (FurnitureSO furniture in furnitures)
        {

            GameObject newFurnitureButton = Instantiate(buttonPrefab);

            newFurnitureButton.transform.SetParent(scrollPanel.transform);

            ButtonSelector buttonSelector = newFurnitureButton.GetComponent<ButtonSelector>();

            buttonSelector.furniture = furniture;

            buttonSelector.enabled = true;

        }

        
    }

    public void SwitchSelector(Boolean value)
    {

        if(!value)
        {

            rectTransform.DOAnchorPosY(initialPosY, 1);

            return;

        }

        rectTransform.DOAnchorPosY(-700, 1);

    }

}
