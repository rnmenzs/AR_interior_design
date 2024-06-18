using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem.HID;
using System;
using UnityEngine.InputSystem.Controls;

public class TouchManager : MonoBehaviour
{

    private PlayerInput playerInput;
    private InputAction touch;
    private ARRaycastManager arRaycast;

    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    [SerializeField]
    private List<FurnitureSO> furnitures;

    public List<FurnitureSO> Furnitures
    {
        get
        {

            return furnitures;

        }

    }

    private RaycastHit objectHit;

    private GameObject selectedObject = null;


    [SerializeField]
    private FurnitureSO placeableObjectFurniture;

    public FurnitureSO PlaceableObjectFurniture
    {
        get { 
            
            return placeableObjectFurniture;

        }

        set
        {

            placeableObjectFurniture = value;

        }

    }

    private TouchManager instance;

    private bool uiWasUsedThisFrame;

    private static string selectedCommand = "ADD";

    private void Awake()
    {

        uiWasUsedThisFrame = false;

        arRaycast = GetComponent<ARRaycastManager>();

        aRPlaneManager = GetComponent<ARPlaneManager>();

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

        Camera mainCam = Camera.main;

        Transform camTransform = mainCam.transform;

        if(arRaycast.Raycast(position, hits, TrackableType.PlaneWithinPolygon)) {
            
            Pose pose = hits[0].pose;

            Vector3 cameraPos = camTransform.position;
            cameraPos.y = 0f;
            Vector3 objPos = pose.position;
            objPos.y = 0f;

            Vector3 direction = cameraPos - objPos;
            Quaternion rotation = Quaternion.LookRotation(direction);

            if (aRPlaneManager.GetPlane(hits[0].trackableId).alignment == PlaneAlignment.HorizontalUp && placeableObjectFurniture.orientation == TypeOrientation.Horizontal)
            {

                objPos.y -= 1f;

                Instantiate(PlaceableObjectFurniture.furniture, objPos, rotation);

            }
            
            if (aRPlaneManager.GetPlane(hits[0].trackableId).alignment == PlaneAlignment.Vertical && placeableObjectFurniture.orientation == TypeOrientation.Vertical)
            {

                Instantiate(PlaceableObjectFurniture.furniture, pose.position, pose.rotation);


            }


        }

    }

    private void RemoveObject()
    {

        Destroy(selectedObject);

        Debug.Log(selectedObject);

    }

    private void SelectObject(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);

        if (Physics.Raycast(ray, out objectHit))
        {

            GameObject hitedObject = objectHit.collider.gameObject;

            if(!hitedObject.CompareTag("Object"))
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

        if (selectedCommand == "REMOVE" && selectedObject)
        {

            RemoveObject();

            selectedCommand = "SELECT";

        }


        if(selectedCommand != "MOVE") { 
        
            yield return new WaitForSeconds(0.01f);

            if (!uiWasUsedThisFrame)
            {

                if(selectedCommand == "ADD")
                {

                    AddObject(context.ReadValue<Vector2>());

                }

                if (selectedCommand == "SELECT")
                {

                    SelectObject(context.ReadValue<Vector2>());

                }

            }

        }
        
        uiWasUsedThisFrame = false;

        StopCoroutine(nameof(WaitFrame));

    }

}
