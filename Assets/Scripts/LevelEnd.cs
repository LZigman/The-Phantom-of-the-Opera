using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.instance.NextLevel();
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlaySfx("LevelComplete");
        }
    }
}
