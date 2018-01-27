using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{

	private int tendency;
	[SerializeField] public int ID;

	[SerializeField] private Animation[] animations;

	private Animator anim;
	// Use this for initialization
	void Start ()
	{
		tendency = 0;
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.CompareTag("Food")) {
			Food incomingFood = other.GetComponent<Food>();
			incomingFood.Eat();
			if (incomingFood.GetID()==ID) {
				
			} else {
				
			}
		}
	}
	
	private void rightFood()
	{
		//anim. = animations[0];
	}

	private void wrongFood()
	{
		
	}
}
