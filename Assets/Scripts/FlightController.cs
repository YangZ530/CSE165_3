using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using MoveServerNS;
using System;

public class FlightController : MonoBehaviour {
	
	public MoveServer moveServer;
	[Range(1, 6)]
	public int controller;
	
	private int r;
	private int g;
	private int b;
	private int counter;
	private bool debug = false;
	private bool triggerHeld = false;
	private bool moveHeld = false;
	private Quaternion oldRotation;
	private Quaternion oldObjectRotation;
	private Vector3 oldPosition;
	private bool traditionalMode;
	public static bool started;

	void Start () {
		r = 0;
		g = 255;
		b = 0;
		counter = 0;
		traditionalMode = true;
		started = false;
	}

	void Update () {

		if (!started)
			return;

		MoveController move = moveServer.getController(controller-1);
		if(move != null) {
			if (move.btnOnPress(MoveButton.BTN_TRIANGLE))
			{
				print("Triangle");
				moveServer.Send_calibrateOrientation(move);
			}

	
			/**
			 *  Tester Code
			 */
			if(debug)
			{
				if(counter == 10)
				{
					Debug.Log(new Vector3(move.getPositionSmooth().x * 10, 
					                      move.getPositionSmooth().y * 10, 
					                      move.getPositionSmooth().z * 10));
					Debug.Log(move.getQuaternion());
					counter = 0;
				}
				else
					counter++;
				transform.rotation = move.getQuaternion();
				transform.position = new Vector3(move.getPositionSmooth().x * 100, 
				                                 move.getPositionSmooth().y * 100, 
				                                 move.getPositionSmooth().z * 100);
			}

			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			/*switch mode*/
			if (move.btnOnPress(MoveButton.BTN_CIRCLE))
			{
				traditionalMode = !traditionalMode;
			}

			/*change direction*/
			if (move.btnPressed(MoveButton.BTN_MOVE) && !traditionalMode){
				/*Record original quaternion and position*/
				if(!moveHeld)
				{
					oldRotation = move.getQuaternion();
					oldObjectRotation = transform.rotation;
					moveHeld = true;
				}
				/*Get and apply rotation*/
				Debug.Log("move pressed");
				Quaternion newRotation, deltaRotation;
				newRotation = move.getQuaternion();
				deltaRotation = Quaternion.Inverse(oldRotation) * newRotation;
				transform.rotation = oldObjectRotation * deltaRotation;
			}
			else
				moveHeld = false;


			if (move.triggerValue > 50)
			{
				/*Record original quaternion and position*/
				if(!triggerHeld)
				{
					oldRotation = move.getQuaternion();
					oldPosition = new Vector3(move.getPositionSmooth().x * 10, 
					                          move.getPositionSmooth().y * 10, 
					                          move.getPositionSmooth().z * 10);
					oldObjectRotation = transform.rotation;
					triggerHeld = true;
				}

				if(traditionalMode){
					/*the SLOWER mode*/
					Vector3 newPosition, deltaPosition;
					Quaternion newRotation, deltaRotation;

					/*Get delta 6 DOF*/
					newRotation = move.getQuaternion();
					newPosition = new Vector3(move.getPositionSmooth().x * 10, 
					                          move.getPositionSmooth().y * 10, 
					                          move.getPositionSmooth().z * 10);
					deltaRotation = Quaternion.Inverse(oldRotation) * newRotation;
					deltaRotation.x = 0.0f;
					deltaRotation.z = 0.0f;
					deltaPosition = newPosition - oldPosition;
					deltaPosition.z = -deltaPosition.z;
				
					/*Apply delta 6 DOF*/
					transform.rotation = oldObjectRotation * deltaRotation;
					deltaPosition = transform.rotation * deltaPosition;
					transform.position = transform.position + deltaPosition / 10;
				}
				else
				{
					/*the FASTER mode*/	
					Vector3 forward = new Vector3(0.0f, 0.0f, 0.01f * move.triggerValue);
					forward = transform.rotation * forward;
					transform.position = transform.position + forward;
				}
			}
			else
				triggerHeld = false;
		}
	}


}
