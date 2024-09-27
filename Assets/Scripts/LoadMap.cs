using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadMap : MonoBehaviour
{
    [SerializeField] private GameObject[] sprite;

    // Start is called before the first frame update
    void Start()
    {
        int[,] levelMap1 =
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
        int row = 15, col = 14;
        int i, j;
        int[,] levelMap = new int[2 * row - 1, 2 * col];
        for (i = 0; i < 2 * row - 1; i++)
            for (j = 0; j < 2 * col; j++)
                if (i < row)
                    if (j < col)
                        levelMap[i, j] = levelMap1[i, j];
                    else
                        levelMap[i, j] = levelMap[i, 2 * col - j - 1];
                else
                    if (j < col)
                        levelMap[i, j] = levelMap[2 * row - i - 2, j];
                    else
                        levelMap[i, j] = levelMap[2 * row - i - 2, 2 * col - j - 1];
        row += row - 1;
        col += col;
        for (i = 0; i < row; i++)
            for (j = 0; j < col; j++)
                switch (levelMap[i,j])
                {
                    case 0:
                        break;
                    case 1:
                        if (i + 1 < row && (levelMap[i + 1, j] == 1 || levelMap[i + 1, j] == 2))
                        {
                            if (j + 1 < col && (levelMap[i, j + 1] == 1 || levelMap[i, j + 1] == 2))
                                GameObject.Instantiate(sprite[0], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                            else if (j > 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                                GameObject.Instantiate(sprite[0], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                        }
                        else if (i > 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2))
                        {
                            if (j + 1 < col && (levelMap[i, j + 1] == 1 || levelMap[i, j + 1] == 2))
                                GameObject.Instantiate(sprite[0], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                            else if (j > 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                                GameObject.Instantiate(sprite[0], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                        }
                        break;
                    case 2:
                        if (i > 0 && (levelMap[i - 1,j] == 1 || levelMap[i - 1, j] == 2) && i + 1 < row && (levelMap[i + 1, j] == 1 || levelMap[i + 1, j] == 2))
                            GameObject.Instantiate(sprite[1], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                        else
                            GameObject.Instantiate(sprite[1], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                        break;
                    case 3:
                        bool flag = false;
                        if (levelMap[i + 1, j] == 3 || levelMap[i + 1, j] == 4 || levelMap[i + 1, j] == 7)
                        {
                            if (levelMap[i, j + 1] == 3 || levelMap[i, j + 1] == 4 || levelMap[i, j + 1] == 7)
                            {
                                if (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4 || levelMap[i - 1, j] == 7 || levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4 || levelMap[i, j - 1] == 7)
                                    flag = true;
                                else
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                            }
                            else if (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4 || levelMap[i, j - 1] == 7)
                            {
                                if (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4 || levelMap[i - 1, j] == 7)
                                    flag = true;
                                else
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                            }
                        }
                        else if (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4 || levelMap[i - 1, j] == 7)
                        {
                            if (levelMap[i, j + 1] == 3 || levelMap[i, j + 1] == 4 || levelMap[i, j + 1] == 7)
                            {
                                if (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4 || levelMap[i, j - 1] == 7)
                                    flag = true;
                                else
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                            }
                            else if (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4 || levelMap[i, j - 1] == 7)
                                GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                        }
                        if (flag)
                            if (levelMap[i + 1, j] == 3 && levelMap[i + 2, j] != 3 && levelMap[i + 2, j] != 4 || levelMap[i + 1, j] == 4 && (levelMap[i + 2, j] == 3 || levelMap[i + 2, j] == 4) || levelMap[i + 1, j] == 7)
                            {
                                if (levelMap[i, j + 1] == 3 && levelMap[i, j + 2] != 3 && levelMap[i, j + 2] != 4 || levelMap[i, j + 1] == 4 && (levelMap[i, j + 2] == 3 || levelMap[i, j + 2] == 4) || levelMap[i, j + 1] == 7)
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                                else if (levelMap[i, j - 1] == 3 && levelMap[i, j - 2] != 3 && levelMap[i, j - 2] != 4 || levelMap[i, j - 1] == 4 && (levelMap[i, j - 2] == 3 || levelMap[i, j - 2] == 4) || levelMap[i, j - 1] == 7)
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                            }
                            else if (levelMap[i - 1, j] == 3 && levelMap[i - 2, j] != 3 && levelMap[i - 2, j] != 4 || levelMap[i - 1, j] == 4 && (levelMap[i - 2, j] == 3 || levelMap[i - 2, j] == 4) || levelMap[i - 1, j] == 7)
                            {
                                if (levelMap[i, j + 1] == 3 && levelMap[i, j + 2] != 3 && levelMap[i, j + 2] != 4 || levelMap[i, j + 1] == 4 && (levelMap[i, j + 2] == 3 || levelMap[i, j + 2] == 4) || levelMap[i, j + 1] == 7)
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                                else if (levelMap[i, j - 1] == 3 && levelMap[i, j - 2] != 3 && levelMap[i, j - 2] != 4 || levelMap[i, j - 1] == 4 && (levelMap[i, j - 2] == 3 || levelMap[i, j - 2] == 4) || levelMap[i, j - 1] == 7)
                                    GameObject.Instantiate(sprite[2], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                            }
                        break;
                    case 4:
                        if ((levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4 || levelMap[i - 1, j] == 7) && (levelMap[i + 1, j] == 3 || levelMap[i + 1, j] == 4 || levelMap[i + 1, j] == 7))
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
                        if (j > 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2 || levelMap[i, j - 1] == 7) && j + 1 < col && (levelMap[i, j + 1] == 1 || levelMap[i, j + 1] == 2 || levelMap[i, j + 1] == 7))
                        {
                            if (i + 1 < row && (levelMap[i + 1, j] == 3 || levelMap[i + 1, j] == 4))
                                GameObject.Instantiate(sprite[6], new Vector3(j - col / 2, row / 2 - i, 0.0f), new Quaternion());
                            else if (i > 0 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4))
                                GameObject.Instantiate(sprite[6], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                        }
                        else if (i > 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2 || levelMap[i - 1, j] == 7) && i + 1 < row && (levelMap[i + 1, j] == 1 || levelMap[i + 1, j] == 2 || levelMap[i + 1, j] == 7))
                        {
                            if (j > 0 && (levelMap[i, j - 1] == 3 || levelMap[i, j - 1] == 4))
                                GameObject.Instantiate(sprite[6], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                            else if (j + 1 < col && (levelMap[i, j + 1] == 3 || levelMap[i, j + 1] == 4))
                                GameObject.Instantiate(sprite[6], new Vector3(j - col / 2, row / 2 - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                        }
                        break;
                }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
