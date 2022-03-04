using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class UtilityAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] MeterUI happyMeter;

    const float MIN_SCORE = 0.2f;

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

        if (activeUtilObj == null)
        {
            var gameObjects = perception.GetGameObjects();
            List<UtilityObject> utilObjects = new List<UtilityObject>();
            foreach (var gO in gameObjects)
            {
                if (gO.TryGetComponent(out UtilityObject uO))
                {
                    uO.Visible = true;
                    uO.Score = GetUtilObjectScore(uO);
                    //if (uO.Score > MIN_SCORE)
                    utilObjects.Add(uO);
                }
            }
            print(utilObjects.Count());

            activeUtilObj = GetHighestUtilityObject(utilObjects.ToArray());

            //set first active util object to first util object
            //activeUtilObj = (utilObjects.Count == 0) ? null : utilObjects[0];
            if (activeUtilObj != null && activeUtilObj.cooldownTimer <= 0)
            {
                StartCoroutine(ExecuteUtilObject(activeUtilObj));
            }
            else
            {
                activeUtilObj = null;
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
        print("dhsfajksgahg");
        if (uO.cooldownTimer <= 0)
        {
            //go to location
            movement.MoveTowards(uO.location.position);
            while (Vector3.Distance(transform.position, uO.location.position) > 1f)
            {
                Debug.DrawLine(transform.position, uO.location.position, Color.green);
                yield return null;
            }

            //start effect
            if (uO.effect != null) uO.effect.SetActive(true);

            //wait duration
            yield return new WaitForSecondsRealtime(uO.duration);

            //stop effect
            if (uO.effect != null) uO.effect.SetActive(false);

            ApplyUtilObject(uO);
            uO.cooldownTimer = 15;
        }
        print("ahhhhhhhhhhhh");
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
                need.input -= effector.change;
                need.input = Mathf.Clamp(need.input, -1, 1);
            }
        }
    }

    float GetUtilObjectScore(UtilityObject uO)
    {
        float score = 0;

        foreach (var effector in uO.effectors)
        {
            Need need = GetNeedByType(effector.type);
            if (need != null)
            {
                float futureNeed = need.GetMotive(need.input + effector.change);
                score += need.motive - futureNeed;
            }
        }

        return score;
    }

    UtilityObject GetHighestUtilityObject(UtilityObject[] utilityObjects)
    {
        UtilityObject highestUtilityObject = null;
        float highestScore = MIN_SCORE;

        if (utilityObjects.Length > 0) highestUtilityObject = utilityObjects[0];
        foreach (var uO in utilityObjects)
        {
            float score = uO.Score;
            // if score > highest score then set new highest score and highest utility object
            if (score > highestScore)
            {
                highestScore = score;
                highestUtilityObject = uO;
            }
        }

        print(highestUtilityObject);
        return highestUtilityObject;
    }

    UtilityObject GetRandomUtilityObject(UtilityObject[] utilityObjects)
    {
        // evaluate all utility objects
        float[] scores = new float[utilityObjects.Length];
        float totalScore = 0;
        for (int i = 0; i < utilityObjects.Length; i++)
        {
            float score = utilityObjects[i].Score;
            scores[i] = score;
            totalScore += score;
        }

        // select random utility object based on score
        // the higher the score the greater the chance of being randomly selected
        // <float random = value between 0 and totalScore>
        UtilityObject randomUO = new UtilityObject();
        float random = Random.Range(0, totalScore);
        randomUO.Score = random;

        for (int i = 0; i < scores.Length; i++)
        {
            // <check if random value is less than scores[i]>
            if (randomUO.Score < scores[i])
            {
                // <return utilityObjects[i] if less than>
                return utilityObjects[i];
            }

            // <subtract scores[i] from random value>
            random -= scores[i];
        }

        return null;
    }

    Need GetNeedByType(Need.Type type)
    {
        return needs.First(n => n.type == type);
    }
}
