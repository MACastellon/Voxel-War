using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterSpawner : MonoBehaviour
{
    public UnityEvent monstersDeathEvent = new UnityEvent();
    [SerializeField] List<GameObject> _monsterPrefabList = null;
    [SerializeField] List<Monstre> _monsterSpawnList = null;
    Monstre _monster = null;
    [SerializeField] List<Transform>  _spawnPointList = null;
    public void  RemoveFromList(Monstre monstre) {
        _monsterSpawnList.Remove(monstre);
        
        if (_monsterSpawnList.Count == 0) {
            
            monstersDeathEvent.Invoke();
        }
    }
   public void SpawnAlly (int nbMonster)
    {
        List<Transform> _savedSpawnPointList = new List<Transform>(_spawnPointList); // Save a copy of the spawn point
        //Start the spell animation
        for (int i = 0; i < nbMonster; i++) {
            int _randomSpawnPoint = Random.Range(0, _savedSpawnPointList.Count);
            _monster = Instantiate(_monsterPrefabList[0].GetComponent<Monstre>(),_savedSpawnPointList[_randomSpawnPoint].position , Quaternion.identity);
            _monsterSpawnList.Add(_monster);
            _monster.mySpawner = this;
            _savedSpawnPointList.RemoveAt(_randomSpawnPoint);
        }
    }
}
