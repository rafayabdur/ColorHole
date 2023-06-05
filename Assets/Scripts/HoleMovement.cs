using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
public class HoleMovement : MonoBehaviour
{
    [Header("HoleMesh")]
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] MeshCollider meshCollider;

    [Header("Hole Vertices Radius")]
    [SerializeField] Vector2 moveLimits;
    [SerializeField] float radius;
    [SerializeField] Transform holeCenter;
    [SerializeField] Transform rotationgCirle;

    [Space]
    [SerializeField] float moveSpeed, touchSpeed, cursorSpeed;
    Mesh mesh;
    List<int> holeVertices;
    List<Vector3> offsets;
    int holeVerticesCount;

    float x, y;
    Vector3 touch, targetPos;



    void Start()
    {
        RotateCircleAnim();
        Game.isMoving = false;
        Game.isGameover = false;

        holeVertices = new List<int>();
        offsets = new List<Vector3>();

        mesh = meshFilter.mesh;

        //find hole Vertices on the mesh
        FindHoleVertices();
    }

    void RotateCircleAnim()
    {
        rotationgCirle.DORotate(new Vector3(90f, 0, -90f), .02f)
        .SetEase(Ease.Linear)
        .From(new Vector3(90f, 0, 0))
        .SetLoops(-1, LoopType.Incremental);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        //mouse
        Game.isMoving = Input.GetMouseButton(0);
        if (!Game.isGameover && Game.isMoving)
        { //Move hole center
            MoveHole();
            //Update hole vertices
            UpdateHoleVerticesPosition();
        }

        //Mobile touch move
#else

        if (Game.isMoving = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if (!Game.isGameover && Game.isMoving)
            { //Move hole center
                MoveHole();
                //Update hole vertices
                UpdateHoleVerticesPosition();
            }
        }
#endif
    }

    void MoveHole()
    {
#if UNITY_EDITOR
        moveSpeed = cursorSpeed;
#else
        moveSpeed = touchSpeed;
#endif

        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(holeCenter.position,
            holeCenter.position + new Vector3(x, 0f, y),
            moveSpeed * Time.deltaTime);

        targetPos = new Vector3
        (
            Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x),
            touch.y,
            Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y)
            );

        holeCenter.position = targetPos;
    }

    void UpdateHoleVerticesPosition()
    {
        Vector3[] vertices = mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }
        //Update Mesh
        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);
            if (distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }
        holeVerticesCount = holeVertices.Count;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(holeCenter.position, radius);
    }
} 


