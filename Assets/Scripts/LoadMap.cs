using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadMap : MonoBehaviour
{
    [SerializeField] private GameObject[] sprite;
    [SerializeField] private GameObject[] Ghosts;
    [SerializeField] private Transform PacStudent;
    [SerializeField] private Camera Camera;
    public Animator PacStudentController;
    private Vector3 startPos;
    private Vector3 endPos;
    private float startTime;
    private float speed;
    int[,] map;
    int row, col;
    int direction;
    int x, y;

    int[,] levelMap =
        {
            {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
            {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
            {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
            {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
            {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
            {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
            {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
            {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
            {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
            {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
            {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
            {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
            {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
        };

    // Start is called before the first frame update
    void Start()
    {
        int i, j;
        Time.timeScale = 0;
        row = levelMap.GetLength(0);
        col = levelMap.GetLength(1);
        map = new int[2 * row - 1, 2 * col];
        for (i = 0; i < 2 * row - 1; i++)
            for (j = 0; j < 2 * col; j++)
                if (i < row)
                    if (j < col)
                        map[i, j] = levelMap[i, j];
                    else
                        map[i, j] = map[i, 2 * col - j - 1];
                else
                    if (j < col)
                        map[i, j] = map[2 * row - i - 2, j];
                    else
                        map[i, j] = map[2 * row - i - 2, 2 * col - j - 1];
        row += row - 1;
        col += col;
        for (i = 0; i < row; i++)
            for (j = 0; j < col; j++)
                switch (map[i,j])
                {
                    case 0:
                        break;
                    case 1:
                        if (i + 1 < row && (map[i + 1, j] == 1 || map[i + 1, j] == 2))
                        {
                            if (j + 1 < col && (map[i, j + 1] == 1 || map[i, j + 1] == 2))
                                GameObject.Instantiate(sprite[0], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                            else if (j > 0 && (map[i, j - 1] == 1 || map[i, j - 1] == 2))
                                GameObject.Instantiate(sprite[0], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                        }
                        else if (i > 0 && (map[i - 1, j] == 1 || map[i - 1, j] == 2))
                        {
                            if (j + 1 < col && (map[i, j + 1] == 1 || map[i, j + 1] == 2))
                                GameObject.Instantiate(sprite[0], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                            else if (j > 0 && (map[i, j - 1] == 1 || map[i, j - 1] == 2))
                                GameObject.Instantiate(sprite[0], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                        }
                        break;
                    case 2:
                        if (i > 0 && (map[i - 1,j] == 1 || map[i - 1, j] == 2) && i + 1 < row && (map[i + 1, j] == 1 || map[i + 1, j] == 2))
                            GameObject.Instantiate(sprite[1], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                        else
                            GameObject.Instantiate(sprite[1], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                        break;
                    case 3:
                        bool flag = false;
                        if (map[i + 1, j] == 3 || map[i + 1, j] == 4 || map[i + 1, j] == 7)
                        {
                            if (map[i, j + 1] == 3 || map[i, j + 1] == 4 || map[i, j + 1] == 7)
                            {
                                if (map[i - 1, j] == 3 || map[i - 1, j] == 4 || map[i - 1, j] == 7 || map[i, j - 1] == 3 || map[i, j - 1] == 4 || map[i, j - 1] == 7)
                                    flag = true;
                                else
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                            }
                            else if (map[i, j - 1] == 3 || map[i, j - 1] == 4 || map[i, j - 1] == 7)
                            {
                                if (map[i - 1, j] == 3 || map[i - 1, j] == 4 || map[i - 1, j] == 7)
                                    flag = true;
                                else
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                            }
                        }
                        else if (map[i - 1, j] == 3 || map[i - 1, j] == 4 || map[i - 1, j] == 7)
                        {
                            if (map[i, j + 1] == 3 || map[i, j + 1] == 4 || map[i, j + 1] == 7)
                            {
                                if (map[i, j - 1] == 3 || map[i, j - 1] == 4 || map[i, j - 1] == 7)
                                    flag = true;
                                else
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                            }
                            else if (map[i, j - 1] == 3 || map[i, j - 1] == 4 || map[i, j - 1] == 7)
                                GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                        }
                        if (flag)
                            if (map[i + 1, j] == 3 && map[i + 2, j] != 3 && map[i + 2, j] != 4 || map[i + 1, j] == 4 && (map[i + 2, j] == 3 || map[i + 2, j] == 4) || map[i + 1, j] == 7)
                            {
                                if (map[i, j + 1] == 3 && map[i, j + 2] != 3 && map[i, j + 2] != 4 || map[i, j + 1] == 4 && (map[i, j + 2] == 3 || map[i, j + 2] == 4) || map[i, j + 1] == 7)
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                                else if (map[i, j - 1] == 3 && map[i, j - 2] != 3 && map[i, j - 2] != 4 || map[i, j - 1] == 4 && (map[i, j - 2] == 3 || map[i, j - 2] == 4) || map[i, j - 1] == 7)
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                            }
                            else if (map[i - 1, j] == 3 && map[i - 2, j] != 3 && map[i - 2, j] != 4 || map[i - 1, j] == 4 && (map[i - 2, j] == 3 || map[i - 2, j] == 4) || map[i - 1, j] == 7)
                            {
                                if (map[i, j + 1] == 3 && map[i, j + 2] != 3 && map[i, j + 2] != 4 || map[i, j + 1] == 4 && (map[i, j + 2] == 3 || map[i, j + 2] == 4) || map[i, j + 1] == 7)
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                                else if (map[i, j - 1] == 3 && map[i, j - 2] != 3 && map[i, j - 2] != 4 || map[i, j - 1] == 4 && (map[i, j - 2] == 3 || map[i, j - 2] == 4) || map[i, j - 1] == 7)
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                            }
                        break;
                    case 4:
                        if ((map[i - 1, j] == 3 || map[i - 1, j] == 4 || map[i - 1, j] == 7) && (map[i + 1, j] == 3 || map[i + 1, j] == 4 || map[i + 1, j] == 7))
                            GameObject.Instantiate(sprite[3], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                        else
                            GameObject.Instantiate(sprite[3], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                        break;
                    case 5:
                        GameObject.Instantiate(sprite[4], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                        break;
                    case 6:
                        GameObject.Instantiate(sprite[5], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                        break;
                    case 7:
                        if (j > 0 && (map[i, j - 1] == 1 || map[i, j - 1] == 2 || map[i, j - 1] == 7) && j + 1 < col && (map[i, j + 1] == 1 || map[i, j + 1] == 2 || map[i, j + 1] == 7))
                        {
                            if (i + 1 < row && (map[i + 1, j] == 3 || map[i + 1, j] == 4))
                                GameObject.Instantiate(sprite[6], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                            else if (i > 0 && (map[i - 1, j] == 3 || map[i - 1, j] == 4))
                                GameObject.Instantiate(sprite[6], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                        }
                        else if (i > 0 && (map[i - 1, j] == 1 || map[i - 1, j] == 2 || map[i - 1, j] == 7) && i + 1 < row && (map[i + 1, j] == 1 || map[i + 1, j] == 2 || map[i + 1, j] == 7))
                        {
                            if (j > 0 && (map[i, j - 1] == 3 || map[i, j - 1] == 4))
                                GameObject.Instantiate(sprite[6], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                            else if (j + 1 < col && (map[i, j + 1] == 3 || map[i, j + 1] == 4))
                                GameObject.Instantiate(sprite[6], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                        }
                        break;
                }

        Camera.orthographicSize = row / 2 + 3;

        PacStudent.position = new Vector3(1 - col / 2, row / 2 - 1, 0.0f);
        speed = 1.0f;
        direction = 0;
        x = 1;
        y = 1;
        //studentMove();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /*void studentMove()
    {
        startPos = PacStudent.position;
        startTime = Time.time;
        switch (direction)
        {
            case 0:
                PacStudentController.SetTrigger("right");
                while (true)
                {
                    if (x + 1 == col)
                        break;
                    if (map[y, x + 1] == 0 || map[y, x + 1] == 5 || map[y, x + 1] == 6)
                        x++;
                    else
                        break;
                    if (y + 1 == row)
                        break;
                    if (map[y + 1, x] == 0 || map[y + 1, x] == 5 || map[y + 1, x] == 6)
                        break;
                }
                endPos = new Vector3(x - col / 2, row / 2 - y, 0.0f);
                break;
            case 1:
                PacStudentController.SetTrigger("down");
                while (true)
                {
                    if (y + 1 == row)
                        break;
                    if (map[y + 1, x] == 0 || map[y + 1, x] == 5 || map[y + 1, x] == 6)
                        y++;
                    else
                        break;
                    if (x == 0)
                        break;
                    if (map[y, x - 1] == 0 || map[y, x - 1] == 5 || map[y, x - 1] == 6)
                        break;
                }
                endPos = new Vector3(x - col / 2, row / 2 - y, 0.0f);
                break;
            case 2:
                PacStudentController.SetTrigger("left");
                while (true)
                {
                    if (x == 0)
                        break;
                    if (map[y, x - 1] == 0 || map[y, x - 1] == 5 || map[y, x - 1] == 6)
                        x--;
                    else
                        break;
                    if (y == 0)
                        break;
                    if (map[y - 1, x] == 0 || map[y - 1, x] == 5 || map[y - 1, x] == 6)
                        break;
                }
                endPos = new Vector3(x - col / 2, row / 2 - y, 0.0f);
                break;
            case 3:
                PacStudentController.SetTrigger("up");
                while (true)
                {
                    if (y == 0)
                        break;
                    if (map[y - 1, x] == 0 || map[y - 1, x] == 5 || map[y - 1, x] == 6)
                        y--;
                    else
                        break;
                    if (x + 1 == col)
                        break;
                    if (map[y, x + 1] == 0 || map[y, x + 1] == 5 || map[y, x + 1] == 6)
                        break;
                }
                endPos = new Vector3(x - col / 2, row / 2 - y, 0.0f);
                break;
        }
    }*/
}