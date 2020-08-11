using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaStars : MonoBehaviour
{
    [SerializeField]
    private float _starSpeed = 5;


    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * _starSpeed);
    }

}
