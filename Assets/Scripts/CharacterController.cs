using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private KeyCode LeftKey;
    [SerializeField] private KeyCode RightKey;
    [SerializeField] private KeyCode DownKey;
    [SerializeField] private KeyCode UpKey;
    // [SerializeField] private Vector2 WalkRightVector;
    // [SerializeField] private Vector2 WalkLeftVector;
    [SerializeField] private Vector2 JumpVector;
    [SerializeField] private _Muscle[] muscles;
    [SerializeField] private Rigidbody2D rbRight;
    [SerializeField] private Rigidbody2D rbLeft;
    [SerializeField] private float MoveDelay;

    private bool Right;
    private bool Left;
    
    private float MoveDelayPointer;

    private PlayerStats _playerStats;
    public Rigidbody2D rb;
    public int jumpForce;

    public Rigidbody2D body;
    public LayerMask groundLayer;
    public bool onGround;
    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset;

    void Start()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    private void Update()
    {

        onGround = Physics2D.OverlapCircle(body.position + bottomOffset, collisionRadius, groundLayer);

        if (!Input.GetKey(DownKey))
        {
            foreach (var muscle in muscles)
            {
                muscle.ActivateMuscle();
            }
        }

        if (Input.GetKeyDown(RightKey))
        {
            Right = true;
        }
        if (Input.GetKeyDown(LeftKey))
        {
            Left = true;
        }
        if (Input.GetKeyUp(RightKey))
        {
            Right = false;
        }
        if (Input.GetKeyUp(LeftKey))
        {
            Left = false;
        }

        if (Input.GetKeyDown(UpKey) && onGround)
        {
            Jump();
        }

        while (Right && !Left && Time.time > MoveDelayPointer)
        {
            Invoke(nameof(Step1Right), 0f);
            Invoke(nameof(Step2Right), 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
        } 
        while (Left && !Right && Time.time > MoveDelayPointer)
        {
            Invoke(nameof(Step1Left), 0f);
            Invoke(nameof(Step2Left), 0.085f);
            MoveDelayPointer = Time.time + MoveDelay;
        }
    }

    private void Jump()
    {
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }

    public void Step1Right()
    {
        rbRight.AddForce(_playerStats.walkRightVector, ForceMode2D.Impulse);
        rbLeft.AddForce(_playerStats.walkRightVector * -0.5f, ForceMode2D.Impulse);
    }
    public void Step2Right()
    {
        rbLeft.AddForce(_playerStats.walkRightVector, ForceMode2D.Impulse);
        rbRight.AddForce(_playerStats.walkRightVector * -0.5f, ForceMode2D.Impulse);
    }
    public void Step1Left()
    {
        rbRight.AddForce(_playerStats.walkLeftVector, ForceMode2D.Impulse);
        rbLeft.AddForce(_playerStats.walkLeftVector * -0.5f, ForceMode2D.Impulse);
    }
    public void Step2Left()
    {
        rbLeft.AddForce(_playerStats.walkLeftVector, ForceMode2D.Impulse);
        rbRight.AddForce(_playerStats.walkLeftVector * -0.5f, ForceMode2D.Impulse);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset};

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
    }
}

[System.Serializable]
public class _Muscle
{
    public Rigidbody2D bone;
    public float restRotation;
    public float force;

    public void ActivateMuscle()
    {
        bone.MoveRotation(Mathf.LerpAngle(bone.rotation, restRotation, force * Time.deltaTime));
    }
}
