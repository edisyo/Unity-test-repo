using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;



public class start_game : MonoBehaviour
{

    public GameObject scan_text;
    [HideInInspector] 
    public bool hasPressedStart;

    public GameObject previewObject;
    public GameObject objectToInstantiate;
    public GameObject touchScreenText;

    private ARRaycastManager ArRaycast;
    private bool hasFoundPlane;
    private bool calibrationIsDone;

    private void Awake() {
        scan_text.SetActive(false);
        hasPressedStart = false;
        previewObject.SetActive(false);
        hasFoundPlane = false;
        touchScreenText.SetActive(false);
        calibrationIsDone = false;

        ArRaycast = FindObjectOfType<ARRaycastManager>();
    }

    void Start()
    {
        
    }


    void Update()
    {
        //if hasnt calibrated
        if(!calibrationIsDone){

            //activate scan text
            if(hasPressedStart){
                scan_text.SetActive(true);
                Debug.Log("has pressed start");

            }

            //activate raycast to planes
            if(scan_text.activeSelf == true && !hasFoundPlane){
                var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
                var hits = new List<ARRaycastHit>();

                //raycast to place preview object
                if(ArRaycast.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon)){
                    hasFoundPlane = true;
                } else {
                    hasFoundPlane = false;
                }
            }

            //If has found planes, start to preview the object
            if(hasFoundPlane){
                Debug.Log("has found plane");
                scan_text.SetActive(false);

                //raycast to preview the object
                if(scan_text.activeSelf == true && !hasFoundPlane){
                    var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
                    var hits = new List<ARRaycastHit>();

                    Pose hitpose = hits[0].pose;
                    updatePreviewObject(hitpose);

                    
                }
            }
        }
        

    }

    //Set preview object active and update its position
    private void updatePreviewObject(Pose objectPose){
        previewObject.SetActive(true);
        touchScreenText.SetActive(true);

        //place at the center of the screen and make the objects look at users camera
        previewObject.transform.position = objectPose.position;
        previewObject.transform.LookAt(ArRaycast.transform);

        if(Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began){
                previewObject.SetActive(false);
                Instantiate(objectToInstantiate, previewObject.transform.position, previewObject.transform.rotation);
                calibrationIsDone = true;
            }
        }
    }
}
