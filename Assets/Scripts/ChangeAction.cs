using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAction : MonoBehaviour
{
    private TouchManager touchManager;

    private void Awake()
    {

        touchManager = GameObject.FindObjectOfType<TouchManager>();

    }

    public void SelectAction(string value)
    {

        touchManager.setSelectedCommand(value);

    }

}
