using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyFieldOfView : MonoBehaviour
{
    [Header("FOV Logic")]
    public float viewRadius;
    public float viewAngle;
    public float viewFrequency;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [Header ("Cone Mesh Drawing")]
    public int meshResolution;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;
    Material viewConeMaterial;
    public Color idleColor, alertColor;

    [Header("Notice Logic")]
    public DisguiseType disguiseIRecognize;
    public float timeToNotice;
    public float velocityThreshold;
    float noticeTimer;
    public EnemyPatrolMove patrolScript;
    bool noticeMovement;

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        viewConeMaterial = viewMeshFilter.GetComponent<MeshRenderer>().material;
        patrolScript = GetComponent<EnemyPatrolMove>();
        InvokeRepeating(nameof(FindVisibleTargets), viewFrequency, viewFrequency);
    }

	private void LateUpdate()
	{
		DrawFieldOfView();
	}
	void DrawFieldOfView()
    {
        int stepCount = meshResolution;
        float stepAngleSize = viewAngle / stepCount;
        List<Vector3> viewPoints = new List<Vector3>();
        for (int i = 0; i < stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.point);
        }
        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }
        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast (float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, viewRadius, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo (false, transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDeg, bool isGlobal)
    {
        if (isGlobal == false)
        {
            angleInDeg += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDeg * Mathf.Deg2Rad));
    }
    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dist;
        public float angle;
        public ViewCastInfo (bool _hit, Vector3 _point, float _dist, float _angle)
        {
            hit = _hit;
            point = _point;
            dist = _dist;
            angle = _angle;
        }
    }

    void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        if(targetsInViewRadius.Length > 0)
        {
            Vector3 playerDirection = targetsInViewRadius[0].transform.position - transform.position;
            playerDirection.Normalize();

            if (Vector3.Angle(transform.forward, playerDirection) < viewAngle / 2)
            {
                RaycastHit Hit;
                if (Physics.Raycast(transform.position, playerDirection, out Hit, viewRadius, obstacleMask))
                {
                    if (Hit.collider.gameObject.CompareTag("Player"))
                    {
                        CheckDisguise(targetsInViewRadius[0].transform);
                        Debug.Log("Checking disguise!");
                    }
                    else
                    {
                        ResetNotice();
                    }
                }
            }
            else
            {
                ResetNotice();
            }
        }
        else
        {
            ResetNotice();
        }
    }

    private void ResetNotice()
    {
        noticeTimer = 0;
        viewConeMaterial.SetColor("_BaseColor", idleColor);
        noticeMovement = false;
		patrolScript.StopPatroling(false);
	}

    private void CheckDisguise(Transform player)
    {
        PlayerMorfing scriptPlayer = player.GetComponent<PlayerMorfing>();
        Rigidbody pRb = player.GetComponent<Rigidbody>();

        if(pRb.velocity.magnitude > velocityThreshold)
            noticeMovement = true;
        if (pRb.velocity.magnitude <= velocityThreshold)
            ResetNotice();
        if (scriptPlayer.currentDisguise == DisguiseType.Undisguised || scriptPlayer.currentDisguise == disguiseIRecognize || noticeMovement)
        {
            noticeTimer += viewFrequency;
            viewConeMaterial.SetColor("_BaseColor", alertColor);
            patrolScript.StopPatroling(true);
            patrolScript.LookAtPlayer(player);
        }
        else
        {
            ResetNotice();
        }

        if (noticeTimer >= timeToNotice)
        {
            noticeTimer = timeToNotice;
            patrolScript.PlayerNoticed();
            Debug.Log("Game Over");
        }
    }
}
