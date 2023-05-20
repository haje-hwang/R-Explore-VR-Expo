using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyText : MonoBehaviour
{
    public Text valueText;
    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetText()
    {
        valueText.text = (slider.value * 100).ToString("0");
    }
}
