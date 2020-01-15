using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public GameObject finger;

    [SerializeField]
    GameObject wantSprite;
    [SerializeField]
    GameObject[] three_optionSprite;

    [SerializeField]
    Sprite[] options;

    public List<int> all_Number = new List<int>();
    public List<int> three_Number = new List<int>();
    public List<int> options_Number = new List<int>();
    public List<int> options_Number_storage = new List<int>();

    int num = 0;

    Button button;

    [SerializeField]
    Text getPointText;
    [SerializeField]
    Text lastPointText;
    int point=0;

    [SerializeField]
    Text timeText;
    [SerializeField]
    float sumTime;
    int nowTime;
    [SerializeField]
    Text goSelect;

    TypefaceAnimator pointObj;

    AudioSource audioSource;
    [SerializeField]
    AudioClip[] getPointSound;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < options.Length; i++) all_Number.Add(i);
        for (int k = 0; k < three_optionSprite.Length; k++) options_Number.Add(k);
        button = GameObject.Find("Option1").GetComponent<Button>();
        button.Select();
        getPointText.text = "Point: " + point.ToString();
        goSelect.enabled = false;
        lastPointText.enabled = false;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //MovePos();
        sumTime -= Time.deltaTime;
        nowTime = (int)sumTime;
        timeText.text = "Time: " + nowTime.ToString();
        if (nowTime < 0)
        {
            timeText.enabled = false;
            goSelect.enabled = true;
            wantSprite.SetActive(false);
            lastPointText.enabled = true;
            lastPointText.text = "Poin " + point.ToString() + "!!!";
            if (Input.GetKey(KeyCode.X))
            {
                SceneManager.LoadScene("SelectScene");
            }
        }
    }

    void MovePos()
    {
        if (Input.GetKey(KeyCode.UpArrow))          finger.transform.position = new Vector3(finger.transform.position.x, finger.transform.position.y + 0.05f, 0);
        else if (Input.GetKey(KeyCode.DownArrow))   finger.transform.position = new Vector3(finger.transform.position.x, finger.transform.position.y - 0.05f, 0);
        else if (Input.GetKey(KeyCode.RightArrow))  finger.transform.position = new Vector3(finger.transform.position.x + 0.05f, finger.transform.position.y, 0);
        else if (Input.GetKey(KeyCode.LeftArrow))   finger.transform.position = new Vector3(finger.transform.position.x - 0.05f, finger.transform.position.y, 0);

    }

    void MangaChange()
    {
        three_Number.Clear();
        List<int> tempList = new List<int> { 0, 1, 2, 3 };
        for (int j = 0; j < 3; j++)
        {
            int choice = Random.Range(0, tempList.Count);
            three_Number.Add(tempList[choice]);
            Debug.Log("Choice; " + options[three_Number[j]]);
            tempList.RemoveAt(choice);
            three_optionSprite[j].GetComponent<SpriteRenderer>().sprite = options[three_Number[j]];
        }
        
        wantSprite.GetComponent<SpriteRenderer>().sprite = options[three_Number[Random.Range(0, three_Number.Count)]];


    }


  

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Options")
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name == wantSprite.GetComponent<SpriteRenderer>().sprite.name)
                {
                    MangaChange();
                }
            }
        }
    }

    public void But(GameObject witch)
    {
        if (nowTime >= 0)
        {
            if (witch.GetComponent<SpriteRenderer>().sprite.name == wantSprite.GetComponent<SpriteRenderer>().sprite.name)
            {
                MangaChange();
                audioSource.PlayOneShot(getPointSound[0]);
                point += 20;
                getPointText.text = "Point: " + point.ToString();
                pointObj = GameObject.Find("Point").GetComponent<TypefaceAnimator>();
                pointObj.enabled = true;
                StartCoroutine("TextEnabled");
            }
            else
            {
                point -= 10;
                getPointText.text = "Point: " + point.ToString();
                audioSource.PlayOneShot(getPointSound[1]);
            }
        }
       
    }

    IEnumerator TextEnabled()
    {
        yield return new WaitForSeconds(0.25f);
        pointObj.enabled = false;
       
    }

   


   
}
