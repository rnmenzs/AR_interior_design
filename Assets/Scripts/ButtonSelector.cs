using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelector : MonoBehaviour
{

    public FurnitureSO furniture;

    [SerializeField]
    private Image furniturePhoto;

    [SerializeField]
    private TMP_Text textMeshPro;

    private TouchManager touchManager;

    private void Awake()
    {

        touchManager = GameObject.FindObjectOfType<TouchManager>();

    }

    private void OnEnable()
    {

        furniturePhoto.sprite = furniture.furnitureIcon;

        textMeshPro.text = furniture.furnitureName;


    }

    public void SelectFurniture()
    {

        touchManager.PlaceableObjectFurniture = furniture;

    }



}
