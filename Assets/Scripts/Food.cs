using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public enum EFood {
        SHIT,
        CARROT,
        GIANT_CARROT
    }

    public EFood FoodEnum;
    
    public float MoveSpeed;
    public float RotateSpeed;
    public float ShootSpeed;

    public bool IsInField;
    // 0: start
    // 1: fly to player
    // 2: caught by player
    // 3: fly away / drop
    private int state;
    
    private int playerNo;
    private Transform destination;
    private Vector3 movingDirection;

    void Update() {
        switch (state) {
            case 1:
                // fly to player
                movingDirection = (destination.position - transform.position).normalized * MoveSpeed * Time.deltaTime;
                transform.Translate(movingDirection, Space.World);
                transform.Rotate(transform.forward, RotateSpeed, Space.World);
                break;
            case 2: 
                break;
            case 3: 
                // fly away
                movingDirection = movingDirection.normalized * ShootSpeed * Time.deltaTime;
                transform.Translate(movingDirection, Space.World);
                transform.Rotate(transform.forward, RotateSpeed, Space.World);
                break;
        }
    }

    public void Initialize(Transform des, float mSpeed, float rSpeed) {
        destination = des;
        MoveSpeed = mSpeed;
        RotateSpeed = rSpeed;
        state = 1;
    }

    public void Shoot(Vector3 direction, int pNo) {
        movingDirection = direction;
        playerNo = pNo;
        state = 3;
    }

    public void MoveToSlot(Slot slot,int pNo) {
        state = 2;
        playerNo = pNo;
        transform.parent = slot.transform;
        transform.position = slot.GetObjectSlotTransform().position;
    }

    public void Drop(int pNo) {
        if (!GetShotState() && !IsCaught()) {
            movingDirection = -1 * movingDirection;
            state = 3;
            playerNo = pNo;
        }
    }

    public void Stop() {
        state = 2;
    }

    public void Eat() {
        if (FoodEnum != EFood.GIANT_CARROT) Destroy(gameObject);
    }

    public bool IsCaught() {
        return state == 2;
    }

    public bool GetShotState() {
        return state > 2;
    }

    public int GetPlayerNo() {
        return playerNo;
    }
}