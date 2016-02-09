using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TrackManager : MonoBehaviour {

	public Text timer;
    public GameObject Sphere;
	public GameObject Copter;
    public Vector3[] _Checkpoints;
    public static Queue<GameObject> checkpointsQueue = new Queue<GameObject>();
    public static Material _Normal;
    public static Material _Highlight;
    public Material normal, highlight;

	private float countDown, countUp;
	private static bool win;

    // Use this for initialization
    void Start () {
        checkpointsQueue = new Queue<GameObject>();
        _Checkpoints = new Vector3[2];
        _Checkpoints[0] = new Vector3(0, 0, 1);
        _Checkpoints[1] = new Vector3(0, 20, 20);
        
        foreach(Vector3 v in _Checkpoints)
        {
            GameObject checkpoint;
            checkpoint = Instantiate(Sphere, transform.position + v, Quaternion.identity) as GameObject;
            checkpointsQueue.Enqueue(checkpoint);
        }

        _Normal = Resources.Load("checkpoint", typeof(Material)) as Material;
        _Highlight = Resources.Load("next_checkpoint", typeof(Material)) as Material;
        checkpointsQueue.Peek().GetComponent<Renderer>().material = _Highlight;

		/*initialize timer*/
		countDown = 0.0f;
		countUp = 0.0f;
		win = false;
    }

	void Update(){
		
		if (countDown >= 0) {
			timer.text = ((int)countDown).ToString();
			countDown -= Time.deltaTime;
			return;
		}

		FlightController.started = true;

		if (!win) {
			timer.text = countUp.ToString ();
			countUp += Time.deltaTime;
			if (countUp - (float)Math.Truncate (countUp) >= 0.60)
				countUp = (float)Math.Truncate (countUp) + 1;
			countUp = (float)(Math.Truncate ((double)countUp * 100.0) / 100.0);
		}

	}

    public static void Proceed()
    {
        //checkpointsQueue.Dequeue().GetComponent<Renderer>().material = _Normal;
		//Destroy(checkpointsQueue.Dequeue()); // destroy current checkpoint
		checkpointsQueue.Dequeue ().SetActive (false);
        if (checkpointsQueue.Count != 0)
        {
            checkpointsQueue.Peek().GetComponent<Renderer>().material = _Highlight;
        }
        else
        {
            // Win
			win = true;
        }
    }
    
}
