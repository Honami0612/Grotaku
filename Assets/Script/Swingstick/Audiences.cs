using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audiences : MonoBehaviour
{
    [SerializeField]
    Text GoSelectText;

    [SerializeField]
    Sprite[] audience_option;
    int number_audience = 0;
    private SpriteRenderer audience;

    [SerializeField]
    float timersum;
    [SerializeField]
    Text timerText;
    int now_time;

    private float swing_interval;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        audience = this.GetComponent<SpriteRenderer>();
        audience.sprite = audience_option[0];
        GoSelectText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timersum -= Time.deltaTime;
        now_time = (int)timersum;
        timerText.text = "time:" + now_time.ToString();
        if (timersum <= 0) timerText.text = "Finish" ;

        Interval();

        if (now_time <= 0)
        {
            //GotoSelectScene
            timerText.enabled = false;
            GoSelectText.enabled = true;
            if (Input.GetKey(KeyCode.X))
            {
                SceneManager.LoadScene("SelectScene");
            }
          
        }
    }

    void Swing()
    {
        if (now_time > 0)
        {
            if (number_audience == 0)
            {
                audience.sprite = audience_option[1];
                number_audience = 1;
            }
            else if (number_audience == 1)
            {
                audience.sprite = audience_option[0];
                number_audience = 0;
            }
        }
        
       
    }

    void Interval()
    {
        timeElapsed += Time.deltaTime;
        if (now_time > 55) swing_interval = 1;
        else if ((now_time <= 55) && (now_time > 40)) swing_interval = 0.5f;
        else if ((now_time <= 40) && (now_time > 30)) swing_interval = 1f;
        else if ((now_time <= 30) && (now_time > 0)) swing_interval = 0.5f;



        if (timeElapsed >= swing_interval)
        {
            Swing();
            timeElapsed = 0.0f;
        }
    }

    public int Number
    {
        get { return number_audience; }
    }
}
