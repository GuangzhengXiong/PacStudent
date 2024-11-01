using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip Background_game_intro;
    [SerializeField] private AudioClip Background_ghosts_normal;
    [SerializeField] private AudioClip Background_ghosts_scared;
    [SerializeField] private AudioClip Background_ghosts_dead;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = Background_ghosts_normal;
            audioSource.Play();
        }
    }
}
