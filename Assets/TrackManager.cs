using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour {

    public GameObject Sphere;
    public Vector3[] _Checkpoints;
    public static Queue<GameObject> checkpointsQueue = new Queue<GameObject>();
    public static Material _Normal;
    public static Material _Highlight;
    public Material normal, highlight;

    // Use this for initialization
    void Start () {
        checkpointsQueue = new Queue<GameObject>();
        _Checkpoints = new Vector3[2];
        _Checkpoints[0] = new Vector3(0, 0, 0);
        _Checkpoints[1] = new Vector3(0, 1, 1);
        
        foreach(Vector3 v in _Checkpoints)
        {
            GameObject checkpoint;
            checkpoint = Instantiate(Sphere, transform.position + v, Quaternion.identity) as GameObject;
            checkpointsQueue.Enqueue(checkpoint);
        }

        _Normal = Resources.Load("checkpoint", typeof(Material)) as Material;
        _Highlight = Resources.Load("next_checkpoint", typeof(Material)) as Material;
        //Debug.Log(checkpointsQueue.Peek());
        checkpointsQueue.Peek().GetComponent<Renderer>().material = _Highlight;
    }
    
    public static void Proceed()
    {
        checkpointsQueue.Dequeue().GetComponent<Renderer>().material = _Normal;
        if (checkpointsQueue.Count != 0)
        {
            checkpointsQueue.Peek().GetComponent<Renderer>().material = _Highlight;
        }
        else
        {
            // Win
        }
    }
    
}
