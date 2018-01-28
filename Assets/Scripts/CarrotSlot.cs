using System.Collections;
using UnityEngine;


public class CarrotSlot : MonoBehaviour {
    public float CarrotStayTime;
    public GameObject YellForShitSign;
    public GameObject DisappointSign;
    public GameObject HappySign;

    private GameObject currentSign;
    
    private Food carrot = null;
    private bool isGrown;

    private Coroutine destroyCoroutine;
    private Coroutine showSignCoroutine;

    private void Start() {
        YellForShitSign.SetActive(false);
        DisappointSign.SetActive(false);
        HappySign.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isGrown) return;
        
        if (other.gameObject.CompareTag("Food")) {
            Food food = other.GetComponent<Food>();
            if (food.IsInField) return;
            switch (food.FoodEnum) {
                case Food.EFood.CARROT:
                    if (carrot == null) {
                        food.transform.position = transform.position;
                        food.transform.rotation = transform.rotation;
                        food.IsInField = true;
                        carrot = food;
                        showSignCoroutine = StartCoroutine(ShowSign(YellForShitSign));
                        destroyCoroutine = StartCoroutine(DestroyCoroutine());
                    }
                    break;
                case Food.EFood.SHIT:
                    if (carrot != null) {
                        isGrown = true;
                        if (destroyCoroutine != null) StopCoroutine(destroyCoroutine);
                        if (showSignCoroutine != null) StopCoroutine(showSignCoroutine);
                        showSignCoroutine = StartCoroutine(ShowSign(HappySign));
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator DestroyCoroutine() {
        yield return new WaitForSeconds(CarrotStayTime);
        
        if (showSignCoroutine != null) StopCoroutine(showSignCoroutine);
        showSignCoroutine = StartCoroutine(ShowSign(DisappointSign));
        yield return new WaitForSeconds(0.6f);
        
        Destroy(carrot);
        carrot = null;
    }

    private IEnumerator ShowSign(GameObject Sign, float duration1 = 0.4f, float duration2 = 0.2f, float largeScale = 1.4f) {
        if (currentSign != null) {
            StartCoroutine(ShrinkSign(currentSign));
            yield return new WaitForSeconds(0.3f);
        }
        
        Sign.SetActive(true);
        currentSign = YellForShitSign;
        Vector3 originalScale = Sign.transform.localScale;
        
        float elapsedTime = 0f;
        while (elapsedTime < duration1) {
            transform.localScale = Vector3.Lerp(Vector3.zero, largeScale * originalScale, elapsedTime / duration1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration2) {
            transform.localScale = Vector3.Lerp(largeScale * originalScale, originalScale, elapsedTime / duration2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        showSignCoroutine = null;
        if (Sign == HappySign) {
            transform.parent.GetComponent<Field>().GrowOne();
        }
    }

    private IEnumerator ShrinkSign(GameObject Sign, float duration = 0.3f) {
        Vector3 originalScale = Sign.transform.localScale;
        float elapsedTime = 0f;
        while (elapsedTime < duration) {
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Sign.SetActive(false);
    }
}