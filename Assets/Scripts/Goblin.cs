using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Goblin : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] hitSounds;
    public AudioClip deathSound;

    public HealthBar healthBar;
    public int health { get { return currentHealth; } }
    public int maxHealth = 5;
    public static float speed = 3.0f;
    public static int damage = 10;

    int currentHealth;
    bool canMove = true;
    float minDistance = 1.5f;
    float range;
    float attackCooldown = 1f;
    int value = 1;

    Transform target;
    Animator animator;
    Vector2 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        GetLookDirection();
        animator.SetFloat("Horizontal", lookDirection.x);
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;
        GetLookDirection();
        animator.SetFloat("Horizontal", lookDirection.x);

        range = Vector2.Distance(transform.position, target.position);

        if (range > minDistance && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (range <= minDistance && attackCooldown <= 0.1f)
        {
            Attack();
            attackCooldown = 1f;
        }
    }

    void Attack()
    {
        int attack = Random.Range(1, 3);

        if (attack == 1)
        {
            animator.SetTrigger("Attack1");
        }
        else if (attack == 2)
        {
            animator.SetTrigger("Attack2");
        }
    }

    void GetLookDirection()
    {
        if(target.position.x < transform.position.x)
        {
            lookDirection.Set(-1, 0);
        }
        else if(target.position.x > transform.position.x)
        {
            lookDirection.Set(1, 0);
        }
    }

    public void ChangeHealth(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        // Checks if dead
        if (currentHealth <= 0)
        {
            Death();
            return;
        }

        int index = UnityEngine.Random.Range(0, hitSounds.Length);
        audioSource.PlayOneShot(hitSounds[index]);
        animator.SetTrigger("Hit");
    }

    public void BurnDamage()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255f / 255f, 102f / 255f, 102f / 255f);
        StartCoroutine("BurnTick");

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    IEnumerator BurnTick()
    {
        yield return new WaitForSeconds(.5f);
        currentHealth -= Player.burnDamage;
        healthBar.SetHealth(currentHealth);
        yield return new WaitForSeconds(.5f);
        currentHealth -= Player.burnDamage;
        healthBar.SetHealth(currentHealth);
        yield return new WaitForSeconds(.5f);
        currentHealth -= Player.burnDamage;
        healthBar.SetHealth(currentHealth);

        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void GetFrozen()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(51f / 255f, 255f / 255f, 255f / 255f);
        canMove = false;

        StartCoroutine("Frozen");
    }

    IEnumerator Frozen()
    {
        yield return new WaitForSeconds(Player.iceLength);
        canMove = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    void Death()
    {
        audioSource.PlayOneShot(deathSound);
        animator.SetTrigger("Death");
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        healthBar.gameObject.SetActive(false);
        AddScore();
        StartCoroutine("RemoveEnemy");
    }

    private void AddScore()
    {
        Money money = GameObject.Find("GameManager").GetComponent<Money>();
        money.GainMoney(value);
    }

    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(animator.gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.ChangeHealth(damage);
        }
    }
}
