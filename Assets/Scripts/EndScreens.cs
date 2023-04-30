using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreens : MonoBehaviour
{
    //Menus
    public GameObject winText;
    public GameObject loseText;
    public Button mainMenuButton;
    public GameObject credits;
    public GameObject exitButton;

    //Credits
    public float startY;
    public float endY;
    public float creditTime;
    public Image space;

    //Cutscene
    public Sprite[] frames;
    public float timeBetween;
    public Image cutscene;
    


    private void Start()
    {
        mainMenuButton.Select();
        if(GameManager.Instance.win)
        {
            StartCoroutine(winSequence(creditTime, new Vector3(960, startY, 0), new Vector3(960, endY, 0)));
        }
        else
        {
            //loseText.SetActive(true);
            //winText.SetActive(false);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    } 
    
    private void Update()
    {
    }

    private IEnumerator winSequence(float time, Vector3 startPos, Vector3 endPos)
    {
        //exitButton.SetActive(false);
        //mainMenuButton.gameObject.SetActive(false);
        winText.SetActive(true);
        loseText.SetActive(false);
        credits.SetActive(true);
        cutscene.gameObject.SetActive(true);

        Image creditImage = credits.GetComponent<Image>();
        var tempColor = creditImage.color;
        tempColor.a = 0;

        space.color = tempColor;
        creditImage.color = tempColor;


        for (int i = 0; i < frames.Length; i++)
        {
            cutscene.sprite = frames[i];

            yield return new WaitForSeconds(timeBetween);
        }

        float elapsedTime = 0;
        while (elapsedTime < 2)
        {
            tempColor.a += elapsedTime * Time.deltaTime;
            creditImage.color = tempColor;
            space.color = tempColor;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        elapsedTime = 0;
        while (elapsedTime < time)
        {
            credits.transform.position = Vector3.Lerp(startPos, endPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        cutscene.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        
        /*elapsedTime = 0;
        while (elapsedTime < time)
        {
            credits.transform.position = Vector3.Lerp(endPos, new Vector3(endPos.x, endPos.y + endY - startY, endPos.z), (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }*/

        elapsedTime = 0;
        while (elapsedTime < 2)
        {
            tempColor.a -= elapsedTime * Time.deltaTime;
            creditImage.color = tempColor;
            space.color = tempColor;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        credits.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
