using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class selectMini : MonoBehaviour
{

    Button button;
    AudioSource audioSource;

   

    [SerializeField]
    Text gameExplainText;
    [SerializeField]
    Animator[] animator;

   
    
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Canvas/MiniGame/manga").GetComponent<Button>();
        button.Select();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.LeftArrow))))
        {
            audioSource.Play();
        }

        ChangeText();
        
      
        //if (ani.SetTrigger=="Selected"))
        //{
        //    gameExplainText.text = "aaa";
        //}
        //else
        //{
        //    gameExplainText.text = "iii";
        //}

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

    void ChangeText()
    {
        if (animator[0].GetCurrentAnimatorStateInfo(0).IsName("Selected"))
        {
            gameExplainText.text = "glasses\naaa";
        }
        else if (animator[1].GetCurrentAnimatorStateInfo(0).IsName("Selected"))
        {
            gameExplainText.text = "~~manga~~\nSelect:⇦⇨\nChose:Enter\nPick up select one";

        }
        else if (animator[2].GetCurrentAnimatorStateInfo(0).IsName("Selected"))
        {
            gameExplainText.text = "~~swing~~\nSwing:Enter\nSwing with the crowd!";

        }
        else if (animator[3].GetCurrentAnimatorStateInfo(0).IsName("Selected"))
        {
            gameExplainText.text = "~~pick~~\nWalk:⇦⇨\n️Get idol band picks!";

        }
    }

    public IEnumerator Change()
    {
        yield return new WaitForSeconds(1);
        
    }
}
