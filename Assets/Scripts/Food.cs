using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

	[SerializeField] private int id;
	[SerializeField] private float error;
	private Transform destination;

	[SerializeField] private float movingSpeed;

	[SerializeField] private float flyingSpeed;

	[SerializeField] private float rotatingSpeed;

	private int state;

	
//	0: start
//	1: moving to roller
//	2: stay in roller
//	3: fly away
//	4: be ate
//	5: drop
	private Vector3 movingDirection;
	
	//-------------TEST-------------

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
//				if (Vector3.Distance(transform.position, destination.position) < error)
//				{
//					state = 2;
//					break;
//				}
//				else
//				{
					movingDirection = (destination.position - transform.position).normalized * movingSpeed * Time.deltaTime;
					transform.Translate(movingDirection,Space.World);
					transform.Rotate(transform.forward,rotatingSpeed,Space.World);
//				}
				break;
			}
			case 2:
			{
				// Destroy(gameObject);
				break;
			}
			case 3:
			{
				movingDirection = movingDirection.normalized * movingSpeed * Time.deltaTime;
				transform.Translate(movingDirection,Space.World);
				transform.Rotate(transform.forward,rotatingSpeed,Space.World);
				break;
			}
		}
	}

	public int GetID()
	{
		return id;
	}
	
	public void Initialize(Transform des,float mSpeed,float rSpeed)
	{
		destination = des;
		movingSpeed = mSpeed;
		rotatingSpeed = rSpeed;
		state = 1;
	}

	public void Shoot(Vector3 direction)
	{
		movingDirection = direction;
		state = 3;
	}

	public void MoveToSlot(Slot slot)
	{
		state = 2;
		transform.parent = slot.transform;
		transform.position = slot.GetObjectSlotTransform().position;
	}

	public void Drop()
	{
		if (!GetShotState() && !IsCaught()) {
			movingDirection = -1 * movingDirection;
			state = 3;
		}
	}

	public void Eat()
	{
		
	}

	public bool IsCaught() {
		return state == 2;
	}
	
	public bool GetShotState()
	{
		return state > 2;
	}

	public int GetPlayerID()
	{
		return 1;
	}

	
}
