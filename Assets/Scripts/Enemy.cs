using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody _rigidBody;
    GameObject _player;

    [SerializeField] float moveSpeed = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (_player.transform.position - transform.position).normalized;
        _rigidBody.AddForce(lookDirection * moveSpeed);

        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
