using System;
using System.Collections.Generic;
using UnityEngine;
using BitButterCORE.V2;

public class SceneManagement : MonoBehaviour
{
    void Start()
    {
        SetupSceneMappings();
        InitializeScene();
    }

    void Update()
    {
        UpdateScene();
    }

    void SetupSceneMappings()
	{
        AddSceneMapping(typeof(NodeData));
        AddSceneMapping(typeof(PlayerData));
        AddSceneMapping(typeof(RogueData));
        AddSceneMapping(typeof(RouteSegmentData));
        AddSceneMapping(typeof(GuardData));
        AddSceneMapping(typeof(RouteData));
        AddSceneMapping(typeof(LevelData));
    }

    void AddSceneMapping(Type objectType)
	{
        var prefabIndex = SceneMappings.Count;
        if (prefabIndex < SceneMappingPrefabs.Count)
		{
            var prefab = SceneMappingPrefabs[prefabIndex];
            SceneMappings.Add(objectType, prefab);
		}
	}

    void InitializeScene()
	{
        foreach (var mapping in SceneMappings)
		{
            Scene.Add(mapping.Key, new Dictionary<uint, GameObject>());
		}
	}

    void UpdateScene()
	{
        if (ObjectFactory.Instance.HasChanges)
		{
            var addedObjects = ObjectFactory.Instance.GetAddedObjects();
            foreach (var addedObject in addedObjects)
			{
                var objectType = addedObject.Type;
                var objectID = addedObject.ID;
                if (!Scene.ContainsKey(objectType))
				{
                    Scene.Add(objectType, new Dictionary<uint, GameObject>());
				}
                if (!Scene[objectType].ContainsKey(objectID))
                {
                    if (SceneMappings.ContainsKey(objectType))
                    {
                        var prefab = SceneMappings[objectType];
                        var gameObject = Instantiate(prefab);
                        var viewObject = gameObject.GetComponent<BaseViewObject>();
                        if (viewObject != null)
						{
                            viewObject.InitializeView(addedObject);
						}
						Scene[objectType].Add(objectID, gameObject);
                    }
                }
            }

            var removedObjects = ObjectFactory.Instance.GetRemovedObjects();
            foreach (var removedObject in removedObjects)
			{
                var objectType = removedObject.Type;
                var objectID = removedObject.ID;
                if (Scene.ContainsKey(objectType) && Scene[objectType].ContainsKey(objectID))
				{
                    Destroy(Scene[objectType][objectID]);
				}
			}

            ObjectFactory.Instance.ClearChanges();
		}
	}

    [SerializeField]
    List<GameObject> SceneMappingPrefabs;

    Dictionary<Type, GameObject> SceneMappings => sceneMappings ?? (sceneMappings = new Dictionary<Type, GameObject>());
    Dictionary<Type, GameObject> sceneMappings;

    Dictionary<Type, Dictionary<uint, GameObject>> Scene => scene ?? (scene = new Dictionary<Type, Dictionary<uint, GameObject>>());
    Dictionary<Type, Dictionary<uint, GameObject>> scene;
}
