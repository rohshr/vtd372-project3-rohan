using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dnd : MonoBehaviour {

	[Tooltip("Do Not alter this value")]
	public int charID;
	//public string charName;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
	}
}
