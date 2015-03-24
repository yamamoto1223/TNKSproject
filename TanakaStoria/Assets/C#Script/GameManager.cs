using UnityEngine;
using System.Collections;


// CharacterBassClass
public class GameManager : MonoBehaviour {

    private float _timer = 0.0f;
    
    public GameObject CharacterKnight;
   
	
    // Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        // スペースキーを押す
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ランダム値
            float rand_y = Random.value * 3.0f - 2.0f;
            // インスタンス生成
            Instantiate(CharacterKnight, new Vector3(-7, rand_y, 0), Quaternion.identity);
        }
	}

    // timer
    public bool timer(int waitingTime)
    {
        _timer += Time.deltaTime;
        if (_timer > waitingTime)
        {
            _timer = 0;
            return true;
        }
        return false;
    }
}
