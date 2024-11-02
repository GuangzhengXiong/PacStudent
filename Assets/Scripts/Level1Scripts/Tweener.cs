using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    //private Tween activeTween;
    private List<Tween> activeTweens = new List<Tween>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = activeTweens.Count - 1; i >= 0; i--)
        {
            if(activeTweens[i].Target == null)
            {
                activeTweens.RemoveAt(i);
                continue;
            }
            if (Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos) > 0.1f)
            {
                float x = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, x);
            }
            else
            {
                activeTweens[i].Target.position = activeTweens[i].EndPos;
                activeTweens.Remove(activeTweens[i]);
            }
        }
    }

    public bool AddTween(Transform targetObject, Vector3 endPos, float duration)
    {
        if (TweenExists(targetObject))
            return false;
        activeTweens.Add(new Tween(targetObject, targetObject.position, endPos, Time.time, duration));
        return true;
    }

    public bool TweenExists(Transform target)
    {
        for (int i = 0; i < activeTweens.Count; i++)
        {
            if (activeTweens[i].Target.transform == target)
                return true;
        }
        return false;
    }
}
