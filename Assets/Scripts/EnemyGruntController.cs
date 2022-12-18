using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entity;

public class EnemyGruntController : MonoBehaviour
{

    public static bool IsActive;
    
    private PlayerManager playerReference;

    private float movementSpeed = 0.01f;

    private float attackRate = 0.5f;
    private float attackCooldown = 0f;

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start() {
        playerReference = FindObjectOfType<PlayerManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        attackCooldown = 1 / attackRate;
        HealthEntity.OnDeath += OnDeath;
    }

    private void OnDeath(int dmg, HealthEntity entity) {
        if (entity == null || this == null) return;  
        if (entity.gameObject.Equals(this.gameObject)) {
            GameManager.Instance.IncrementGruntsDefeated();
            GetComponent<Collider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attackCooldown += Time.deltaTime;
        if (!IsActive) return;
        _rigidbody.MovePosition(Vector2.MoveTowards(this.transform.position, playerReference.transform.position, movementSpeed));
    }

    void OnCollisionStay2D(Collision2D collision) {
        if (attackCooldown < (1 / attackRate) || !IsActive) return;
        
        // Damage player
        if (collision.gameObject.Equals(playerReference.gameObject)) {
            playerReference.GetComponent<Entity.HealthEntity>().TakeDamage(1); // TODO: adjust
            attackCooldown = 0;
        }

    }
}
