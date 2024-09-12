using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMorfing : MonoBehaviour
{
	public DisguiseType currentDisguise;
	[SerializeField] private MeshFilter mf;
	[SerializeField] private Mesh BackstageBoxMesh, SecurityGuardMesh, TrashCanMesh, defaultMesh;
	[SerializeField] private float disguiseTimer;

	private void Start()
	{
		currentDisguise = DisguiseType.Undisguised;
	}

	public void MorfInto (DisguiseType morfObject)
	{
		currentDisguise = morfObject;

        if (morfObject == DisguiseType.BackstageBox)
		{
			mf.mesh = BackstageBoxMesh;
			StartCoroutine(MorfTimer());
            Debug.Log($"Morfed into {morfObject}!");
		}
		else if (morfObject == DisguiseType.SecurityGuard)
		{
			mf.mesh = SecurityGuardMesh;
			StartCoroutine(MorfTimer());
			Debug.Log($"Morfed into {morfObject}!");
		}
		else if (morfObject == DisguiseType.TrashCan)
		{
			mf.mesh = TrashCanMesh;
			StartCoroutine(MorfTimer());
			Debug.Log($"Morfed into {morfObject}!");
		}
	}

	IEnumerator MorfTimer ()
	{
		yield return new WaitForSeconds( disguiseTimer );
		mf.mesh = defaultMesh;
		currentDisguise = DisguiseType.Undisguised;
	}
}
