using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WeaverCore;
using WeaverCore.Attributes;
using WeaverCore.Utilities;

namespace MoreGodhomeSpace
{
    public static class OnSceneStart
    {
        static List<string> deletedObjects = new List<string>
        {
            "Chunk 2 0",
            "Chunk 2 1",
            "Chunk 2 2",
            "Chunk 2 3",
            "Chunk 2 4",
            "GG_scenery_0001_1 (21)",
            "Chunk 1 0",
            "Chunk 2 5",
            "Roof Collider (3)"
        };


        [OnRuntimeInit]
        static void OnGameStart()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        }

        static void DeleteObjects(GameObject obj)
        {
            if (deletedObjects.Contains(obj.name))
            {
                GameObject.Destroy(obj);
            }

            if (obj.transform.childCount > 0)
            {
                for (int i = obj.transform.childCount - 1; i >= 0; i--)
                {
                    DeleteObjects(obj.transform.GetChild(i).gameObject);
                }
            }
        }

        private static void SceneManager_sceneLoaded(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.LoadSceneMode arg1)
        {
            try
            {
                if (arg0.name == "GG_Workshop")
                {
                    foreach (var obj in arg0.GetRootGameObjects())
                    {
                        DeleteObjects(obj);
                        if (obj.name == "BlurPlane")
                        {
                            obj.transform.SetYLocalPosition(57.5149f);
                            obj.transform.SetYLocalScale(12.82298f);
                        }
                        else if (obj.name == "water_fog (1)")
                        {
                            obj.transform.SetPositionX(130.2658f);
                            obj.transform.SetScaleX(319.3503f);
                        }
                        else if (obj.name == "water_fog (2)")
                        {
                            obj.transform.SetPositionX(129.9462f);
                            obj.transform.SetScaleX(320.3779f);
                        }
                    }
                }

                var tilemapType = typeof(HeroController).Assembly.GetType("tk2dTileMap");

                if (tilemapType != null)
                {
                    foreach (var obj in arg0.GetRootGameObjects())
                    {
                        if (obj.TryGetComponent(tilemapType, out var component))
                        {
                            //var widthField = tilemapType.GetField("width");
                            var heightField = tilemapType.GetField("height");

                            heightField.SetValue(component, 93);
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Something failed when trying to replace scene objects. May or may not be a big problem");
                Debug.LogException(e);
            }
        }
    }
}
