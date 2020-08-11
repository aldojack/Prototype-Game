using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyFollows();
    }

    void EnemyFollows()
    {
        transform.LookAt(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ninja Stars"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
