using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundLayer;

    private bool isPlayerMoving;
    private float playerSpeed = 0.5f;
    private float rotationSpeed = 4f;
    private float jumpForce = 3f;
    private bool canJump;
    private float moveHorizontal, moveVertical;
    private float rotationY = 0;

    Rigidbody rb;
    Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rotationY = transform.localRotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        MoveAndRotate();
    }

    void PlayerMoveKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveHorizontal = -1;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveHorizontal = 0;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveHorizontal = 1;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveHorizontal = 0;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveVertical = 1;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            moveVertical = 0;
        }
    }

    void MoveAndRotate()
    {
        if (moveVertical != 0)
        {
            rb.MovePosition(transform.position + transform.forward * (moveVertical * playerSpeed));
        }

        rotationY += moveHorizontal * rotationSpeed;
        rb.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    void AnimatePlayer()
    {
        if (moveVertical != 0)
        {
            if (!isPlayerMoving)
            {
                // Checks if the Run animation is currently playing in the Base Layer (0)
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_ANIMATION))
                {
                    isPlayerMoving = true;
                    anim.SetTrigger(MyTags.RUN_TRIGGER);
                }
            }
        }
        else
        {
            if (isPlayerMoving)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(MyTags.RUN_ANIMATION))
                {
                    isPlayerMoving = false;
                    anim.SetTrigger(MyTags.STOP_TRIGGER);
                }
            }
        }
    }
}
