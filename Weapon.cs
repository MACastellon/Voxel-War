using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   [SerializeField] int _damage = 0;
     Transform parent = null;
     string _monsterTag = "Monster";
     string _playerTag = "Player";
    public int damage {
        get{return _damage;}
    }
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.root;
    }
    void OnTriggerEnter(Collider other){
        Being ennemy; // Ennemy attacked
        if(parent.tag == _playerTag && other.gameObject.tag == _monsterTag && other.GetType() == typeof(CapsuleCollider)) {
            ennemy =  other.GetComponent<Being>();
            ennemy.currentHp -= damage;

        }else if(parent.tag == _monsterTag && other.gameObject.tag == _playerTag) {
            ennemy =  other.GetComponent<Being>();
            ennemy.currentHp -= damage;
            
        }
    }
}
