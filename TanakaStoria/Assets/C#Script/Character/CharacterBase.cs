using UnityEngine;
using System.Collections;

using define;

namespace character
{
    public class CharacterBase : MonoBehaviour
    {
        // paramator
        public int      iUnitHp         = 2000;
        public int      iUnitAtk        = 1000;
        public float    fUnitMoveSpd    = 5.0f;

        // vector2D
        public Vector2 vDirection2D = new Vector2(1.0f, 0).normalized;  // 移動する向き
        public Vector2 vUnitPosition2D;  // 座標
        public Vector2 vUnitVelocity2D;  // 速度

        // conpornent,gameobjedct
        public GameObject   objTargetObject;    // 敵オブジェクト
        public Animator     cnpAnimator;        // アニメーター
        public StateManager cStateManager = new StateManager(); // 状態遷移
      
        // animationtabale
        protected string[] AnimationTable = new string[(int)MotionIndex.MOTION_MAX]{
            "isStay",
            "isWalk",
            "isAttack",
            "isDamage",
            "isDead",
        };
        protected int iAnimationIndex; 

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        // getter, setter
        public void SetUnitHp(int hp)   { iUnitHp = hp; }
        public int  GetUnitHp()         { return iUnitHp; }
        
        // ユニットの初期化
        protected void CharacterBaseInit()
        { 
        
        }

        // ユニットの基本動作
        protected void CharacterBaseUpdate()
        {
            // 移動
            Move(vDirection2D);

            // 自動スプライト反転
            SpriteReverse();
        }

        // アニメーション変更
        public void ChangeAnimation( int index )
        {
            iAnimationIndex = index;

            // アニメーション変更
            for (int i = 0; i < (int)MotionIndex.MOTION_MAX; i++)
            {
                bool animFlag = i == (int)iAnimationIndex;
                cnpAnimator.SetBool(AnimationTable[i], animFlag);

            }
        }

        // スプライト反転
        protected void SpriteReverse()
        {
            Vector3 scale = transform.localScale; // サイズの取得
            if( vUnitVelocity2D.x >= 0.1f )
            {
                scale.x = 1; // そのまま（右向き）
            }
            if (vUnitVelocity2D.x <= -0.1f )
            {
                scale.x = -1; // 反転する（左向き）
            }
            transform.localScale = scale;
        }

        

        // move
        public void Move(Vector2 direction)
        {
            rigidbody2D.velocity = direction * fUnitMoveSpd;
        }

        // Stateクラス
        public abstract class State
        {
            // Instance
            protected GameObject animator;
            protected CharacterBase unit;
            protected EffectCreater effect;
            // setter
            public void SetUnitInstance(CharacterBase parent) { unit = parent; }
            public void SetAnimatorObj(GameObject obj) { animator = obj; }
            public void SetEffectCreater(EffectCreater _effect) { effect = _effect; }
            //状態に依存する振る舞いのインタフェースを定義します。
            public abstract void Init();
            public abstract void Exe();
            public abstract void Exit();
        }

        // StateManagerクラス
        public class StateManager
        {
            // 状態を保持するプロパティを定義します。
            private State current_state;
            protected GameObject animator;
            protected CharacterBase unit;
            protected EffectCreater effect;

            // Setter&Getter関数
            public void             SetAnimatorObj(GameObject obj) { animator = obj; }
            public GameObject       GetAnimatorObj()  { return animator; }
            
            public void             SetUnitInstance(CharacterBase parent) { unit = parent; }
            public CharacterBase    GetUnitInstance() { return unit; }
            
            public void             SetEffectCreater(EffectCreater _effect) { effect = _effect; }
            public EffectCreater    GetEffectCreater() { return effect; }


            // 初期化
            public void InitializeState(State state)
            {
                // インスタンスセット
                state.SetUnitInstance(unit);
                state.SetAnimatorObj(animator);
                state.SetEffectCreater(effect);

                state.Init();
                current_state = state;
            }

            // 状態遷移
            public void ChangeState(State next_state)
            {
                // 現在の状態の終了
                current_state.Exit();

                // 次の状態へ遷移する準備
                next_state.SetUnitInstance(unit);
                next_state.SetEffectCreater(effect);
                next_state.SetAnimatorObj(animator);
                next_state.Init();
                current_state = next_state;
            }

            //保持している状態オブジェクトに対して処理を送ります。
            public void StateExe()
            {
                current_state.Exe();
            }
        }
    }
}
