using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager1 : MonoBehaviour
{
    public static UIManager1 Instance;
    [SerializeField] private GameObject morfIndicator;
	private void Awake()
	{
		Instance = this;
	}
	public void SetMorfIndicator (bool toggle)
    {
        morfIndicator.SetActive (toggle);
    }
}
