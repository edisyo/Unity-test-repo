using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class arui_manager : MonoBehaviour
{
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI description_text;
    // Start is called before the first frame update
    void Start()
    {
        name_text.text = "";
        description_text.text = "Press on one of the exercises to see more info";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Exercise_name_Button(string name){
        name_text.text = name;
    }
    public void Exercise_description_Button(string description){
        description_text.text = description;
    }
}
