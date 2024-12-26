using System;
using Source.Libraries.KBLib2;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class ItemObject : Kb2Behaviour
    {
        public Item.ItemType type;

        public float deleteTime = 1;
        public float amplitude, period;

        private SpriteRenderer mRenderer;
        private Item           mItem;

        private Vector2 mStartPos;
        private float   mSinStart;

        private float mDelTimer     = 0;
        private bool  mShouldDelete = false;

        private BoxCollider2D mCollider;

        protected override void Awake()
        {
            base.Awake();

            mRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            mItem = ItemRegistry.Items[type];

            mRenderer.sprite = mItem.sprite;
            mStartPos        = tf.position;
            mSinStart        = Random.Range(-3, 3);

            mCollider           = GetComponent<BoxCollider2D>();
            mCollider.size      = mRenderer.size;
            mCollider.isTrigger = true;
        }

        private void Update()
        {

            if (mShouldDelete)
            {
                mDelTimer += Time.deltaTime;
                if (mDelTimer >= deleteTime)
                {
                    return;
                }

                mRenderer.color = new Color(1, 1, 1, 1 - (mDelTimer / deleteTime));
            }
            else
            {
                tf.position = mStartPos + new Vector2(0, 0.5F * (1 + Mathf.Sin(mSinStart + Time.time * period)) * amplitude);
            }
        }

        public void Delete()
        {
            mShouldDelete = true;
            Destroy(gameObject, deleteTime);
        }
    }
}