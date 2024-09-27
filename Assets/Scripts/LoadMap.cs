using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadMap : MonoBehaviour
{
    [SerializeField] private GameObject[] item;

    // Start is called before the first frame update
    void Start()
    {
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
        int row = 15; int col = 14;
        int i, j;
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
                                GameObject.Instantiate(item[1], new Vector3(j - col, row - i, 0.0f), new Quaternion());
                            if (j > 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                                GameObject.Instantiate(item[1], new Vector3(j - col, row - i, 0.0f), Quaternion.Euler(0, 0, -90.0f));
                        }
                        if (i > 0 && (levelMap[i - 1, j] == 1 || levelMap[i - 1, j] == 2))
                        {
                            if (j + 1 < col && (levelMap[i, j + 1] == 1 || levelMap[i, j + 1] == 2))
                                GameObject.Instantiate(item[1], new Vector3(j - col, row - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                            if (j > 0 && (levelMap[i, j - 1] == 1 || levelMap[i, j - 1] == 2))
                                GameObject.Instantiate(item[1], new Vector3(j - col, row - i, 0.0f), Quaternion.Euler(0, 0, 180.0f));
                        }
                        break;
                    case 2:
                        if (i > 0 && (levelMap[i - 1,j] == 1 || levelMap[i - 1, j] == 2) && i + 1 < row && (levelMap[i + 1, j] == 1 || levelMap[i + 1, j] == 2))
                            GameObject.Instantiate(item[2], new Vector3(j - col, row - i, 0.0f), new Quaternion());
                        else
                            GameObject.Instantiate(item[2], new Vector3(j - col, row - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                        break;
                    case 3:
                        GameObject.Instantiate(item[3], new Vector3(j - col, row - i, 0.0f), new Quaternion());
                        break;
                    case 4:
                        if (i > 0 && (levelMap[i - 1, j] == 3 || levelMap[i - 1, j] == 4) && i + 1 < row && (levelMap[i + 1, j] == 3 || levelMap[i + 1, j] == 4))
                            GameObject.Instantiate(item[4], new Vector3(j - col, row - i, 0.0f), new Quaternion());
                        else
                            GameObject.Instantiate(item[4], new Vector3(j - col, row - i, 0.0f), Quaternion.Euler(0, 0, 90.0f));
                        break;
                    case 5:
                        GameObject.Instantiate(item[5], new Vector3(j - col, row - i, 0.0f), new Quaternion());
                        break;
                    case 6:
                        GameObject.Instantiate(item[6], new Vector3(j - col, row - i, 0.0f), new Quaternion());
                        break;
                    case 7:
                        break;
                }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
