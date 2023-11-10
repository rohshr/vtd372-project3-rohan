using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

/*
 * this script is used to set the camera for the local player - not the network players
 * Add to the character prefab
*/


namespace Assets.p3.scripts.pun
{

	public class photonSetCharacter : MonoBehaviourPunCallbacks
	{
		protected PhotonView m_PhotonView;
		private bool doSetCamera = true;
		[Tooltip("DO NOT Alter - Will AutoSet")]
		public photonAddCharacter photonAddCharacterScript;
		[Tooltip("This is the name of the Game Object holding the global scripts - Default name = scripts")]
		public string scriptsObjectName = "scripts";
		

		private string CharName;


		// Use this for initialization
		void Start () 
		{
				photonAddCharacterScript = GameObject.Find(scriptsObjectName).GetComponent<photonAddCharacter> ();
				CharName = this.transform.name;
		}
		// Update is called once per frame
		void Update () {
				if (doSetCamera) {
					setCamera ();
				}
		}
		void setCamera()
		{
			m_PhotonView = GetComponent<PhotonView> ();

			if (m_PhotonView.IsMine) {
				doSetCamera = false;
				photonAddCharacterScript.setCamera(this.gameObject.transform);
				m_PhotonView.Owner.NickName = CharName;
				
			} 
			else 
			{
				doSetCamera = false;
				
			}
		}

	}
}