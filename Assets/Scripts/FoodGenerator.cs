using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour
{

	[SerializeField] private GameObject[] foods;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Generate(int id, double place)
	{
		if (id >= 0 && id < foods.Length)
		{
			GameObject food = foods[id];
			
		}
	}
}
