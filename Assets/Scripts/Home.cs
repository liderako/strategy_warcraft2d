using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : Unit
{
    [SerializeField] private Vilage _vilage;
    // Start is called before the first frame update
    void Start()
    {
        setHp(500);
        setMaxHp(getHp());
    }

    private void Update()
    {
        if (getHp() <= 0)
        {
            _vilage.deleteHome(this);
            Destroy(gameObject);
        }
    }
}
