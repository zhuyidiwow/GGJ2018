using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour {
    public Vector2 ScaleRange;
    public AudioClip[] CaughtClips;
    public AudioClip[] ComeOutClips;
    public AudioClip[] EatShitClips;
    public GameObject LoveEffect;
    public GameObject HateEffect;

    public Vector2 PositionRange_X;
    public Vector2 PositionRange_Z;

    private Animator animator;
    private bool isHit;
    private bool isReady;
    private Vector3 originalScale;
    private AudioSource audioSource;

    private Coroutine movingCoroutine;
    private bool isCaught;

    //----------------test----------------
    private Vector3 Destin;

    void Start() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        transform.localScale *= Random.Range(ScaleRange.x, ScaleRange.y);
        originalScale = transform.localScale;
        StartCoroutine(PopCoroutine());
        LoveEffect.SetActive(false);
        HateEffect.SetActive(false);
        Utilities.Audio.PlayAudio(audioSource, ComeOutClips[Random.Range(0, ComeOutClips.Length)]);
        movingCoroutine = null;
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

        if (other.gameObject.CompareTag("Food") || other.gameObject.CompareTag("Giant Carrot")) {
            Food incomingFood = other.GetComponent<Food>();

            switch (incomingFood.FoodEnum) {
                case Food.EFood.CARROT:
                    Love(incomingFood.GetPlayerNo());
                    break;
                case Food.EFood.GIANT_CARROT:
                    Love(incomingFood.GetPlayerNo(), false);
                    break;
                case Food.EFood.SHIT:
                    Hate(incomingFood.GetPlayerNo());
                    break;
            }

            isHit = true;
            incomingFood.Eat();
        }
    }

    private void Love(int pNo, bool useEffect = true) {
        LoveEffect.transform.parent = null;
        if (!useEffect) {
            if (Random.value < 0.3f) {
                LoveEffect.SetActive(true);
            }
        } else {
            LoveEffect.SetActive(true);
        }

        RunTo(GameManager.Instance.GetPlayer(pNo), useEffect);
        Utilities.Audio.PlayAudio(audioSource, CaughtClips[Random.Range(0, CaughtClips.Length)]);
        Destroy(LoveEffect, 3f);
    }

    private void Hate(int pNo) {
        pNo = pNo == 1 ? 2 : 1;
        RunTo(GameManager.Instance.GetPlayer(pNo));
        Utilities.Audio.PlayAudio(audioSource, EatShitClips[Random.Range(0, EatShitClips.Length)]);
        HateEffect.SetActive(true);
        Destroy(LoveEffect, 3f);
    }

    private void RunTo(Player player, bool useCoroutine = true) {
        Destroy(GetComponent<Collider2D>());
        isCaught = true;
        player.GetScore();
        player.AddRabit(this);
        Vector3 destination = player.GetRabbitAreaCenter();
        destination += new Vector3(player.RabbitAreaSize.x * Random.Range(-0.5f, 0.5f), player.RabbitAreaSize.y * Random.Range(-0.5f, 0.5f));
        destination.z = transform.position.z;
        if (movingCoroutine != null) {
            StopCoroutine(movingCoroutine);
            movingCoroutine = null;
        }

        if (useCoroutine) StartCoroutine(MoveCoroutine(transform.parent.gameObject, destination));
        else transform.parent.position = destination;

        transform.parent.parent = null;
    }

    private IEnumerator MoveCoroutine(GameObject go, Vector3 destination, float factor = 10f) {
        while (Vector3.Distance(go.transform.position, destination) > 0.01f) {
            go.transform.position = Vector3.Lerp(go.transform.position, destination, Time.deltaTime * factor);

            yield return null;
        }

        movingCoroutine = null;
    }

    private void randomMoving() {
        Destin = new Vector3(Random.Range(-2f, 2f), Random.Range(-4f, 4f), transform.position.y);
        transform.parent.localScale = new Vector3(transform.parent.localScale.x * Mathf.Sign(transform.parent.position.x - Destin.x),
            transform.parent.localScale.y,
            transform.parent.localScale.z);
        movingCoroutine = StartCoroutine(MoveCoroutine(transform.parent.gameObject, Destin,
            transform.localScale.x * 6f / Vector3.Distance(transform.position, Destin)));
    }

    private void Update() {
        if (isCaught) return;
        if (movingCoroutine == null && Random.value < 0.2 * Time.deltaTime) {
            randomMoving();
        }
    }
}