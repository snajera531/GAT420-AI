using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityObject : MonoBehaviour
{
    [System.Serializable]
    public class Effector
    {
        public Need.Type type;
        [Range(-2, 2)] public float change;
    }

    public Dictionary<Need.Type, float> registry = new Dictionary<Need.Type, float>();
    public Effector[] effectors;
    public float cooldownTimer;
    public float duration;
    public GameObject effect;
    public Transform location;

    [SerializeField] MeterUI meterPrefab;
    MeterUI meter;

    public bool Visible { get; set; }
    public float Score { get; set; }

    void Start()
    {
        meter = Instantiate(meterPrefab, GameObject.Find("Canvas").transform);
        meter.name = name;
        meter.text.text = name;

        foreach(var effector in effectors)
        {
            registry[effector.type] = effector.change;
        }
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        meter.gameObject.SetActive(Visible);
        meter.worldPosition = transform.position + Vector3.up * 5;
        meter.slider.value = Score;
        Visible = false;
    }

    public float GetEffectorChange(Need.Type type)
    {
        registry.TryGetValue(type, out float change);

        return change;
    }

    public bool HasEffector(Need.Type type)
    {
        return registry.ContainsKey(type);
    }
}
