using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class GhostController : MonoBehaviour
{
    [SerializeField] private Transform PacStudent;
    [SerializeField] private Transform[] Ghosts;
    [SerializeField] private Tweener tweener;
    private Animator[] animators;
    private int[] ghostsX;
    private int[] ghostsY;
    private int[] ghostsDir;
    private float[] ghostsDeath;
    public float speed = 2.0f;
    public static bool isScaredStart = false;
    public static bool isRecovering = false;
    public static bool isScaredEnd = false;
    private int alive = 4;
    public static bool ifStart = false;
    private bool isScaredTime = false;


    private void Start()
    {
        int[] mapPos;

        animators = new Animator[4];
        animators[0] = Ghosts[0].GetComponent<Animator>();
        animators[1] = Ghosts[1].GetComponent<Animator>();
        animators[2] = Ghosts[2].GetComponent<Animator>();
        animators[3] = Ghosts[3].GetComponent<Animator>();


        ghostsX = new int[4];
        ghostsY = new int[4];

        Ghosts[0].position = new Vector3(-1, 1, 0);
        mapPos = LoadMap.getMapPos(-1, 1);
        ghostsX[0] = mapPos[0];
        ghostsY[0] = mapPos[1];

        Ghosts[1].position = new Vector3(0, 1, 0);
        mapPos = LoadMap.getMapPos(0, 1);
        ghostsX[1] = mapPos[0];
        ghostsY[1] = mapPos[1];

        Ghosts[2].position = new Vector3(-1, -1, 0);
        mapPos = LoadMap.getMapPos(-1, -1);
        ghostsX[2] = mapPos[0];
        ghostsY[2] = mapPos[1];

        Ghosts[3].position = new Vector3(0, -1, 0);
        mapPos = LoadMap.getMapPos(0, -1);
        ghostsX[3] = mapPos[0];
        ghostsY[3] = mapPos[1];


        ghostsDir = new int[4] { 0, 0, 2, 2 };
        ghostsDeath = new float[4] { 0, 0, 0, 0 };
    }


    private void Update()
    {
        int i;

        if (!ifStart)
        {
            return;
        }

        if (alive < 4)
        {
            for (i = 0; i < 4; i++)
            {
                if (ghostsDeath[i] > 0)
                {
                    ghostsDeath[i] -= Time.deltaTime;
                    if (ghostsDeath[i] <= 0)
                    {
                        animators[i].SetTrigger("revive");
                        Ghosts[i].tag = "Ghost";
                        alive++;
                        if (!isScaredTime)
                            animators[i].SetBool("scared", false);
                        if (alive == 4)
                            BackgroundMusicManager.isGhostsRevive = true;
                    }
                }
            }
        }
        if (isScaredTime)
        {
            for (i = 0; i < 4; i++)
            {
                if (animators[i].GetBool("death"))
                {
                    ghostsDeath[i] = 5.0f;
                    alive--;
                    animators[i].SetBool("death", false);
                    animators[i].CrossFade("Ghosts_death", 0);
                    tweener.TweenCancel(Ghosts[i]);
                    tweener.AddTween(Ghosts[i], new Vector3(-1, 1, 0), 5.0f);
                    int[] mapPos = LoadMap.getMapPos(-1, 1);
                    ghostsX[i] = mapPos[0];
                    ghostsY[i] = mapPos[1];
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
            isScaredTime = true;
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
                if (ghostsDeath[i] <= 0)
                {
                    animators[i].SetBool("scared", false);
                    animators[i].CrossFade("GhostAnimator_" + i, 0);
                }
            }
            isScaredTime = false;
        }
        for (i = 0; i < 4; i++)
        {
            if (Ghosts[i].tag == "Ghost" && !tweener.TweenExists(Ghosts[i]))
            {
                if (animators[i].GetBool("scared"))
                    GhostMove1(i);
                else
                {
                    switch (i)
                    {
                        case 0: GhostMove1(i); break;
                        case 1: GhostMove2(i); break;
                        case 2: GhostMove3(i); break;
                        case 3: GhostMove4(i); break;
                    }
                }
            }
        }
    }


    private void GhostMove1(int index)
    {
        List<Vector3> targetPos1 = new List<Vector3>();
        List<Vector3> targetPos2 = new List<Vector3>();
        List<int[]> targetMapPos1 = new List<int[]>();
        List<int[]> targetMapPos2 = new List<int[]>();
        List<int> dir1 = new List<int>();
        List<int> dir2 = new List<int>();
        Vector3 temp;
        float distance = Vector3.Distance(Ghosts[index].position, PacStudent.position);
        int[] target;
        int[] dirs = new int[3];
        int randomIndex;
        dirs[0] = ghostsDir[index];
        dirs[1] = (ghostsDir[index] + 1) % 4;
        dirs[2] = (ghostsDir[index] + 3) % 4;
        foreach (int dir in dirs)
        {
            target = moveAvailablity(dir, index);
            if (target.Length == 2)
            {
                temp = LoadMap.getPos(target[0], target[1]);
                targetPos2.Add(temp);
                targetMapPos2.Add(target);
                dir2.Add(dir);
                if (Vector3.Distance(temp, PacStudent.position) >= distance)
                {
                    targetPos1.Add(temp);
                    targetMapPos1.Add(target);
                    dir1.Add(dir);
                }
            }
        }
        if (targetPos1.Count() > 0)
        {
            randomIndex = UnityEngine.Random.Range(0, targetPos1.Count());
            ghostsX[index] = targetMapPos1[randomIndex][0];
            ghostsY[index] = targetMapPos1[randomIndex][1];
            ghostsDir[index] = dir1[randomIndex];
            tweener.AddTween(Ghosts[index], targetPos1[randomIndex], 1 / speed);
        }
        else if (targetPos2.Count() > 0)
        {
            randomIndex = UnityEngine.Random.Range(0, targetPos2.Count());
            ghostsX[index] = targetMapPos2[randomIndex][0];
            ghostsY[index] = targetMapPos2[randomIndex][1];
            ghostsDir[index] = dir2[randomIndex];
            tweener.AddTween(Ghosts[index], targetPos2[randomIndex], 1 / speed);
        }
        else
        {
            target = moveAvailablity((ghostsDir[index] + 2) % 4, index);
            ghostsX[index] = target[0];
            ghostsY[index] = target[1];
            ghostsDir[index] = (ghostsDir[index] + 2) % 4;
            tweener.AddTween(Ghosts[index], LoadMap.getPos(ghostsX[index], ghostsY[index]), 1 / speed);
        }
    }


    private void GhostMove2(int index)
    {
        List<Vector3> targetPos1 = new List<Vector3>();
        List<Vector3> targetPos2 = new List<Vector3>();
        List<int[]> targetMapPos1 = new List<int[]>();
        List<int[]> targetMapPos2 = new List<int[]>();
        List<int> dir1 = new List<int>();
        List<int> dir2 = new List<int>();
        Vector3 temp;
        float distance = Vector3.Distance(Ghosts[index].position, PacStudent.position);
        int[] target;
        int[] dirs = new int[3];
        int randomIndex;
        dirs[0] = ghostsDir[index];
        dirs[1] = (ghostsDir[index] + 1) % 4;
        dirs[2] = (ghostsDir[index] + 3) % 4;
        foreach (int dir in dirs)
        {
            target = moveAvailablity(dir, index);
            if (target.Length == 2)
            {
                temp = LoadMap.getPos(target[0], target[1]);
                targetPos2.Add(temp);
                targetMapPos2.Add(target);
                dir2.Add(dir);
                if (Vector3.Distance(temp, PacStudent.position) <= distance)
                {
                    targetPos1.Add(temp);
                    targetMapPos1.Add(target);
                    dir1.Add(dir);
                }
            }
        }
        if (targetPos1.Count() > 0)
        {
            randomIndex = UnityEngine.Random.Range(0, targetPos1.Count());
            ghostsX[index] = targetMapPos1[randomIndex][0];
            ghostsY[index] = targetMapPos1[randomIndex][1];
            ghostsDir[index] = dir1[randomIndex];
            tweener.AddTween(Ghosts[index], targetPos1[randomIndex], 1 / speed);
        }
        else if (targetPos2.Count() > 0)
        {
            randomIndex = UnityEngine.Random.Range(0, targetPos2.Count());
            ghostsX[index] = targetMapPos2[randomIndex][0];
            ghostsY[index] = targetMapPos2[randomIndex][1];
            ghostsDir[index] = dir2[randomIndex];
            tweener.AddTween(Ghosts[index], targetPos2[randomIndex], 1 / speed);
        }
        else
        {
            target = moveAvailablity((ghostsDir[index] + 2) % 4, index);
            ghostsX[index] = target[0];
            ghostsY[index] = target[1];
            ghostsDir[index] = (ghostsDir[index] + 2) % 4;
            tweener.AddTween(Ghosts[index], LoadMap.getPos(ghostsX[index], ghostsY[index]), 1 / speed);
        }
    }


    private void GhostMove3(int index)
    {
        int[] target;
        int[] dirs = new int[3];
        dirs[0] = ghostsDir[index];
        dirs[1] = (ghostsDir[index] + 1) % 4;
        dirs[2] = (ghostsDir[index] + 3) % 4;
        dirs = dirs.OrderBy(x => UnityEngine.Random.value).ToArray();
        foreach (int dir in dirs)
        {
            target = moveAvailablity(dir, index);
            if (target.Length == 2)
            {
                ghostsX[index] = target[0];
                ghostsY[index] = target[1];
                ghostsDir[index] = dir;
                tweener.AddTween(Ghosts[index], LoadMap.getPos(ghostsX[index], ghostsY[index]), 1 / speed);
                return;
            }
        }
        target = moveAvailablity((ghostsDir[index] + 2) % 4, index);
        ghostsX[index] = target[0];
        ghostsY[index] = target[1];
        ghostsDir[index] = (ghostsDir[index] + 2) % 4;
        tweener.AddTween(Ghosts[index], LoadMap.getPos(ghostsX[index], ghostsY[index]), 1 / speed);
    }


    private void GhostMove4(int index)
    {
        int[] target;
        int[] dirs = new int[4];
        dirs[0] = (ghostsDir[index] + 1) % 4;
        dirs[1] = ghostsDir[index];
        dirs[2] = (ghostsDir[index] + 3) % 4;
        dirs[3] = (ghostsDir[index] + 2) % 4;
        foreach (int dir in dirs)
        {
            target = moveAvailablity(dir, index);
            if (target.Length == 2)
            {
                ghostsX[index] = target[0];
                ghostsY[index] = target[1];
                ghostsDir[index] = dir;
                tweener.AddTween(Ghosts[index], LoadMap.getPos(ghostsX[index], ghostsY[index]), 1 / speed);
                break;
            }
        }
    }


    private int[] moveAvailablity(int dir, int index)
    {
        int x, y;
        switch (dir)
        {
            case 0: x = ghostsX[index] - 1; y = ghostsY[index]; break;
            case 1: x = ghostsX[index]; y = ghostsY[index] + 1; break;
            case 2: x = ghostsX[index] + 1; y = ghostsY[index]; break;
            case 3: x = ghostsX[index]; y = ghostsY[index] - 1; break;
            default: x = 0; y = 0; break;
        }
        if (LoadMap.ifAvailable(x, y) == -1)
            return new int[] {x, y};
        return Array.Empty<int>();
    }
}
