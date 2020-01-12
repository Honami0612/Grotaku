using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Catch : MonoBehaviour
{

    GameObject otaku;

    [SerializeField]
    GameObject pick;

    int spanTime;
    float countTime;

    private Camera mainCamera;
    private Vector2 destination;

    GameObject c;

    [SerializeField]
    Text pointText;
    int point=0;

    [SerializeField]
    float sumTime;
    [SerializeField]
    Text timeText;
    int nowTime;
    [SerializeField]
    Text pushX;

    
    // Start is called before the first frame update
    void Start()
    {
        otaku = this.gameObject;
        spanTime = Random.Range(3, 5);
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        pointText.text = "Point: " + point.ToString();
        pushX.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        sumTime -= Time.deltaTime;
        nowTime = (int)sumTime;
        timeText.text = "Time: " + nowTime.ToString();
        countTime += Time.deltaTime;
        if (countTime >= spanTime) Pick();
        Move();
        if (c != null) c.transform.position = Vector2.Lerp(c.transform.position, destination, Time.deltaTime * 1);
        if (nowTime < 0)
        {
            timeText.enabled = false;
            pushX.enabled = true;
            if (Input.GetKey(KeyCode.X)) SceneManager.LoadScene("SelectScene");
            
        }

    }

    void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow)) otaku.transform.position = new Vector3(otaku.transform.position.x + 0.1f, otaku.transform.position.y, 0);
        else if (Input.GetKey(KeyCode.LeftArrow)) otaku.transform.position = new Vector3(otaku.transform.position.x - 0.1f, otaku.transform.position.y, 0);
    }

    void Pick()
    {
        c=Instantiate(pick);
        destination = mainCamera.ScreenToWorldPoint(new Vector2(Random.Range(0,Screen.width), -6));
        StartCoroutine("Wait");
        spanTime = Random.Range(3, 5);
        countTime = 0;
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pick")
        {
            Destroy(collision.gameObject);
            point += 20;
            pointText.text = "Point: " + point.ToString();
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.4f);
        Destroy(c);
    }
}
