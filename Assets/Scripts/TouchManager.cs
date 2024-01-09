using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;

public class TouchManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction touch;
    private ARRaycastManager arRaycast;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private RaycastHit objectHit;

    private GameObject selectedObject;


    [SerializeField]
    private GameObject prefab;
    

    private TouchManager instance;

    private bool uiWasUsedThisFrame;

    private static string selectedCommand = "ADD";

    private void Awake()
    {

        uiWasUsedThisFrame = false;

        arRaycast = GetComponent<ARRaycastManager>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        playerInput = GetComponent<PlayerInput>();

        touch = playerInput.actions.FindAction("touch");


    }

    private void OnEnable()
    {

        touch.Enable();
        touch.performed += TouchInteract;

    }

    private void OnDisable()
    {

        touch.Disable();
        touch.performed -= TouchInteract;

    }

    private void TouchInteract(InputAction.CallbackContext context)
    {

        StartCoroutine(WaitFrame(context));
        
    }

    public void setUiWasUsedThisFrame(bool value)
    {
        uiWasUsedThisFrame = value;

    }

    public void setSelectedCommand(string value)
    {

        selectedCommand = value;

    }

    private void AddObject(Vector2 position)
    {

        if(arRaycast.Raycast(position, hits, TrackableType.PlaneWithinPolygon)) {

            Pose pose = hits[0].pose;

            Instantiate(prefab, pose.position, pose.rotation);

        }

    }

    private void RemoveObject(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out objectHit))
        {

            GameObject hitedObject = objectHit.collider.gameObject;

            if (hitedObject.tag != "Object")
            {

                return;

            }

            Destroy(hitedObject);

        }

    }

    private void SelectObject(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out objectHit))
        {

            GameObject hitedObject = objectHit.collider.gameObject;

            if(hitedObject.tag != "Object")
            {

                if(selectedObject)
                {

                    ObjectController oldObjectController = selectedObject.GetComponent<ObjectController>();

                    oldObjectController.SetOutline();

                    selectedObject = null;

                }

                return;

            }
            

            if(hitedObject != selectedObject && selectedObject)
            {
                ObjectController oldObjectController = selectedObject.GetComponent<ObjectController>();

                oldObjectController.SetOutline();

            }

            ObjectController objectController = hitedObject.GetComponent<ObjectController>();

            objectController.SetOutline();

            selectedObject = hitedObject;


        }

    }


    public IEnumerator WaitFrame(InputAction.CallbackContext context)
    {

        yield return new WaitForSeconds(0.1f);

        if (!uiWasUsedThisFrame)
        {

            if(selectedCommand == "ADD")
            {

                AddObject(context.ReadValue<Vector2>());

            }

            if (selectedCommand == "REMOVE")
            {

                RemoveObject(context.ReadValue<Vector2>());

            }

            if (selectedCommand == "SELECT")
            {

                SelectObject(context.ReadValue<Vector2>());

            }

        }

        
        uiWasUsedThisFrame = false;
        
        StopCoroutine("WaitFrame");


    }




}
