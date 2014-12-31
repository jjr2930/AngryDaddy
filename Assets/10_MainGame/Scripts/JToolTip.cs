using UnityEngine;
using System.Collections;

public class JToolTip : MonoBehaviour {
    static JToolTip _instance;
    public static JToolTip Instance
    {
        get
        {
            if(_instance == null)
                _instance = GameObject.FindObjectOfType<JToolTip>();
            return _instance;
        }
    }
    public Camera _uiCamera;
    public UILabel _label;
    public UISprite _back;
    public float _showTime; //second
    public float _appearTime; //second
    public bool _isShow;
    public string _filter;

    private Vector3 _pickPos;
    private string _mouseOverObjName;
    private float _mouseOverStartTime;

    private Ray _ray;
    private RaycastHit _hit;

    private float _deltaAlpha;
	// Use this for initialization
	void Start () {
	    _hit = new RaycastHit();
        _isShow = false;
        SetAlpha(0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        _ray = _uiCamera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(_ray,out _hit, 10000f))
        {            
            if((_mouseOverObjName != _hit.transform.name))
            {
                //Debug.Log(_hit.transform.name);
                _mouseOverObjName = _hit.transform.name;
                _mouseOverStartTime = Time.unscaledTime;
                _pickPos = _hit.transform.localPosition;
                Hide();
            }            
        }
        else
        {
            _mouseOverObjName = "";
            _mouseOverStartTime = Time.unscaledTime;
            _pickPos = Vector3.zero;
            Hide();
        }
        if(Time.unscaledTime - _mouseOverStartTime >= _appearTime)
        {
            if (null != _hit.transform)
            {
                if (null != _hit.transform.GetComponent<JToolTipControl>())
                {
                    _hit.transform.GetComponent<JToolTipControl>().OnToolTip();
                }
            }
        }
	}
    void SetAlpha(float value)
    {
        Color nowColor = _label.color;
        nowColor.a = value;
        _label.color = nowColor;

        nowColor = _back.color;
        nowColor.a = value;
        _back.color = nowColor;
    }
    public void Show(string value)
    {
        _label.text = value;
        _back.height = _label.height;
        SetAlpha(1f);
        transform.localPosition = _pickPos + new Vector3(_back.width,_back.height/2);
        //Debug.Log(transform.localPosition);
        //화면을 넘어가지 않도록 해준다.
        Vector3 ScreenSizetoWorld = _uiCamera.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0));    //스크린의 월드 좌표를 구한다.
        if(transform.position .x> ScreenSizetoWorld.x)
        {
            transform.position = new Vector3(ScreenSizetoWorld.x,transform.position.y,0);
        }        
        /*
        if(ScreenPos.x + _back.width/2 >= Screen.width) //화면을 넘어가면
        {
            Vector3 newPos = new Vector3(Screen.width - +_back.width/2 ,ScreenPos.y,0); //보정해준다.
            transform.position = newPos;
        }
        transform.localPosition = _pickPos;
        */
    }
    public void Hide()
    {
        SetAlpha(0f);
    }

}
