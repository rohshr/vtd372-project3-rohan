/*
	* updated from tutorial file
	*updated by cb cleveley
	*August 2018
	*for use in VTD Classes - Please update and modify as necessary
	*add to a character Prefab
*/

using UnityEngine;
using System.Collections;

using Photon.Realtime;
using Photon.Pun;

namespace Assets.p3.scripts.pun
{

    public class VTDPlayer_p3_PUN : MonoBehaviour
    {

        protected Animator animator;
        [Tooltip("DONT ALTER")]
        public Transform characterTransform;
        [Tooltip("Name of the Animator Controller Parameter used to start and stop walking - this is a script value")]
        public string walk;
        //public Transform pivot; //pivot object in player
        public float rotationSnap = 5f;
        public float lerpSpeed = 0.1f;

        private float currentYRotation;

        //photon variables
        protected PhotonView m_PhotonView;
        //PhotonTransformView m_TransformView;
        private Vector3 m_LastPosition;

        // Use this for initialization
        void Start()
        {
            //Photon Setups
            m_PhotonView = GetComponent<PhotonView>();
            //m_TransformView = GetComponent<PhotonTransformView>();
            //common setups
            animator = GetComponent<Animator>();
            characterTransform = gameObject.transform;
            
            currentYRotation = characterTransform.eulerAngles.y;
        }

        // Update is called once per frame
        void Update()
        {
            //Photon condition - only accept if m_PhotonView.isMine == true - or the local character
            if (m_PhotonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return; //escape from the Update function if the PhotonView is NOT mine (local)
            }

            if (animator)
            {
                //Debug.Log ("pivot: " + pivot.rotation.ToString ());
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Vertical") > 0.2f) animator.SetBool(walk, true);
                if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetAxis("Vertical") < 0.2f) animator.SetBool(walk, false);
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") < -0.2f)
                {
                    //currentYRotation = currentYRotation - rotationSnap;
                    updateRotation(-rotationSnap);
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") > 0.2f)
                {
                    //currentYRotation = currentYRotation + rotationSnap;
                    updateRotation(rotationSnap);
                }

               
            }
        }
        void updateRotation(float val)
        {
            currentYRotation = currentYRotation + val;
            Quaternion goTO;
            goTO = Quaternion.Euler(0, currentYRotation, 0);
            characterTransform.rotation = Quaternion.Lerp(characterTransform.rotation, goTO, Time.time * lerpSpeed);
        }
    }
}
