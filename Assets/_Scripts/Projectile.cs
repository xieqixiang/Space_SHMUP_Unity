﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField]
    private WeaponType _type;

    public WeaponType type {
        get
        {
            return _type;
        }
        set {
            SetType(value);
        }
    }

    void Awake() {
        InvokeRepeating("CheckOffscreen",2f,2f);
    }

    public void SetType(WeaponType eType) {

        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        this.GetComponent<Renderer>().material.color = def.projectileColor;
    }

    void CheckOffscreen() {
        if (Utils.ScreenBoundsCheck(this.GetComponent<Collider>().bounds,Utils.BoundsTest.offScreen) != Vector3.zero) {
            Destroy(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
