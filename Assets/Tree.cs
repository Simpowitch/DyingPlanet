using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public static List<Tree> trees = new List<Tree>();


    public int wood = 10;

    private void Start()
    {
        trees.Add(this);
    }


    public int TakeTrees(int numberOfTrees)
    {
        int woodTaken = Mathf.Min(numberOfTrees, wood);
        wood -= woodTaken;
        if (wood <= 0)
        {
            Despawn();
        }
        return woodTaken;
    }

    private void Despawn()
    {
        trees.Remove(this);
        Destroy(this.gameObject);
    }
}
