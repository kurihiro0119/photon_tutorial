using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame{


    public class GameManager : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        public override void OnLeftRoom(){
            SceneManager.LoadScene(0);
        }

        public void LeaveRoom(){
            PhotonNetwork.LeaveRoom();
        }

        void LoadArena(){
            if(!PhotonNetwork.IsMasterClient){
                Debug.LogError("PhotonNetwork : Trying vel to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LoadLevel("Room for " + PhotonNetwork.CurrentRoom.PlayerCount);
        }

        public override void OnPlayerEnteredRoom(Player other){
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName);

            if(PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom ISMasterClient {0}", PhotonNetwork.IsMasterClient);

                LoadArena();
            }
        }

        public override void OnPlayerLeftRoom(Player other){
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName);

            if(PhotonNetwork.IsMasterClient){
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient);

                LoadArena();
            }
        }

    }
}