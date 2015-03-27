using UnityEngine;
using System.Collections;

using define;

namespace character
{

    public class KnightBase : CharacterPlayerUnit
    {
        // スクリプトのインスペクタ上に設定項目が追加される 
        public GameObject base_object;  // 自身のキャラクターオブジェクト
        public GameObject anim_object;  // アニメーションオブジェクト

        public State_Walk walk = new State_Walk();
        public State_Stay stay = new State_Stay();
        public State_Attack attack = new State_Attack();
        public CharacterBase parent;
       

        // Use this for initialization
        void Start()
        {
            // AnimationControllerコンポーネント取得
            _animator = anim_object.GetComponent<Animator>();

            // 初期化
            CharacterBaseInit();
            PlayerUnitInit();

            // stateクラス初期化
            state_manager.SetUnitInstance(this);
            state_manager.SetAnimatorObj(anim_object);
            state_manager.InitializeState( walk);
        }

        // Update is called once per frame
        void Update()
        {
            // キャラクタの基本動作
            CharacterBaseUpdate();
            PlayerUnitUpdate();

            // アニメーション
            //BaseAnimation();

            // stateクラス実行
            state_manager.StateExe();
        }

        //public GameObject GetAnimationObject(){ return anim_object; }
        //public GameObject GetBaseObject() { return base_object; }

        // Collider2D
        void OnTriggerStay2D(Collider2D unit_collider)
        {
            // レイヤー名を取得
            string layer_name = LayerMask.LayerToName(unit_collider.gameObject.layer);

            // 反対勢力ユニットの場合
            if (layer_name == "EnemyUnit" && _target_object == null)
            {
                _target_object = unit_collider.gameObject;
            }
        }

        //　待機
        public class State_Stay : State
        {
            public override void Init()
            {
                unit._move_speed = 0.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTION_STAY);
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
                unit._move_speed = 1.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTION_WALK);
            }
            public override void Exe()
            {
                if (unit._target_object != null) 
                {
                    unit.state_manager.ChangeState(new State_Attack());
                }
            }
            public override void Exit()
            {

            }
        }

        //　攻撃
        public class State_Attack : State
        {
            private int attack_cnt = 0;
            public override void Init()
            {
                unit._move_speed = 0.0f;
                unit.ChangeAnimation((int)MotionIndex.MOTION_STAY);
            }
            public override void Exe()
            {
                GameObject animator_obj = unit.state_manager.GetAnimatorObj();
                if (unit._target_object != null)
                {
                    attack_cnt++;
                    if (attack_cnt > 120)
                    {
                        unit.ChangeAnimation((int)MotionIndex.MOTIOM_ATTACK);
                        attack_cnt = 0;
                    }
                }
                Animator            animator = animator_obj.GetComponent<Animator>();
                Animation           animation = animator_obj.GetComponent<Animation>();
                AnimatorStateInfo   animInfo = animator.GetCurrentAnimatorStateInfo(0);

                if (animInfo.nameHash == Animator.StringToHash("Base Layer.Attack"))
                {
                    //if( !animation.isPlaying )
                    if (animInfo.normalizedTime > 1.0f)
                    {
                        Destroy(unit._target_object);
                        if (unit._target_object == null)
                        {
                            unit.state_manager.ChangeState(new State_Walk());
                        }
                        else 
                        {
                            unit.state_manager.ChangeState(new State_Attack());                        
                        }
                    }
                }
            }
            public override void Exit()
            {
                unit._target_object = null;
            }
        }
    }
}
