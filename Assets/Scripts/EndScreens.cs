// handles UI upond game completion

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
            StartCoroutine(WinSequence(creditTime, new Vector3(960, startY, 0), new Vector3(960, endY, 0)));
        }
        else
        {
            loseText.SetActive(true);
            winText.SetActive(false);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    } 

    private IEnumerator WinSequence(float time, Vector3 startPos, Vector3 endPos)
    {
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

            switch(i)
            {
                case 1:
                    break;

                case 3:
                    AudioManager.Instance.Play("CloseBook");
                    break;

                case 9:
                    AudioManager.Instance.Play("PlaceBook");
                    break;
                    
                case 20:
                    yield return new WaitForSeconds(0.25f);
                    AudioManager.Instance.Play("ButtonPress");
                    break;

                case 23:
                    AudioManager.Instance.Play("ExplosionSound");
                    break;
            }

            if (i == 4 || i == 5 || i == 6 || i == 15 || i == 16 || i == 17)
            {
                AudioManager.Instance.Play("CutsceneStepSound");
            }

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

        elapsedTime = 0;
        while (elapsedTime < 2)
        {
            tempColor.a -= elapsedTime * Time.deltaTime;
            creditImage.color = tempColor;
            space.color = tempColor;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        AudioManager.Instance.Stop("FinalCutsceneTheme");
        AudioManager.Instance.Play("WinTheme");
        credits.SetActive(false);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
