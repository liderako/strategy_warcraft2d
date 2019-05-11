using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]private List<UnitController> _units;

    // Start is called before the first frame update
    void Start()
    {
        _units = new List<UnitController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast( worldPoint, Vector2.zero );
            if (hit.collider == null)
            {
                foreach (var unit in _units)
                {
                    unit.startMove(worldPoint);
                }
                _units.Clear();
            }
            else if (hit.collider != null && hit.collider.gameObject.layer == 8)
            {
                foreach (var unit in _units)
                {
                    unit.startMove(worldPoint);
                }
                _units.Clear();
            }
        }   
    }

    public void addUnit(UnitController unitController)
    {
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            foreach (var unit in _units)
            {
                unit.activeZona(false);
            }
            _units.Clear();
        }
        int currentId = unitController.getId();
        foreach (var unit in _units)
        {
            if (unit.getId() == currentId)
            {
                return;
            }
        }
        _units.Add(unitController);
    }
}
