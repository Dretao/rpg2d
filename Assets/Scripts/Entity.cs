using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fX { get; private set; }

    #endregion

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDir;
    protected bool isKnocked;

    public Transform attackCheck;
    public float attackCheckRadius;
    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public int facingDir{ get; private set; } = 1;
    protected bool isFacingRight = true;

    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {
        fX = GetComponent<EntityFX>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update()
    {
        
    }

    #region Collision
    public virtual bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    public virtual bool isWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip 
    public virtual void Flip()
    {
        facingDir *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    public virtual void FlipController(float _x)
    {
        if(_x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(_x < 0 && isFacingRight)
        {
            Flip();
        }
    }
    #endregion

    #region Velocity
    public void SetVelocity(float _xVelocity,float _yVelocity)
    {
        if(isKnocked)
        {
            return;
        }
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }
    public void SetZeroVelocity()
    {
        rb.velocity = Vector2.zero;
        if(isKnocked)
        {
            return;
        }
    }
    #endregion

    public virtual void Damage()
    {
        fX.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
        Debug.Log(gameObject.name+"was damaged!");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDir.x * -facingDir, knockbackDir.y);
        yield return new WaitForSeconds(0.07f);//knockback duration
        isKnocked = false;
    }
}
