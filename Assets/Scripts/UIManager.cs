using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIManager : MonoBehaviour
{

    #region Singleton Class: UIManager

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            StartCoroutine(DelayScreenStatus(splashScreen,2,GamePlayScreen));
        }
        
    }
    #endregion
    
    [Header("Level Progress UI")]
    [SerializeField] int sceneoffset;
    [SerializeField] TMP_Text nextLevelText;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] Image progressFillImage;

    [Space]
    [SerializeField] TMP_Text levelCompleteText;
    [Space]
    [SerializeField] Image fade_Panel;
    public Text scoreText;

    [Space]
    public GameObject splashScreen;
    public GameObject mainMenuScreen;
    public GameObject GamePlayScreen;





    void Start()
    {

        //SplashScreenStatus(true);
        FadeAtStart();
        progressFillImage.fillAmount = 0f;
        GamePlayScreenStatus(false);       
        SetLevelProgressText();
    }

    void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneoffset;
        currentLevelText.text = level.ToString();
        nextLevelText.text = (level + 1).ToString();

            }
    public void UpdateLevelProgress()
    {
        float val = 1f - ((float)Level.Instance.objectsInScene / Level.Instance.totalObjects);
        //progressFillImage.fillAmount = val;
        progressFillImage.DOFillAmount(val, 0.4f);
    }

    public void ShowLevelCompleteUI()
    {
        levelCompleteText.DOFade(1f,0.6f).From(0f);

    }
    public void FadeAtStart()
    {
        fade_Panel.DOFade(0f, 0.6f).From(1f);
    }

    public void SplashScreenStatus(bool status)
    {
        splashScreen.SetActive(status);
    }
    
    public void GamePlayScreenStatus(bool status)
    {
        GamePlayScreen.SetActive(status);
    }

    public void MainMenuScreenStatus(bool status)
    {
        mainMenuScreen.SetActive(status);
    }


    public void Play()
    {
       
        GamePlayScreenStatus(true);
    }

  
    

    public IEnumerator DelayScreenStatus(GameObject screen, int time, GameObject next)
    {
        screen.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        screen.SetActive(false);

        //if (screen.name == LoadingScreen.name)
        //{
        //    Time.timeScale = 1;
        //}
        next.SetActive(true);
    }
}
