using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{   ParticleSystem _particle;
    Vector3 movement;
    int damage = 2;
    int _speed = 5;
    float lifeTime = 10f;
    string _playerTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        StartCoroutine(RandomMovement());
        StartCoroutine(Die());
    }

    // Update is called once per frame
    void Update()
    {  
        transform.Translate(movement *_speed * Time.deltaTime);
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {   Being ennemy;
         if (other.gameObject.tag == _playerTag) {
            ennemy = other.gameObject.GetComponent<Being>();
            ennemy.currentHp -=damage;         
        }
    }
    IEnumerator RandomMovement () {
        while(true) {
            float randomX = Random.Range(-1f,1f);
            float randomZ = Random.Range(-1f,1f);
            movement = new Vector3(randomX,0,randomZ);
            yield return new WaitForSeconds(3f);
        }
    }
    void RandomBehaviour() {
    }
    IEnumerator Die () {
        yield return new WaitForSeconds(lifeTime);
        _particle.Stop();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
