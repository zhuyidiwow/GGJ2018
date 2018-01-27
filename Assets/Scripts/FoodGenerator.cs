using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour {
	public GameObject[] foods;
	public float AmountPerSecond;
	public Transform FoodContainer;
	public Transform Wheel;
	
	public float initialSpeed;
	public float rotationSpeed;

	public float edge;
	public AnimationCurve PositionDistribution;
	
	
	//-----------TEST---------------
	
	// Update is called once per frame
	void Update () {
		if (Random.value/Time.deltaTime<AmountPerSecond)
		{
			Generate(Random.Range(0,foods.Length),
				transform.position+transform.up*PositionDistribution.Evaluate(Random.value)*edge*2+new Vector3(0,-edge,-0.1f));
				//transform.position+new Vector3(0,PositionDistribution.Evaluate(Random.value)*edge*2-edge,-0.1f));
			
		}
	}

	void Generate(int id, Vector3 place)
	{
		if (id >= 0 && id < foods.Length)
		{
			GameObject food = Instantiate(foods[id]);
			food.transform.position = place;
			food.transform.parent = FoodContainer;
			food.GetComponent<Food>().Initialize(Wheel,initialSpeed,rotationSpeed);
		}
	}
}
