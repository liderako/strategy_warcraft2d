using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObject : MonoBehaviour
{
    [SerializeField]private int _hp;
    [SerializeField] private String type;
    private int _maxHp;
    public bool dead;

    public void setMaxHp(int hp)
    {
        _maxHp = hp;
    }
    
    public void setHp(int hp)
    {
        _hp = hp;
    }

    public int getHp()
    {
        return (_hp);
    }

    public String getTypeUnit()
    {
        return type;
    }

    public int getMaxHp()
    {
        return _maxHp;
    }
}
