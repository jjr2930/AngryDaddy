using UnityEngine;
using System.Collections;

public class Behavior_Title_bg : MonoBehaviour {

    public Vector2 animRate = new Vector2(1f, 0f);

    void Update()
    {
        UITexture tex = GetComponent<UITexture>();

        if (tex != null)
        {
            Rect rect = tex.uvRect;
            rect.x += animRate.x * Time.deltaTime;
            rect.y += animRate.y * Time.deltaTime;
            tex.uvRect = rect;
        }
    }
}
