using System;
using Source.Libraries.KBLib2;
using UnityEngine;

namespace Source.Game
{
    public class CameraLimiter : Kb2Behaviour
    {
        public Transform player;

        private Camera mCam;

        private void Start()
        {
            mCam = GetComponent<Camera>();
        }

        private void Update()
        {
            // var height = mCam.orthographicSize;
            // var width  = height * mCam.aspect;
            //
            // var limit = new Bounds(bounds.offset, bounds.size);
            //
            // var minX = limit.min.x + width;
            // var maxX = limit.extents.x - width;
            //
            // var minY = limit.min.y + height;
            // var maxY = limit.extents.y - height;
            //
            // var camBounds = new Bounds();
            // camBounds.SetMinMax(new Vector3(minX, minY, 0.0f), new Vector3(maxX, maxY, 0.0f));
            //
            // tf.position = new Vector3(Mathf.Clamp(tf.position.x, camBounds.min.x, camBounds.max.x), Mathf.Clamp(tf.position.y, camBounds.min.y, camBounds.max.y), tf.position.z);

            var pos = player.position;
            pos.z       = -10;
            tf.position = pos;
        }
    }
}