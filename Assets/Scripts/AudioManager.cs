﻿using UnityEngine;
using System.Collections;


//Sound class for each clip
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randVolume = 0.1f; //Multiplier for volume
    [Range(0f, 0.5f)]
    public float randPitch = 0.1f; //Multipler for pitch;

    public bool loop = false;

    private AudioSource source;

    public void setSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
    }

    public void Play()
    {
        source.volume = volume * (1 + Random.Range(-randVolume / 2f, randVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randPitch / 2f, randPitch / 2f));
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

}

public class AudioManager : MonoBehaviour {

    public static AudioManager current;

    [SerializeField]
    Sound[] sounds;

    void Awake()
    {
        if (current != null)
        {
            if(current != this)
            {
                Destroy(this.gameObject);
            }

        }
        else
        {
            current = this;
            DontDestroyOnLoad(this); //For scene changes, if needed, in the future;
        }
        
    }

    void Start()
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_"  + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].setSource(_go.AddComponent<AudioSource>());
        }

        PlaySound("BGM");

    }


    public void PlaySound(string _name)
    {
        for(int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        //No sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }

    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        //No sound with _name
        Debug.LogWarning("AudioManager: Sound not found in list: " + _name);
    }

    public void delayPlaySound(string _name, float value)
    {
        StartCoroutine(_delayPlaySound(_name, value));
    }

    IEnumerator _delayPlaySound(string _name, float value)
    {

        yield return new WaitForSeconds(value);

        PlaySound(_name);


    }

    ///GLOBAL SOUND METHODS///
    public void playUICLICK()
    {
        PlaySound("UIClick");
    }


}