using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    bool _movingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovingWall();
    }

    void MovingWall()
    {


        if (gameObject.CompareTag("Moving Wall"))
        {
            float _wallUp = 1.69f;
            float _wallDown = -0.7f;


            if (_movingUp)
            {

                if (transform.position.y < _wallUp)
                {
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                }

                else
                {
                    _movingUp = false;
                }
            }

            else if (!_movingUp)

            {
                if (transform.position.y > _wallDown)
                {
                    transform.Translate(Vector3.down * speed * Time.deltaTime);
                }

                else
                {
                    _movingUp = true;
                }
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ninja Stars"))
        {
            Destroy(other.gameObject);
        }
    }
}
