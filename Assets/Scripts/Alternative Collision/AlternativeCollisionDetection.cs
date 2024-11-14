using System.Collections.Generic;
using UnityEngine;

public sealed class AlternativeCollisionDetection : MonoBehaviour
{
    private static readonly List<ICollisionHandler> _collisionHandlers = new List<ICollisionHandler>();


    public static void Add(ICollisionHandler collisionHandler)
    {
        if (_collisionHandlers.Contains(collisionHandler))
        {
            Debug.LogError("Failed to add ICollisionHandle. It is already added!");
        }
        else
        {
            _collisionHandlers.Add(collisionHandler);
        }
    }

    public void FixedUpdate()
    {
        CheckCollisions();
    }

    private static void CheckCollisions()
    {
        for (int i = 0; i < _collisionHandlers.Count; i++)
        {
            ICollisionHandler a = _collisionHandlers[i];
            if (a.Collider.gameObject.activeInHierarchy == false)
                continue;

            for (int j = i + 1; j < _collisionHandlers.Count; j++)
            {
                ICollisionHandler b = _collisionHandlers[j];
                if (b.Collider.gameObject.activeInHierarchy == false)
                    continue;

                if (a.Type == AlternativeRigidBody.BodyType.Static && b.Type == AlternativeRigidBody.BodyType.Static)
                    continue;

                if (CheckCollision(a.Collider, b.Collider, out AlternativeCollision collision))
                {
                    collision.collider = b.Collider;
                    a.OnAlternativeCollisionEnter(collision);

                    collision.collider = a.Collider;
                    b.OnAlternativeCollisionEnter(collision);
                }
            }
        }
    }

    public static bool CheckCollision(Collider collider1, Collider collider2, out AlternativeCollision collisionInfo)
    {
        if (collider1 is BoxCollider a && collider2 is BoxCollider b)
        {
            return CheckCollision(a, b, out collisionInfo);
        }
        else
        {
            Debug.LogError("Unsupported collider types");

            collisionInfo = new AlternativeCollision();
            return false;
        }
    }

    public static bool CheckCollision(BoxCollider box1, BoxCollider box2, out AlternativeCollision collisionInfo)
    {
        collisionInfo = new AlternativeCollision();

        if (box1.bounds.Intersects(box2.bounds))
        {
            Vector3[] box1Vertices = GetBoxVertices(box1);
            Vector3[] box2Vertices = GetBoxVertices(box2);

            if (SATCollisionDetection(box1Vertices, box2Vertices, box1.transform.rotation, box2.transform.rotation, out Vector3 mtv, out float depth))
            {
                collisionInfo.contactPoint = GetContactPoint(box1Vertices, box2Vertices);
                collisionInfo.contactNormal = mtv.normalized;
                return true;
            }
        }
        return false;
    }

    private static Vector3[] GetBoxVertices(BoxCollider box)
    {
        Vector3 center = box.transform.TransformPoint(box.center);
        Vector3 size = box.size * 0.5f;
        Quaternion rotation = box.transform.rotation;

        Vector3[] vertices = new Vector3[8];
        vertices[0] = center + rotation * new Vector3(-size.x, -size.y, -size.z);
        vertices[1] = center + rotation * new Vector3(size.x, -size.y, -size.z);
        vertices[2] = center + rotation * new Vector3(size.x, -size.y, size.z);
        vertices[3] = center + rotation * new Vector3(-size.x, -size.y, size.z);
        vertices[4] = center + rotation * new Vector3(-size.x, size.y, -size.z);
        vertices[5] = center + rotation * new Vector3(size.x, size.y, -size.z);
        vertices[6] = center + rotation * new Vector3(size.x, size.y, size.z);
        vertices[7] = center + rotation * new Vector3(-size.x, size.y, size.z);

        return vertices;
    }

    private static bool SATCollisionDetection(Vector3[] vertices1, Vector3[] vertices2, Quaternion rotation1, Quaternion rotation2, out Vector3 mtv, out float minPenetration)
    {
        mtv = Vector3.zero;
        minPenetration = float.MaxValue;

        List<Vector3> axes = new List<Vector3>();

        axes.AddRange(GetRotatedAxes(rotation1));
        axes.AddRange(GetRotatedAxes(rotation2));

        foreach (Vector3 axis in axes)
        {
            (float min1, float max1) = ProjectVerticesOnAxis(vertices1, axis);
            (float min2, float max2) = ProjectVerticesOnAxis(vertices2, axis);

            if (min1 > max2 || min2 > max1)
            {
                return false;
            }

            float penetration = Mathf.Min(max1 - min2, max2 - min1);
            if (penetration < minPenetration)
            {
                minPenetration = penetration;
                mtv = axis;
            }
        }

        return true;
    }

    private static Vector3[] GetRotatedAxes(Quaternion rotation)
    {
        Vector3[] rotatedAxes = new Vector3[3];
        rotatedAxes[0] = rotation * Vector3.right;
        rotatedAxes[1] = rotation * Vector3.up;
        rotatedAxes[2] = rotation * Vector3.forward;
        return rotatedAxes;
    }

    private static (float min, float max) ProjectVerticesOnAxis(Vector3[] vertices, Vector3 axis)
    {
        float min = Vector3.Dot(vertices[0], axis);
        float max = min;

        for (int i = 1; i < vertices.Length; i++)
        {
            float projection = Vector3.Dot(vertices[i], axis);
            if (projection < min) min = projection;
            if (projection > max) max = projection;
        }

        return (min, max);
    }

    private static Vector3 GetContactPoint(Vector3[] vertices1, Vector3[] vertices2)
    {
        return (vertices1[0] + vertices2[0]) / 2f;
    }
}