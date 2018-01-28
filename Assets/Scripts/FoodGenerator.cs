using UnityEngine;

public class FoodGenerator : MonoBehaviour {
    public GameObject Carrot;
    public GameObject Shit;
    
    public float AmountPerSecond;
    
    public Vector3 LeftCrowd;
    public Vector3 RightCrowd;

    public float InitialSpeed;
    public float RotationSpeed;

    public float Height;
    public AnimationCurve PositionDistribution;

    void Update() {
        if (Random.value / Time.deltaTime < AmountPerSecond) {
            Vector3 offset = transform.up * PositionDistribution.Evaluate(Random.value) * Height * 2 + new Vector3(0, -Height, -0.1f);
            Generate(offset);
        }
    }

    void Generate(Vector3 offset) {
        GameObject food1;
        GameObject food2;
        
        if (Random.value < 0.8f) {
            food1 = Instantiate(Carrot, LeftCrowd + offset, Quaternion.identity, transform);
            food2 = Instantiate(Carrot, RightCrowd + offset, Quaternion.identity, transform);
        } else {
            food1 = Instantiate(Shit, LeftCrowd + offset, Quaternion.identity, transform);
            food2 = Instantiate(Shit, RightCrowd + offset, Quaternion.identity, transform);
        }

        if (GameManager.Instance != null) {
            food1.GetComponent<Food>().Initialize(GameManager.Instance.P1.transform, InitialSpeed, RotationSpeed);
            food2.GetComponent<Food>().Initialize(GameManager.Instance.P2.transform, InitialSpeed, RotationSpeed);
        } else {
            food1.GetComponent<Food>().Initialize(MenuManager.Instance.P1.transform, InitialSpeed, RotationSpeed);
            food2.GetComponent<Food>().Initialize(MenuManager.Instance.P2.transform, InitialSpeed, RotationSpeed);
        }

    }
}