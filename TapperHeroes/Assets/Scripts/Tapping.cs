using UnityEngine;
using System.Collections;

public class Tapping : MonoBehaviour {
    public bool CanIHit = true;
    float time = 0;
    
	void OnPress(bool _IsPress)
    {
        if (CanIHit  && _IsPress)
        {
            CanIHit = false;
            Vector3 position = new Vector3(UICamera.lastTouchPosition.x, UICamera.lastTouchPosition.y, 0);
            GameMgr.GetInstance().PlayEffect(position, "Hit");
            GameMgr.GetInstance().Attack(GameMgr.GetInstance().Damage, Color.white);
        }

        else if(!_IsPress)
        {
            CanIHit = true;
        }
    }
}
