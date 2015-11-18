using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public float LifeTime = 0.5f;
    float CurrentLifeTime = 0;
    UILabel Label = null;
    Camera UICam = null;
    Vector3 Target = Vector3.zero;
    
    public void Init(uint _Damage, Vector3 _Target, Color _Color)
    {
        Target = _Target;
        Label = GetComponent<UILabel>();
        Label.text = _Damage.ToString();
        Label.color = _Color;
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
            Position.y += Time.deltaTime * 0.5f;
            transform.position = Position;

            Color tempColor = Label.color;
            tempColor.a -= 255.0f / (Time.deltaTime * 0.5f);
        }
        else
        {
            Init(0, Target, Color.yellow);
            gameObject.SetActive(false);
        }
	}
}
