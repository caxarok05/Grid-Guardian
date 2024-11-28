using System;
using UnityEngine;


namespace Scripts.Logic
{
    public class UniqueId : MonoBehaviour
    {
        public string Id;
        private void Awake()
        {
            Id = $"{gameObject.scene.name}_{Guid.NewGuid()}";
        }
    }
   
}