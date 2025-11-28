using Player;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyAi : MonoBehaviour
{
    [SerializeField]private Transform player;
    
    [SerializeField]private float chaseSpeed = 2f;
    [SerializeField]private float jumpForce = 2f;
    [SerializeField] private LayerMask _groundLayer;
    private float direction;
    
    private Rigidbody2D rb;
    GroundCheck _groundCheck;
    
    private bool isGrounded;
    private bool shouldJump;
    private bool isPlayerAbove;
    private bool isFacingRight;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponent<GroundCheck>();
    }
    
    private void Update()
    {
        direction = Mathf.Sign(player.position.x - transform.position.x );
        
        isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 10f , 1 << player.gameObject.layer);

        Chase();
        Flip();
        
    }

     private void FixedUpdate()
    {
        if (_groundCheck.IsGrounded())
            isGrounded = true;
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
                
        RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 10f, _groundLayer);

            if (groundInFront && gapAhead.collider)
            {
                
                shouldJump = true;
            }
         
            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true; 
            }
            
    }
    
    private void Jump()
    {   
        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            
            Vector2 jumpVector = new Vector2(directionToPlayer.x, 1f).normalized * jumpForce;
            
            rb.AddForce(new Vector2(jumpVector.x, jumpForce), ForceMode2D.Impulse);
        }
        
    }
    
    private void Flip()
    {
        if (isFacingRight && direction > 0 ||  isFacingRight && direction < 0)
        {
            Debug.Log("Flip");
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }
    
}
