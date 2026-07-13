using System;
using UnityEngine;

namespace _Game.Scripts.Chunks
{
public class ChunkController: MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with " + collision.gameObject.name);
        }
    }
}
}