using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    [SerializeField]
    private Animator enemyAnim;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponentInChildren<Animator>();    
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Collided with floor");
            enemyAnim.SetBool("Moving", false);
            enemyAnim.SetBool("Grounded", true);
        }
    }
}
