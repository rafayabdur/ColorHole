using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    #region Singleton class : Level
    public static Level Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion
    [SerializeField] ParticleSystem winfx;
    [Space]
    public int objectsInScene;
    public int totalObjects;
    [Space]
    [Header("Level Objects and Obstacles")]
    [SerializeField] Material groundMaterial;
    [SerializeField] Material objectMaterial;
    [SerializeField] Material obstacleMaterial;
    [SerializeField] SpriteRenderer groundBorderSprite;
    [SerializeField] SpriteRenderer groundSideSprite;
    [SerializeField] Image progressFillImage;
    [SerializeField] SpriteRenderer bgfadeSprite;

    [Space]
    [Header("Level Colors")]
    [Header("Ground")]
    [SerializeField] Color groundColor;
    [SerializeField] Color bordersColor;
    [SerializeField] Color sideColor;

    [Header("Objects & Obstacles")]
    [SerializeField] Color objectColor;
    [SerializeField] Color obstacleColor;

    [Header("UI Progress")]
    [SerializeField] Color progressFillColor;

    [Header("Background")]
    [SerializeField] Color cameraColor;
    [SerializeField] Color fadeColor;

    public GameObject holeCenter;
    public GameObject[] collectibles;
    public GameObject[] obstacles;
    int NewScore;


    [SerializeField] Transform objectsParent;
    void Start()
    {
        CountObjects();
        UpdateLevelColor();
    }

    // Update is called once per frame
    void CountObjects()
    {
        totalObjects = objectsParent.childCount;
        objectsInScene = totalObjects;
    }
    public void playWinFx()
    {
        winfx.Play();
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void RestartLevel()
    {
        //UIManager.Instance.SplashScreenStatus(false);
        //UIManager.Instance.MainMenuScreenStatus(false);
        //holeCenter.transform.position = new Vector3(0.013f , 0 , -0.966f);
        //UIManager.Instance.scoreText.text = "Score : " + NewScore.ToString();
        //for (int i = 0; i < collectibles.Length; i++)
        //{
        //    if (!collectibles[i].activeSelf)
        //    {
        //        collectibles[i].SetActive(true);
        //    }
        //}
        //for (int i = 0; i < obstacles.Length; i++)
        //{
        //    if (!obstacles[i].activeSelf)
        //    {
        //        obstacles[i].SetActive(true);
        //    }
        //}
        //progressFillImage.fillAmount = 0;


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
        
    void UpdateLevelColor()
    {
        groundMaterial.color = groundColor;
        groundSideSprite.color = sideColor;
        groundBorderSprite.color = bordersColor;
        obstacleMaterial.color = obstacleColor;
        objectMaterial.color = objectColor;

        progressFillImage.color = progressFillColor;
        Camera.main.backgroundColor = cameraColor;
        bgfadeSprite.color = fadeColor;
    }
    void OnValidate()
    {
        UpdateLevelColor();    
    }
}
