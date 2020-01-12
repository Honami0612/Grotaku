using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallGlasses : MonoBehaviour
{
    [SerializeField]
    GameObject glasses;
    Vector3 firstPos;

    AudioSource audioSource;

    [SerializeField]
    Text pointText;
    int point = 0;

    // Start is called before the first frame update
    void Start()
    {
        firstPos = glasses.transform.position;
        audioSource = this.GetComponent<AudioSource>();
        pointText.text = "shakinPoint:" + point.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        glasses.transform.position = new Vector3(glasses.transform.position.x, glasses.transform.position.y - 0.05f, 0);
        if (Input.GetKeyDown(KeyCode.S))
        {
            glasses.transform.position = firstPos;
            audioSource.Play();
            point += 20;
            pointText.text = "shakinPoint:" + point.ToString();

        }
    }
}
