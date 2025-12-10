using System.Collections;
using Player;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    [SerializeField]private Transform player;
    
    [SerializeField]private float chaseSpeed = 2f;
    [SerializeField]private float jumpForce = 2f;
    [SerializeField] private float jumpCoolDown = 2f;
    [SerializeField] private LayerMask _groundLayer;
    private float direction;
    
    private Rigidbody2D rb;
    GroundCheck _groundCheck;
    
    private bool isGrounded;
    private bool canJump;
    private bool shouldJump;
    private bool isPlayerAbove;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<GroundCheck>();
        canJump = true;
        AssignPlayer();
    }

    public void AssignPlayer()
    {
        player = PlayerGetter.GetPlayerPosition();
    }
    
    private void Update()
    {
        direction = Mathf.Sign(player.position.x - transform.position.x );
        
        isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f , 1 << player.gameObject.layer);

        Chase();
    }

     private void FixedUpdate()
    {
        if (_groundCheck.IsGrounded())
        {
            StartCoroutine(_JumpCoolDown());
            isGrounded = true;
        }
        else
            isGrounded = false;        
        
        Jump();
    }
     
    private void Chase()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);
        }
        
        RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, _groundLayer);
                
        RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0 , 0), Vector2.down, 2f, _groundLayer);
                
        RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, _groundLayer);

      
        if (!groundInFront && !gapAhead.collider && canJump)
        {
            shouldJump = true;
        }
         
        else if (isPlayerAbove && platformAbove.collider && canJump)
        {
            shouldJump = true; 
        }
            
    }
    
    private void Jump()
    {   
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            
            canJump = false;
            
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            
            Vector2 jumpVector = new Vector2(directionToPlayer.x, 1f).normalized * jumpForce;
            
            rb.AddForce(new Vector2(jumpVector.x, jumpForce), ForceMode2D.Impulse);
        }
        
    }

    private IEnumerator _JumpCoolDown ()
    {
        yield return new WaitForSeconds(jumpCoolDown);
        
        canJump = true;
    }
    
}
