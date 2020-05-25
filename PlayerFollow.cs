using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] Transform _playerTransform = null;
    Vector3 _cameraOffset;
    [Range(0.01f,1f)] [SerializeField] float _smoothFactor = 0.5f;
    void Awake() {
        Debug.Log("Camera follow");
        
    }
    // Start is called before the first frame update
    void Start()
    {  
        StartCoroutine(FindPlayerCouroutine()); 
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!_playerTransform) return;
        Vector3 newPos = _playerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, _smoothFactor);
    }
    IEnumerator FindPlayerCouroutine () {
        yield return new WaitForSeconds(0.01f);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _cameraOffset = transform.position - _playerTransform.position;
         
    }

    
}
