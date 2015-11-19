using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public float LifeTime = 0.5f;
    float CurrentLifeTime = 0;
    float UpSpeed = 0.5f;
    UILabel Label = null;
    Camera UICam = null;
    Vector3 Target = Vector3.zero;
    
    public void Init(long _Damage, Vector3 _Target, Color _Color, int _FontSize = 40, float _UpSpeed = 0.5f)
    {
        Target = _Target;
        Label = GetComponent<UILabel>();
        Label.text = _Damage.ToString();
        Label.color = _Color;
        Label.fontSize = _FontSize;
        UpSpeed = _UpSpeed;
        CurrentLifeTime = LifeTime;
        if (UICam == null)
        {
            UICam = NGUITools.FindCameraForLayer(gameObject.layer);
        }
        Vector3 pos = Camera.main.WorldToScreenPoint(_Target);
        transform.position = UICam.ScreenToWorldPoint(new Vector3(pos.x, pos.y + 100.0f, 0));
    }

    void Update ()
    {
	    if( (CurrentLifeTime -= Time.deltaTime) > 0)
        {
            Vector3 Position = transform.position;
            Position.y += Time.deltaTime * UpSpeed;
            transform.position = Position;

            Color tempColor = Label.color;
            tempColor.a -= Time.deltaTime;
            Label.color = tempColor;
        }
        else
        {
            Init(0, Target, Color.white);
            gameObject.SetActive(false);
        }
	}
}
