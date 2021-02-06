using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Com.MyCompany.MyGame
{
    public class PlayerManager : MonoBehaviourPunCallbacks
    {
        #region Private Fields
        [Tooltip("The Beams Gameobject to control")]
        [SerializeField]
        private GameObject beams;

        [Tooltip("The current Health of our player")]
        public float Health = 1f;

        //True, when the user is firing
        bool IsFiring;

        #endregion

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){

            if(stream.IsWriting){
                stream.SendNext(IsFiring);
            }
            else{
                this.IsFiring = (bool)stream.ReceiveNext();
            }
        }

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        private void Awake()
        {
            if (beams == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }
        }

        void Start(){
            Debug.Log("Start!!!!!!!!!");
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();

            if(_cameraWork != null){
                if(photonView.IsMine){
                    Debug.Log("photon is mine!!!!!!!!");
                    _cameraWork.OnStartFollowing();
                }
                else{
                    Debug.Log("isMine ではない");
                }
            }
            else{
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {
            if(photonView.IsMine){
                ProcessInputs();
            }

            //trigger Beams active state
            if (beams != null && IsFiring != beams.activeInHierarchy)
            {
                beams.SetActive(IsFiring);
            }

            if (photonView.IsMine)
            {
                ProcessInputs();
                if(Health <= 0f)
                {
                    GameManager.Instance.LeaveRoom();
                }
            }
        }
        #endregion

        void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (IsFiring)
                {
                    IsFiring = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }

            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!photonView.IsMine)
            {
                return;
            }
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f * Time.deltaTime;
        }
    }

}