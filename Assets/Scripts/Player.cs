using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float playerSpeed;
    [SerializeField]
    private float horizontalInput;
    private float verticalInput;
    [SerializeField]
    private GameObject ninjaStarPrefab;
    [SerializeField]
    private int _ninjaAmmo = 10;
    [SerializeField]
    private Animator playerAnim;

    [SerializeField]
    private bool isGrounded;
    
    [SerializeField]
    private float _jumpForce = 100f;
    [SerializeField]
    private float gravityModifier = 2f;
    private bool canDoubleJump;

    [SerializeField]
    private Rigidbody _rb;
    [SerializeField]
    private Transform _cameraPos;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponentInChildren<Animator>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager does not exist in Player Script");
        }

        _spawnManager.StartEnemySpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlayerFire();
        }

    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal") * playerSpeed;
        verticalInput = Input.GetAxis("Vertical") * playerSpeed;

        Vector3 direction = new Vector3(horizontalInput, _rb.velocity.y, verticalInput);
        _rb.velocity = direction;


        //Controls position character faces with animation
        if (horizontalInput > 0f)
        {
            
            float forwards = 0f;

            playerAnim.SetBool("Moving", true);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, forwards, transform.eulerAngles.z);
        }

        else if (horizontalInput <0f)
        {
            float backwards = 180f;

            playerAnim.SetBool("Moving", true);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, backwards, transform.eulerAngles.z);

        }

        else
        {
            playerAnim.SetBool("Moving", false);
        }

        //Contols player jump and double jump actions

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerAnim.SetBool("IsGrounded", false);
            playerAnim.SetFloat("VelocityY", 1 * Mathf.Sign(_rb.velocity.y));
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
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _cameraPos.position.x - 10f, 152.5f), transform.position.y, transform.position.z);

        //Restrict player movement so can't fall off sides
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z,-4.5f,5f));

    }

    void PlayerFire()
    {
        if (_ninjaAmmo > 0)
        {
            _ninjaAmmo--;
            playerAnim.SetTrigger("Attack1Trigger");
            Vector3 firePos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            Instantiate(ninjaStarPrefab, firePos, Quaternion.identity);
            playerAnim.SetTrigger("StarThrown");
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
            playerAnim.SetBool("IsGrounded", true);
            playerAnim.SetFloat("VelocityY", 0);
        }
    }
}
