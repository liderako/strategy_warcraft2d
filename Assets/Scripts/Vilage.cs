using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vilage : AliveObject
{
    private int count;
    [SerializeField] private GameObject _units;
    private float oldTime;
    [SerializeField]private Vector3 _positionSpawn;
    [SerializeField]private List<Home> _homes;
    
    // Start is called before the first frame update
    void Start()
    {
        setHp(2000);
        setMaxHp(getHp());
        count = 1;
        oldTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - oldTime > 10.0f + (10.0f - _homes.Count * 2.5f))
        {
            oldTime = Time.time;
            GameObject go = Instantiate(_units, _positionSpawn, Quaternion.identity);
            go.GetComponent<Unit>().setId(count);
            count += 1;
        }
        if (getHp() <= 0)
        {
            if (getTypeUnit().Equals("HumanVilage"))
            {
                Debug.Log("The Ork Team wins.");
            }
            else
            {
                Debug.Log("The Human Team wins.");
            }
            Destroy(gameObject);
        }
    }


    public void deleteHome(Home home)
    {
        List<Home> itemsToRemove = new List<Home>();
        foreach (var h in _homes)
        {
            if (h.getId() == home.getId())
            {
                itemsToRemove.Add(h);
            }
        }
        foreach (var x in itemsToRemove)
        {
            _homes.Remove(x);
        }
    }
}
