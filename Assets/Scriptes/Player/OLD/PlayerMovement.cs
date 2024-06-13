using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    //Move Speed
    public float initialMoveSpeed;
    public float moveSpeed;

    // Dash
    private bool isDashing;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    public float dashSpeed;
    public float dashTime;
    public float dashCoolDown;
    public float distanceBetweenImages;

    //Jump
    public float jumpForce;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisonLayers;

    public Rigidbody2D Rb;
    public Animator animator;

    private Vector3 velocity = Vector3.zero;

    public SpriteRenderer spriteRenderer;
    public bool isJumping;
    public bool isGrounding;

    private float horizontalMovement;
    private float verticalMovement;

    public Transform[] attacksPoints;

    public static PlayerMovement instance;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        moveSpeed = initialMoveSpeed;
    }
    void Update()
    {
        CheckInput();
        Flip(Rb.velocity.x);
        float characterVelocity = Mathf.Abs(Rb.velocity.x);

        animator.SetFloat("SpeedX", characterVelocity);
        animator.SetFloat("SpeedY", Rb.velocity.y);
        animator.SetBool("Onground", isGrounding);
        animator.SetBool("Dash", isDashing);
    }

    public void CheckInput()
    {
        if (Input.GetButtonDown("Jump") && isGrounding == true)
        {
            isJumping = true;
        }
        if (Input.GetKeyDown(KeyCode.R) && isGrounding == true)
        {
            if (Time.time >= (lastDash + dashCoolDown))
            {

                AttemptToDash();
                

            }
        }
        if (Input.GetKeyUp(KeyCode.R) && isGrounding == true)
        {
            StopDash();
        }

    }

    public void StopDash()
    {
        dashTimeLeft = 0;
        isDashing = false;
    }
    public void AttemptToDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;

    }



    void CheckDash(float _horizontalMovement)
    {
        if (isDashing)
        {
            if (dashTimeLeft > 0)
            {
                moveSpeed = dashSpeed;
                dashTimeLeft -= Time.deltaTime;

            }
            if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
            {
                PlayerAfterImagePool.Instance.GetFromPool();
                lastImageXpos = transform.position.x;
            }
        }
        if (dashTimeLeft <= 0 && isGrounding == true)
        {
            isDashing = false;
            moveSpeed = initialMoveSpeed;
        }

    }
    void FixedUpdate()
    {
        verticalMovement = Input.GetAxis("Vertical") * jumpForce * Time.deltaTime;
        isGrounding = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisonLayers);
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        MovePlayer(horizontalMovement);
        CheckDash(horizontalMovement);

    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, Rb.velocity.y);
        Rb.velocity = Vector3.SmoothDamp(Rb.velocity, targetVelocity, ref velocity, 0.05f);
        if (isJumping == true)
        {
            Rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }


    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
            foreach (Transform point in attacksPoints)
            {
                point.localPosition = new Vector3(Mathf.Abs(point.localPosition.x), point.localPosition.y, point.localPosition.z);
            }
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
            foreach (Transform point in attacksPoints)
            {
                point.localPosition = new Vector3(-Mathf.Abs(point.localPosition.x), point.localPosition.y, point.localPosition.z);
            }
        }
    }

    // Stop le mouvement du joueur
    public void Stop()
    {
        moveSpeed = 0f;
    }

    // Relance le mouvement du joueur
    public void Go()
    {
        moveSpeed = initialMoveSpeed;
    }



    public void ResetDashTime()
    {
        dashTime = 0.7f;
    }
}
