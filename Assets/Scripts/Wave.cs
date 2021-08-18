using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float moveSpeed = 0.05f;
    public float InvokeRate = 0.01f;
    public float AppearToHeight = -3f;
    [Range(-5,5)]
    public float XSpeed = 0f;
    public bool isWaving = false;
    public float WavingFreq = 5f;
    public float WaingAmp = 2f;
    private Animator Animator;
    private Rigidbody rb;
    private float original_y;
    private float currentTime = 0;
    private ObjectPool objectPool;
    private enum MoveState
    {
        up,
        down,
        straight
    }
    private MoveState currentState = 0;
    void Start()
    {        
        rb = GetComponent<Rigidbody>();
        objectPool = FindObjectOfType<ObjectPool>();
    }
    void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (transform.position.y < -10)
        {
            objectPool.Enduse(gameObject);
            currentState = MoveState.up;
        }

        if (currentState == MoveState.down)//Down
                rb.velocity = new Vector3(0, -moveSpeed, 0);            

        if (currentState == MoveState.up)//Up
                if (transform.position.y < AppearToHeight)
                    rb.velocity = new Vector3(0, moveSpeed, 0);
                else
                {
                    currentState = MoveState.straight;
                    currentTime = Time.time;
                    original_y = transform.position.y;
                }
                    
        if (currentState == MoveState.straight)//Straight
            if (transform.position.z > -20)
            {
                rb.velocity = new Vector3(0, 0, -moveSpeed);
                if (isWaving)
                    transform.position = new Vector3(transform.position.x, original_y + Mathf.Sin((Time.time - currentTime) * WavingFreq) * WaingAmp, transform.position.z);
                Shift();
            }
            else
                currentState = MoveState.down;
    }
    void Shift()
    {
        rb.velocity = new Vector3(XSpeed, rb.velocity.y, rb.velocity.z);
    }
}