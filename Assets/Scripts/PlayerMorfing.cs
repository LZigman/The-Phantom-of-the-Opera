using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMorfing : MonoBehaviour
{
	public DisguiseType currentDisguise;
	[SerializeField] private MeshFilter mf;
	[SerializeField] private Mesh BackstageBoxMesh, SecurityGuardMesh, TrashCanMesh;

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
            Debug.Log("Morfed into backstage box!");
		}
		else if (morfObject == DisguiseType.SecurityGuard)
		{
			mf.mesh = SecurityGuardMesh;
			Debug.Log("Morfed into backstage box!");
		}
		else if (morfObject == DisguiseType.TrashCan)
		{
			mf.mesh = TrashCanMesh;
			Debug.Log("Morfed into backstage box!");
		}
	}
}
