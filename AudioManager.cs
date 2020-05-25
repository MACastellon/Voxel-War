using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource _audio;

    [Header("[ -- Audio Clips -- ]")]
    [SerializeField] public AudioClip introClip;
    [SerializeField] public AudioClip combatClip;
    [SerializeField] public AudioClip villageClip;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeMusic(AudioClip clip) {
        _audio.clip = clip;
        _audio.Play();
    }
}
