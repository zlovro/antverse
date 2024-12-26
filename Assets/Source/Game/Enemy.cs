using System;
using Source.Libraries.KBLib2;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Game
{
    public class Enemy : Kb2Behaviour
    {
        private NavMeshAgent mAgent;

        private void Start()
        {
            mAgent                = GetComponent<NavMeshAgent>();
            mAgent.updateRotation = true;
            mAgent.updateUpAxis   = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var screenToWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                screenToWorldPoint.z = tf.position.z;
                mAgent.SetDestination(screenToWorldPoint);
            }
        }
    }
}