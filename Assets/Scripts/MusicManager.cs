using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudio;
    [SerializeField] private AudioSource PacStudentAudio;
    [SerializeField] private AudioClip Background_game_intro;
    [SerializeField] private AudioClip Background_ghosts_normal;
    [SerializeField] private AudioClip Background_ghosts_scared;
    [SerializeField] private AudioClip Background_ghosts_dead;
    [SerializeField] private AudioClip PacStudent_move;
    [SerializeField] private AudioClip PacStudent_eat;
    [SerializeField] private AudioClip PacStudent_collide;
    [SerializeField] private AudioClip PacStudent_dead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!backgroundAudio.isPlaying)
        {
            Time.timeScale = 1;
            backgroundAudio.clip = backgroundAudioClips[1];
            backgroundAudio.Play();
        }
        if (Vector3.Distance(PacStudent.position, endPos) > 0.1f)
        {
            PacStudent.position = Vector3.Lerp(startPos, endPos, (Time.time - startTime) / (Vector3.Distance(startPos, endPos) / speed));
            if (!PacStudentAudio.isPlaying && Time.timeScale != 0)
            {
                PacStudentAudio.clip = PacStudentAudioClips[0];
                PacStudentAudio.Play();
            }
        }
        else
        {
            PacStudent.position = endPos;
            PacStudentController.SetTrigger("endMove");
            PacStudentAudio.Stop();
            direction = (direction + 1) % 4;
            studentMove();
        }*/
    }
}
