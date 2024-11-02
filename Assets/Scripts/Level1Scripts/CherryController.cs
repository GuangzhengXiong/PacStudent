using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    [SerializeField] private Tweener tweener;
    [SerializeField] private GameObject cherry;
    [SerializeField] private Transform PacStudent;
    public float speed = 5.0f;
    private List<GameObject> cherries;
    private List<float> cherryX;
    private List<float> cherryY;

    // Start is called before the first frame update
    void Start()
    {
        cherries = new List<GameObject>();
        cherryX = new List<float>();
        cherryY = new List<float>();

        InvokeRepeating("creatCherry", 0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = cherries.Count - 1; i >= 0; i--)
        {
            if (cherries[i] == null)
            {
                cherries.RemoveAt(i);
                cherryX.RemoveAt(i);
                cherryY.RemoveAt(i);
                continue;
            }
            if (cherries[i].transform.position.x == cherryX[i] && cherries[i].transform.position.y == cherryY[i])
            {
                GameObject c = cherries[i];
                cherries.RemoveAt(i);
                cherryX.RemoveAt(i);
                cherryY.RemoveAt(i);
                Destroy(c);
            }
        }
    }


    private void creatCherry()
    {
        Vector3 screen = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        float x, y;
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0:
                x = screen.x - 1f;
                y = Random.Range(screen.y, -screen.y);
                break;
            case 1:
                x = -screen.x + 1f;
                y = Random.Range(screen.y, -screen.y);
                break;
            case 2:
                x = screen.y - 1f;
                y = Random.Range(screen.x, -screen.x);
                break;
            case 3:
                x = -screen.y + 1f;
                y = Random.Range(screen.x, -screen.x);
                break;
            default:
                x = 0f;
                y = 0f;
                break;
        }

        Vector3 startPos = new Vector3(x, y, 0f);
        Vector3 endPos = new Vector3(-x, -y, 0f);
        GameObject c = Instantiate(cherry, startPos, new Quaternion());
        tweener.AddTween(c.transform, endPos, Vector3.Distance(startPos, endPos) / speed);
        cherries.Add(c);
        cherryX.Add(-x);
        cherryY.Add(-y);
    }
}
