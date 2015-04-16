using UnityEngine;
using System.Collections;

public class EffectCreater : MonoBehaviour {

    public GameObject SampleEffect;

    // カメラにアタッチ
    //[RequireComponent(typeof(Camera))]
    public void CreateEffect_DamageNum(Vector3 pos, int damage)
    {
        string str_damage = damage.ToString();
        // 桁数取得
        //int digit = damage.ToString().Length;

        // 親の生成
        GameObject root = new GameObject();

        // エフェクト生成
        GameObject obj = (GameObject)Instantiate(SampleEffect, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        Effect_DamageNumGroup effect = obj.GetComponent<Effect_DamageNumGroup>();

        
        // スクリーン座標からワールド座標へ
        obj.transform.SetParent(root.transform);
        Camera _camera = GameObject.Find("MainCamera").camera;
        //root.transform.position = GameObject.Find("MainCamera").camera.WorldToScreenPoint(pos);
        var sp = _camera.ViewportToWorldPoint( new Vector3(1,1,0) );    // 画面右上のワールド座標 

        float sw = sp.x; 
        float sh = sp.y;

        // スクリーンサイズ
        float w = Screen.width;     
        float h = Screen.height;

        pos.x = pos.x * ((w/2.0f) / sw);
        pos.y = pos.y * ((h/2.0f) / sh);
        root.transform.position = pos;
   
        //GameObject objNumObject = effect.GetN umberObj();
        //RectTransform t = objNumObject.GetComponent<RectTransform>();
        //t.position = pos;
          
        
        effect.SetBaseObject(root);
        //effect.SetDamageNum(damage);
        effect.SetDamageStr(str_damage);
    }
}

//public class PopupDamageGen : MonoBehaviour
//{
//    public GameObject TargetCanvas;     // キャンバスオブジェクト
//    public GameObject PopupTextObject;  // テキストオブジェクト

//    public string PopupString;          // 表示する文字 
//    public Vector3 PopupPosition;       // 表示する座標
//    public float PopupTextWidth;        // 表示する幅

//    /// <summary>
//    /// ポップアップの実行
//    /// </summary>
//    public void Popup()
//    {
//        StartCoroutine(Execute());
//    }

//    /// <summary>
//    /// ポップアップ実行
//    /// </summary>
//    private IEnumerator Execute()
//    {
//        Vector3 pos = this.PopupPosition;
//        var texts = new List<Effect_DamageNum>();

//        GameObject root = new GameObject();
//        CanvasGroup canvasGroup = root.AddComponent<CanvasGroup>();
//        root.transform.SetParent(this.TargetCanvas.transform);

//        // 桁数分ループ
//        foreach (var s in this.PopupString)
//        {
//            var obj = new GameObject();
//            obj.transform.position = pos;
//            obj.transform.SetParent(root.transform);

//            // 1文字ずつ生成
//            var valueText = (GameObject)Instantiate(this.PopupTextObject, pos, Quaternion.identity);
//            var textComp = valueText.GetComponent<Text>();
//            textComp.text = s.ToString();
//            valueText.transform.SetParent(obj.transform);
//            texts.Add(valueText.GetComponent<NumberTextScript>());

//            // 0.03秒待つ(適当)
//            yield return new WaitForSeconds(0.03f);

//            // 次の位置
//            pos.x += this.PopupTextWidth;
//        }

//        // 適当に待ち
//        while (!texts.TrueForAll(t => t.IsFinish))
//        {
//            yield return new WaitForSeconds(0.1f);
//        }

//        // フェードアウト
//        for (int n = 9; n >= 0; n--)
//        {
//            canvasGroup.alpha = n / 10.0f;
//            yield return new WaitForSeconds(0.01f);
//        }

//        // 破棄
//        Destroy(root);
//        Destroy(gameObject);
//    }
//}