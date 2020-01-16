using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swingOtaku : MonoBehaviour
{
    private SpriteRenderer otaku;
   
    int number_otaku= 0;
    int number_audience;
    int before;
    Audiences audiences;

    private int sum_point = 0;
    [SerializeField]
    Text pointText;
    bool point = true;
   
    Animator nowAnimator;

  

    private TypefaceAnimator typefaceAnimator;

    [SerializeField]
    GameObject[] otakuLight;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        typefaceAnimator = GameObject.Find("Point").GetComponent<TypefaceAnimator>();
        typefaceAnimator.enabled = false;
        audiences = GameObject.Find("Audience").GetComponent<Audiences>();
        otaku = this.GetComponent<SpriteRenderer>();
        nowAnimator = GetComponent<Animator>();
        pointText.text = "Point:" + sum_point.ToString();
        otakuLight[1].SetActive(false);
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        GetPoint();
        if (Input.GetKeyDown(KeyCode.Return)) Swing();
        GetPoint();
        number_audience = audiences.Number;
        if (before != number_audience) point = true;
    }

    void Swing()
    {
        if ((number_otaku == 0))
        {

            otakuLight[0].SetActive(false);
            otakuLight[1].SetActive(true);
            number_otaku = 1;
        }
        else if ((number_otaku == 1))
        {
            otakuLight[0].SetActive(true);
            otakuLight[1].SetActive(false);
            number_otaku = 0;
        }
        
    }


    void GetPoint()
    {
        number_audience = audiences.Number;
        
        if (point)
        {
            if (number_otaku == number_audience)
            {
                typefaceAnimator.enabled = true;
                sum_point += 1;
                audioSource.Play();
                point = false;
                StartCoroutine("type");
            }
            before = number_audience;
        }
        
        pointText.text = "Point:" + sum_point.ToString();
       
    }

    IEnumerator type()
    {
        yield return new WaitForSeconds(0.25f);
        typefaceAnimator.enabled = false;
    }

}
