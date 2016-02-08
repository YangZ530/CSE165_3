using UnityEngine;
using System.Collections;

public class CheckpointManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        if(this.gameObject == TrackManager.checkpointsQueue.Peek())
        {
            TrackManager.Proceed();
        }
        
    }
}
