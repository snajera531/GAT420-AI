using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityAgent : Agent
{
    [SerializeField] MeterUI happyMeter;
    Need[] needs;

    public float happiness
    {
        get
        {
            float totalMotive = 0;
            foreach (var need in needs)
            {
                totalMotive += need.motive;
            }

            return totalMotive / needs.Length;
        }
    }

    void Start()
    {
        needs = GetComponentsInChildren<Need>();
        happyMeter.name = "HAPPINESS";
        happyMeter.text.text = "HAPPINESS";
    }

    private void Update()
    {
        //animator.SetFloat("Speed")
        happyMeter.slider.value = happiness;
        happyMeter.worldPosition = transform.position;
    }
}
