using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedBorder : MonoBehaviour
{
    public GameObject dot;
    private string[] tags = { "Dot1", "Dot2", "Dot3", "Dot4" };
    private int tagIndex = 0;
    private GameObject[] dots;
    public float switchInterval = 0.5f;
    public float x = 10.0f;
    public float y = 6.0f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject d;
        float i;
        int n = 0;
        dots = new GameObject[(int)(4 * (x + y) / 0.25f)];
        for (i = -x; i < x; i += 0.25f)
        {
            d = GameObject.Instantiate(dot, new Vector3(i, y, 0.0f), new Quaternion());
            d.tag = tags[n % tags.Length];
            dots[n++] = d;
        }
        for (i = y; i > -y; i -= 0.25f)
        {
            d = GameObject.Instantiate(dot, new Vector3(x, i, 0.0f), new Quaternion());
            d.tag = tags[n % tags.Length];
            dots[n++] = d;
        }
        for (i = x; i > -x; i -= 0.25f)
        {
            d = GameObject.Instantiate(dot, new Vector3(i, -y, 0.0f), new Quaternion());
            d.tag = tags[n % tags.Length];
            dots[n++] = d;
        }
        for (i = -y; i < y; i += 0.25f)
        {
            d = GameObject.Instantiate(dot, new Vector3(-x, i, 0.0f), new Quaternion());
            d.tag = tags[n % tags.Length];
            dots[n++] = d;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= switchInterval)
        {
            tagIndex = (tagIndex + 1) % tags.Length;
            timer -= switchInterval;
            foreach (GameObject d in dots)
            {
                d.SetActive(d.tag == tags[tagIndex]);
            }
        }
    }
}
