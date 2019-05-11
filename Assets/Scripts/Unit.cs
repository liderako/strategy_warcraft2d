using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : AliveObject
{
    [SerializeField]private int _id;

    public void setId(int id)
    {
        _id = id;
    }

    public int getId()
    {
        return (_id);
    }
}
