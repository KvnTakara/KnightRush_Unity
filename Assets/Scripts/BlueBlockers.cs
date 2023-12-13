using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBlockers : MonoBehaviour
{
    void Start()
    {
        ++GameManager.instance.activeBlueBlockers;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<CircleCollider2D>().isTrigger = false;

        yield return new WaitForSeconds(8f);

        --GameManager.instance.activeBlueBlockers;
        Destroy(gameObject);
    }
}
