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


        //void Awake()
        //{
        //    if (photonView.IsMine)
        //    {
        //        PlayerManager.LocalPlayerInstance = this.gameObject;
        //    }

        //    DontDestroyOnLoad(this.gameObject);

        //}

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