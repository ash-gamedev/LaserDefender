using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class SpriteScroller : MonoBehaviour
    {
        [SerializeField] Vector2 moveSpeed;

        Vector2 offset;
        Material material;

        private void Awake()
        {
            material = GetComponent<SpriteRenderer>().material;
        }

        void Update()
        {
            offset = moveSpeed * Time.deltaTime;
            material.mainTextureOffset += offset;
        }
    }
}