using System;
using System.IO;
using Managers;
using UnityEngine;

namespace InGameObjects.Interaction
{
    [RequireComponent(typeof(BoxCollider2D))]
    public sealed class InteractionObject : InteractingObject
    {
        [SerializeField] private SpriteRenderer SpriteRenderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material selectingMaterial;

        public void SetSelecting(bool selecting)
        {
            Debug.Log("Selecting");
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