using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2d;
    AudioSource audioSouce;

    //Audio
    public AudioClip[] swordSwingSounds;
    public AudioClip castSound;

    //Health
    public int health { get { return currentHealth; } }
    public HealthBar healthBar;
    public static int maxHealth = 100;
    public static int currentHealth;
    public static int livesCount = 3;
    public GameObject[] lives;

    //Casting
    GameObject shotType;
    public GameObject shotPrefab;
    public GameObject fireShotPrefab;
    public GameObject iceShotPrefab;
    public static int castDamage = 1;
    public static float castRate = 2f;
    public static float castSpeed = 300f;
    public static float castSize = 1f;
    public static int burnDamage = 1;
    public static float iceLength = 1f;
    public static int extraShot = 0;
    public static bool backShot = false;
    public static bool fireShot = false;
    public static bool iceShot = false;
    float nextCastTime = 0f;

    //Sword
    public static int swordDamage = 1;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public static float attackRate = 2f;
    float nextAttackTime = 0f;

    //Being hit
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    bool isDead;

    //Movement
    public static float speed = 5.0f;
    float horizontal;
    float vertical;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 backDirection = new Vector2(-1, 0);
    public Transform upDirection;
    public Transform downDirection;

    bool isInputEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        LoadHearts();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        audioSouce = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        shotType = shotPrefab;
        if (iceShot)
            shotType = iceShotPrefab;
        else if (fireShot)
            shotType = fireShotPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInputEnabled)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f))
        {
            lookDirection.Set(move.x, 0);
            lookDirection.Normalize();
            backDirection.Set(-move.x, 0);
            backDirection.Normalize();
        }

        animator.SetFloat("Horizontal", lookDirection.x);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            isInputEnabled = false;
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
                isInputEnabled = true;
            }
        }

        if (Time.time >= nextCastTime)
        {
            if (Input.GetKeyDown(KeyCode.C) && isInputEnabled)
            {
                Cast();
                nextCastTime = Time.time + 1f / castRate;
            }
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isInputEnabled)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void FixedUpdate()
    {
        if (isInputEnabled)
        {
            Vector2 position = rigidbody2d.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;

            rigidbody2d.MovePosition(position);
        }
    }

    void LoadHearts()
    {
        for(int i = 0; i < livesCount; i++)
        {
            lives[i].SetActive(true);
        }

        for(int i = livesCount; i < lives.Length; i++)
        {
            lives[i].SetActive(false);
        }
    }

    void Cast()
    {
        audioSouce.PlayOneShot(castSound);
        animator.SetTrigger("Cast");

        if (extraShot == 0 || extraShot == 2)
        {
            GameObject normalShot = Instantiate(shotType, rigidbody2d.position + Vector2.down * .5f, Quaternion.identity);
            Projectile projectile = normalShot.GetComponent<Projectile>();
            
            normalShot.transform.localScale = new Vector3(castSize, castSize, castSize);

            projectile.Launch(lookDirection, castSpeed);
        }

        // NEED TO FIX THIS PART EVENTUALLY
        if (extraShot >= 1)
        {
            GameObject upShot = Instantiate(shotType, rigidbody2d.position + Vector2.down * .5f, upDirection.rotation);
            GameObject downShot = Instantiate(shotType, rigidbody2d.position + Vector2.down * .5f, downDirection.rotation);

            upShot.transform.localScale = new Vector3(castSize, castSize, castSize);
            downShot.transform.localScale = new Vector3(castSize, castSize, castSize);

            Rigidbody2D bulletup = upShot.GetComponent<Rigidbody2D>();
            Rigidbody2D bulletdown = downShot.GetComponent<Rigidbody2D>();

            Vector3 pos = bulletup.transform.forward * (castSpeed / 30);
            bulletup.velocity = pos;

            Vector3 pos2 = bulletdown.transform.forward * (castSpeed / 30);
            bulletdown.velocity = pos2;
        }

        if (backShot)
        {
            GameObject backShot = Instantiate(shotType, rigidbody2d.position + Vector2.down * .5f, Quaternion.identity);
            Projectile projectile3 = backShot.GetComponent<Projectile>();

            backShot.transform.localScale = new Vector3(castSize, castSize, castSize);

            projectile3.Launch(backDirection, castSpeed);
        }
    }

    void Attack()
    {
        int index = UnityEngine.Random.Range(0, swordSwingSounds.Length);
        audioSouce.PlayOneShot(swordSwingSounds[index]);
        animator.SetTrigger("Attack");

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Hit them
        foreach(Collider2D enemy in hitEnemies)
        {
            Goblin goblin = enemy.GetComponent<Goblin>();
            if(goblin != null) {
                enemy.GetComponent<Goblin>().ChangeHealth(swordDamage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void ChangeHealth(int amount)
    {
        if (isInvincible)
            return;
        if (isDead)
            return;

        currentHealth -= amount;
        healthBar.SetHealth(currentHealth);

        // Checks if dead
        if (currentHealth <= 0)
        {
            Death();
            isInputEnabled = false;
            isDead = true;
            return;
        }

        animator.SetTrigger("Hit");
        isInvincible = true;
        invincibleTimer = timeInvincible;
    }

    void Death()
    {
        animator.SetTrigger("Death");
        livesCount--;

        if (livesCount > 0)
        {
            StartCoroutine("Reset");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(1f);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
