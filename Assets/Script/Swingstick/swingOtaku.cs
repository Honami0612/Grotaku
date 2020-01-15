using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swingOtaku : MonoBehaviour
{
    private SpriteRenderer otaku;
    [SerializeField]
    Sprite[] otaku_option;
    int number_otaku= 0;
    int number_audience;
    int before;
    Audiences audiences;

    private int sum_point = 0;
    [SerializeField]
    Text pointText;
    bool point = true;

   
   
    Animator nowAnimator;

    string state;

    // Start is called before the first frame update
    void Start()
    {
        audiences = GameObject.Find("Audience").GetComponent<Audiences>();
        otaku = this.GetComponent<SpriteRenderer>();
        otaku.sprite = otaku_option[0];
        nowAnimator = GetComponent<Animator>();
        state = "Green";
        pointText.text = "Point:" + sum_point.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        GetPoint();
        if (Input.GetKeyDown(KeyCode.S)) Swing();
        GetPoint();
        if (before != number_audience) point = true;
        ChangeAnimation();
    }

    void Swing()
    {
        if ((number_otaku == 0)||(state=="Green"))
        {
            state = "Blue";

            otaku.sprite = otaku_option[1];
            number_otaku = 1;
        }
        else if ((number_otaku == 1)||(state=="Blue"))
        {
            state = "Green";
            otaku.sprite = otaku_option[0];
            number_otaku = 0;
        }
        
    }


    void ChangeAnimation()
    {
        switch (state)
        {
            case "Blue":
                nowAnimator.SetBool("Blue", true);
                nowAnimator.SetBool("Green", false);
                break;
            case "Green":
                nowAnimator.SetBool("Blue", false);
                nowAnimator.SetBool("Green", true);
                break;

        }
    }
    void GetPoint()
    {
        number_audience = audiences.Number;
        
        if (point)
        {
            if (number_otaku == number_audience)
            {
                sum_point += 1;
                point = false;
            }
            before = number_audience;
        }
        
        pointText.text = "Point:" + sum_point.ToString();
       
    }

}
