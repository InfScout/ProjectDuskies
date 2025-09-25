using System;
using UnityEngine;

namespace Player
{
    public class Camerafollow : MonoBehaviour
    {
        [SerializeField]private Transform target;

        private void FixedUpdate()
        {
            transform.position = target.position;
        }
    }
}