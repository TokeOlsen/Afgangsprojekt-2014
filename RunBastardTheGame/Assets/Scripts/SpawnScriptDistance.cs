﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnScriptDistance : MonoBehaviour {

    public List<GameObject> objectTypes;          //Listen over mulige spawnobjekter
    public GameObject uniqueObject;               //Specielt objekt der ikke passer i listen. F.eks slutplatformen
    public float distanceBetweenObjects = 10f;    //Hvor langt er der i afstand mellem de objekter der skal spawnes
    public int numberOfObjectsToSpawn = 10;       //Hvor mange objekter skal der spawnes i lvl
    
    private GameObject _lastObject;               //Det sidst spawnede objekt
    private int _spawnedObjects = 0;              //Delta antal spawnede objekter
    private bool _allObjectsSpawned = false;      //Bruges til at se om alle objekter er spawnet
    

	// Use this for initialization
	void Start () {
        PoolSpawns();
        Spawn();
    
	}

	
	// Update is called once per frame
	void Update () {

        if(_allObjectsSpawned == false)
        {
            //Finder afstanden imellem dette objekt og det sidst spawnede objekt
            float distance = Vector3.Distance(this.transform.position, _lastObject.transform.position);

            //hvis afstanden mellem dette objekt og det sidste spawnede er større end det brugerdefinerede
            //Og det ikke er slutningen af lvl.
            if(distance >= distanceBetweenObjects && _spawnedObjects != numberOfObjectsToSpawn)
            {
                //spawn nyt objekt
                Spawn();
            }

            //hvis afstanden mellem dette objekt og det sidste spawnede er større end det brugerdefinerede
            //og det ER slutningen af lvl.
            if (distance >= distanceBetweenObjects && _spawnedObjects == numberOfObjectsToSpawn)
            {
                SpawnUnique();
            }

        }
	}

    /// <summary>
    /// Sæt alle platformtyper til inaktive.
    /// </summary>
    private void PoolSpawns()
    {
 
        for (int i = 0; i < objectTypes.Count; i++)
        {

            objectTypes[i].SetActive(false);
                    
        }

        //Debug.Log(platformTypes.Count.ToString());
    }

    /// <summary>
    /// Vælger et tilfældigt inaktiv objekt fra listen, og sætter den til aktiv.
    /// </summary>
    private void Spawn()
    {
        bool found = false;

        //Så længe der ikke er fundet et inaktiv objekt
         while (!found)
         {
             //Vælg et tilfældigt objekt, her ved vi ikke hvilke der er inaktive
            int i = Random.Range(0, objectTypes.Count);
            
            //Se om det valgte objekt er aktivt
            if (!objectTypes[i].activeInHierarchy)
            {
                //Hvis den er inaktiv, så opret den og opdater spillet med antal spawnede objekter
                _spawnedObjects++;
                _lastObject = objectTypes[i];
                objectTypes[i].transform.position = this.transform.position;
                objectTypes[i].SetActive(true);
                Instantiate(objectTypes[i], this.transform.position, Quaternion.identity);
                found = true;
            }
        }

        

    }

    /// <summary>
    /// Spawner det unikke objekt, og sætter slutvariablen.
    /// </summary>
    private void SpawnUnique()
    {
        Instantiate(uniqueObject, transform.position, Quaternion.identity);
        _allObjectsSpawned = true;
    }

}
