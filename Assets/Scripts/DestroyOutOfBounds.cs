using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField]
    private Transform _cameraOutOfBounds;
    [SerializeField]
    private float _offSet = 22;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OutOfBounds();
    }

    void OutOfBounds()
    {
        //Destory level as moves out of camera view
        if (gameObject.transform.position.x + _offSet < _cameraOutOfBounds.position.x)
        {
            Destroy(this.gameObject);
        }
    }
}
