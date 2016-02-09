using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public GameObject arrow;
	private Vector3 originalOffset;
	private Vector3 arrowOffset;
	private Quaternion arrowOriginalRotation;
	
	void Start () {
		originalOffset = transform.position - player.transform.position;
		arrowOffset = transform.position - arrow.transform.position;
		arrowOriginalRotation = arrow.transform.rotation;
	}

	void Update () {
		Vector3 offset = player.transform.rotation * originalOffset;
		transform.rotation = player.transform.rotation;
		transform.position = player.transform.position + offset;

		if (TrackManager.checkpointsQueue.Count != 0) {
			Vector3 direction = TrackManager.checkpointsQueue.Peek ().transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation(direction.normalized);		
			Vector3 rotatedOffset;
			arrow.transform.rotation = rotation * arrowOriginalRotation;
			rotatedOffset = player.transform.rotation * arrowOffset;
			arrow.transform.position = transform.position - rotatedOffset;
		}
	}
}
