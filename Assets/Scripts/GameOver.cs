using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;

    public void GameEnd()
    {
        gameOver.SetActive(true);
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlaySfx("GameOver");
    }
}
