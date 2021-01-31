using System;
using System.IO;
using UnityEngine;

namespace Script.Game.FinalScene
{
    public class MeteorBehavior : MonoBehaviour
    {
        public float moveSpeed = 15f;

        public Rigidbody2D rgd2d;

        private void Reset()
        {
            rgd2d = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rgd2d.velocity = new Vector2(0, -moveSpeed);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // TODO: explosion!
            var dino = other.gameObject.GetComponent<Dragon>();
            if (dino)
            {
                dino.SetState(eDragonState.Injurd);
            }
            Destroy(gameObject);
        }
    }
}
