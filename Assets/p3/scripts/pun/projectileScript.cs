using UnityEngine;
using System.Collections;

/*
 * 2018
 * used to deploy a prefab that has two particle systems - one to spray paint and one to leave a paint splat on a surface
 * place this script in the paint ball prefab
*/

public class projectileScript : MonoBehaviour {

	[Tooltip("This is the amount of time before the projectile is destroyed after colliding with an object - Default = 1.0f")]
	public float waitTimeDestroy = 1.0f;
//	[Tooltip("This is the particle system to be played with the projectile strikes an object")]
//	public ParticleSystem hitParticles;
	[Tooltip("This is an array of three (3) the game objects containing the splotch map")]
	public GameObject[] splatPF;
	[Tooltip("This is the audio source for the sound of the paint ball hitting a surface")]
	public AudioSource audioSplat;

	private bool playOnce = true;

	//function runs when paint ball prefab his a designated surface
	void OnCollisionEnter(Collision collision)
	{
		if (playOnce) {
			playOnce = false;
			//get first hit point
			ContactPoint hitPoint = collision.contacts [0];

			//for debugging
//				print("Points colliding: " + collision.contacts.Length);
//				print("First normal of the point that collide: " + collision.contacts[0].normal);

			//Get the actual hit point and convert to a quaterion
			Vector3 hit = hitPoint.point;
			Vector3 hitNormal = hitPoint.normal;
			Quaternion hitNormQ = Quaternion.LookRotation (hitNormal);

			//for debugging
//				Debug.Log ("hitNormal: " + hitNormal.ToString ());
//				Debug.Log ("LocalNormal: " + LocalNormal.ToString ());
//				Debug.Log ("hitNorQ: " + hitNormQ.ToString ());

			//get a random number to select on the splat prefabs
			int pointer = Random.Range (0, splatPF.Length);

			//for debugging
//				Debug.Log (pointer.ToString ());

			if (pointer == splatPF.Length)
				pointer = splatPF.Length - 1;
			GameObject splatOBJ = splatPF [pointer];
			//instantiate the prefab
			GameObject splotch = Instantiate (splatOBJ, hit, hitNormQ);
			//play the paint ball hit audio
			audioSplat.Play();
			//destroy this object uting the waitTimeDestroy value
			Destroy (this.transform.gameObject, waitTimeDestroy);
		}

	}

}
