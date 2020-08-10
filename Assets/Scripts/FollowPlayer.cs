using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    private Vector3 _offset = new Vector3(-4f, 12.36f, -16.92f);
    public Vector3 tempLockPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = _player.transform.position + _offset;

        //if (transform.position.x >= 4f)

        //{
        //    transform.position = new Vector3(Mathf.Clamp(transform.position.x, transform.position.x, 142f), transform.position.y, transform.position.z);
        //}

        //else
        //{
        //    transform.position = _player.transform.position + _offset;

        //}

        //tempLockPos = new Vector3(1.5f, transform.position.y, transform.position.z);
        //if ( tempLockPos.x <= transform.position.x )
        //{
        //    //tempLockPos.x = transform.position.x;

        //    tempLockPos = new Vector3(Mathf.Clamp(transform.position.x, transform.position.x, 142f), transform.position.y, transform.position.z);
        //    tempLockPos = transform.position;
        //    //Debug.Break();

        //}
        //else
        //{
        //    transform.position = _player.transform.position + _offset;
        //}


    }
}
