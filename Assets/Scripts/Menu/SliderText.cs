using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderText : MonoBehaviour
{
    public void UpdateText(float value)
    {
        GetComponent<TextMeshProUGUI>().text = value.ToString();
    }
}
