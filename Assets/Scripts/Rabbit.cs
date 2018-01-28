using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {

    private Animator animator;
    private bool isHit;
    private bool isReady;
    private Vector3 originalScale;
    
    void Start() {
        animator = GetComponent<Animator>();
        originalScale = transform.localScale;
        StartCoroutine(PopCoroutine());
        
    }

    private IEnumerator PopCoroutine() {
        float elapsedTime = 0f;
        while (elapsedTime < 0.4f) {
            transform.localScale = Vector3.Lerp(Vector3.zero, 1.3f * originalScale, elapsedTime / 0.4f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0f;
        while (elapsedTime < 0.2f) {
            transform.localScale = Vector3.Lerp(1.3f * originalScale, originalScale, elapsedTime / 0.2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isReady = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (isHit || !isReady) return;
        
        if (other.gameObject.CompareTag("Food")) {
            Food incomingFood = other.GetComponent<Food>();

            switch (incomingFood.FoodEnum) {
                case Food.EFood.CARROT:
                    Love(incomingFood.GetPlayerNo());
                    break;
                case Food.EFood.SHIT:
                    Hate(incomingFood.GetPlayerNo());
                    break;
            }

            isHit = true;
            incomingFood.Eat();
        }
    }

    private void Love(int pNo) {
        RunTo(GameManager.Instance.GetPlayer(pNo));
    }

    private void Hate(int pNo) {
        pNo = pNo == 1 ? 2 : 1;
        RunTo(GameManager.Instance.GetPlayer(pNo));
    }

    private void RunTo(Player player) {
        player.GetScore();
        Vector3 destination = player.GetRabbitAreaCenter();
        destination += new Vector3(player.RabbitAreaSize.x * Random.Range(-0.5f, 0.5f), player.RabbitAreaSize.y * Random.Range(-0.5f, 0.5f));
        destination.z = transform.position.z;
        StartCoroutine(MoveCoroutine(destination));
    }

    private IEnumerator MoveCoroutine(Vector3 destination, float factor = 5f) {
        while (Vector3.Distance(transform.position, destination) > 0.01f) {
            transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * factor);
            yield return null;
        }
    }
}