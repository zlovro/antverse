using System;
using Source.Libraries.KBLib2;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Source.Game
{
    public class DayNightSystem : Kb2Behaviour
    {
        public float dayNightDurationSeconds = 150F;
        public float fadeDurationSeconds     = 30F;

        public float dayLight, nightLight;
        
        public SpriteRenderer dayRenderer, nightRenderer;
        public Light2D        globalLight;
        
        private float mTime;
        private float mFadeTimer;
        
        private void Update()
        {
            mTime += Time.deltaTime;

            if (mTime % dayNightDurationSeconds >= dayNightDurationSeconds - fadeDurationSeconds)
            {
                mFadeTimer += Time.deltaTime;
                
                var sunset = mTime % (dayNightDurationSeconds * 2) < dayNightDurationSeconds;
                var t      = mFadeTimer / fadeDurationSeconds;
                
                dayRenderer.color   = WithAlpha(Color.white, sunset ? 1 - t : t);
                nightRenderer.color = WithAlpha(Color.white, sunset ? t : 1 - t);

                globalLight.intensity = Mathf.Lerp(dayLight, nightLight, sunset ? t : 1 - t);
            }
            else
            {
                mFadeTimer = 0;
            }
        }

        private Color WithAlpha(Color pColor, float pA)
        {
            return new Color(pColor.r, pColor.g, pColor.b, pA);
        }
        
        public int GetDay()
        {
            return (int)(mTime / (dayNightDurationSeconds * 2));
        }
    }
}