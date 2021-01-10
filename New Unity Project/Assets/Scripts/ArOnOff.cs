using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArOnOff : MonoBehaviour
{

    public GameObject text;
    public GameObject ARSession;
    public GameObject ARSessionOrigin;

    public GameObject[] UIElements;

    
    private bool isOn;

    private string textOn = "AR is ON";
    private string textOff = "AR is OFF";

    TextMeshProUGUI textComponent;

    start_game start_game_script;

    void Start()
    {
        textComponent = text.GetComponent<TextMeshProUGUI>();
        start_game_script = FindObjectOfType<start_game>();
        
        //turn off AR
        //isOn = false;
        textComponent.text = textOff;

        //ARSession.SetActive(false);
        //ARSessionOrigin.SetActive(false);

        //turn on main menu
        turnOnMainMenu();
        
    }

    private void Update() {
        //checking_AR();   
    }
    
    public void checking_AR()
    {
        if(ARSession.activeSelf == true)
        {
            Debug.Log("AR is on");
            textComponent.text = textOn;
            
        } else if(ARSession.activeSelf == false)
        {
            Debug.Log("AR is off");
            textComponent.text = textOff;
        } 
        
        
    }

    public void Start_button(){

        //turn on AR
        ARSession.SetActive(true);
        ARSessionOrigin.SetActive(true);

        //turn off main menu
        foreach (var item in UIElements)
        {
            item.SetActive(false);
        }

        if(start_game_script != null){
            //if has found this script, change bool to true
            start_game_script.hasPressedStart = true;
        }
    }

    public void turnOnMainMenu(){
        foreach (var item in UIElements)
        {
            item.SetActive(true);
        }
    }
}
