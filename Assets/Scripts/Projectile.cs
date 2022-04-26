using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public static GameObject shot;
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 2000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Goblin goblin = other.collider.GetComponent<Goblin>();
        if (goblin != null)
        {
            goblin.ChangeHealth(Player.castDamage);

            if (Player.iceShot)
            {
                goblin.GetFrozen(); // Could possibly put this in Enemy class to apply to all enemies
            }

            if (Player.fireShot)
            {
                goblin.BurnDamage(); // Could possibly put this in Enemy class to apply to all enemies
            }
        }

        Destroy(gameObject);

        
    }
}
