using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedPlayerMovement : MonoBehaviour
{
    float moveSpeed = 4;
    float originalSpeed = 4;
    float dashSpeed = 12;

    private float blockerCooldownTime = 0.15f; // Tempo de cooldown em segundos
    private float lastBlockerActionTime; // Hora da última ação

    public GameObject blocker;
    public TMP_Text scoreText;

    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        transform.position = new Vector2(-4, 0);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    void Update()
    {
        #region Start Movement Locker
        if (GameManager.instance.gameState == "Gameplay")
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        #endregion

        #region Sets Animations
        if (rb.velocity.sqrMagnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        #endregion

        #region Score Text
        scoreText.text = GameManager.instance.scoreRed.ToString();
        #endregion

        #region Instantiate Blockers
        if (Input.GetKey(KeyCode.Space) && GameManager.instance.maxBlockers > GameManager.instance.activeRedBlockers)
        {
            if (Time.time - lastBlockerActionTime >= blockerCooldownTime)
            {
                // Realiza a ação aqui
                GameObject blockerTrail = Instantiate(blocker, transform.position, Quaternion.identity);

                // Atualiza o tempo da última ação
                lastBlockerActionTime = Time.time;
            }
        }
        #endregion
    }

    private void FixedUpdate()
    {
        #region Player Movement
        if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = true;
        } else if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponentInChildren<SpriteRenderer>().flipX = false;
        }

        rb.velocity = new Vector2 (Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);
        #endregion

        #region SideScreen Portals
        if (transform.position.x < -9.15f)
        {
            transform.position = new Vector2 (9.15f, transform.position.y);
        } else if (transform.position.x > 9.15f)
        {
            transform.position = new Vector2(-9.15f, transform.position.y);
        } else if (transform.position.y < -5.5f)
        {
            transform.position = new Vector2(transform.position.x, 5.5f);
        } else if (transform.position.y > 5.5f)
        {
            transform.position = new Vector2(transform.position.x, -5.5f);
        }
        #endregion
    }

    #region Transpasing Own Blockers
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RedBlocker")
        {
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            moveSpeed = dashSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        moveSpeed = originalSpeed;
    }
    #endregion
}
