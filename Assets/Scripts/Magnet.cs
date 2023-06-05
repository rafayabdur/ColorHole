using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    #region Singleton Class : Magnet

    public static Magnet Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] float magnetForce;
    [SerializeField] List<Rigidbody> affectedrigidbodies = new List<Rigidbody> ();
    Transform magnet;



    // Start is called before the first frame update
    void Start()
    {
        magnet = transform;
        affectedrigidbodies.Clear();
    }

    private void FixedUpdate()
    {
        if (!Game.isGameover && Game.isMoving)
        {
            foreach (Rigidbody rb in affectedrigidbodies)
            {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (!Game.isGameover && (tag.Equals("Obstacle") || tag.Equals("Object")))
        {
            AddToMagnetField(other.attachedRigidbody);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        string tag = other.tag;
        if (!Game.isGameover && (tag.Equals("Obstacle") || tag.Equals("Object")))
        {
            RemoveFromMagnetField(other.attachedRigidbody);
        }
    }

    public void AddToMagnetField(Rigidbody rb)
    {
        affectedrigidbodies.Add(rb);

    }
    public void RemoveFromMagnetField(Rigidbody rb)
    {
        affectedrigidbodies.Remove(rb);

    }
}
