using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CheckPoint : MonoBehaviour
{   
    public bool isActive = false;

    [SerializeField] Animator _animator;
    [SerializeField] GameObject _playerPrefab = null;
    [SerializeField] Transform _playerSpawnPos = null;
    [SerializeField] ParticleSystem _particleSystem;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        if (isActive) {
            SetActiveAnimation(true);
             Player.instance.currentCheckPoint = this;
        }
    }
    
    public void SpawnPlayer() {
        Instantiate(_playerPrefab,_playerSpawnPos.position,Quaternion.identity);
    }
    public void Resurection() {
        Player.instance.transform.position = _playerSpawnPos.position;
        Debug.Log("Player Resurected");
    }
    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    void OnMouseDown()
    {   CheckPoint newCheckPoint;
        if(!isActive) {
            if (!Player.instance.currentCheckPoint) {
                Player.instance.currentCheckPoint = this;
            }else {
                newCheckPoint = this;
                Player.instance.currentCheckPoint.isActive = false;
                Player.instance.currentCheckPoint.SetActiveAnimation(false);
                Player.instance.currentCheckPoint = newCheckPoint;
            }
            isActive = true;
            SetActiveAnimation(true);
        }
        Debug.Log("this is you new checkpoint " + isActive);
    }
    public void SetActiveAnimation(bool value) {
        _animator.SetBool("isActive", value);
        if (value) {
            _particleSystem.Play();
        } else {
            _particleSystem.Stop();
        }
    }
    
}
