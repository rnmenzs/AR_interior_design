using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectController : MonoBehaviour
{

    private Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();

        outline.enabled = false;
    }

    public void SetOutline()
    {
        if(outline.enabled)
        {
            outline.enabled = false;

            return;
        }

        outline.enabled = true;

    }

}
