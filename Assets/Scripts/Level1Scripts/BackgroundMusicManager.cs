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
    public static bool isGhostsScaredStart = false;
    public static bool isGhostsScaredEnd = false;
    public static bool isGhostsDead = false;
    private bool ifPlayingDead = false;
    public static bool isGhostsRevive = false;
    private AudioClip lastClip;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Background_ghosts_normal;
    }


    private void Update()
    {
        if (isGhostsScaredStart)
        {
            isGhostsScaredStart = false;
            audioSource.Stop();
            audioSource.clip = Background_ghosts_scared;
        }
        if (isGhostsScaredEnd)
        {
            isGhostsScaredEnd = false;
            if (ifPlayingDead)
                lastClip = Background_ghosts_normal;
            else
            {
                audioSource.Stop();
                audioSource.clip = Background_ghosts_normal;
            }
        }
        if (isGhostsDead)
        {
            isGhostsDead = false;
            if (!ifPlayingDead)
            {
                lastClip = audioSource.clip;
                audioSource.Stop();
                audioSource.clip = Background_ghosts_dead;
            }
            ifPlayingDead = true;
        }
        if (isGhostsRevive)
        {
            isGhostsRevive = false;
            audioSource.Stop();
            audioSource.clip = lastClip;
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
