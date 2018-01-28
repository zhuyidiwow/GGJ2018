using UnityEngine;

public class FoodGenerator : MonoBehaviour {
    public GameObject[] foods;
    
    public float AmountPerSecond;
    public Transform FoodContainer;
    public Transform Player;

    public float InitialSpeed;
    public float RotationSpeed;

    public float Height;
    public AnimationCurve PositionDistribution;

    void Update() {
        if (Random.value / Time.deltaTime < AmountPerSecond) {
            Generate(Random.Range(0, foods.Length),
                transform.position + transform.up * PositionDistribution.Evaluate(Random.value) * Height * 2 + new Vector3(0, -Height, -0.1f));
        }
    }

    void Generate(int index, Vector3 place) {
        GameObject food = Instantiate(foods[index]);
        food.transform.position = place;
        food.transform.parent = FoodContainer;
        food.GetComponent<Food>().Initialize(Player, InitialSpeed, RotationSpeed);
        
    }
}