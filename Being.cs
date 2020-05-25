using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Being : MonoBehaviour
{
    List<string> stringlist = new List<string>();

    //UI Section
    //[SerializeField] Text _hpUI = null;
    // Being Stats
    [SerializeField] protected int _totalHp = 200;
    
    public int totalHp {
        get {return _totalHp;}
        set{
            _totalHp = value;
            // update the txt value;
            if (_currentHp <= 0)
            {
                Debug.Log("Dead");
            }
        }
    }
    [SerializeField] protected int _currentHp = 200;
    public virtual int currentHp {
        get {return _currentHp;}
        set {
            _currentHp = value;
            if(_healthBar) {
                _healthBar.value = _currentHp;
            }
             SetPhase();
        }
    }
    
    [SerializeField] Slider _healthBar = null;
    public Slider healthBar {
        get{return _healthBar;}
        set {
            _healthBar = value;
            _healthBar.maxValue = _totalHp;
            _healthBar.value = _currentHp;
        }
     
    }
    //Experience point
    private int _currentXp = 0;
    public int currentXp {
        get{return _currentXp;}
        set {_currentXp = value; }
    }
    //Total xp needed for next level up
    [SerializeField] private int _totalXp = 500;
    [SerializeField] private int _level = 1;
    public int level {
        get{return _level;}
        set{
            _level = value;
            if (_currentXp == _totalXp)
            {
                _level ++;
                _totalXp += _totalXp * 150/100;
                _currentXp = 0;
            }
        }
    }
    void HpUpdate (int currentHp, int totalHp) {
        _currentHp = currentXp;
        _totalHp = totalHp;
    }
    
    void Start()
    {
    _currentHp = _totalHp;
    }
    protected virtual void SetPhase(){}

}
