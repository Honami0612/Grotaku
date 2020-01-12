using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FallGlasses : MonoBehaviour
{
    [SerializeField]
    GameObject glasses;
    Vector3 firstPos;

    AudioSource audioSource;

    [SerializeField]
    Text pointText;
    int point = 0;

    [SerializeField]
    Text timeText;
    public float timeSum;
    int nowTime;

    [SerializeField]
    Text gotoSelect;

    // Start is called before the first frame update
    void Start()
    {
        firstPos = glasses.transform.position;
        audioSource = this.GetComponent<AudioSource>();
        pointText.text = "shakinPoint:" + point.ToString();
        gotoSelect.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeSum -= Time.deltaTime;
        nowTime = (int)timeSum;
        timeText.text = "Timer: " + nowTime.ToString();

        MoveGlasses();

        if (nowTime < 0)
        {
            timeText.enabled = false;
            gotoSelect.enabled = true;
            glasses.transform.position = firstPos;

            if (Input.GetKey(KeyCode.X))
            {
                SceneManager.LoadScene("SelectScene");
            }
        }

    }

    void MoveGlasses()
    {
        if (nowTime > 0)
        {
            glasses.transform.position = new Vector3(glasses.transform.position.x, glasses.transform.position.y - 0.05f, 0);
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (glasses.transform.position.y <= firstPos.y - 1)
                {
                    glasses.transform.position = firstPos;
                    audioSource.Play();
                    point += 20;
                    pointText.text = "shakinPoint:" + point.ToString();
                }

            }
        }
       
    }
}
