using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CarrotSlot : MonoBehaviour {
    public float CarrotStayTime;
    public GameObject YellForShitSign;
    public GameObject DisappointSign;
    public GameObject HappySign;
    public Image Ring;
    public TextMeshProUGUI TmProText;
    
    [HideInInspector] public Food Carrot = null;
    
    private GameObject currentSign;
    private bool isGrown;
    private bool isGone;

    private Coroutine destroyCoroutine;
    private Coroutine showSignCoroutine;

    private void Start() {
        YellForShitSign.SetActive(false);
        DisappointSign.SetActive(false);
        HappySign.SetActive(false);
        Ring.gameObject.SetActive(false);
        TmProText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isGrown) return;
        
        if (other.gameObject.CompareTag("Food")) {
            Food food = other.GetComponent<Food>();
            if (food.IsInField) return;
            switch (food.FoodEnum) {
                case Food.EFood.CARROT:
                    if (Carrot == null) {
                        food.Stop();
                        food.transform.parent = transform;
                        food.transform.position = transform.position + Vector3.up * 0.2f;
                        food.transform.rotation = transform.rotation;
                        food.transform.Rotate(0f, 0f, 30f);
                        food.IsInField = true;
                        Carrot = food;
                        isGone = false;
                        showSignCoroutine = StartCoroutine(ShowSign(YellForShitSign));
                        destroyCoroutine = StartCoroutine(DestroyCoroutine());
                    }
                    break;
                case Food.EFood.SHIT:
                    if (Carrot != null && !isGone) {
                        isGrown = true;
                        Carrot.transform.localScale *= 1.5f;
                        Carrot.transform.position += Vector3.up * 0.09f;
                        if (destroyCoroutine != null) {
                            StopCoroutine(destroyCoroutine);
                            Ring.gameObject.SetActive(false);  
                            TmProText.gameObject.SetActive(false);
                        }
                        if (showSignCoroutine != null) StopCoroutine(showSignCoroutine);
                        currentSign.SetActive(false);
                        showSignCoroutine = StartCoroutine(ShowSign(HappySign));
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public void ResetSlot() {
        if (destroyCoroutine != null) {
            StopCoroutine(destroyCoroutine);
            Ring.gameObject.SetActive(false);
            TmProText.gameObject.SetActive(false);
        }
        if (showSignCoroutine != null) StopCoroutine(showSignCoroutine);
        YellForShitSign.SetActive(false);
        DisappointSign.SetActive(false);
        HappySign.SetActive(false);
        Carrot = null;
        isGone = false;
        isGrown = false;
    }
    
    public void ShrinkAllSign() {
        if (showSignCoroutine != null) StopCoroutine(showSignCoroutine);
        if (DisappointSign.activeSelf) StartCoroutine(ShrinkCoroutine(DisappointSign));
        if (YellForShitSign.activeSelf) StartCoroutine(ShrinkCoroutine(YellForShitSign));
        if (HappySign.activeSelf) StartCoroutine(ShrinkCoroutine(HappySign));
    }

    private IEnumerator DestroyCoroutine() {
        yield return new WaitForSeconds(0.6f);
        Ring.gameObject.SetActive(true);
        TmProText.gameObject.SetActive(true);
        float elapsedTime = 0.6f;
        while (elapsedTime < CarrotStayTime) {
            float percentage = elapsedTime / CarrotStayTime;
            Ring.fillAmount = 1f - percentage;
            Ring.color = Color.Lerp(Color.green, Color.red, percentage);
            // TmProText.text = (CarrotStayTime - elapsedTime).ToString().Substring(0, 4);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        Ring.gameObject.SetActive(false);
        TmProText.text = "";
        isGone = true;
        if (showSignCoroutine != null) StopCoroutine(showSignCoroutine);
        showSignCoroutine = StartCoroutine(ShowSign(DisappointSign));
        yield return new WaitForSeconds(1f);
        StartCoroutine(ShrinkCoroutine(DisappointSign));
        StartCoroutine(ShrinkCoroutine(Carrot.gameObject));
        yield return new WaitForSeconds(0.3f);
        Destroy(Carrot.gameObject);
        Carrot = null;
        
    }

    private IEnumerator ShowSign(GameObject Sign, float duration1 = 0.4f, float duration2 = 0.2f, float largeScale = 1.4f) {
        if (currentSign != null) {
            StartCoroutine(ShrinkCoroutine(currentSign));
            yield return new WaitForSeconds(0.3f);
        }
        
        Sign.SetActive(true);
        currentSign = Sign;
        Vector3 originalScale = Vector3.one * 2.5f;
        
        float elapsedTime = 0f;
        while (elapsedTime < duration1) {
            Sign.transform.localScale = Vector3.Lerp(Vector3.zero, largeScale * originalScale, elapsedTime / duration1);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < duration2) {
            Sign.transform.localScale = Vector3.Lerp(largeScale * originalScale, originalScale, elapsedTime / duration2);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        showSignCoroutine = null;
        if (Sign == HappySign) {
            transform.parent.GetComponent<Field>().GrowOne();
        }
    }

    private IEnumerator ShrinkCoroutine(GameObject O, float duration = 0.3f) {
        Vector3 originalScale = O.transform.localScale;
        float elapsedTime = 0f;
        while (elapsedTime < duration) {
            O.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (O != currentSign) currentSign = null;
    }
}