using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class GameManager : MonoSingleton<GameManager>
{
    [HideInInspector] public bool gameStart = false;
    [HideInInspector] public bool levelFnish = false;
    [HideInInspector] public bool levelFailed;
    public int levelAge;
    public int currentAge;
    public GameObject passedPanel, failedPanel;
    public ParticleSystem successParticle;


    private void Start()
    {
        successParticle.Stop();
        levelFailed = false;
    }
   
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && levelFailed == false ) // Game start when we touch screen .
        {
            gameStart = true;
        }

        if (currentAge < 0) gameOver();

        PlayerController.Instance.CharacterUpdate.SetAgeText();
    }

    public void fnishCheck()
    {
        if(currentAge == levelAge)
        {
            passedPanel.SetActive(true);
            successParticle.Play();
            PlayerManager.Instance.Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FnishAnimations/Dancing"); // Changing running animation with "success" animation at the runtime. (Resources/FnishAnimations/Dancing).
        }
        else
        {
            failedPanel.SetActive(true);
            PlayerManager.Instance.Animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("FnishAnimations/Crying"); // Changing running animation with "fail" animation at the runtime.(Resources/FnishAnimations/Crying).
        }
    }

    public void gameOver()
    {
        PlayerController.Instance.CharacterUpdate.currentCharacter.transform.DOScale(0, 2);
        gameStart = false;
        currentAge = 0;
        levelFailed = true;
        failedPanel.SetActive(true);
    }


    #region Scene Control
    public void nextLevelButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void retryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion
}
