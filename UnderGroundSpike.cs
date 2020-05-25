using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderGroundSpike : MonoBehaviour
{
    [SerializeField] int _damage = 25;
    int _speedUp = 20;
    int _speedDown = 10;
    [SerializeField] ParticleSystem _particleSystem = null;
    [SerializeField] GameObject _spike = null;
    bool _canGoUp = false;
    bool _canDie = false;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0,0,0);
        _spike = gameObject.transform.parent.gameObject;
        StartCoroutine(SpikeActionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (_canGoUp) {
            transform.Translate(0,0,-1 * _speedUp * Time.deltaTime);
        } else {
            transform.Translate(Vector3.zero);
        }

        if (!_canDie) {
            return;
        } else {
            transform.Translate(0,0,1 * _speedDown * Time.deltaTime);
        }
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Being ennemy;
        if (other.gameObject.tag == "Player") {
            ennemy = other.gameObject.GetComponent<Being>();
            ennemy.currentHp -= _damage;
        }
    }
    IEnumerator SpikeActionCoroutine() {
        yield return new WaitForSeconds(4f);
        _particleSystem.Stop();
        yield return new WaitForSeconds(0.5f);
        _canGoUp = true;
        yield return new WaitForSeconds(0.15f);
        _canGoUp = false;
        StartCoroutine(SpikeDeathCoroutine());
        
    }
    IEnumerator SpikeDeathCoroutine() {
        yield return new WaitForSeconds(3f);
        _canDie = true;
        yield return new WaitForSeconds(0.75f);
        Destroy(_spike);

    }
}
