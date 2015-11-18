using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;


public class GameMgr : MonoBehaviour
{
    GameMgr()
    {
        Instance = this;
    }

    static GameMgr Instance = null;

    public static GameMgr GetInstance()
    {
        if(Instance == null)
        {
            Instance = new GameMgr();
        }
        return Instance;
    }

    public Transform Monster = null;
    public Vector3 MonsterUIPosition = Vector3.zero;
    public GameObject Player = null;
    public GameObject Effect = null;
    public GameObject Heroes = null;
    public List<GameObject> TextList = new List<GameObject>();
    public List<GameObject> EffectList = new List<GameObject>();
    public bool OnDebuging = false;
    public bool IsGamePlaying = true;    
    public uint WeaponLevel = 1;
    public uint Damage = 1;
    public uint ActivatedHeroCount = 0;
    public float AttackTimer = 30.0f;
    public float CriticalPercent = 10;

    List<GameObject> HeroList = new List<GameObject>();
    float time = 0.05f;
    float currenttime = 0;
    uint StageNumber = 1;
    uint lastStageNumber = 0;
    uint Gold = 0;
    
    float DamageRand = 0;
    float CurrentTimer = 0;
    

    void Awake()
    {
        DamageRand = Damage * 0.05f;
        ResetTimer();
        if (Player == null)
        {
            Player = GameObject.Find("FreeCharacter_model");
        }
        if (TextList.Count == 0)
        {
            var Temp = GameObject.Find("UI Root/UICam/Panel/DMGText");
            int ChildCount = Temp.transform.childCount;
            for( int i = 0; i < ChildCount; i++)
            {
                TextList.Add(Temp.transform.GetChild(i).gameObject);
            }
        }
        if( Monster == null)
        {
            Monster = GameObject.Find("Monster").transform;
        }
        if ( Effect == null)
        {
            Effect = GameObject.Find("UI Root/UICam/Panel/Effect");
        }
        if( Heroes == null)
        {
            Heroes = GameObject.Find("Heroes");
        }
        if( HeroList.Count == 0)
        {
            int ChildCount = Heroes.transform.childCount;
            for (int i = 0; i < ChildCount; i++)
            {
                HeroList.Add(Heroes.transform.GetChild(i).gameObject);
                if (ActivatedHeroCount >= i + 1)
                {
                    HeroList[i].SetActive(true);
                }
            }
        }
        Vector3 pos = Camera.main.WorldToScreenPoint(Monster.position);
        Camera UICam = NGUITools.FindCameraForLayer(Effect.layer);
        MonsterUIPosition = UICam.ScreenToWorldPoint(new Vector3(pos.x, pos.y + 50.0f, 0));
        Debug.Log("Monsterpos x : " + MonsterUIPosition.x + ", y : " + MonsterUIPosition.y);
    }

    void Update()
    {
        UpdateEffect();
    }

    public void PlayEffect(Vector3 _Position, string _EffectName)
    {
        string Path = string.Concat("PreFab/Effect/", _EffectName);

        GameObject Temp = NGUITools.AddChild(Effect, (GameObject)Resources.Load(Path));
        Temp.transform.position = UICamera.currentCamera.ScreenToWorldPoint(_Position);
        Debug.Log("position x : " + Temp.transform.position.x + ", y : " + Temp.transform.position.y);
    }

    void LoadData()
    {

    }

    void SaveData()
    {

    }

    // Call when App quit
    void OnApplicationQuit()
    {

    }

    // Call when App paused
    void OnApplicationPause()
    {
        IsGamePlaying = false;
    }

    public void Attack(uint _Damage, Color _Color)
    {
        uint CurrntDamage = _Damage + (uint)DamageRand;
        bool IsCritical = false;
        if( Random.Range(0.0f, 100.0f) <= CriticalPercent)
        {
            CurrntDamage = (uint)(CurrntDamage * 2f);
            IsCritical = true;
        }

        DamagePrint(CurrntDamage, IsCritical);
    }

    void DamagePrint(uint _Damage, bool _IsCritical)
    {
        Color _FontColor = Color.yellow;
        foreach(GameObject _object in TextList)
        {
            if(!_object.active)
            {
                if(_IsCritical)
                {
                    _FontColor = Color.red;
                }
                _object.SetActive(true);
                _object.GetComponent<DamageText>().Init(_Damage, Monster.position, _FontColor);
                return;
            }
        }
    }

    void ChangeEnvironment()
    {

    }

    void ResetTimer()
    {
        CurrentTimer = AttackTimer;
    }

    public void AddAttackEffect(GameObject _EffectPrefab)
    {
        EffectList.Add(_EffectPrefab);
    }

    void UpdateEffect()
    {
        if (EffectList.Count == 0)
        {
            return;
        }

        // list 대신 dictionary로 바꾸고 sqr거리 판단 true 시 데미지가 뜨도록 수정해야 함
        for( int i = 0; i < EffectList.Count; i++)
        {
            if (EffectList[i] == null)
            {
                return;
            }
            if (Vector3.SqrMagnitude(MonsterUIPosition - EffectList[i].transform.position) >= 0.0001f)
            {
                Vector3 Normalize = MonsterUIPosition - EffectList[i].transform.position;
                Normalize = Vector3.Normalize(Normalize);
                EffectList[i].transform.position += Normalize * Time.deltaTime * 0.5f;
            }
            else
            {
                EffectList[i].SetActive(false);
                GameObject Temp = EffectList[i];
                Destroy(Temp);
                    EffectList.Remove(EffectList[i]);
            }
        }
    }
}
