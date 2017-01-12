using UnityEngine;
using System.Collections;

public abstract class Projectile : MonoBehaviour
{
    protected Owner _owner;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public abstract void Initialize(int level, Owner owner, Vector3 position, Vector3 eulerAngles);
}
