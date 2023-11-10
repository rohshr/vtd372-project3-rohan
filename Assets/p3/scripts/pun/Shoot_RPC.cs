/*
 * shoot_PUN.cs
 * 2018 - brian cleveley
 * for use as is or modified in VTD372
 * used to control the avatar's shooting sequence in a multiplayer world
 * place script in character prefab
*/
using UnityEngine;
using System.Collections;
using Photon.Pun;

namespace Assets.p3.scripts.pun
{
public class Shoot_RPC : MonoBehaviourPunCallbacks {
//		protected Animator animator;
		[Tooltip("The PreFab of your projectile")]
		public GameObject projectile; //the game object which will be used as the projectile
		[Tooltip("The maximum speed of your projectile - default is 1000")]
		public float maxProjectileSpeed = 1000f; //maximum force to add to the projectile
		[Tooltip("The object used at the spawn point for the projectile")]
		public GameObject projectileEmitter; //game object used as the cannon - can be a empty game object
		[Tooltip("The audio file to played when shooting a projectile")]
		public AudioSource audioShoot;
		[Tooltip("The seconds to delay the projectile firing - default is 0.25")]
		public float waitTime = 0.25f; 
		[Tooltip("The game object hosting the global scripts")]
		public string scriptObjectName = "scripts";
		[Tooltip("The Layer your Interactiable Objects are on")]
		public LayerMask layerMask;
		[Tooltip("RayCastEmitter Tag")]
		public string rayCastEmitterTag = "raycastemitter";
		[Tooltip("Name of the TAG used for the Exit Object")]
		public string exitSignTag = "exitSign";

		public Transform rayCastEmitter;
		private exitApp m_ExitApp; //maybe private
		private PhotonView m_PhotonView;
		private float projectileSpeed; //force to add to the projectile

	// Use this for initialization
	void Start () 
	{
		rayCastEmitter = GameObject.FindGameObjectWithTag(rayCastEmitterTag).transform;
		projectileSpeed=maxProjectileSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
			if(m_PhotonView == null) 
			{ 
				m_PhotonView = GetComponent<PhotonView>();
				if (m_PhotonView)
				{
					m_ExitApp = GameObject.Find(scriptObjectName).GetComponent<exitApp>();
				}
				else
				{
					return;
				}
			}
			if( m_PhotonView.IsMine == false && PhotonNetwork.IsConnected == true )
			{
				return;
			}
			//input manages keyboard, controller for Win and Mac
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton16))
			{
				getHitShoot(); 
			}
	}
		void getHitShoot()
		{
            
			RaycastHit hit;
			
			if (Physics.Raycast(rayCastEmitter.position, rayCastEmitter.forward, out hit, Mathf.Infinity, layerMask))
			{
				if(hit.transform.tag == exitSignTag)
                {
					m_ExitApp.doExit();
                }
				else
                {
					Vector3 hitPos = hit.point;
					GetComponent<PhotonView>().RPC("doShootRPC", RpcTarget.All, hitPos);
				}

			}
		}
	IEnumerator shootDelay(Vector3 hitVal)
	{
		yield return new WaitForSeconds(waitTime);
		Vector3 relativePos = hitVal - projectileEmitter.transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos);
		GameObject clone;
		clone = Instantiate(projectile, projectileEmitter.transform.position, rotation)as GameObject;
		clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * projectileSpeed);
		audioShoot.Play ();
	}

	//RPC Area
	[PunRPC]
	void doShootRPC(Vector3 hitVal)
	{
		StartCoroutine(shootDelay(hitVal));
	}
}
}