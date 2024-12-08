using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PercentageSlider : MonoBehaviour
{
    [SerializeField] Slider volSlider;
    [SerializeField] TextMeshProUGUI percentageText;

    public void SetPercentage() {
        percentageText.text = (volSlider.value * 100).ToString("F0") + "%";
    }
}
