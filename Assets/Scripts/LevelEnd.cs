using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    public GameObject missionComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            missionComplete.SetActive(true);
            AudioManager.Instance.musicSource.Stop();
            AudioManager.Instance.PlaySfx("LevelComplete");
        }
    }
}
