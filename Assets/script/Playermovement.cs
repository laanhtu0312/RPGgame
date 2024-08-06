using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int maxHealth = 50;
    public float attackCooldown = 1f;
    public GameObject enemyPrefab;

    private int currentHealth;
    private bool isAttacking = false;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        // Đảm bảo các animation không tự động chạy khi khởi động game
        animator.SetBool("run", false);
        animator.SetBool("idle", true);
        animator.ResetTrigger("attack");
        animator.ResetTrigger("died");
    }

    void Update()
    {
        // Di chuyển nhân vật
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (!isAttacking && currentHealth > 0)
        {
            if (movement != Vector2.zero)
            {
                animator.SetBool("run", true);
                animator.SetBool("idle", false);
            }
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("idle", true);
            }
        }

        // Lật nhân vật dựa trên hướng di chuyển
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Tấn công
        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking && currentHealth > 0)
        {
            StartCoroutine(Attack());
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật trong FixedUpdate
        if (!isAttacking && currentHealth > 0)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetTrigger("attack");

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            // Chạy trạng thái animation khi hết máu
            animator.SetTrigger("died");
            // Vô hiệu hóa điều khiển nhân vật
            rb.velocity = Vector2.zero;
            enabled = false;
        }
    }

    // Hàm này để reset máu mỗi khi nhân vật bắt đầu hoặc hồi sinh
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isAttacking)
        {
            collision.GetComponent<EnemyController>().TakeDamage(1);
        }
    }
}