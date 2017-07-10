﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DivisionLike
{
    public class ScreenHUD : MonoBehaviour
    {
        public static ScreenHUD instance = null;

        private Text levelText;
        private Slider xpSlider;
        private Slider ammoSlider;

        void Awake()
        {
            Debug.Log( "ScreenHUD Awake()" );

            if ( instance == null )
            {
                instance = this;
            }
            else if ( instance != null )
            {
                Destroy( gameObject );
            }

            levelText = transform.Find( "LevelText" ).GetComponent<Text>();
            xpSlider = transform.Find( "ExpSlider" ).GetComponent<Slider>();
            ammoSlider = transform.Find( "AmmoSlider" ).GetComponent<Slider>();
            
            SetLevelText();
            CalculateExpSlider( 0 );
            SetAmmoSlider();
        }

        public void SetLevelText()
        {
            levelText.text = Player.instance.stats.level.ToString();
            
        }

        public void CalculateExpSlider( int xpToAdd )
        {
            Player.instance.stats.xp += (ulong) xpToAdd;
            Player.instance.stats.CheckLevel();
            
            float normalizedXP = (float) (Player.instance.stats.xp) / (float) (Player.instance.stats.xpRequire[ Player.instance.stats.level - 1 ]);
            //Debug.Log( "player xp = " + Player.instance.stats.xp );
            //Debug.Log( "normalized xp = " + normalizedXP );
            xpSlider.normalizedValue = normalizedXP;
        }

        private void SetAmmoSlider()
        {
            float normalizedAmmo = (float) (Player.instance.weaponHandler.currentWeapon.ammo.clipAmmo) / (float) (Player.instance.weaponHandler.currentWeapon.ammo.maxClipAmmo);
            ammoSlider.normalizedValue = normalizedAmmo;
        }

        // Update is called once per frame
        void Update()
        {
            SetAmmoSlider();
        }
    }
}
