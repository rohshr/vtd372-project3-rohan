using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Permissions;

public class startButtonScript : MonoBehaviour {
	
	//variables 
	public int charID = 0;
	public string dndObjectName = "dnd";
	public int newScene = 1;
	public Material buttonMaterialOn;
	public Material buttonMaterialOff;
	
	private dnd dndScript;
	private Renderer buttonRenderer;
	
	// Use this for initialization
	void Start () {
		buttonRenderer = gameObject.GetComponent<Renderer>();
		dndScript = GameObject.Find(dndObjectName).GetComponent<dnd> ();
	}

	void startPlay()
    {
		SceneManager.LoadScene(newScene);
	}

	public void SelectCharacter()
    {
		dndScript.charID = charID;
		startPlay();
	}

	public void OnPointerExit()
    {
		buttonRenderer.material = buttonMaterialOff;
    }
	public void OnPointerEnter()
	{
		buttonRenderer.material = buttonMaterialOn;
	}

}
