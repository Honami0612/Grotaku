using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Catch : MonoBehaviour
{
    //OtakuObject
    GameObject otaku;

    //PickObject
    [SerializeField]
    GameObject pick;
    AudioSource audioSourceSpawn;
    float speed;


    int spanTime;
    float countTime;

    private Camera mainCamera;
    private Vector2 destination;
    GameObject c;

    //Point
    [SerializeField]
    Text pointText;
    int point=0;
    [SerializeField]
    Text lastScoreText;

    //Timer
    [SerializeField]
    float sumTime;
    [SerializeField]
    Text timeText;
    int nowTime;
    [SerializeField]
    Text pushX;
    private TypefaceAnimator typefaceAnimator;

    //SpriteRote
    int key;
    Rigidbody2D rb;

    //Sound
    AudioSource audioSourceOtaku;
    [SerializeField]
    AudioClip[] getdestroy;

    //AnimationState
    string state;
    Animator characterAnimation;

    [SerializeField]
    GameObject[] particle;

    // Start is called before the first frame update
    void Start()
    {
        typefaceAnimator = GameObject.Find("Point").GetComponent<TypefaceAnimator>();
        typefaceAnimator.enabled = false;
        otaku = this.gameObject;
        spanTime = Random.Range(1, 3);
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        pointText.text = "Point: " + point.ToString();
        pushX.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        audioSourceOtaku = GetComponent<AudioSource>();
        audioSourceSpawn = GameObject.Find("c_idol1_0").GetComponent<AudioSource>();
        characterAnimation = GetComponent<Animator>();
        lastScoreText.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        sumTime -= Time.deltaTime;
        nowTime = (int)sumTime;
        timeText.text = "Time: " + nowTime.ToString();
        countTime += Time.deltaTime;

        if (c != null)
        {
            speed = Random.Range(0.8f, 3f);
            c.transform.position = Vector2.Lerp(c.transform.position, destination, Time.deltaTime * speed);
        }
        if (nowTime>0)
        {
            if (countTime >= spanTime) Pick();
            Move();
            transform.localScale = new Vector3(key * 0.65f, 0.75f, 0);
            ChangeAnimation();
        }
        else if(nowTime < 0)
        {
           
            lastScoreText.enabled = true;
            pointText.enabled = false;
            lastScoreText.text = "Point: " + point.ToString() + "!!!";
            timeText.enabled = false;
            pushX.enabled = true;
            if (Input.GetKey(KeyCode.X)) SceneManager.LoadScene("SelectScene");
        }
    }

    void Move()
    { 

        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            key = 1;
            otaku.transform.position = new Vector3(otaku.transform.position.x + 0.1f, otaku.transform.position.y, 0);
            state = "Walk";
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
           
            key = -1;
            otaku.transform.position = new Vector3(otaku.transform.position.x - 0.1f, otaku.transform.position.y, 0);
            state = "Walk";
        }
        else
        {
            
            state = "Stay";
        }
        Debug.Log(state);
    }

    void Pick()
    {
        audioSourceSpawn.Play();
        c =Instantiate(pick);
        destination = mainCamera.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width),-6));
        spanTime = Random.Range(1, 4);
        countTime = 0;
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int parNumber;
        if (collision.gameObject.tag == "pick")
        {
            parNumber = Random.Range(0, particle.Length);
            typefaceAnimator.enabled = true;
            Destroy(collision.gameObject);
            point += 20;
            sumTime += 3f;
            pointText.text = "Point: " + point.ToString();
            audioSourceOtaku.PlayOneShot(getdestroy[0]);
            StartCoroutine("Typeface");
            Instantiate(particle[parNumber], GetRandomPosition(), Quaternion.identity);
        }

       
    }


    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(0.0f, 1.0f);
        float y = Random.Range(0.0f, 1.0f);
        Vector3 randViewPoint = new Vector3(x, y, 0.0f);
        Vector3 position = Camera.main.ViewportToWorldPoint(randViewPoint);
        position.z = 0.0f;
        return position;
    }

    void ChangeAnimation()
    {
        switch (state)
        {
            case "Walk":
                characterAnimation.SetBool("Walk", true);
                characterAnimation.SetBool("Stay", false);
                break;
            case "Stay":
                characterAnimation.SetBool("Walk", false);
                characterAnimation.SetBool("Stay", true);
                break;

        }
    }
   

    IEnumerator Typeface()
    {
        yield return new WaitForSeconds(0.25f);
        typefaceAnimator.enabled = false;
    }
}
