using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    int _damage = 10;
    float _speed = 45;
    Being _target;
    public Being target {
        get{return _target;}
        set{_target = value;}
    }
    Being _parent;
    public Being parent {
        get{return _parent;}
        set{_parent = value;}
    }
    Vector3 _posToGo = Vector3.zero;
    Vector3 _pos = new Vector3();
    bool isMoving = false;
    Rigidbody rb;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        StartCoroutine(SpikeLauchCoroutine());
        transform.parent = null;
        _pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) {
            transform.LookAt(_target.transform);
             _posToGo = _target.transform.position;
        }
         else {
            SpikeMovement();
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
             Destroy(gameObject);
        }
       
    }
    void SpikeMovement() {
        //ransform.position = Vector3.Lerp(transform.position, _posToGo, _speed * Time.deltaTime);
        transform.Translate((_posToGo - _pos).normalized * _speed * Time.deltaTime, Space.World);
        //rb.AddForce ((_posToGo - transform.position).normalized * _speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, _posToGo, 2 * Time.deltaTime);

    }

    IEnumerator SpikeLauchCoroutine() {
        yield return new WaitForSeconds(3f);
        isMoving = true;
    }
}
