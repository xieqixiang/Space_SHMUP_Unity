﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour {

    static public Main S;
    static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;


    public GameObject[] prefabEnemies;
    public float enemySpawnPerSecond = 0.5f;
    public float enemySpawnPadding = 1.5f;

    public bool _______________;
    public float enemySpawnRate;

    public WeaponDefinition[] weaponDefinitions;
    public WeaponType[] activeWeaponTypes;

    

    void Awake() {
        S = this;
        Utils.SetCameraBounds(this.GetComponent<Camera>());

        enemySpawnRate = 1f / enemySpawnPerSecond;

        Invoke("SpawnEnemy",enemySpawnRate);

        W_DEFS = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions) {
            W_DEFS[def.type] = def;
        }
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType wt) {
        if (W_DEFS.ContainsKey(wt)) {
            return  W_DEFS[wt];
        }

        return new WeaponDefinition();
         
    }

    public void SpawnEnemy() {
        int ndx = Random.Range(0,prefabEnemies.Length);

        GameObject go = Instantiate(prefabEnemies[ndx]) as GameObject;

        Vector3 pos = Vector3.zero;

        float xMin = Utils.camBounds.min.x + enemySpawnPadding;
        float xMax = Utils.camBounds.max.x - enemySpawnPadding;

        pos.x = Random.Range(xMin,xMax);
        pos.y = Utils.camBounds.max.y + enemySpawnPadding;

        go.transform.position = pos;

        Invoke("SpawnEnemy",enemySpawnRate);

    }

    public void DelayedRestart(float delay) {
        Invoke("Restart",delay);
    }

    public void Restart() {
        SceneManager.LoadScene("_Scene_0");
    }

	// Use this for initialization
	void Start () {
        activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
        for (int i = 0; i < weaponDefinitions.Length; i++)
        {
            activeWeaponTypes[i] = weaponDefinitions[i].type;

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
