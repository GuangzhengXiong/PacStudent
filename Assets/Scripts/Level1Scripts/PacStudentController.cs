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
    [SerializeField] private Tweener tweener;
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private ParticleSystem collide;
    [SerializeField] private ParticleSystem deathEffect;
    public float speed = 3.0f;
    public static bool playMusicEat = false;
    private Animator animator;
    private AudioSource[] audioSource;
    private string lastInput;
    private string currentInput;
    private int x = 1, y = 1;
    public static bool ifStart = false;

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
        if (!ifStart)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W))
            lastInput = "up";
        if (Input.GetKeyDown(KeyCode.D))
            lastInput = "right";
        if (Input.GetKeyDown(KeyCode.S))
            lastInput = "down";
        if (Input.GetKeyDown(KeyCode.A))
            lastInput = "left";

        if (!tweener.TweenExists(gameObject.transform) && lastInput != "")
        {
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
                collide.Clear();
                collide.transform.position = gameObject.transform.TransformPoint(new Vector3(0.3f, 0, 0));
                collide.Play(true);
                audioSource[1].clip = PacStudent_collide;
                audioSource[1].Play();
                dust.Stop();
                audioSource[0].Stop();
                animator.SetBool(currentInput, false);
                currentInput = "";
            }
        }
    }


    private bool Move(string dir)
    {
        int x1, y1, result;
        switch (dir)
        {
            case "up": x1 = x; y1 = y - 1; break;
            case "right": x1 = x + 1; y1 = y; break;
            case "down": x1 = x; y1 = y + 1; break;
            case "left": x1 = x - 1; y1 = y; break;
            default: return false;
        }
        result = LoadMap.ifAvailable(x1, y1);
        if (result == -2)
            return false;
        y = y1;
        if (result >= 0)
        {
            x = result;
            gameObject.transform.position = LoadMap.getPos(x, y);
            return true;
        }
        x = x1;
        tweener.AddTween(gameObject.transform, LoadMap.getPos(x, y), 1 / speed);
        return true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ifStart)
        {
            return;
        }
        if (collision.tag == "Pellet_normal")
        {
            UHDManager.score += 10;
            audioSource[1].clip = PacStudent_eat;
            audioSource[1].Play();
            Destroy(collision.gameObject);
            LoadMap.pelletNum--;
        }
        else if (collision.tag == "Pellet_power")
        {
            audioSource[1].clip = PacStudent_eat;
            audioSource[1].Play();
            Destroy(collision.gameObject);
            UHDManager.isGhostsScaredStart = true;
            BackgroundMusicManager.isGhostsScaredStart = true;
            GhostController.isScaredStart = true;
            LoadMap.pelletNum--;
        }
        else if (collision.tag == "Cherry")
        {
            UHDManager.score += 100;
            audioSource[1].clip = PacStudent_eat;
            audioSource[1].Play();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Ghost")
        {
            Animator animatorCollision = collision.GetComponent<Animator>();
            if(animatorCollision.GetBool("scared"))
            {
                UHDManager.score += 300;
                animatorCollision.SetBool("death", true);
                collision.tag = "DeadGhost";
                BackgroundMusicManager.isGhostsDead = true;
            }
            else
            {
                StartCoroutine(death());
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Pellet_normal" && ifStart)
        {
            UHDManager.score += 10;
            audioSource[1].clip = PacStudent_eat;
            audioSource[1].Play();
            Destroy(collision.gameObject);
            LoadMap.pelletNum--;
        }
    }


    IEnumerator death()
    {
        dust.Stop();
        deathEffect.Clear();
        deathEffect.Play();
        audioSource[1].clip = PacStudent_dead;
        audioSource[1].Play();
        if (currentInput != "")
            animator.SetBool(currentInput, false);
        currentInput = "";
        lastInput = "";
        animator.SetTrigger("death");
        tweener.TweenCancel(gameObject.transform);
        UHDManager.ifLostLife = true;

        yield return new WaitForSeconds(3);

        animator.SetTrigger("revive");

        x = 1;
        y = 1;
        gameObject.transform.position = LoadMap.getPos(x, y);
    }
}
