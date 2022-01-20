using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPoint;
    [SerializeField] Vector3 movementVector;
    //[SerializeField] [Range(0,1)] float movementFactor; creates a range from 0 to 1. Not needed
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; } //smallest float rather than using 0. Always compare to this rather than 0
        float cycles = Time.time / period; //time elapsed grows over time, divided by the period we define is 1 cycle

        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //gives value between -1 and 1

        movementFactor = (rawSinWave + 1f) / 2f; //makes value output 0 through 2, divided by 2, so 0 and 1 instead of -1 and 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPoint + offset;
    }
}
