using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Countdown : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI countdownText;
    [SerializeField]public float counttime;
    public float div=1;

   
    void Update()
    {
        if (counttime > 0)
        {
            counttime -= Time.deltaTime/div;
        }
        else if (counttime<0)
        {
            counttime = 0;
            countdownText.color= Color.red;
        }
        
        int minutes = Mathf.FloorToInt(counttime/60);
        int seconds = Mathf.FloorToInt(counttime%60);
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
