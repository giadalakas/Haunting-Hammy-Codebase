using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessedPlatform : MonoBehaviour
{
    public Transform pointA, pointB;
    public int Speed;
    Vector2 targetPos;

    void Start()
    {
        targetPos = pointB.position;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, pointA.position) < 0.1f) targetPos = pointB.position;

        if (Vector2.Distance(transform.position, pointB.position) < 0.1f) targetPos = pointA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

}