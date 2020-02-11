using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public enum PersonState { GetWood, LeaveWood }
    public PersonState state = PersonState.GetWood;

    public Transform home;
    public Transform targetTree;

    public float speed = 0.5f;

    public int carryCapacity = 1;
    public int carrying = 1;

    private void Start()
    {
        speed += Random.Range(-0.1f, 0.1f);
        FindTree();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetTree == null)
        {
            targetTree = FindTree();
            if (targetTree == null)
            {
                return;
            }
        }

        //Find target
        Vector3 target = Vector3.zero;
        switch (state)
        {
            case PersonState.GetWood:
                target = targetTree.position;
                break;
            case PersonState.LeaveWood:
                target = home.position;
                break;
        }


        //Find direction
        Vector3 moveDir = target - transform.position;

        //Move
        this.transform.position += (moveDir * speed * Time.deltaTime);
    }

    //Finds a random forest
    private Transform FindTree()
    {
        if (Tree.trees.Count > 0)
        {
            return Tree.trees[Random.Range(0, Tree.trees.Count)].transform;
        }
        else
        {
            return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (state == PersonState.GetWood && collision.gameObject.GetComponent<Tree>() != null)
        {
            //Get Wood
            carrying = collision.gameObject.GetComponent<Tree>().TakeTrees(carryCapacity);
            state = PersonState.LeaveWood;
        }
        else if (state == PersonState.LeaveWood && collision.gameObject.GetComponent<House>() != null)
        {
            //Leave wood
            collision.gameObject.GetComponent<House>().AddMaterials(carrying);
            carrying = 0;
            state = PersonState.GetWood;
        }
    }
}
