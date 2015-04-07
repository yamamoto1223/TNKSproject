﻿using UnityEngine;
using System.Collections;

namespace character
{

    public class PlayerUnitBase : CharacterBase
    {
        // Use this for initialization
        void Start()
        {
            // この箇所に記述しても子クラスには影響がない
        }

        // Update is called once per frame
        void Update()
        {
        }

        // 初期化
        protected void PlayerUnitInit()
        {
            // 初期化
            fUnitMoveSpd = 1.0f;
            vDirection2D.Set(1.0f, 0.0f);
        }

        // 実行
        protected void PlayerUnitUpdate()
        {
        }


    }
}

