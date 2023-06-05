using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class UndergroundCollision : MonoBehaviour
{
    
    int score;

    private void Start()
    {
        score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameover)
        {
            string tag = other.tag;
            if (tag.Equals("Object"))
            {
                //Debug.Log("Object");
                Level.Instance.objectsInScene--;
                UIManager.Instance.UpdateLevelProgress();
                Magnet.Instance.RemoveFromMagnetField(other.attachedRigidbody);
                score++;
               UIManager.Instance.scoreText.text = "" + score.ToString();
                
                Destroy(other.gameObject);

                //check if win
                if (Level.Instance.objectsInScene == 0)
                {
                    //no more objects to collect
                    //Level.Instance.LoadNextLevel();
                    UIManager.Instance.ShowLevelCompleteUI();
                    Level.Instance.playWinFx();
                    Game.isGameover = true;
                    Invoke("NextLevel" , 2f);
                    
                }
            }
            if (tag.Equals("Obstacle"))
            {
                //Debug.Log("Obstacle");
                Game.isGameover = true;
                Camera.main.transform
                    .DOShakePosition(1f, 0.2f, 20, 90f)
                    .OnComplete(() =>
                    {
                        Level.Instance.RestartLevel();
                    });
                Level.Instance.RestartLevel();
            }
        }

    }
    void NextLevel()
    {
        Level.Instance.LoadNextLevel();
    }
}
