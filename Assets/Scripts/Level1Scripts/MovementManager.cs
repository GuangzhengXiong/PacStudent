using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private Transform[] Ghosts;
    private UnitInfo[] GhostsInfo;
    public float GhostSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GhostsInfo = new UnitInfo[4];

        Ghosts[0].position = new Vector3(-1, 1, 0);
        GhostsInfo[0] = new UnitInfo(-1, 1, false);

        Ghosts[1].position = new Vector3(0, 1, 0);
        GhostsInfo[1] = new UnitInfo(0, 1, false);

        Ghosts[2].position = new Vector3(-1, -1, 0);
        GhostsInfo[2] = new UnitInfo(-1, -1, false);

        Ghosts[3].position = new Vector3(0, -1, 0);
        GhostsInfo[3] = new UnitInfo(0, -1, false);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
