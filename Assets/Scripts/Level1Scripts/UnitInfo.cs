using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class UnitInfo
{
    public int x;
    public int y;
    public int direction;
    public int status;

    public UnitInfo(int X = 0, int Y = 0, bool isMapPos = true)
    {
        if(isMapPos)
        {
            x = X;
            y = Y;
        }
        else
        {
            int[] mapPos = LoadMap.getMapPos(X, Y);
            x = mapPos[0];
            y = mapPos[1];
        }
        direction = 0;
        status = 0;
    }
}
