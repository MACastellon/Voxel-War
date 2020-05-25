using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class Shaman : Being
{

      

    //Spell Section 
    [Header("[ --- Spell --- ]")]
    [SerializeField]  GameObject _fireballInstance = null;
    [SerializeField] GameObject _tornadoInstance = null;
    [SerializeField] GameObject _undergroundSpikeInstance = null;
    [SerializeField] GameObject _spikeLanceInstance = null;
    [SerializeField] List<Transform> _teleportPoint = null;
    [SerializeField] List<GameObject> _aliveAllyList = null;
    [SerializeField] List<Transform> _allySpawnPoint = null;
    Vector3 target;
    Vector3 myPos;

    [SerializeField] Transform monsterPhasePos;
    [SerializeField] Transform attackPhasePos;
    [SerializeField] GameObject _spellCastingPos;
    [SerializeField] List<Transform> _spikesCastingPos;
    [SerializeField] List<Transform> _tornadoSpawnPos = null;
    bool launched = false;
    [SerializeField] Collider _targetingCol;
    int phase = 0;
    bool canAttack = false;
    bool canRollNb = false;
    Animator _animator;
    MonsterSpawner _monsterSpawner;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        transform.position = monsterPhasePos.position;
        _monsterSpawner = GetComponent<MonsterSpawner>();
        _animator = GetComponent<Animator>();
        _monsterSpawner.monstersDeathEvent.AddListener(OnAllyDeath);
    }
    // Start is called before the first frame update
    void Start()
    {   
        _monsterSpawner.SpawnAlly(3);
        
    }
    // Update is called once per frame
    void OnAllyDeath() 
    {
        Debug.Log("Event Death");
       transform.position = attackPhasePos.position;
       phase++;
       canAttack = true;
        
    }
    protected override void SetPhase() {
        int hpPercent = (_currentHp * 100 /_totalHp);
        if (hpPercent <= 75 && phase == 1) {
            transform.position = monsterPhasePos.position;
            _monsterSpawner.SpawnAlly(3);
            phase++;
            canAttack = false;
        } else if (hpPercent <=50 && phase == 2) {
            transform.position = monsterPhasePos.position;
            _monsterSpawner.SpawnAlly(3);
            phase++;
            canAttack = false;
        } else if (hpPercent <= 25 && phase == 3) {
            transform.position = monsterPhasePos.position;
            _monsterSpawner.SpawnAlly(3);
            phase++;
            canAttack = true;
        } else if (hpPercent <= 0 ) {
            //Dead animation
            //Destroy
            healthBar.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    void FireBall() {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        Fireball fireball;
        fireball = Instantiate(_fireballInstance,_spellCastingPos.transform.position, Quaternion.identity).GetComponent<Fireball>(); // Spawn a faire ball
        fireball.damage = 1; // The level of the fireball
        fireball.target = target; // Set the fireball target
    }
    void Tornado (int nbTornado) {
      List<Transform> _savedSpawnPointList = new List<Transform>(_tornadoSpawnPos); // Save a copy of the spawn point
        //Start the spell animation
        for (int i = 0; i < nbTornado; i++) {
            int _randomSpawnPoint = Random.Range(0, _savedSpawnPointList.Count);
            Debug.Log("Monster Spawned");
            Instantiate(_tornadoInstance,_savedSpawnPointList[_randomSpawnPoint].position , Quaternion.identity);
            _savedSpawnPointList.RemoveAt(_randomSpawnPoint);
        }
    }
    void UnderGroundSpike() {
        Instantiate(_undergroundSpikeInstance, target, Quaternion.identity);
    }
    
    // RandomTeleportPos is called when you need a Vector3 within a List of teleport
    Vector3 RandomTeleportPos()
    {
        Vector3 _teleportPos = _teleportPoint[Random.Range(0, _teleportPoint.Count)].transform.position;
        return _teleportPos;
    }

    void Teleport() {
        Vector3 _teleportPos = RandomTeleportPos();//Random position
        transform.position = _teleportPos; // this boss position = the teleport position
    }
    IEnumerator TeleportCoroutine () 
    {
        while(true) {
            Vector3 _teleportPos = RandomTeleportPos();//Random position
            //While the boss position is equal to the teleport positon
            while(_teleportPos.x == this.transform.position.x && _teleportPos.z == this.transform.position.z) {
                _teleportPos = RandomTeleportPos(); // Generate a new position 
            }
            this.transform.position = _teleportPos; // this boss position = the teleport position
            yield return new WaitForSeconds(2f);
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            healthBar.maxValue = totalHp;
            healthBar.value = currentHp;
            healthBar.gameObject.SetActive(true);
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
        if (other.gameObject.tag == "Player" ){
            target = other.transform.position;
            Vector3 targetPostition = new Vector3( target.x, this.transform.position.y, target.z ) ;
            this.transform.LookAt( targetPostition);
            if (!launched && canAttack) {
                int _randomSpell = Random.Range(0,101);
                if (_randomSpell < 65) {
                    _animator.SetTrigger("Attack");
                    StartCoroutine(FireBallCoroutine());
                }else if (_randomSpell < 85) {
                    _animator.SetTrigger("Tornado");
                    StartCoroutine(UnderGroundSpikeCoroutine(10));
                } else{
                    _animator.SetTrigger("Tornado"); 
                    StartCoroutine(TornadoCoroutine());
                }
            }
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            healthBar.gameObject.SetActive(true);
        }
    }
    IEnumerator AttackAnimation() 
    {
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.01f);
        _animator.ResetTrigger("Attack");
    }
    IEnumerator FireBallCoroutine() 
    {   
        launched = true;
        yield return new WaitForSeconds(0.8f);
        FireBall();
        yield return new WaitForSeconds(2f);
        launched = false;
    }
    IEnumerator TornadoCoroutine(){ 
        launched = true;
        yield return new WaitForSeconds(0.8f);
        Tornado(2);
        yield return new WaitForSeconds(2f);
        launched = false;
    }
    IEnumerator UnderGroundSpikeCoroutine (int nbSpike) {
        launched = true;
        for (int i = 0; i <= nbSpike; i++) {
            UnderGroundSpike();
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(1f);
        launched = false;
    }
    IEnumerator SpikeLanceCoroutine() {
        launched = true;
        Being target = GameObject.FindGameObjectWithTag("Player").GetComponent<Being>();
        List<Transform> _savedSpawnPointList = new List<Transform>(_spikesCastingPos); // Save a copy of the spawn point
        Spike spike;
        //Start the spell animation
        for (int i = 0; i < 4; i++) {
            int _randomSpawnPoint = Random.Range(0, _savedSpawnPointList.Count);
            Debug.Log("Monster Spawned");
            spike = Instantiate(_spikeLanceInstance,_savedSpawnPointList[_randomSpawnPoint].position , Quaternion.identity).GetComponent<Spike>();
            spike.target = target;
            _savedSpawnPointList.RemoveAt(_randomSpawnPoint);
            yield return new  WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        launched = false;
    }
}
