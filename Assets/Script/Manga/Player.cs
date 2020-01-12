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
    int point=0;

    [SerializeField]
    Text timeText;
    [SerializeField]
    float sumTime;
    int nowTime;
    [SerializeField]
    Text goSelect;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < options.Length; i++) all_Number.Add(i);
        for (int k = 0; k < three_optionSprite.Length; k++) options_Number.Add(k);
        button = GameObject.Find("Option1").GetComponent<Button>();
        button.Select();
        getPointText.text = "Point: " + point.ToString();
        goSelect.enabled = false;

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

        for (int j = 0; j < 3; j++)
        {
            int choice = Random.Range(0, all_Number.Count);
            int ransu = all_Number[choice];
            three_Number.Add(ransu);
            all_Number.RemoveAt(choice);
        }
       
        all_Number.Clear();
        
        wantSprite.GetComponent<SpriteRenderer>().sprite = options[three_Number[0]];

        for (int h = 0; h < options_Number.Count; h++)
        {
            //どこに正解を置くか配列内にて数値を変更してる
            int aaa = options_Number[h];
            int three_ransu = Random.Range(0, options_Number.Count);
            options_Number[h] = options_Number[three_ransu];
            options_Number[three_ransu] = aaa;
        }
        for(int ff = 0; ff < 3; ff++)
        {
           //選択肢sprite変更
            three_optionSprite[options_Number[ff]].GetComponent<SpriteRenderer>().sprite = options[three_Number[num]];
            num++;
        }
        num = 0;
        three_Number.Clear();

        options_Number_storage.Clear();
        for (int i = 0; i < options.Length; i++) all_Number.Add(i);
    }


  

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Options")
        {
            Debug.Log(collision.gameObject.GetComponent<SpriteRenderer>().sprite.name);
            if (Input.GetKeyDown(KeyCode.S))
            {

                if (collision.gameObject.GetComponent<SpriteRenderer>().sprite.name == wantSprite.GetComponent<SpriteRenderer>().sprite.name)
                {
                    Debug.Log("Collect");
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
                point += 20;
                getPointText.text = "Point: " + point.ToString();
            }
            else
            {
                point -= 10;
                getPointText.text = "Point: " + point.ToString();
            }
        }
       
    }

   


   
}
