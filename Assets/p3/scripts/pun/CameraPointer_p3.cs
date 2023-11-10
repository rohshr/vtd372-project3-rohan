//-----------------------------------------------------------------------
// <copyright file="CameraPointer.cs" company="Google LLC">
// Copyright 2020 Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

/*
 * CameraPointer_p3.cs
 * adapted from CameraPointer.cs
 * used for Menu system
 * Add to camera in startup scene
 * Will trigger when looking at Character "buttons" - these can be quads with images
 */

using System.Collections;
using UnityEngine;

/// <summary>
/// Sends messages to gazed GameObject.
/// </summary>
public class CameraPointer_p3 : MonoBehaviour
{
    //private const float _maxDistance = 10;
    public float _maxDistance = 20;
    private GameObject _gazedAtObject = null;

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit");
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter");
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit");
            _gazedAtObject = null;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton16))
        {
            _gazedAtObject?.SendMessage("SelectCharacter");
        }

        //// Checks for screen touches.
        //if (Google.XR.Cardboard.Api.IsTriggerPressed)
        //{
        //    _gazedAtObject?.SendMessage("OnPointerClick");
        //}
    }
}
