using Photon;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
/*
 * This script gets the Character ID from the start scene, and uses it to add a player prefab
 * then the script will adds the player camera prefab to the sceneCameara variable
 * Add this to the scripts object
 * This version of the script is for use with GVR - and updates the MouseLook script with the attached camera to use
*/


namespace Assets.p3.scripts.pun
{

public class photonAddCharacter : MonoBehaviourPunCallbacks
{
		#region Properties

		//public string charName = "nil";
		//public Text m_UICharNameText;
		[Tooltip("Object to act as Spawnpoint for the characters")]
		public Transform spawnPoint; // spawnPoint for instantiated characters 
		[Tooltip("The names of the character prefabs")]
		public string[] charctersPF; //list of all character prefabs to be available for scene
		[Tooltip("This is a temporary camera to be used during development but turned off during playback")]
		public GameObject bldCamera; // temporary camera for development - will be turned off when scene loads
		[Tooltip("This is the Prefab of the Playercamera")]
		public GameObject PlayerCamera; //PF
		//public GameObject CharNamePF; //pf
		[Tooltip("The name of the DoNotDestroy object - same name as used in Start Scene - Default = dndObject")]
		public string dndObjectName = "dnd";
		[Tooltip("Spawn Range - a float value to vary how far away from the actual spawn point the characters is added")]
		public float spawnRange = 1.0f;
		//[Tooltip("DO NOT Alter - Will AUtoSet")]
		//public GameObject CharNameUI;
		[Tooltip("Activate for Debugging ONLY")]
		public bool isDebug;

		private int numberOfButtons; //number of buttons used = max of 5 (0-4)
		private dnd dndScript; // the do not destroy script
		private string characterPF; // name of currently used character PreFab
		private int charID = 0; //pointer to character in array
		
		private GameObject sceneCamera; //the scene camera object that the player camera is assigned to
		private MouseLook m_mouseLook;
		

    #endregion


    #region Members
    
    #endregion


    #region Unity
		public void Awake()
		{
			sceneCamera = Instantiate (PlayerCamera, spawnPoint);
		}

    public void Start()
    {
			//remove build camera if connected
			if (bldCamera) bldCamera.SetActive(false);
	}

    #endregion


    #region Photon

	    public override void OnJoinedRoom() //called after the network has established a connection with the target room
	    {

			if (isDebug) 
			{
				int numberOfCharacters = charctersPF.Length;  //get the length of the characterPF array
				if(numberOfCharacters == 1) //if the array is one (1) element in size then set the charID to 0, pointing to the first and only element
				{
					charID = 0;
				}
				else //if the array is larger than one element then randomly choose one of the elements
                {

					charID = (int)Random.Range(0f, (float)numberOfCharacters);
				}
				
			} 
			else 
			{
				//check to see if the dndObject exists - if it does then get the dnd script and read the charID and charName from it
				if(GameObject.Find (dndObjectName))
				{
					dndScript = GameObject.Find (dndObjectName).GetComponent<dnd> ();
					charID = dndScript.charID;
					//charName = dndScript.charName;
				}
				else //if the dndObject does not exist then set the charID to 0
					charID = 0;
			}
				
			CreatePlayerObject (); // call the CreatePlayerObject method

	    }

	    private void CreatePlayerObject()
	    {
			characterPF = charctersPF [charID];
			//Set randome spawnPoint
			Vector3 position = spawnPoint.position;
	        position.x += Random.Range( -spawnRange, spawnRange);
	        position.z += Random.Range( -spawnRange, spawnRange);
			//instantiate character
			GameObject newPlayerObject = PhotonNetwork.Instantiate(characterPF, position, spawnPoint.rotation);
			//newPlayerObject.name = charName;
		}
		public void setCamera(Transform plyr)
		{
			//attach the camera to the PLYR object
			sceneCamera.transform.SetParent(plyr, false);
			//inform the PLYR object to use the camera for EDITOR GVR usage - simulates the camera movement from the mobile device
			plyr.gameObject.GetComponent<MouseLook>().setCamera(sceneCamera.transform);
		}

    #endregion
	}
}