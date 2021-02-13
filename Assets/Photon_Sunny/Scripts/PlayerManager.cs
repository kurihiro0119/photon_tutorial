using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.MyCompany.SunnyLand
{

    public class PlayerManager : MonoBehaviourPunCallbacks
    {

        float speed;
        Rigidbody2D rigidbody2D;
        //ジャンプ力を設定します。
        float jumpPower = 300;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;


        void Awake()
        {
            // #Important
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instanciation when levels are synchronized
            if (photonView.IsMine)
            {
                LocalPlayerInstance = this.gameObject;
            }

            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

        }

        // Start is called before the first frame update
        void Start()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }


        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
            {
                return;
            }

            float x = Input.GetAxis("Horizontal");

            if (x == 0)
            {
                speed = 0;
            }
            else if (x > 0)
            {
                speed = 3;
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (x < 0)
            {
                speed = -3;
                transform.localScale = new Vector3(-1, 1, 1);
            }

            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);

            if (Input.GetKeyDown("space"))
            {
                Jump();
            }


        }

        void Jump()
        {
            rigidbody2D.AddForce(Vector2.up * jumpPower);
        }

    }
}