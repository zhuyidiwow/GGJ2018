using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour {
    public int tendency;
    public int tend_last;
    [SerializeField] public int ID;

    [SerializeField] private Animation[] animations;

    private Animator anim;

    private Coroutine moveCoroutine;
    
    // Use this for initialization
    void Start() {
        tendency = 0;
        tend_last = 0;
        anim = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Food")) {
            Food incomingFood = other.GetComponent<Food>();

            if (incomingFood.GetID() == ID) {
                //anim.ResetTrigger();
                tendency += incomingFood.GetPlayerID() == 2 ? 1 : -1;
                anim.SetTrigger("Nod");
                
            } else {
                tendency -= incomingFood.GetPlayerID() == 2 ? 1 : -1;
                anim.SetTrigger("Shake");
            }

            incomingFood.Eat();
        }
    }

    // Update is called once per frame
    void Update() {
        if (tendency != tend_last && anim.GetCurrentAnimatorStateInfo(0).IsName("Quiet")) {
            
            transform.parent.Translate(new Vector3(0.3f,0,0)*(tendency-tend_last));
            tend_last = tendency;
        }
    }

    public void UpdateTendency() {
        if (tendency!=tend_last) {
            
            transform.parent.Translate(new Vector3(0.3f,0,0)*(tendency-tend_last));
            tend_last = tendency;
        }
    }
}