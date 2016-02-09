using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Debug.Log (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{

		if (!other.CompareTag("Player"))
        {
            return;
        }
        
		if(TrackManager.checkpointsQueue.Count != 0 && this.gameObject == TrackManager.checkpointsQueue.Peek())
		{
			TrackManager.Proceed();
		}	
	}
}
