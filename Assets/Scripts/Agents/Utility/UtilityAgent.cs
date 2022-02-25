using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class UtilityAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] MeterUI happyMeter;

    const float MIN_SCORE = 0.1f;

    Need[] needs;
    UtilityObject activeUtilObj;

    public bool UsingUtilObject { get { return activeUtilObj != null; } }

    public float Happiness
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
        animator.SetFloat("Speed", movement.Velocity.magnitude);

        if(activeUtilObj == null)
        {
            var gameObjects = perception.GetGameObjects();
            List<UtilityObject> utilObjects = new List<UtilityObject>();
            foreach (var gO in gameObjects)
            {
                if(gO.TryGetComponent<UtilityObject>(out UtilityObject uO))
                {
                    uO.Visible = true;
                    uO.Score = GetUtilObjectScore(uO);
                    if(uO.Score > MIN_SCORE) utilObjects.Add(uO);
                }
            }

            //set first active util object to first util object
            activeUtilObj = (utilObjects.Count == 0) ? null : utilObjects[0];
            if(activeUtilObj != null)
            {
                StartCoroutine(ExecuteUtilObject(activeUtilObj));
            }
        }
    }

    private void LateUpdate()
    {
        happyMeter.slider.value = Happiness;
        happyMeter.worldPosition = transform.position + Vector3.up * 4;
    }

    IEnumerator ExecuteUtilObject(UtilityObject uO)
    {
        //go to location
        movement.MoveTowards(uO.location.position);
        while (Vector3.Distance(transform.position, uO.location.position) > 0.25)
        {
            Debug.DrawLine(transform.position, uO.location.position, Color.magenta);
            yield return null;
        }

        print("start");

        //start effect
        if (uO.effect != null) uO.effect.SetActive(true);

        //wait duration
        yield return new WaitForSeconds(uO.duration);
        print("stop");

        //stop effect
        if (uO.effect != null) uO.effect.SetActive(false);

        ApplyUtilObject(uO);

        activeUtilObj = null;
        yield return null;
    }

    void ApplyUtilObject(UtilityObject uO)
    {
        foreach (var effector in uO.effectors)
        {
            Need need = GetNeedByType(effector.type);
            if (need != null)
            {
                need.input = effector.change;
                need.input = Mathf.Clamp(need.input, -1, 1);
            }
        }
    }

    float GetUtilObjectScore(UtilityObject uO)
    {
        float score = 0;

        foreach(var effector in uO.effectors)
        {
            Need need = GetNeedByType(effector.type);
            if(need != null)
            {
                float futureNeed = need.GetMotive(need.input + effector.change);
                score += need.motive - futureNeed;
            }
        }

        return score;
    }

    Need GetNeedByType(Need.Type type)
    {
        return needs.First(n => n.type == type);
    }
}
