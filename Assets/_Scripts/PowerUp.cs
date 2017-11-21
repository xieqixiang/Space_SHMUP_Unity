﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public Vector2 rotMinMax = new Vector2(15,90);
    public Vector2 driftMinMax = new Vector2(.25f,2);
    public float leftTime = 6f;
    public float fadeTime = 4f;

    public bool _______________;

    public WeaponType type;
    public GameObject cube;
    public TextMesh letter;
    public Vector3 rotPerSecond;
    public float birthTime;

    void Awake() {
        cube = transform.Find("Cube").gameObject;

        letter = GetComponent<TextMesh>();

        Vector3 vel = Random.onUnitSphere;

        vel.z = 0;
        vel.Normalize();

        vel *= Random.Range(driftMinMax.x,driftMinMax.y);

        this.GetComponent<Rigidbody>().velocity = vel;

        transform.rotation = Quaternion.identity;

        rotPerSecond = new Vector3(Random.Range(rotMinMax.x,rotMinMax.y),Random.Range(rotMinMax.x,rotMinMax.y),Random.Range(rotMinMax.x,rotMinMax.y));

        InvokeRepeating("CheckOffscreen",2f,2f);

        birthTime = Time.time;


    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        cube.transform.rotation = Quaternion.Euler(rotPerSecond*Time.time);

        float u = (Time.time - fadeTime)/leftTime;

        if (u >= 1) {
            Destroy(this.gameObject);
            return;
        }

        if ( u > 0) {
            Color c = cube.GetComponent<Renderer>().material.color;
            c.a = 1f - u;
            cube.GetComponent<Renderer>().material.color = c;
            c = letter.color;
            c.a = 1f - (u*0.5f);
            letter.color = c;
        }
	}

    public void SetType(WeaponType wt) {

        WeaponDefinition def = Main.GetWeaponDefinition(wt);

        cube.GetComponent<Renderer>().material.color = def.color;

        letter.text = def.letter;

        type = wt;
    }

    public void AbsorbedBy(GameObject target) {
        Destroy(this.gameObject);
    }

    void CheckOffscreen() {
        if (Utils.ScreenBoundsCheck(cube.GetComponent<Collider>().bounds,Utils.BoundsTest.offScreen) != Vector3.zero) {
            Destroy(this.gameObject);
        }
    }
}
