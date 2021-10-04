using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using Photon.Pun;
using Photon.Realtime;


namespace MRTK.Tutorials.MultiUserCapabilities
{
    public class GameManager : MonoBehaviourPunCallbacks
    {

        #region Private Methods

        void LoadArena()
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
            }
            Debug.LogFormat("PhotonNetwork : Loading Level : {0}", Launcher.launcher_static.getRoomNumber());
            PhotonNetwork.LoadLevel("Room for " + Launcher.launcher_static.getRoomNumber());
        }


        #endregion

        #region Photon Callbacks

        public override void OnPlayerEnteredRoom(Player other)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }


        public override void OnPlayerLeftRoom(Player other)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom


                LoadArena();
            }
        }

        /// <summary>
        /// Called when the local player left the room. We need to load the launcher scene.
        /// </summary>
        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(0);
        }


        #endregion


        #region Public Methods

        [Tooltip("The prefab to use for representing the player")]
        public GameObject playerPrefab;

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }


        #endregion
    }
}