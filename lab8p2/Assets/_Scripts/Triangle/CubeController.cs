using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Triangle
{
public class CubeController : MonoBehaviour
{
    private CubeController target;

    public CubeState ToRecord ()
    {
        if (target != null)
            return new CubeState(transform.position, transform.rotation, name, target.name);
        else 
            return new CubeState(transform.position, transform.rotation, name, "NULL");
    }
    public static GameObject FromRecord(CubeState state)
    {
        GameObject obj = GameObject.Instantiate(PrefabProvider.CubePrefab, state.position, state.rotation);
        obj.name = state.name;
        return obj;
    }

    public void Link(Dictionary<string, GameObject> objects, Dictionary<string, string> links)
    {
        string targetName;
        links.TryGetValue(name, out targetName);
        GameObject obj;
        objects.TryGetValue(targetName, out obj);
        if (obj != null)
            target = obj.GetComponent<CubeController>();
    }

    void Awake ()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        bool takeFirst = UnityEngine.Random.Range(0, 2) == 1;
        bool first     = true;
        CubeController[] cubes = FindObjectsOfType<CubeController>();
        for (int i = 0; i < cubes.Length; i++)
        {
            if (cubes[i] != this)
            {
                if (first)
                {
                    if (takeFirst)
                        target = cubes[i];
                    first = false;
                }
                else if(!takeFirst) 
                {
                    target = cubes[i];
                }
            }
        }
    }

    void OnCollisionEnter(Collision c)
    {
        CubeController other = c.collider.GetComponent<CubeController>();
        if (other == target && other.target == this)
        {
            Destroy(gameObject);
        }
    }

    void Update ()
    {
        if (target != null)
        {
            Vector3 direction   = (target.transform.position - transform.position).normalized;
            transform.position += direction*Time.deltaTime;
        }
        else
        {
            transform.RotateAround(transform.position, new Vector3(1,1,1), 100 * Time.deltaTime);
        }
    }
}

[Serializable]
public struct CubeState 
{
    public Vector3    position;
    public Quaternion rotation;
    public string name;
    public string targetName;

    public CubeState(Vector3 position, Quaternion rotation, string name, string targetName)
    { 
        this.position      = position;
        this.rotation      = rotation;
        this.name          = name;
        this.targetName    = targetName;
    }
}
}