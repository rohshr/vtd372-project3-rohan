using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Permissions;

public class startUpScript : MonoBehaviour {

	//public Button[] uiButtons;

	//public string charName = "nil";
	public int charID = 0;
	public string dndObjectName = "dnd";
	public int newScene = 1;
	public Texture2D buttonImageInactive;
	public Texture2D buttonImageActive;
	
	private dnd dndScript;
	public Material buttonMaterial;
	
	// Use this for initialization
	void Start () {
		buttonMaterial = gameObject.GetComponent<Renderer>().material;
		dndScript = GameObject.Find(dndObjectName).GetComponent<dnd> ();
		//setButtonInteraction(false); //turn the buttons' interactable setting to False
	}
	//public void setButtonInteraction(bool val)
 //   {
 //       for (int i = 0; i < uiButtons.Length; i++)
 //       {
	//		//setting the buttons' interactiable to true comes from InputField activity
	//		uiButtons[i].interactable = val;
 //       }
 //   }
	

	void startPlay()
    {
		SceneManager.LoadScene(newScene);
	}
	//public void setCharName(string val)
	//{
	//	dndScript.charName = val;
	//	//setButtonInteraction(true); //turn the buttons' interactable setting to True
	//}
	public void SelectCharacter()
    {
		dndScript.charID = charID;
		startPlay();
	}

	public void OnPointerExit()
    {
		buttonMaterial.mainTexture = buttonImageInactive;
    }
	public void OnPointerEnter()
	{
		buttonMaterial.mainTexture = buttonImageActive;
	}

}
