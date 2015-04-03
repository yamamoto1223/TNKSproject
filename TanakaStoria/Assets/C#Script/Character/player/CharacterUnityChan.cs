﻿using UnityEngine;
using System.Collections;

namespace character
{
    public class CharacterUnityChan : PlayerUnitBase
    {

        // Use this for initialization
        void Start()
        {
            //_character = GameObject.Find("UnityChan2D");
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // キャラクタの基本動作
            base.CharacterBaseUpdate();

            // アニメーション
            BaseAnimation();
        }
    }
}
