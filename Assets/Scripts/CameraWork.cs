using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{

    public class CameraWork : MonoBehaviour
    {
        #region Private Fields
        [Tooltip("The distance in the local x-z plane to the target")]
        [SerializeField]
        private float distance = 7.0f;

        [Tooltip("The height we want the camera to be above the target")]
        [SerializeField]
        float height = 3.0f;

        [Tooltip("Allow the camera to be offseted vertically from the target, for example giving more view pf the sceneray and less ground,")]
        [SerializeField]
        private Vector3 centerOffset = Vector3.zero;

        [Tooltip("Set this as false if a component of a prefab being instanciated by Photon Network, and manually call OnStartFollowing() when and if needed.")]
        [SerializeField]
        private bool followOnStart = false;

        [Tooltip("The Smoothing for the camera to follow tha target")]
        [SerializeField]
        private float smoothSpeed = 0.125f;

        //cached transform of the target
        Transform cameraTransform;

        //maintain a flan internally to reconnect if target is lost or camera is switched
        bool isFollowing;

        //Cache for camera offset
        Vector3 cameraOffset = Vector3.zero;

        #endregion

        #region MonoBehaviour Callbacks

        // Start is called before the first frame update
        void Start()
    {
            if(followOnStart)
            {
                OnStartFollwing();
            }
    }
        private void LateUpdate()
        {
            if (cameraTransform == null && isFollowing)
            {
                OnStartFollwing();
            }

            if (isFollowing)
            {
                Follow();
            }
        }


        private void OnStartFollwing()
        {
            cameraTransform = Camera.main.transform;
            isFollowing = true;

            Cut();
        }

        void Follow()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = Vector3.Lerp(cameraTransform.position, this.transform.position + this.transform.TransformVector(cameraOffset), smoothSpeed * Time.deltaTime);
        }

        void Cut()
        {
            cameraOffset.z = -distance;
            cameraOffset.y = height;

            cameraTransform.position = this.transform.position + this.transform.TransformVector(cameraOffset);
            cameraTransform.LookAt(this.transform.position + centerOffset);

        }
    }

        #endregion
}