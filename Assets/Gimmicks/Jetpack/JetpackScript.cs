using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetpackScript : MonoBehaviour
{
    public GameObject jetpackIcon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Player")
        {
            Destroy(gameObject);
            jetpackIcon.SetActive(true);
        }
       
    }
}
