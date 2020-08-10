using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Clamp : MonoBehaviour
{ 
    [SerializeField]
    private Transform targetToFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, transform.position.x, 142f), 
            transform.position.y, transform.position.z);  
    }
}
