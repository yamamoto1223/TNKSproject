using UnityEngine;
using System.Collections;

using define;

namespace character
{
    public class CharacterBase : MonoBehaviour
    {
        public float _move_speed = 5.0f;
        public Vector2 _direction = new Vector2(1.0f, 0).normalized;  // 移動する向き
        public Vector2 _chara_pos;  // 座標
        public Vector2 _chara_vel;  // 速度

        public GameObject _target_object;
        public Animator _animator;
        //private GameManager     _game_manager;

        public StateManager state_manager = new StateManager();
      
        // アニメーションテーブル
        protected string[] AnimationTable = new string[(int)MotionIndex.MOTION_MAX]{
            "isStay",
            "isWalk",
            "isAttack",
            "isDamage",
            "isDead",
        };
        protected int animIndex;// = MotionIndex.MOTION_STAY;
        
        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
        
        // ユニットの初期化
        protected void CharacterBaseInit()
        { 
        
        }

        // ユニットの基本動作
        protected void CharacterBaseUpdate()
        {
            // 座標・速度
            //_chara_pos = rigidbody2D.position;   // 座標を取得
            //_chara_vel = rigidbody2D.velocity;   // 速度を取得

            // 移動
            Move(_direction);

            // アニメーション
            //BaseAnimation();

            // 自動スプライト反転
            SpriteReverse();
        }

        // animation
        protected void BaseAnimation()
        {

            // 移動
            if (_chara_vel.x < -0.1f || _chara_vel.x > 0.1f)
            {
                animIndex = (int)MotionIndex.MOTION_WALK;
            // 待機
            }else{
                animIndex = (int)MotionIndex.MOTION_STAY;
            }

            // 攻撃
            if (_target_object != null)
            {
                animIndex = (int)MotionIndex.MOTIOM_ATTACK;
            }

            // アニメーション変更
            for (int i = 0; i < (int)MotionIndex.MOTION_MAX; i++)
            {
                bool animFlag = i == (int)animIndex;
                _animator.SetBool(AnimationTable[i], animFlag);

            }
        }

        public void ChangeAnimation( int index )
        {
            animIndex = index;

            // アニメーション変更
            for (int i = 0; i < (int)MotionIndex.MOTION_MAX; i++)
            {
                bool animFlag = i == (int)animIndex;
                _animator.SetBool(AnimationTable[i], animFlag);

            }
        }

        // reverse sprite
        protected void SpriteReverse()
        {
            Vector3 scale = transform.localScale; // サイズの取得
            if( _chara_vel.x >= 0.1f )
            {
                scale.x = 1; // そのまま（右向き）
            }
            if (_chara_vel.x <= -0.1f )
            {
                scale.x = -1; // 反転する（左向き）
            }
            transform.localScale = scale;
        }

        

        // move
        public void Move(Vector2 direction)
        {
            //if (_chara_pos.x > 4.0f)
            //{
            //    direction.Set(0.0f, 0.0f);
            //}
            rigidbody2D.velocity = direction * _move_speed;

        }

        // Stateクラス
        public abstract class State
        {
            protected GameObject animator;
            protected CharacterBase unit;
            public void SetUnitInstance(CharacterBase parent) { unit = parent; }
            public void SetAnimatorObj(GameObject obj) { animator = obj; }

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

            // Setter&Getter関数
            public void SetAnimatorObj(GameObject obj) { animator = obj; }
            public GameObject GetAnimatorObj() { return animator; }
            public void SetUnitInstance(CharacterBase parent) { unit = parent; }
            public CharacterBase GetUnitInstance() { return unit; }


            // 初期化
            public void InitializeState(State state)
            {
                // インスタンスセット
                state.SetUnitInstance(unit);
                state.SetAnimatorObj(animator);

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
