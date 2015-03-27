using UnityEngine;
using System.Collections;

namespace character
{

    public class KnightBase : CharacterPlayerUnit
    {
        // スクリプトのインスペクタ上に設定項目が追加される 
        public GameObject base_object;  // 自身のキャラクターオブジェクト
        public GameObject anim_object;  // アニメーションオブジェクト

        public StateManager state_manager = new StateManager();
        public State_Walk walk = new State_Walk();
        public State_Stay stay = new State_Stay();

        // Use this for initialization
        void Start()
        {
            //_character = GameObject.Find("character_knight");
            _animator = anim_object.GetComponent<Animator>();
            //_animator = GameObject.Find("body").GetComponent<Animator>();
            //_animator = GetComponent<Animator>();

            // 初期化
            InitPlayerUnit();

            // stateクラス初期化
            state_manager.InitializeState(stay);

        }

        // Update is called once per frame
        void Update()
        {
            // キャラクタの基本動作
            CharacterBaseUpdate();

            // アニメーション
            BaseAnimation();

            // stateクラス実行
            state_manager.StateExe();

            // 殺害
            if (_target_object != null)
            {
                Destroy(_target_object);
                _move_speed = 1.0f;
            }
        }

        // Collider2D
        void OnTriggerEnter2D(Collider2D unit_collider)
        {
            // レイヤー名を取得
            string layer_name = LayerMask.LayerToName(unit_collider.gameObject.layer);

            // 反対勢力ユニットの場合
            if (layer_name == "EnemyUnit" && _target_object == null)
            {
                _target_object = unit_collider.gameObject;
                _move_speed = 0.0f;
                
            }
        }

        //　待機
        public class State_Stay : State
        {
            public override void Init()
            {

            }
            public override void Exe()
            {

            }
            public override void Exit()
            {

            }
        }

        //　動く
        public class State_Walk : State
        {
            public override void Init()
            {

            }
            public override void Exe()
            {

            }
            public override void Exit()
            {

            }
        }


        // 状況クラスの定義
        public class StateManager
        {
            // 状態を保持するプロパティを定義します。
            private State CurrentState;

            // 初期化
            public void InitializeState(State state)
            {
                state.Init();
                CurrentState = state;
            }

            // 状態遷移
            public void ChangeState( State next_state )
            {
                CurrentState.Exit();
                next_state.Init();
                CurrentState = next_state;
            }

            //保持している状態オブジェクトに対して処理を送ります。
            public void StateExe()
            {
                CurrentState.Exe();
            }
        }
    }
}
