using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingScript : MonoBehaviour
{
    Vector3 originalPosition;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("Player"))
        {
            Invoke("DropPlatform", 0.3f);
            StartCoroutine(respawn());
            //Destroy(gameObject, 10f);
        }

    }

    void DropPlatform()
    {
        rb.isKinematic = false;
    }

    private IEnumerator respawn()
    {
        yield return new WaitForSeconds(5);
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        gameObject.transform.position = originalPosition;
        //Debug.Log("test");
    }
}
