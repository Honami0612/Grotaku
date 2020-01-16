using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Audiences : MonoBehaviour
{
    [SerializeField]
    Text GoSelectText;

    //[SerializeField]
    //Sprite[] audience_option;
    int number_audience = 0;
    private SpriteRenderer audience;

    [SerializeField]
    float swintimersum;
    [SerializeField]
    Text timerText;
    int now_time;

    private float swing_interval;
    private float timeElapsed;

    private Animator animator;

    private Animator green;
    private Animator yellow;

    // Start is called before the first frame update
    void Start()
    {
        audience = this.GetComponent<SpriteRenderer>();
        //audience.sprite = audience_option[0];
        GoSelectText.enabled = false;
        animator = GetComponent<Animator>();
        green = GameObject.Find("green").GetComponent<Animator>();
        yellow = GameObject.Find("yellow").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        swintimersum -= Time.deltaTime;
        now_time = (int)swintimersum;
        timerText.text = "time:" + now_time.ToString();
        if (swintimersum <= 0) timerText.text = "Finish" ;

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
                //audience.sprite = audience_option[1];
                number_audience = 1;
            }
            else if (number_audience == 1)
            {
                //audience.sprite = audience_option[0];
                number_audience = 0;
            }
        }
        
       
    }

    void Interval()
    {
        timeElapsed += Time.deltaTime;
        if (now_time > 55)
        {
            swing_interval = 1;
            animator.speed = 0.5f;
            green.speed = 0.5f;
            yellow.speed = 0.5f;

        }
        else if ((now_time <= 55) && (now_time > 40))
        {
            swing_interval = 0.5f;
            animator.speed = 1f;
            green.speed = 1f;
            yellow.speed = 1f;
        }
        else if ((now_time <= 40) && (now_time > 30))
        {
            swing_interval = 1f;
            animator.speed = 0.5f;
            green.speed = 0.5f;
            yellow.speed = 0.5f;
        }
        else if ((now_time <= 30) && (now_time > 0))
        {
            swing_interval = 0.5f;
            animator.speed = 1f;
            green.speed = 1f;
            yellow.speed = 1f;
        }



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
