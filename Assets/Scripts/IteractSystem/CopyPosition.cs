using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] private Transform target; 
    [SerializeField] private Vector3 offset = Vector3.zero; 
    [SerializeField] private float followSpeed = 5f;

    private void Start()
    {
        offset = new Vector3(0,1,0);
    }
    void Update()
    {
        if (target != null)
        {
            // Calcula la posición deseada con el desplazamiento
            Vector3 desiredPosition = target.position + offset;

            // Interpola suavemente hacia la posición deseada
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
        }
    }
}
