using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _objective;
    [SerializeField] private float _range;

    Vector3 _direction;

    void Update()
    {
        LookPlayer();
    }

    private void LookPlayer()
    {
        if (Vector3.Distance(transform.position, _objective.transform.position) <= _range)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, _objective.transform.rotation, 0.5f);
            //transform.LookAt(_objective.transform);

            _direction = _objective.transform.position - transform.position;
            _direction.y = 0;
            
            transform.rotation = Quaternion.LookRotation(_direction);

        }  
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
