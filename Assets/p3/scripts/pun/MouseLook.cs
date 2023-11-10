using System;
using UnityEngine;

/*
 * The version of the Mouse Look script is derived from the UNITY version with the 1st person player standard asset
 * it has been modified to work only in the editor playback when testing GVR on a computer
 * this will not function in the Android.IOS playback
 */

public class MouseLook : MonoBehaviour
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;
    public Transform theCamera;
    public Transform thePlayer;
    public bool isStartUpScript = false;


    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private bool m_cursorIsLocked = true;
    private bool doMouselook = false;


    void Start()
    {
        if (isStartUpScript)
        {
            setCameraStartUp();
        }
        m_CharacterTargetRot = thePlayer.localRotation;
    }

    public void setCamera(Transform camera)
    {
        theCamera = camera;
        m_CameraTargetRot = theCamera.localRotation;
        doMouselook = true;
    }

    void setCameraStartUp()
    {
        m_CameraTargetRot = theCamera.localRotation;
        doMouselook = isStartUpScript;
    }

    void LateUpdate()
    {
        if(!doMouselook)
        {
            return;
        }
#if UNITY_EDITOR

        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
            float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

            if (clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

            if (smooth)
            {
                thePlayer.localRotation = Quaternion.Slerp(thePlayer.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                theCamera.localRotation = Quaternion.Slerp(theCamera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                thePlayer.localRotation = m_CharacterTargetRot;
                theCamera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
#endif
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if(!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

        angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}
