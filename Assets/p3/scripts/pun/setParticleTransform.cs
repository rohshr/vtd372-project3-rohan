using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * used to detect a collision of the splat objects
 * align the particles with the normal of the surface it collided with
 * play the splat particle system that leaves a paint splat on the surface
 * Add to the Prefab containing the two particle systems
*/

public class setParticleTransform : MonoBehaviour {
	[Tooltip("Choose the Particle system to leaves a paint splat")]
	public ParticleSystem splatArtifactParticles;

	void OnCollisionEnter(Collision collision)
	{
		//get first hit point
		ContactPoint hitPoint = collision.contacts [0];
		//align particles to surface
		splatArtifactParticles.transform.rotation = Quaternion.LookRotation (hitPoint.normal,Vector3.up);
		//play particles
		splatArtifactParticles.Play ();
	}
}
