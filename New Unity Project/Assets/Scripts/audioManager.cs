using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static audioManager instance;
    // Start is called before the first frame update
    private void Awake() {

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound soundFromArray = Array.Find(sounds, Sound => Sound.name == name);
        if(soundFromArray == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        } 
        soundFromArray.source.Play();
    }
}
