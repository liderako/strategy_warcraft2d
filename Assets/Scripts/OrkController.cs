using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrkController : Unit
{
    [SerializeField] private Animator _animator;
    private AudioSource _audioData;
    [SerializeField]private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private bool _move;
    [SerializeField] private GameObject _gameObjectPositionTriger;
    private AliveObject _currentEnemy;

    
    // Update is called once per frame

    void Start()
    {
        setMaxHp(getHp());
        dead = false;
    }
    
    void Update()
    {
        if (dead)
            return;
        if (_move)
        {
            if (Vector3.Distance(transform.position, _gameObjectPositionTriger.transform.position) < 0.01f)
            {
                _move = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, _gameObjectPositionTriger.transform.position, _speed * Time.deltaTime);
        }
        if (getHp() <= 0 && !dead)
        {
            _animator.SetBool("Dead", true);
            _audioData = GetComponent<AudioSource>();
            _audioData.Play(1);
            dead = true;
            _move = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
    
    public void attack()
    {
        if (dead)
            return;
        if (_currentEnemy != null)
        {
            _currentEnemy.setHp(_currentEnemy.getHp() - _damage);
            if (_currentEnemy.getHp() >= 0)
            {
                Debug.Log(_currentEnemy.getTypeUnit() + "[" + _currentEnemy.getHp() + "/" + _currentEnemy.getMaxHp() +
                          "]Hp has been attacked.");
            }
            if (_currentEnemy.dead)
            {
                _animator.SetBool("Attack", false);
                _currentEnemy = null;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (dead)
            return;
        if (other.gameObject.layer == 9 && !other.gameObject.GetComponent<AliveObject>().dead)
        {
            _currentEnemy = other.gameObject.GetComponent<AliveObject>();
            _animator.SetBool("Attack", true);
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (dead)
            return;
        if (other.gameObject.layer == 9 && _currentEnemy != null)
        {
            _animator.SetBool("Attack", false);
            _currentEnemy = null;
        }
    }
}
