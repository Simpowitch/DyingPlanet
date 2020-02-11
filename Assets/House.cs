using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private int materials;

    public int materialsNeed = 10;


    public int personsPerHouse = 4;
    public GameObject personPrefab = null;
    private Vector3[] personSpawnOffsets = new Vector3[] { new Vector3(2, 0), new Vector3(-2, 0), new Vector3(0, 2), new Vector3(0, -2) };

    private void Start()
    {
        SpawnThisHouse();
    }

    public void SpawnThisHouse()
    {
        for (int i = 0; i < personsPerHouse; i++)
        {
            GameObject newPerson = Instantiate(personPrefab);
            newPerson.transform.position = this.transform.position + personSpawnOffsets[Random.Range(0, personSpawnOffsets.Length)];
            newPerson.GetComponent<Person>().home = this.transform;
        }
    }

    public void AddMaterials(int add)
    {
        materials += add;
        if (materials >= materialsNeed)
        {
            SpawnAnotherHouse();
        }
    }

    private void SpawnAnotherHouse()
    {
        materials -= materialsNeed;
        //Spawn house close by
    }
}
