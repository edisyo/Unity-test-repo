using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;



public class start_game : MonoBehaviour
{

    public GameObject scan_text;
    [HideInInspector] 
    public bool hasPressedStart;
    public Camera ARCamera;

    public GameObject previewObject;
    public GameObject objectToInstantiate;
    public GameObject touchScreenText;
    public TextMeshProUGUI debugText;
    public TextMeshProUGUI debugText2;
    public GameObject backButton;

    private ARRaycastManager ArRaycast;
    private bool hasFoundPlane;
    private bool calibrationIsDone;
    private bool readyToPreview;
    


    private Pose placementPose;
    private bool placementIsValid = false;

    private void Awake() {
        scan_text.SetActive(false);
        hasPressedStart = false;
        previewObject.SetActive(false);
        hasFoundPlane = false;
        touchScreenText.SetActive(false);
        calibrationIsDone = false;
        readyToPreview = false;
        backButton.SetActive(false);

        objectToInstantiate.SetActive(false);

        
    }

    void Start()
    {
        ArRaycast = FindObjectOfType<ARRaycastManager>();
    }


    void Update()
    {   
        if(hasPressedStart)
        {
            backButton.SetActive(true);

            if(!calibrationIsDone)
            {
                //update preview object
                updatePlacementPose();
                updatePreviewObject();
            }
            
            //if touches screen - place real object where is preview object and end previewing
            if(placementIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
                placeObject();
            }
        }

        //when AR object is placed
        if(calibrationIsDone)
        {
            debugText2.text = "PLACED";
        }
        
    }

    private void updatePlacementPose()
    {                
        var screenCenter = ARCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        //update pose
        ArRaycast.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon);
            
        Debug.Log("raycastin");
        placementIsValid = hits.Count > 0;
        debugText.text = "Placement: " + placementIsValid;
        if(placementIsValid)
        {
            Debug.Log("placement is valid, updating pose");
            placementPose = hits[0].pose;
        }
    }
    //Set preview object active and update its position
    private void updatePreviewObject()
    {
        Debug.Log("updateing preview");
        if(placementIsValid)
        {
            previewObject.SetActive(true);
            scan_text.SetActive(false);
            touchScreenText.SetActive(true);
            previewObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);

        }
        else
        {
            scan_text.SetActive(true);
            previewObject.SetActive(false);
        }
    }

    private void placeObject(){

        //set preview off and turn on real AR object, apply pose position and rotation
        previewObject.SetActive(false);
        objectToInstantiate.SetActive(true);
        objectToInstantiate.transform.position = placementPose.position;
        objectToInstantiate.transform.rotation = placementPose.rotation;

        calibrationIsDone = true;
        scan_text.SetActive(false);

    }

    public void back_Button()
    {
        previewObject.SetActive(false);
        objectToInstantiate.SetActive(false);
        

        //UI objects
        backButton.SetActive(false);
        scan_text.SetActive(false);
        touchScreenText.SetActive(false);

        FindObjectOfType<ArOnOff>().turnOnMainMenu();

        previewObject.transform.position = new Vector3(0f,0f,0f);
        objectToInstantiate.transform.position = new Vector3(0f,0f,0f);

        calibrationIsDone = false;
        hasPressedStart = false;

        
    }
}
