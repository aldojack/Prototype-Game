using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float playerSpeed;
    private float horizontalInput;
    private float verticalInput;
    [SerializeField]
    private GameObject ninjaStarPrefab;
    [SerializeField]
    private int _ninjaAmmo = 10;

    private bool isGrounded = true;
    
    [SerializeField]
    private float _jumpForce = 100f;
    [SerializeField]
    private float gravityModifier = 2f;
    private bool canDoubleJump;

    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Transform _cameraPos;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //Delete comment
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerFire();
        }

    }

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal") * playerSpeed;
        verticalInput = Input.GetAxis("Vertical") * playerSpeed;

        Vector3 direction = new Vector3(horizontalInput, _rb.velocity.y, verticalInput);
        //_rb.AddForce(direction * Time.deltaTime * playerSpeed);

        _rb.velocity = direction;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            isGrounded = false;
            canDoubleJump = true;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            canDoubleJump = false;
        }

        //Restrict player movement based on camera position
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _cameraPos.position.x - 8f, 152.5f), transform.position.y, transform.position.z);

        //Restrict player movement so can't fall off sides
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z,-3f,5f));

    }

    void PlayerFire()
    {
        if (_ninjaAmmo > 0)
        {
            _ninjaAmmo--;
            Vector3 firePos = new Vector3(transform.position.x + 2f, transform.position.y, transform.position.z);
            Instantiate(ninjaStarPrefab, firePos, Quaternion.identity);
        }

        else
        {
            _ninjaAmmo = 0;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
