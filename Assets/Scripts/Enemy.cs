using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Animator enemyAnim;
    private NavMeshAgent _navMesh;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();
        _navMesh = GetComponentInChildren<NavMeshAgent>();
        _navMesh.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ninja Stars"))
        {
            Debug.Log("Hit with ninja star");
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            enemyAnim.SetBool("Moving", false);
            enemyAnim.SetBool("Grounded", true);
            StartCoroutine(EnableNavMesh());
            //Debug.Break();
            //_navMesh.enabled = true;
        }
    }

    IEnumerator EnableNavMesh()
    {
        yield return new WaitForSeconds(1f);
        _navMesh.enabled = true;
    }
}
