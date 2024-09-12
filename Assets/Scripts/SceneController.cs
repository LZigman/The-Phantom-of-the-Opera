using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] private Button[] buttons; 
    [SerializeField] private Animator transitionAnim;
    private static readonly int EndLevel = Animator.StringToHash("End");
    private static readonly int StartLevel = Animator.StringToHash("Start");

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void MenuLevelSelect(int levelId)
    {
        var levelName = "Level " + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }
    
    public void Retry()
    {
        StartCoroutine(ReloadLevel());
    }

    private IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger(EndLevel);
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transitionAnim.SetTrigger(StartLevel);
    }

    private IEnumerator ReloadLevel()
    {
        transitionAnim.SetTrigger(EndLevel);
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        transitionAnim.SetTrigger(StartLevel);
    }
}
