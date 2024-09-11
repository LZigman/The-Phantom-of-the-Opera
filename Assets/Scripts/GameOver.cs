using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameOver.SetActive(true);
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlaySfx("GameOver");
        }
    }
}
