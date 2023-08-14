using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Slider bar;

    public void Set(int curr, int max)
    {
        bar.maxValue = max;
        bar.value = curr;

        text.text = max.ToString() + "/" + curr.ToString();
    }
}
