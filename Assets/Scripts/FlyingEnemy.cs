using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlyingEnemy : MonoBehaviour
{
    public Transform TargetPlayer;

    public float moveSpeed;
    public bool isRed;

    bool isStunned = false;

    void Update()
    {
        if (isRed)
        {
            moveSpeed = GameManager.instance.scoreRed * 0.1f + 1;
        } else
        {
            moveSpeed = GameManager.instance.scoreBlue * 0.1f + 1;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direcao = (TargetPlayer.position - transform.position).normalized;
        if (direcao.x < 0) GetComponent<SpriteRenderer>().flipX = true;
        else GetComponent<SpriteRenderer>().flipX = false;

        if (!isStunned)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPlayer.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedPlayer")
        {
            --GameManager.instance.scoreRed;
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(StunForSeconds(5f));
        }
        else if(collision.gameObject.tag == "BluePlayer")
        {
            --GameManager.instance.scoreBlue;
            GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(StunForSeconds(5f));
        }
    }

    IEnumerator StunForSeconds(float seconds)
    {
        isStunned = true;

        // Lógica de atordoamento (pode ser uma animação, efeitos sonoros, etc.)
        GetComponent<SpriteRenderer>().color = Color.grey;

        // Aguarde pelo tempo especificado
        yield return new WaitForSeconds(seconds);

        // Fim do atordoamento
        isStunned = false;
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
