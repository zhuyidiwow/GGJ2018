using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

	public int id;
	public float error;
	public Transform destination;

	public float movingSpeed;

	public float rotatingSpeed;

	public int state;
	
	//-------------TEST-------------

	public Vector3 trans;
	// Use this for initialization
	void Start ()
	{
		//state = 0;
	}
	
	// Update is called once per frame
	void Update()
	{
		switch (state){
			case 1:
			{
				if (Vector3.Distance(transform.position, destination.position) < error)
				{
					state = 2;
					break;
				}
				else
				{
					trans = (destination.position - transform.position).normalized * movingSpeed * Time.deltaTime;
					transform.Translate(trans,Space.World);
					transform.Rotate(transform.forward,rotatingSpeed,Space.World);
				}
				break;
			}
			case 2:
			{
				Destroy(gameObject);
				break;
			}
			
		}
	}

	public void Initialize(Transform des,float mSpeed,float rSpeed)
	{
		destination = des;
		movingSpeed = mSpeed;
		rotatingSpeed = rSpeed;
		state = 1;
	}
}
