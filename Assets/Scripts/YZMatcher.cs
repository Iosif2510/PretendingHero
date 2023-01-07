using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YZMatcher : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, pivot.position.y);
    }
}
