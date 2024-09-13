using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMorfing : MonoBehaviour
{
	public DisguiseType currentDisguise;
	//[SerializeField] private MeshFilter mf;
	//[SerializeField] private Mesh BackstageBoxMesh, SecurityGuardMesh, TrashCanMesh;

	[SerializeField] GameObject defaultPlayerObj, backstageBoxObj, securityGuardObj, trashCanObj;
	Animator playerAnim, securityAnim;
	Player scriptPlayer;

    private void Start()
	{
		currentDisguise = DisguiseType.Undisguised;

		scriptPlayer = GetComponent<Player>();
		playerAnim = defaultPlayerObj.GetComponentInChildren<Animator>();
		securityAnim = securityGuardObj.GetComponentInChildren<Animator>();

		scriptPlayer.SwitchAnimator(playerAnim);
	}

	//public void MorfIntoOldmethod (DisguiseType morfObject)
	//{
	//	currentDisguise = morfObject;

 //       if (morfObject == DisguiseType.BackstageBox)
	//	{
	//		mf.mesh = BackstageBoxMesh;
 //           Debug.Log("Morfed into backstage box!");
	//	}
	//	else if (morfObject == DisguiseType.SecurityGuard)
	//	{
	//		mf.mesh = SecurityGuardMesh;
	//		Debug.Log("Morfed into backstage box!");
	//	}
	//	else if (morfObject == DisguiseType.TrashCan)
	//	{
	//		mf.mesh = TrashCanMesh;
	//		Debug.Log("Morfed into backstage box!");
	//	}
	//}

    public void MorfInto(DisguiseType morfObject)
    {
        currentDisguise = morfObject;
		defaultPlayerObj.SetActive(false);

        if (morfObject == DisguiseType.BackstageBox)
        {
            securityGuardObj.SetActive(false);
            trashCanObj.SetActive(false);

            backstageBoxObj.SetActive(true);
        }
        else if (morfObject == DisguiseType.SecurityGuard)
        {
            trashCanObj.SetActive(false);
			backstageBoxObj.SetActive(false);

            scriptPlayer.SwitchAnimator(securityAnim);
            securityGuardObj.SetActive(true);
        }
        else if (morfObject == DisguiseType.TrashCan)
        {
            securityGuardObj.SetActive(false);
            backstageBoxObj.SetActive(false);

            trashCanObj.SetActive(true);
        }
    }
}
