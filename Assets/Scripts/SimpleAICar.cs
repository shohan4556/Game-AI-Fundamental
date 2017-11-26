using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleAICar : MonoBehaviour {

	public Transform goal; /*consider this the eye of this NPC tank/car */
	public float rotSpeed = 2f;
	public float accleration = 1f;
	public float deaccleration = 1f;
	public float breakeAngle = 20f; /*used to deacclerate speed when come at corner */
	public float minSpeed = 1f;
	public float maxSpeed = 1f;
	[Space(15)]
	public float speed = 100f;
	public Text speedText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		Vector3 lookAtTarget = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
		//get direction 
		Vector3 direction = lookAtTarget - transform.position;
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime*rotSpeed);

		if(Vector3.Angle(goal.forward,transform.forward) > breakeAngle){
			speed = Mathf.Clamp(speed - (deaccleration*Time.deltaTime), minSpeed, maxSpeed);
			
			Debug.Log("deaccleration - Angle : "+Vector3.Angle(goal.forward, transform.forward));
		}
		else{
			speed = Mathf.Clamp(speed + (accleration*Time.deltaTime), minSpeed, maxSpeed);
		}
		
		transform.Translate(0,0,speed);

		AnalogueSpeedConverter.ShowSpeed(speed, minSpeed, maxSpeed);
		speedText.text = Mathf.RoundToInt(speed).ToString();
	}
}
