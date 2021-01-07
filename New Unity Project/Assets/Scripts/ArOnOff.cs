using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArOnOff : MonoBehaviour
{

    public GameObject text;
    public GameObject ARSession;
    public GameObject ARSessionOrigin;
    public GameObject ARPlanes;

    
    private bool isOn;

    private string textOn = "AR is ON";
    private string textOff = "AR is OFF";

    TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = text.GetComponent<TextMeshProUGUI>();
        
        
        //turn off AR
        isOn = false;
        textComponent.text = textOff;
        turnOnOff_AR_objects(false);
        
    }
    
    public void button()
    {
        if(isOn == true)
        {
            Debug.Log("turn off");
            isOn = false;
            textComponent.text = textOff;
            turnOnOff_AR_objects(false);
            return;
        }

        if(isOn == false)
        {
            Debug.Log("turn on");
            isOn = true;
            textComponent.text = textOn;
            turnOnOff_AR_objects(true);
            return;
        } 
        
        
    }

    private void turnOnOff_AR_objects(bool state){
        ARSession.SetActive(state);
        ARSessionOrigin.SetActive(state);
        ARPlanes.SetActive(state);
    }
}
