using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectMini : MonoBehaviour
{

    Button button;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Canvas/MiniGame/glasses").GetComponent<Button>();
        button.Select();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.LeftArrow))))
        {
            Debug.Log("e");
            audioSource.Play();
        }
    }
    public void SceneChange(int number)
    {
        
        switch (number)
        {
            case 0:
                SceneManager.LoadScene("GlassesScene");
                break;
            case 1:
                SceneManager.LoadScene("MangaScene");
                break;
            case 2:
                SceneManager.LoadScene("SwingstickScene");
                break;
            case 3:
                SceneManager.LoadScene("PickCatchScene");
                break;
        }
       
    }

    public IEnumerator Change()
    {
        yield return new WaitForSeconds(1);
        

    }
}
