using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UnitController : Unit
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _zona;
    [SerializeField] private int _damage;

    private AliveObject _currentEnemy;
    private AudioSource _audioData;
    private Vector3 _positionMove;
    private bool _move;
    private String _direction;
    // Start is called before the first frame update
    void Start()
    {
        _positionMove = new Vector3();
        _move = false;
        setHp(100);
        setMaxHp(getHp());
    }

    // Update is called once per frame
    void Update()
    {
        if (_move)
        {
            transform.position = Vector3.MoveTowards(transform.position, _positionMove, _speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _positionMove) < 0.01f)
            {
                _animator.SetBool(_direction, false);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                gameObject.GetComponent<SpriteRenderer>().flipY = false;
                _move = false;
            }
        }
        if (getHp() <= 0)
        {
            _animator.SetBool(_direction, false);
            _animator.SetBool("Dead", true);
            dead = true;
            _move = false;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 8 && !other.gameObject.GetComponent<AliveObject>().dead)
        {
            _currentEnemy = other.gameObject.GetComponent<AliveObject>();
            _animator.SetBool(_direction, false);
            _animator.SetBool("AttackUp", true);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.tag.Equals("ALIVE"))
        {
            _animator.SetBool(_direction, false);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            gameObject.GetComponent<SpriteRenderer>().flipY = false;
            _move = false;
        }
        if (other.gameObject.layer == 8 && !other.gameObject.GetComponent<AliveObject>().dead)
        {
            _currentEnemy = other.gameObject.GetComponent<AliveObject>();
            _animator.SetBool(_direction, false);
            _animator.SetBool("AttackUp", true);
        }
    }

    public void attack()
    {
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
                _animator.SetBool("AttackUp", false);
                _currentEnemy = null;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
//        if (other.gameObject.layer == 8 && _currentEnemy != null)
//        {
            _animator.SetBool("AttackUp", false);
            _currentEnemy = null;
//        }
    }

    void OnMouseDown()
    {
        if (dead)
            return;
        _playerManager.addUnit(this);
        _zona.SetActive(true);
        _audioData = GetComponent<AudioSource>();
        _audioData.Play(1);
    }

    public void startMove(Vector2 positionMove)
    {
        _animator.SetBool(_direction, false);
        _positionMove = positionMove;
        _zona.SetActive(false);
        findDirection();
        _animator.SetBool(_direction, true);
        _move = true;
    }

    private void findDirection()
    {
        Vector3 targetDir = _positionMove - gameObject.transform.position;
        float angle = Vector3.SignedAngle(targetDir, Vector3.up, -Vector3.forward);

        if (angle <= 0)
        {
            angle *= -1;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        
        if (angle >= 0 && angle <= 45)
        {
            _direction = "MoveUp";
        }
        else if (angle >= 140 && angle <= 180)
        {
            _direction = "MoveDown";   
        }
        else if (angle >= 45 && angle <= 75)
        {
            _direction = "MoveDiagonalUp";
        }
        else if (angle >= 75 && angle <= 105)
        {
            _direction = "MoveHorizont";
        }
        else
        {
            _direction = "MoveDiagonalDown";
        }
    }

    public void activeZona(bool status)
    {
        _zona.SetActive(status);
    }
}
