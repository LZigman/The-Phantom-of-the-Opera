using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    void Start() => Time.timeScale = 1; //Set time to normal scale when the game starts
	private void Awake()
	{
        //SceneManager.LoadScene("MainMenu");
	}
	public void StartGame ()
    {
        SceneManager.LoadScene("LevelIntro");
    }
    public void StartNextLevel ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
