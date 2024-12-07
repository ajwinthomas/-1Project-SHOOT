using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class counttimer : MonoBehaviour
{
    TextMeshProUGUI counter;
    public float time = 120;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        counter = GetComponent<TextMeshProUGUI>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
        }
        if (time <= 0)
        {
            time = 0;
        }

       
        counter.text = time.ToString();



    }
}
