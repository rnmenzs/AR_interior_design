using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Furniture", menuName = "SO/Furniture")]
public class FurnitureSO : ScriptableObject
{
    
    public string furnitureName;

    public Sprite furnitureIcon;

    public GameObject furniture;

    public TypeOrientation orientation;

}

public enum TypeOrientation
{

    Horizontal,

    Vertical,

}
