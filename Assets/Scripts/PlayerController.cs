using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] GameObject focalPoint;
    [SerializeField] bool hasPowerup = false;
    [SerializeField] GameObject powerupIndicator;

    Rigidbody _rigidbody;
    [SerializeField] float powerUpStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    { 
        float forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f,0);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerUpCountdown());
        }
    }

    IEnumerator PowerUpCountdown()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    //Used instead of OnTriggerEnter when dealing with Physics
    //Here we're dealing with bouncing with greater force when you have a powerup
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 towardEnemy = (collision.gameObject.transform.position - transform.position);
            enemyRigidbody.AddForce(towardEnemy * powerUpStrength, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.collider.name + ".");
        }
    }
}
