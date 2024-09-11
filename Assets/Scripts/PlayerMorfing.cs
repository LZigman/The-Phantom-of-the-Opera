using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MorfObjects
{
    BackstageBox,
    SecurityGuard,
    TrashCan,
	Default
}

public class PlayerMorfing : MonoBehaviour
{
    public MorfObjects currentMorf;
	[SerializeField] private MeshFilter mf;
	[SerializeField] private Mesh BackstageBoxMesh, SecurityGuardMesh, TrashCanMesh;

	private void Start()
	{
		currentMorf = MorfObjects.Default;
	}

	public void MorfInto (MorfObjects morfObject)
	{
		if (morfObject == MorfObjects.BackstageBox)
		{
			mf.mesh = BackstageBoxMesh;
			Debug.Log("Morfed into backstage box!");
		}
		else if (morfObject == MorfObjects.SecurityGuard)
		{
			mf.mesh = SecurityGuardMesh;
			Debug.Log("Morfed into backstage box!");
		}
		else if (morfObject == MorfObjects.TrashCan)
		{
			mf.mesh = TrashCanMesh;
			Debug.Log("Morfed into backstage box!");
		}
	}
}
