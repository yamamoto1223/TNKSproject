﻿using UnityEngine;
using System.Collections;

namespace character
{

    public class KnightBase : CharacterPlayerUnit
    {
        // スクリプトのインスペクタ上に設定項目が追加される 
        public GameObject anim_object;

        // Use this for initialization
        void Start()
        {
            _charactor = GameObject.Find("character_knight");
            //_animator = GameObject.Find("body").GetComponent<Animator>();
            //_animator = GetComponent<Animator>();
            _animator = anim_object.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            // キャラクタの基本動作
            CharacterBaseUpdate();

            // アニメーション
            BaseAnimation();
        }
    }
}