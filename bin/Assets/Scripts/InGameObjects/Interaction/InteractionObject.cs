using System;
using System.IO;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class InteractionObject : InteractingObject
    {
        [Header("게임 시작 전 꼭 설정해 줘야함")]
        [SerializeField] private SpriteRenderer SpriteRenderer;
        [Header("게임 시작 후 알아서 설정됨")]
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material selectingMaterial;

        public void SetSelecting(bool selecting)
        {
            if (SpriteRenderer == null)
                return;
            if (selecting)
            {
                SpriteRenderer.material = selectingMaterial;
            }
            else
            {
                SpriteRenderer.material = defaultMaterial;
            }
        }
        
        private void Start()
        {
            if (SpriteRenderer == null)
                return;
            defaultMaterial = SpriteRenderer.material;
            selectingMaterial = Resources.Load<Material>(Path.Combine("ShaderMaterials", "Border Material"));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerManager.Instance.interactingPlayer.AddInteractionList(this);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerManager.Instance.interactingPlayer.RemoveInteractionObj(this);
            }
        }
    }
}