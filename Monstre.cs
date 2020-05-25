using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstre : Being
{
    public MonsterSpawner mySpawner = null;
  
    Vector3 target;
    Vector3 myPos;
    Vector3 deltaPos;
    [SerializeField] Collider _closeCollider;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = totalHp;
        healthBar.value = currentHp;
        _animator = GetComponent<Animator>();
    }
    void Update() {
        healthBar.GetComponentInParent<Canvas>().transform.Rotate(Vector3.zero);
    }
    protected override void SetPhase() {
        int hpPercent = (_currentHp * 100 /_totalHp);
        if (hpPercent <= 0 ) {
            //Dead anim
            //remove me from the list;
            if (mySpawner) {
                mySpawner.RemoveFromList(this);
            }
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        float distance = Vector3.Distance(target,myPos);
        if (other.gameObject.tag == "Player") {
            _animator.SetBool("Walk",true);
            target = other.transform.position;
            myPos = transform.position;        
            //transform.Translate((target-myPos).normalized * 2*Time.deltaTime);
        }
    }
    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        myPos = transform.position;
        float distance = Vector3.Distance(target,myPos);
        if (other.gameObject.tag == "Player"){
            target = other.transform.position;
            Vector3 targetPostition = new Vector3( target.x, this.transform.position.y, target.z ) ;
            this.transform.LookAt( targetPostition ) ;
            if( distance < 6) {
                _animator.SetBool("Walk",false);
                _animator.SetTrigger("Attack");
            } else
            {
                _animator.SetBool("Walk",true);
             
                //_animator.ResetTrigger("Attack");
            }
        }
       
    }
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            _animator.SetBool("Walk",false);
            transform.Translate(Vector3.zero);
        }
    }
}
