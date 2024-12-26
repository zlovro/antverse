using System;
using System.Collections.Generic;
using System.Linq;
using Source.Libraries.KBLib2;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

namespace Source.Game
{
    public class Movement : Kb2Behaviour
    {
        public Camera cam;

        public Inventory inventory;

        public float mouseDeadzone = 10;
        public float jumpForce     = 5F;
        public float rotationSpeed = 20F;
        public float speed         = 5F;
        public float dampingTime   = 1F;

        private Vector2 mDir;

        private float mCurrentSpeed;
        private float mDampingTimer;

        private Vector2 mLastDirMovement;

        private bool        mUnderground;
        private Rigidbody2D mRb;
        private void Start()
        {
            mRb                = GetComponent<Rigidbody2D>();
            mRb.freezeRotation = true;
        }

        private void FixedUpdate()
        {
            // if (!Input.GetKey(KeyCode.W))
            // {
            //     return;
            // }

            // var mousePos        = Input.mousePosition;
            // var playerScreenPos = cam.WorldToScreenPoint(tf.position);
            //
            // var deadzone  = mouseDeadzone * (Screen.width / 1920F);
            // var accelZone = mouseAccelZone * (Screen.width / 1920F);
            //
            // var distance = Vector2.Distance(mousePos, playerScreenPos);
            // if (distance <= deadzone)
            // {
            //     mDir = Vector2.zero;
            // }
            // else
            // {
            //     var delta = (Vector2)(mousePos - playerScreenPos);
            //     mDir = delta / delta.magnitude * ((distance - deadzone) / accelZone);
            // }

            mRb.gravityScale = mUnderground ? 0 : 1;

            var dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            // if (!mUnderground)
            // {
            //     dir.y = Mathf.Min(0, dir.y);
            // }

            if (!mUnderground)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                     mRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }

            if (dir != Vector2.zero)
            {
                mDampingTimer    += Time.fixedDeltaTime;
                mDampingTimer    =  Math.Min(mDampingTimer, dampingTime);
                mLastDirMovement =  dir;
            }
            else
            {
                if (mDampingTimer > 0)
                {
                    mDampingTimer -= Time.fixedDeltaTime;
                    dir           =  mLastDirMovement;
                }
            }

            mDampingTimer = Math.Clamp(mDampingTimer, 0, dampingTime);

            var dampFactor = mDampingTimer / dampingTime;
            mCurrentSpeed = speed * dampFactor;

            if (dir != Vector2.zero)
            {
                mRb.SetRotation(Quaternion.RotateTowards(Quaternion.Euler(0, 0, mRb.rotation), Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x)), Time.smoothDeltaTime * rotationSpeed));
            }

            mDir = dir;

            mRb.velocity = dir * mCurrentSpeed;
        }

        private void OnTriggerEnter2D(Collider2D pCollider)
        {
            if (pCollider.CompareTag("Underground"))
            {
                mUnderground = true;
            }

            if (!pCollider.CompareTag("Item"))
            {
                return;
            }

            var item = pCollider.GetComponent<ItemObject>();
            inventory.AddItem(item.type, 100);

            item.Delete();
        }

        private void OnTriggerExit2D(Collider2D pCollider)
        {
            if (pCollider.CompareTag("Underground"))
            {
                mUnderground = false;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Underground"))
            {
                mUnderground = true;
            }
        }
    }
}