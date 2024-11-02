using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Transform[] Ghosts;
    private Animator[] animators;
    private int[] GhostsX;
    private int[] GhostsY;
    private float[] GhostsDeath;
    public float GhostSpeed = 2.0f;
    public static bool isScaredStart = false;
    public static bool isRecovering = false;
    public static bool isScaredEnd = false;
    private bool ifCheckDeath = false;
    private int alive = 4;

    // Start is called before the first frame update
    void Start()
    {
        int[] mapPos;

        animators = new Animator[4];
        animators[0] = Ghosts[0].GetComponent<Animator>();
        animators[1] = Ghosts[1].GetComponent<Animator>();
        animators[2] = Ghosts[2].GetComponent<Animator>();
        animators[3] = Ghosts[3].GetComponent<Animator>();


        GhostsX = new int[4];
        GhostsY = new int[4];

        Ghosts[0].position = new Vector3(-1, 1, 0);
        mapPos = LoadMap.getMapPos(-1, 1);
        GhostsX[0] = mapPos[0];
        GhostsY[0] = mapPos[1];

        Ghosts[1].position = new Vector3(0, 1, 0);
        mapPos = LoadMap.getMapPos(0, 1);
        GhostsX[1] = mapPos[0];
        GhostsY[1] = mapPos[1];

        Ghosts[2].position = new Vector3(-1, -1, 0);
        mapPos = LoadMap.getMapPos(-1, -1);
        GhostsX[2] = mapPos[0];
        GhostsY[2] = mapPos[1];

        Ghosts[3].position = new Vector3(0, -1, 0);
        mapPos = LoadMap.getMapPos(0, -1);
        GhostsX[3] = mapPos[0];
        GhostsY[3] = mapPos[1];


        GhostsDeath = new float[4];
        GhostsDeath[0] = 0;
        GhostsDeath[1] = 0;
        GhostsDeath[2] = 0;
        GhostsDeath[3] = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int i;

        if (alive < 4)
        {
            for (i = 0; i < 4; i++)
            {
                if (GhostsDeath[i] > 0)
                {
                    GhostsDeath[i] -= Time.deltaTime;
                    if (GhostsDeath[i] <= 0)
                    {
                        animators[i].SetTrigger("revive");
                        animators[i].SetBool("scared", false);
                        Ghosts[i].tag = "Ghost";
                        alive++;
                        if (alive == 4)
                            BackgroundMusicManager.isGhostsRevive = true;
                    }
                }
            }
        }
        if (ifCheckDeath)
        {
            for (i = 0; i < 4; i++)
            {
                if (animators[i].GetBool("death"))
                {
                    GhostsDeath[i] = 5.0f;
                    alive--;
                    animators[i].SetBool("death", false);
                    animators[i].CrossFade("Ghosts_death", 0);
                }
            }
        }
        if (isScaredStart)
        {
            isScaredStart = false;
            for (i = 0; i < 4; i++)
            {
                animators[i].SetBool("scared", true);
            }
            ifCheckDeath = true;
        }
        if (isRecovering)
        {
            isRecovering = false;
            for (i = 0; i < 4; i++)
            {
                animators[i].SetTrigger("recover");
            }
        }
        if (isScaredEnd)
        {
            isScaredEnd = false;
            for (i = 0; i < 4; i++)
            {
                if (GhostsDeath[i] <= 0)
                {
                    animators[i].SetBool("scared", false);
                    animators[i].CrossFade("GhostAnimator_" + i, 0);
                }
            }
            ifCheckDeath = false;
        }
    }
}
