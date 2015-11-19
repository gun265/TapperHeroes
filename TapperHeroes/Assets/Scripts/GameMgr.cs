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
    public List<Dictionary<string, object>> AttackList = new List<Dictionary<string, object>>();
    public bool OnDebuging = false;
    public bool IsGamePlaying = true;    
    public uint WeaponLevel = 1;
    public long Damage = 1;
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
        UpdateEffectList(Time.deltaTime);
    }

    public void PlayEffect(Vector3 _Position, string _EffectName)
    {
        string Path = string.Concat("PreFab/Effect/", _EffectName);

        GameObject Temp = NGUITools.AddChild(Effect, (GameObject)Resources.Load(Path));
        Temp.transform.position = UICamera.currentCamera.ScreenToWorldPoint(_Position);
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

    public void Attack(long _Damage, Color _Color, GameObject _Owner = null)
    {
        long CurrntDamage = _Damage + (long)DamageRand;
        bool IsCritical = false;
        if(_Owner == null && Random.Range(0.0f, 100.0f) <= CriticalPercent)
        {
            CurrntDamage = (uint)(CurrntDamage * 2f);
            IsCritical = true;
        }
        DamagePrint(CurrntDamage, IsCritical, _Color);
    }

    void DamagePrint(long _Damage, bool _IsCritical, Color _Color)
    {
        int FontSize = 40;
        float UpSpeed = 0.5f;
        foreach(GameObject _object in TextList)
        {
            if(!_object.active)
            {
                if(_IsCritical)
                {
                    _Color = Color.red;
                    FontSize = 60;
                    UpSpeed *= 0.5f;
                }
                _object.SetActive(true);
                _object.GetComponent<DamageText>().Init(_Damage, Monster.position, _Color, FontSize, UpSpeed);
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
    
    public void AddAttackList(GameObject _Owner, long _Damage, float _AttackSpeed, GameObject _AttackEffect)
    {
        Dictionary<string, object> Element = new Dictionary<string, object>();
        Element.Add("Owner", _Owner);
        Element.Add("Damage", _Damage);
        Element.Add("AttackSpeed", _AttackSpeed);
        Element.Add("AttackEffect", _AttackEffect);
        AttackList.Add(Element);
    }

    void UpdateEffectList(float _TimeDeltatime)
    {
        if( AttackList.Count == 0)
        {
            return;
        }

        for (int i = 0; i < AttackList.Count; i++)
        {
            if (AttackList[i] == null)
            {
                return;
            }
            GameObject EffectObject = AttackList[i]["AttackEffect"] as GameObject;
            Vector3 EffectPos = EffectObject.transform.position;
            float AttackSpeed = (float)AttackList[i]["AttackSpeed"];
            long AttackDamage = (long)AttackList[i]["Damage"];

            if (Vector3.SqrMagnitude(MonsterUIPosition - EffectPos) >= 0.0001f)
            {
                Vector3 Normalize = MonsterUIPosition - EffectPos;
                Normalize = Vector3.Normalize(Normalize);
                EffectObject.transform.position += (Normalize * _TimeDeltatime * 0.5f) / AttackSpeed;
            }
            else
            {
                EffectObject.SetActive(false);
                GameObject Owner = AttackList[i]["Owner"] as GameObject;
                GameObject Temp = EffectObject;
                Destroy(Temp);
                AttackList.Remove(AttackList[i]);
                Attack(AttackDamage, Color.yellow, Owner);
            }
        }

    }
}
