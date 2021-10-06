using System.IO;
using System.Linq;
using _Game.Libraries;
using _Game.Libraries.PathCreator.Examples.Scripts;
using UnityEngine;
using UnityEditor;
using PathCreation;
using UnityEditor.Experimental.SceneManagement;


namespace PathCreation.Examples
{
    [CustomEditor(typeof(PathSceneTool), true)]
    public class PathSceneToolEditor : Editor
    {
        protected PathSceneTool pathTool;
        bool isSubscribed;

        public override void OnInspectorGUI()
        {
            using (var check = new EditorGUI.ChangeCheckScope())
            {
                DrawDefaultInspector();

                if (check.changed)
                {
                    if (!isSubscribed)
                    {
                        TryFindPathCreator();
                        Subscribe();
                    }

                    if (pathTool.autoUpdate)
                    {
                        TriggerUpdate();
                    }
                }
            }

            if (GUILayout.Button("Manual Update"))
            {
                if (TryFindPathCreator())
                {
                    TriggerUpdate();
                    SceneView.RepaintAll();
                }
            }

            if (GUILayout.Button("Save Mesh"))
            {
                if (TryFindPathCreator())
                {
                    RoadMeshCreator roadMeshCreator = target as RoadMeshCreator;
                    MeshFilter meshFilter = roadMeshCreator?.meshFilter;

                    if (meshFilter == null)
                    {
                        Debug.LogError("There must be a mesh filter component.");
                        return;
                    }

                    var prefab = PrefabStageUtility.GetCurrentPrefabStage();
                    string directory = null;

                    if (prefab != null)
                    {
                        directory = prefab.assetPath
                                        .Replace(".prefab", "")
                                        .TrimEnd(Path.DirectorySeparatorChar)
                                    + Path.DirectorySeparatorChar;
                        
                    }

                    Mesh mesh = meshFilter.sharedMesh;
                    MeshSaverEditor.SaveMesh(mesh, mesh.name, true, true, directory);
                }
            }
        }


        void TriggerUpdate()
        {
            if (pathTool.pathCreator != null)
            {
                pathTool.TriggerUpdate();
            }
        }


        protected virtual void OnPathModified()
        {
            if (pathTool.autoUpdate)
            {
                TriggerUpdate();
            }
        }

        protected virtual void OnEnable()
        {
            pathTool = (PathSceneTool)target;
            pathTool.onDestroyed += OnToolDestroyed;

            if (TryFindPathCreator())
            {
                Subscribe();
                TriggerUpdate();
            }
        }

        void OnToolDestroyed()
        {
            if (pathTool != null)
            {
                pathTool.pathCreator.pathUpdated -= OnPathModified;
            }
        }


        protected virtual void Subscribe()
        {
            if (pathTool.pathCreator != null)
            {
                isSubscribed = true;
                pathTool.pathCreator.pathUpdated -= OnPathModified;
                pathTool.pathCreator.pathUpdated += OnPathModified;
            }
        }

        bool TryFindPathCreator()
        {
            // Try find a path creator in the scene, if one is not already assigned
            if (pathTool.pathCreator == null)
            {
                if (pathTool.GetComponent<PathCreator>() != null)
                {
                    pathTool.pathCreator = pathTool.GetComponent<PathCreator>();
                }
                else if (FindObjectOfType<PathCreator>())
                {
                    pathTool.pathCreator = FindObjectOfType<PathCreator>();
                }
            }

            return pathTool.pathCreator != null;
        }
    }
}