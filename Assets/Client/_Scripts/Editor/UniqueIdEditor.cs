using System;
using System.Linq;
using UnityEditor.SceneManagement;
using UnityEditor;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Editor
{
    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueId uniqueId = (UniqueId)target;

            if (IsPrefab(uniqueId))
               return;

            if (string.IsNullOrEmpty(uniqueId.Id))
                Generate(uniqueId);
            else
            {
                UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();

                if (uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                    Generate(uniqueId);
            }
        }

        private bool IsPrefab(UniqueId uniqueId) =>
          uniqueId.gameObject.scene.rootCount == 0;

        private void Generate(UniqueId uniqueId)
        {
            uniqueId.Id = $"{uniqueId.gameObject.scene.name}_{Guid.NewGuid().ToString()}";

            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }
}
