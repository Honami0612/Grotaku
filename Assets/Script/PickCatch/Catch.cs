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

    int spanTime;
    float countTime;

    private Camera mainCamera;
    private Vector2 destination;

   
    //Point
    [SerializeField]
    Text pointText;
    int point=0;

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
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] getdestroy;

    //AnimationState
    string state;

    [SerializeField]
    Sprite[] particle;

    // Start is called before the first frame update
    void Start()
    {
        typefaceAnimator = GameObject.Find("Point").GetComponent<TypefaceAnimator>();
        typefaceAnimator.enabled = false;
        otaku = this.gameObject;
        spanTime = Random.Range(3, 5);
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        pointText.text = "Point: " + point.ToString();
        pushX.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        sumTime -= Time.deltaTime;
        nowTime = (int)sumTime;
        timeText.text = "Time: " + nowTime.ToString();
        countTime += Time.deltaTime;
       
        if (c != null) c.transform.position = Vector2.Lerp(c.transform.position, destination, Time.deltaTime * 1);
        if (nowTime>0)
        {
            if (countTime >= spanTime) Pick();
            Move();
            //rb.velocity = new Vector2(rote * 5f, 0);
            transform.localScale = new Vector3(key * 0.65f, 0.75f, 0);

        }
        else if(nowTime < 0)
        {
            state = "Stay";
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
        
    }

    void Pick()
    {
        GameObject c;
        c =Instantiate(pick);
        destination = mainCamera.ScreenToWorldPoint(new Vector2(Random.Range(0,Screen.width), -6));
        StartCoroutine("Wait");
        spanTime = Random.Range(3, 5);
        countTime = 0;
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pick")
        {
            typefaceAnimator.enabled = true;
            Destroy(collision.gameObject);
            point += 20;
            pointText.text = "Point: " + point.ToString();
            audioSource.PlayOneShot(getdestroy[0]);
            StartCoroutine("Typeface");
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.6f);
        Destroy(c);
        if (c != null)
        {
            audioSource.PlayOneShot(getdestroy[1]);
        }
    }

    IEnumerator Typeface()
    {
        yield return new WaitForSeconds(0.25f);
        typefaceAnimator.enabled = false;
    }
}
