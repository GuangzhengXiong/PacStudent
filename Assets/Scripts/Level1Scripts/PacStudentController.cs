using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PacStudentController : MonoBehaviour
{
    [SerializeField] private AudioClip PacStudent_move;
    [SerializeField] private AudioClip PacStudent_eat;
    [SerializeField] private AudioClip PacStudent_collide;
    [SerializeField] private AudioClip PacStudent_dead;
    public Tweener tweener;
    public ParticleSystem dust;
    public float speed = 2.0f;
    private Animator animator;
    private AudioSource[] audioSource;
    private string lastInput;
    private string currentInput;
    private int x = 1, y = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = LoadMap.getPos(x, y);
        animator = GetComponent<Animator>();
        audioSource = GetComponents<AudioSource>();
        lastInput = "";
        currentInput = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            lastInput = "up";
        if (Input.GetKeyDown(KeyCode.D))
            lastInput = "right";
        if (Input.GetKeyDown(KeyCode.S))
            lastInput = "down";
        if (Input.GetKeyDown(KeyCode.A))
            lastInput = "left";

        if (!tweener.TweenExists(gameObject.transform))
        {
            if (LoadMap.ifEating(x, y))
            {
                audioSource[1].clip = PacStudent_eat;
                audioSource[1].Play();
            }
            if (Move(lastInput))
            {
                dust.Play(true);
                if (!audioSource[0].isPlaying)
                {
                    audioSource[0].clip = PacStudent_move;
                    audioSource[0].Play();
                }
                if (currentInput != lastInput)
                {
                    if (currentInput != "")
                        animator.SetBool(currentInput, false);
                    currentInput = lastInput;
                    animator.SetBool(currentInput, true);
                }
            }
        }
        if (!tweener.TweenExists(gameObject.transform) && currentInput != "")
        {
            if (Move(currentInput))
            {
                dust.Play(true);
                if (!audioSource[0].isPlaying)
                {
                    audioSource[0].clip = PacStudent_move;
                    audioSource[0].Play();
                }
            }
            else
            {
                dust.Stop();
                audioSource[0].Stop();
                animator.SetBool(currentInput, false);
                currentInput = "";
            }
        }
    }


    private bool Move(string dir)
    {
        int x1, y1;
        switch (dir)
        {
            case "up": x1 = x; y1 = y - 1; break;
            case "right": x1 = x + 1; y1 = y; break;
            case "down": x1 = x; y1 = y + 1; break;
            case "left": x1 = x - 1; y1 = y; break;
            default: return false;
        }
        if (!LoadMap.ifAvailable(x1, y1))
            return false;
        x = x1;
        y = y1;
        tweener.AddTween(gameObject.transform, LoadMap.getPos(x, y), speed);
        return true;
    }
}
