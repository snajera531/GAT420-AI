using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class MeterUI : MonoBehaviour
{
    public Slider slider;
    public TMP_Text text;

    public Vector3 worldPosition
    {
        set
        {
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(value);
            GetComponent<RectTransform>().anchorMin = viewportPoint;
            GetComponent<RectTransform>().anchorMax = viewportPoint;
        }
    }
}
