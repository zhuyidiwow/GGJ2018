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
	
	
	private void OnTriggerEnter2D(Collider2D other) {
		
		if (other.gameObject.CompareTag("Food")) {
			Food incomingFood = other.GetComponent<Food>();
			
			if (incomingFood.GetID()==ID) {
				//anim.ResetTrigger();
				tendency += incomingFood.GetPlayerID();
				anim.SetTrigger("Nod");
			} else
			{
				tendency -= incomingFood.GetPlayerID();
				anim.SetTrigger("Shke");
			}
			incomingFood.Eat();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	
	
}
