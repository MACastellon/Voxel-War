using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fireball : MonoBehaviour
{
    int _speed = 30;
    Vector3 _target;
    public Vector3 target {
        get {return _target;}
        set {_target = value;}
    }
    
    [SerializeField ]int _damage = 50;
    int _level = 1;
    string _monsterTag = "Monster";
     string _playerTag = "Player";
    Transform parent;
    
    public int level {
        get {return _level;}
        set {
//            level = value;
            
        }
    }
    public int damage {
        get{return _damage;}
        set
        {
            _damage = ScaledDamage(value);
        }
    }
    //Scale the damage
    int ScaledDamage (int lvl) 
    {
        _damage = _damage * lvl;
        return _damage;
    }
    Vector3 _pos;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.root;
        _pos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(this.transform.position, target ,1 * Time.deltaTime); 
        transform.Translate((target-_pos).normalized * _speed*Time.deltaTime);
    }
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Being ennemy;
        if (other.gameObject.tag == _playerTag) {
            ennemy = other.gameObject.GetComponent<Being>();
            ennemy.currentHp -=damage;
            Destroy(gameObject);
        
           
        }
    }
    
   
}
