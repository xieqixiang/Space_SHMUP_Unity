using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    static public Hero S;

    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;

    [SerializeField]
    private float _shieldLevel = 1;

    public bool _____________;

    public Bounds bounds;

    public GameObject lastTriggerGo = null;

    public float gameRestartDelay = 2f;

    public delegate void WeaponFireDelegate();

    public WeaponFireDelegate fireDelegate;

    void Awake() {
        S = this;

        bounds = Utils.CombineBoundsOfChildren(this.gameObject);
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float xAxis = Input.GetAxis("Horizontal");

        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;

        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;

        transform.position = pos;

        bounds.center = transform.position;

        Vector3 off = Utils.ScreenBoundsCheck(bounds,Utils.BoundsTest.center);

        if (off != Vector3.zero) {
            pos -= off;
            transform.position = pos;
        }

        transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);

        string sss = Input.inputString;
        print(sss);
        if (Input.GetAxis("Fire1") == 1 && fireDelegate != null) {
            fireDelegate();
        }
	}

    void OnTriggerEnter(Collider other) {
        GameObject go = Utils.FindTaggedParent(other.gameObject);

        if (go != null)
        {
            if (go == lastTriggerGo) {
                return;
            }
            lastTriggerGo = go;

            if (go.tag == "Enemy") {
                shieldLevel--;
                Destroy(go);
            }
        }
        else
        {
            print("触发碰撞事件："+other.gameObject.name);
        }
    }

    public float shieldLevel {
        get {
            return _shieldLevel;
        }
        set {
            _shieldLevel = Mathf.Min( value , 4.0f );

            if (value < 0) {
                Destroy(this.gameObject);

                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }
}
