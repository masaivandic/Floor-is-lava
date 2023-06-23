using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float jumpPower;
    public float jumpLimit = 1;
    public bool canJump = false;

    PhotonView photonView;

    public Transform orientation;

    float InputHorizontal;
    float InputVertical;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        TextMeshProUGUI YouSign = transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        photonView = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        //YouSign.text = "You";
    }

    private void Update()
    {
        if(PhotonNetwork.PlayerList.Length < 2)
        {
            Destroy(gameObject);
        }
        if (photonView.IsMine)
        {
            Jump();
            InputHorizontal = Input.GetAxisRaw("Horizontal");
            InputVertical = Input.GetAxisRaw("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if(photonView.IsMine)
        {
            Movement();
        }
    }
    private void Movement()
    {
        if(photonView.IsMine)
        {
            if (InputHorizontal == 0 && InputVertical == 0)
                return;
            moveDirection = orientation.forward * InputVertical + orientation.right * InputHorizontal;
            rb.AddForce(moveDirection.normalized * speed, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Jump")
        {
            canJump = true;
			rb.velocity = new Vector3(0, 0, 0);
            rb.AddForce(transform.position, ForceMode.Impulse);
		}
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            canJump = false;
            rb.velocity = new Vector3(0, jumpPower, 0);
        }
    }




}
