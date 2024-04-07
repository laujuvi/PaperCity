using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _objective;
    [SerializeField] private float _range;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookPlayer();
    }

    private void LookPlayer()
    {
        if (Vector3.Distance(transform.position, _objective.transform.position) <= _range)
        {
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, _objective.transform.rotation, 0.5f);
            transform.LookAt(_objective.transform);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
