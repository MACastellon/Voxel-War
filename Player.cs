using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Player : Being
{
    public static Player instance;
    
    [SerializeField] private int _speed = 10;
    FixedJoystick _joystic = null;
    // Data section
    [SerializeField] Text playerNameText = null;
    Vector3 _moveDirection = Vector3.zero;
    CharacterController _cc;
    
    Animator _animator;
    Camera _camera;
    Transform _spawnPoint;
    public CheckPoint currentCheckPoint = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        healthBar = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        _spawnPoint = GameObject.Find("Spawn").GetComponent<Transform>();
        transform.position = _spawnPoint.position;
        //_dodgeBtn.onClick.AddListener(Dodge);
        _cc = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _joystic = GameObject.Find("Fixed Joystick").GetComponent<FixedJoystick>();
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMovement();        
        
         if(Input.GetMouseButtonDown(0))
        { 
            StartCoroutine(AttackCouroutine());
            
       
        }
         if(Input.GetKey(KeyCode.Space) || Input.GetButton("Xbox B"))
        { 
            StartCoroutine(DodgeCouroutine());
           
            
        }
        if (currentHp <= 0) {
            currentCheckPoint.Resurection();
            currentHp = totalHp;
        }
    }
    protected override void SetPhase() 
    {
       
    }
    void PlayerMovement() {
        float currentSpeed = _speed;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Sprinting Forward Roll")){
           currentSpeed = 0;
        } else {
            currentSpeed = _speed;
            Vector3 playerMovement = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")) * currentSpeed * Time.deltaTime;
            _animator.SetFloat("Speed",playerMovement.magnitude);
            if (playerMovement.magnitude > 0) {
                Quaternion newDirection = Quaternion.LookRotation(playerMovement);
                transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * 10);
            }
        } 
    }
    void Attack()
    {
        StartCoroutine(AttackCouroutine());
    }
    public IEnumerator AttackCouroutine(){
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.01f);
        _animator.ResetTrigger("Attack");
    }
    void Dodge() {
        StartCoroutine(DodgeCouroutine());
    }
    IEnumerator DodgeCouroutine() {
        _animator.SetTrigger("Dodge");
        
        yield return new WaitForSeconds(0.01f);
        _animator.ResetTrigger("Dodge");
    } 
}
