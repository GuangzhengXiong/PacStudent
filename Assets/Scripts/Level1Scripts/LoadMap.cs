using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadMap : MonoBehaviour
{
    [SerializeField] private GameObject[] sprite;
    [SerializeField] private Camera Camera;
    public Animator PacStudentController;
    private static int[,] map;
    private static GameObject[,] mapObjects;
    private static int row, col;

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


    private void Awake()
    {
        int i, j;
        row = levelMap.GetLength(0);
        col = levelMap.GetLength(1);
        map = new int[2 * row - 1, 2 * col];
        mapObjects = new GameObject[2 * row - 1, 2 * col];
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
    }

    // Start is called before the first frame update
    void Start()
    {
        int i, j;
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
                                mapObjects[i,j] = GameObject.Instantiate(sprite[0], getPos(j, i), new Quaternion());
                            else if (j > 0 && (map[i, j - 1] == 1 || map[i, j - 1] == 2))
                                mapObjects[i, j] = GameObject.Instantiate(sprite[0], getPos(j, i), Quaternion.Euler(0, 0, -90.0f));
                        }
                        else if (i > 0 && (map[i - 1, j] == 1 || map[i - 1, j] == 2))
                        {
                            if (j + 1 < col && (map[i, j + 1] == 1 || map[i, j + 1] == 2))
                                mapObjects[i, j] = GameObject.Instantiate(sprite[0], getPos(j, i), Quaternion.Euler(0, 0, 90.0f));
                            else if (j > 0 && (map[i, j - 1] == 1 || map[i, j - 1] == 2))
                                mapObjects[i, j] = GameObject.Instantiate(sprite[0], getPos(j, i), Quaternion.Euler(0, 0, 180.0f));
                        }
                        break;
                    case 2:
                        if (i > 0 && (map[i - 1,j] == 1 || map[i - 1, j] == 2) && i + 1 < row && (map[i + 1, j] == 1 || map[i + 1, j] == 2))
                            mapObjects[i, j] = GameObject.Instantiate(sprite[1], getPos(j, i), new Quaternion());
                        else
                            mapObjects[i, j] = GameObject.Instantiate(sprite[1], getPos(j, i), Quaternion.Euler(0, 0, 90.0f));
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
                                    mapObjects[i, j] = GameObject.Instantiate(sprite[2], getPos(j, i), new Quaternion());
                            }
                            else if (map[i, j - 1] == 3 || map[i, j - 1] == 4 || map[i, j - 1] == 7)
                            {
                                if (map[i - 1, j] == 3 || map[i - 1, j] == 4 || map[i - 1, j] == 7)
                                    flag = true;
                                else
                                    mapObjects[i, j] = GameObject.Instantiate(sprite[2], getPos(j, i), Quaternion.Euler(0, 0, -90.0f));
                            }
                        }
                        else if (map[i - 1, j] == 3 || map[i - 1, j] == 4 || map[i - 1, j] == 7)
                        {
                            if (map[i, j + 1] == 3 || map[i, j + 1] == 4 || map[i, j + 1] == 7)
                            {
                                if (map[i, j - 1] == 3 || map[i, j - 1] == 4 || map[i, j - 1] == 7)
                                    flag = true;
                                else
                                    mapObjects[i, j] = GameObject.Instantiate(sprite[2], getPos(j, i), Quaternion.Euler(0, 0, 90.0f));
                            }
                            else if (map[i, j - 1] == 3 || map[i, j - 1] == 4 || map[i, j - 1] == 7)
                                mapObjects[i, j] = GameObject.Instantiate(sprite[2], getPos(j, i), Quaternion.Euler(0, 0, 180.0f));
                        }
                        if (flag)
                            if (map[i + 1, j] == 3 && map[i + 2, j] != 3 && map[i + 2, j] != 4 || map[i + 1, j] == 4 && (map[i + 2, j] == 3 || map[i + 2, j] == 4) || map[i + 1, j] == 7)
                            {
                                if (map[i, j + 1] == 3 && map[i, j + 2] != 3 && map[i, j + 2] != 4 || map[i, j + 1] == 4 && (map[i, j + 2] == 3 || map[i, j + 2] == 4) || map[i, j + 1] == 7)
                                    mapObjects[i, j] = GameObject.Instantiate(sprite[2], getPos(j, i), new Quaternion());
                                else if (map[i, j - 1] == 3 && map[i, j - 2] != 3 && map[i, j - 2] != 4 || map[i, j - 1] == 4 && (map[i, j - 2] == 3 || map[i, j - 2] == 4) || map[i, j - 1] == 7)
                                    mapObjects[i, j] = GameObject.Instantiate(sprite[2], getPos(j, i), Quaternion.Euler(0, 0, -90.0f));
                            }
                            else if (map[i - 1, j] == 3 && map[i - 2, j] != 3 && map[i - 2, j] != 4 || map[i - 1, j] == 4 && (map[i - 2, j] == 3 || map[i - 2, j] == 4) || map[i - 1, j] == 7)
                            {
                                if (map[i, j + 1] == 3 && map[i, j + 2] != 3 && map[i, j + 2] != 4 || map[i, j + 1] == 4 && (map[i, j + 2] == 3 || map[i, j + 2] == 4) || map[i, j + 1] == 7)
                                    mapObjects[i, j] = GameObject.Instantiate(sprite[2], getPos(j, i), Quaternion.Euler(0, 0, 90.0f));
                                else if (map[i, j - 1] == 3 && map[i, j - 2] != 3 && map[i, j - 2] != 4 || map[i, j - 1] == 4 && (map[i, j - 2] == 3 || map[i, j - 2] == 4) || map[i, j - 1] == 7)
                                    mapObjects[i, j] = GameObject.Instantiate(sprite[2], getPos(j, i), Quaternion.Euler(0, 0, 180.0f));
                            }
                        break;
                    case 4:
                        if ((map[i - 1, j] == 3 || map[i - 1, j] == 4 || map[i - 1, j] == 7) && (map[i + 1, j] == 3 || map[i + 1, j] == 4 || map[i + 1, j] == 7))
                            mapObjects[i, j] = GameObject.Instantiate(sprite[3], getPos(j, i), new Quaternion());
                        else
                            mapObjects[i, j] = GameObject.Instantiate(sprite[3], getPos(j, i), Quaternion.Euler(0, 0, 90.0f));
                        break;
                    case 5:
                        mapObjects[i, j] = GameObject.Instantiate(sprite[4], getPos(j, i), new Quaternion());
                        break;
                    case 6:
                        mapObjects[i, j] = GameObject.Instantiate(sprite[5], getPos(j, i), new Quaternion());
                        break;
                    case 7:
                        if (j > 0 && (map[i, j - 1] == 1 || map[i, j - 1] == 2 || map[i, j - 1] == 7) && j + 1 < col && (map[i, j + 1] == 1 || map[i, j + 1] == 2 || map[i, j + 1] == 7))
                        {
                            if (i + 1 < row && (map[i + 1, j] == 3 || map[i + 1, j] == 4))
                                mapObjects[i, j] = GameObject.Instantiate(sprite[6], getPos(j, i), new Quaternion());
                            else if (i > 0 && (map[i - 1, j] == 3 || map[i - 1, j] == 4))
                                mapObjects[i, j] = GameObject.Instantiate(sprite[6], getPos(j, i), Quaternion.Euler(0, 0, 180.0f));
                        }
                        else if (i > 0 && (map[i - 1, j] == 1 || map[i - 1, j] == 2 || map[i - 1, j] == 7) && i + 1 < row && (map[i + 1, j] == 1 || map[i + 1, j] == 2 || map[i + 1, j] == 7))
                        {
                            if (j > 0 && (map[i, j - 1] == 3 || map[i, j - 1] == 4))
                                mapObjects[i, j] = GameObject.Instantiate(sprite[6], getPos(j, i), Quaternion.Euler(0, 0, 90.0f));
                            else if (j + 1 < col && (map[i, j + 1] == 3 || map[i, j + 1] == 4))
                                mapObjects[i, j] = GameObject.Instantiate(sprite[6], getPos(j, i), Quaternion.Euler(0, 0, -90.0f));
                        }
                        break;
                }

        Camera.orthographicSize = row / 2 + 3;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static Vector3 getPos(int x, int y)
    {
        return new Vector3(x - col / 2, row / 2 - y, 0.0f);
    }

    public static int[] getMapPos(int x, int y)
    {
        int[] position = new int[2];
        position[0] = col / 2 + x;
        position[1] = row / 2 - y;
        return position;
    }

    public static bool ifAvailable(int x, int y)
    {
        if (x < 0 || x >= col || y < 0 || y >= row)
            return false;
        if (mapObjects[y, x] == null)
            return true;
        return mapObjects[y, x].tag != "Wall";
    }
    public static bool ifEating(int x, int y)
    {
        if (mapObjects[y, x] != null)
        {
            if (mapObjects[y, x].tag == "Pellet_normal")
            {
                UHDManager.score++;
                mapObjects[y, x].SetActive(false);
                mapObjects[y, x] = null;
                map[y, x] = 0;
                return true;
            }
            if (mapObjects[y, x].tag == "Pellet_power")
            {
                UHDManager.score += 10;
                mapObjects[y, x].SetActive(false);
                mapObjects[y, x] = null;
                map[y, x] = 0;
                return true;
            }
        }
        return false;
    }
}