using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    void Start()
    {
        transform.position = Vector2.zero;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "RedPlayer")
        {
            ++GameManager.instance.scoreRed;
            transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-4f, 4f));
        } 
        else if (collider.gameObject.tag == "BluePlayer")
        {
            ++GameManager.instance.scoreBlue;
            transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-4f, 4f));
        } 
        else
        {
            transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(-4f, 4f));
        }
    }
}
