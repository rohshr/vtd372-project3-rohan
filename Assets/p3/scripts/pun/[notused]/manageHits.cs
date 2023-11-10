using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
 * this script is used by the system to track when a user is looking at an interactable surface
 * place the script in the "Scripts" game object
*/

public class manageHits : MonoBehaviour {
	//variables
	[Tooltip("Do NOT alter")]
	public bool isLooking; //is currently looking at an interactabel surface

	public void updateIsLooking(bool val)
	{
		isLooking = val;
	}
}
