using System;
using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;
using UnityEngine;
using static Constants;

public class LevelManagement : MonoBehaviour
{
    void Start()
    {
		CurrentLevel = 1;
		LoadCurrentLevel();
	}

    static Dictionary<int, Action> LevelLoaders
	{
        get
		{
            if (levelLoaders == null)
			{
                levelLoaders = new Dictionary<int, Action>()
                {
                    { 1, () =>
                    {
                        var levelReference = ObjectFactory.Instance.Create<LevelData>(1, 100);
                        var nodeReferenceStart = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Start, new Vector2(0, 0));
                        var nodeReferenceGoal = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Goal, new Vector2(20, 0));
                        var nodeReferenceNormal1 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, 0));
                        var nodeReferenceNormal2 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, 10));

                        var rogueReference1 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal1, RogueAbility.MoveDown);
                        var rogueReference2 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal2, RogueAbility.MoveLeft);

                        var routeReference1 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceStart, nodeReferenceNormal1, 1);
                        var routeReference2 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal1, nodeReferenceNormal2, 2);
                        var routeReference3 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal1, nodeReferenceGoal, 1);

                        var segmentReference1 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference1, true, 0);
                        var segmentReference2 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference2, false, 10);
                        var segmentReference3 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference3, true, 0);

                        (segmentReference1.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceStart, nodeReferenceNormal1 });
                        (segmentReference2.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal1, nodeReferenceNormal2 });
                        (segmentReference3.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal1, nodeReferenceGoal });


                        var guardReference = ObjectFactory.Instance.Create<GuardData>(routeReference3);
                        (guardReference.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.BottomRight, LootIcon.Mask, true));

                        var playerReference = ObjectFactory.Instance.Create<PlayerData>();
                        if (playerReference.Object is PlayerData playerData)
                        {
                            playerData.CurrentNode = nodeReferenceStart;
                            playerData.InitLootData(InventoryPosition.TopRight, 0);
                        }
                    }},

                    { 2, () =>
                    {
                        var levelReference = ObjectFactory.Instance.Create<LevelData>(2, 100);
                        var nodeReferenceStart = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Start, new Vector2(0, 0));
                        var nodeReferenceGoal = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Goal, new Vector2(30, 0));
                        var nodeReferenceNormal1 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, 0));
                        var nodeReferenceNormal2 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(20, 0));
                        var nodeReferenceNormal3 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(20, 10));
                        var nodeReferenceNormal4 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, 10));

                        var rogueReference1 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal1, RogueAbility.MoveRight);
                        var rogueReference2 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal2, RogueAbility.MoveLeft);
                        var rogueReference3 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal3, RogueAbility.MoveUp);
                        var rogueReference4 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal4, RogueAbility.MoveDown);

                        var routeReference1 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceStart, nodeReferenceNormal1, 1);
                        var routeReference2 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal1, nodeReferenceNormal2, 1);
                        var routeReference3 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal2, nodeReferenceNormal3, 1);
                        var routeReference4 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal3, nodeReferenceNormal4, 2);
                        var routeReference5 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal4, nodeReferenceNormal1, 2);
                        var routeReference6 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal2, nodeReferenceGoal, 1);

                        var segmentReference1 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference1, true, 0);
                        var segmentReference2 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference2, true, 0);
                        var segmentReference3 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference3, false, 20);
                        var segmentReference4 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference4, true, 10);
                        var segmentReference5 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference5, false, 10);
                        var segmentReference6 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference6, true, 0);

                        (segmentReference1.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceStart, nodeReferenceNormal1 });
                        (segmentReference2.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal1, nodeReferenceNormal2 });
                        (segmentReference3.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal2, nodeReferenceNormal3 });
                        (segmentReference4.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal3, nodeReferenceNormal4 });
                        (segmentReference5.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal4, nodeReferenceNormal1 });
                        (segmentReference6.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal2, nodeReferenceGoal });


                        var guardReference1 = ObjectFactory.Instance.Create<GuardData>(routeReference6);
                        (guardReference1.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.TopRight, LootIcon.Rabbit, true));
                        (guardReference1.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.TopRight, LootIcon.Coin, true));

                        var guardReference2 = ObjectFactory.Instance.Create<GuardData>(routeReference2);
                        (guardReference2.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.BottomRight, LootIcon.Card, true));

                        var playerReference = ObjectFactory.Instance.Create<PlayerData>();
                        if (playerReference.Object is PlayerData playerData)
                        {
                            playerData.CurrentNode = nodeReferenceStart;
                            playerData.InitLootData(InventoryPosition.TopRight, 0);
                        }
                    }},

                    { 3, () =>
                    {
                        var levelReference = ObjectFactory.Instance.Create<LevelData>(3, 100);
                        var nodeReferenceStart = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Start, new Vector2(0, 0));
                        var nodeReferenceGoal = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Goal, new Vector2(30, 0));
                        var nodeReferenceNormal1 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, 0));
                        var nodeReferenceNormal2 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(20, 0));
                        var nodeReferenceNormal3 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(20, 10));
                        var nodeReferenceNormal4 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, 10));
                        var nodeReferenceNormalDown = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, -10));

                        var rogueReference1 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal1, RogueAbility.MoveDown);
                        var rogueReference2 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal2, RogueAbility.MoveDown);
                        var rogueReference3 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal3, RogueAbility.MoveRight);
                        var rogueReference4 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal4, RogueAbility.MoveUp);
                        var rogueReferenceDown = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormalDown, RogueAbility.MoveRight);

                        var routeReference1 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceStart, nodeReferenceNormal1, 1);
                        var routeReference2 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal1, nodeReferenceNormal2, 2);
                        var routeReference3 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal2, nodeReferenceNormal3, 2);
                        var routeReference4 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal3, nodeReferenceNormal4, 2);
                        var routeReference5 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal4, nodeReferenceNormal1, 2);
                        var routeReference6 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal2, nodeReferenceGoal, 1);
                        var routeReferenceDown = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal1, nodeReferenceNormalDown, 2);

                        var segmentReference1 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference1, true, 0);
                        var segmentReference2 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference2, true, 0);
                        var segmentReference3 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference3, false, 20);
                        var segmentReference4 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference4, true, 10);
                        var segmentReference5 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference5, false, 10);
                        var segmentReference6 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference6, true, 0);
                        var segmentReferenceDown = ObjectFactory.Instance.Create<RouteSegmentData>(routeReferenceDown, false, 10);

                        (segmentReference1.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceStart, nodeReferenceNormal1 });
                        (segmentReference2.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal1, nodeReferenceNormal2 });
                        (segmentReference3.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal2, nodeReferenceNormal3 });
                        (segmentReference4.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal3, nodeReferenceNormal4 });
                        (segmentReference5.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal4, nodeReferenceNormal1 });
                        (segmentReference6.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal2, nodeReferenceGoal });
                        (segmentReferenceDown.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal1, nodeReferenceNormalDown});


                        var guardReference = ObjectFactory.Instance.Create<GuardData>(routeReference6);
                        (guardReference.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.TopRight, LootIcon.Card, false));

                        var playerReference = ObjectFactory.Instance.Create<PlayerData>();
                        if (playerReference.Object is PlayerData playerData)
                        {
                            playerData.CurrentNode = nodeReferenceStart;
                            playerData.InitLootData(InventoryPosition.TopRight, 0);
                        }
                    }},

                    { 4, () =>
                    {
                        var levelReference = ObjectFactory.Instance.Create<LevelData>(4, 100);
                        var nodeReferenceStart = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Start, new Vector2(-20, -10));
                        var nodeReferenceGoal = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Goal, new Vector2(30, -10));
                        var nodeReferenceNormal1 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(-10, -10));
                        var nodeReferenceNormal2 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(0, -10));
                        var nodeReferenceNormal3 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(0, 0));
                        var nodeReferenceNormal4 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(-10, 0));
                        var nodeReferenceNormal5 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, -10));
                        var nodeReferenceNormal6 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(10, -20));
                        var nodeReferenceNormal7 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(20, -20));
                        var nodeReferenceNormal8 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(20, -10));
                        var nodeReferenceNormalUp = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Normal, new Vector2(-10, 10));

                        var rogueReference1 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal1, RogueAbility.MoveDown);
                        var rogueReference2 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal2, RogueAbility.MoveRight);
                        var rogueReference3 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal3, RogueAbility.MoveDown);
                        var rogueReference4 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal4, RogueAbility.MoveUp);
                        var rogueReference5 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal5, RogueAbility.MoveUp);
                        var rogueReference6 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal6, RogueAbility.MoveDown);
                        var rogueReference7 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal7, RogueAbility.MoveDown);
                        var rogueReference8 = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormal8, RogueAbility.MoveLeft);
                        var rogueReferenceUp = ObjectFactory.Instance.Create<RogueData>(nodeReferenceNormalUp, RogueAbility.MoveRight);

                        var routeReference1 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceStart, nodeReferenceNormal1, 1);
                        var routeReference2 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal1, nodeReferenceNormal2, 2);
                        var routeReference3 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal2, nodeReferenceNormal3, 1);
                        var routeReference4 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal3, nodeReferenceNormal4, 5);
                        var routeReference5 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal4, nodeReferenceNormal1, 1);
                        var routeReference6 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal2, nodeReferenceNormal5, 1);
                        var routeReference7 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal5, nodeReferenceNormal6, 2);
                        var routeReference8 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal6, nodeReferenceNormal7, 2);
                        var routeReference9 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal7, nodeReferenceNormal8, 2);
                        var routeReference10 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal8, nodeReferenceNormal5, 1);
                        var routeReference11 = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal8, nodeReferenceGoal, 1);
                        var routeReferenceUp = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReferenceNormal4, nodeReferenceNormalUp, 2);

                        var segmentReference1 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference1, true, -10);
                        var segmentReference2 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference2, true, -10);
                        var segmentReference3 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference3, false, 0);
                        var segmentReference4 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference4, true, 0);
                        var segmentReference5 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference5, false, -10);
                        var segmentReference6 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference6, true, -10);
                        var segmentReference7 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference7, false, 10);
                        var segmentReference8 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference8, true, -20);
                        var segmentReference9 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference9, false, 20);
                        var segmentReference10 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference10, true, -10);
                        var segmentReference11 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference11, true, -10);
                        var segmentReferenceUp = ObjectFactory.Instance.Create<RouteSegmentData>(routeReferenceUp, false, -10);


                        (segmentReference1.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceStart, nodeReferenceNormal1 });
                        (segmentReference2.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal1, nodeReferenceNormal2 });
                        (segmentReference3.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal2, nodeReferenceNormal3 });
                        (segmentReference4.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal3, nodeReferenceNormal4 });
                        (segmentReference5.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal4, nodeReferenceNormal1 });
                        (segmentReference6.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal2, nodeReferenceNormal5 });
                        (segmentReference7.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal5, nodeReferenceNormal6 });
                        (segmentReference8.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal6, nodeReferenceNormal7 });
                        (segmentReference9.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal7, nodeReferenceNormal8 });
                        (segmentReference10.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal8, nodeReferenceNormal5 });
                        (segmentReference11.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal8, nodeReferenceGoal });
                        (segmentReferenceUp.Object as RouteSegmentData).SetConnected(new[] { nodeReferenceNormal4, nodeReferenceNormalUp });


                        var guardReference1 = ObjectFactory.Instance.Create<GuardData>(routeReference4);
                        (guardReference1.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.TopRight, LootIcon.Coin, true));

                        var guardReference2 = ObjectFactory.Instance.Create<GuardData>(routeReference3);
                        (guardReference2.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.BottomRight, LootIcon.Card, true));

                        var guardReference3 = ObjectFactory.Instance.Create<GuardData>(routeReference6);
                        (guardReference3.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.TopRight, LootIcon.Card, false));

                        //var guardReference4 = ObjectFactory.Instance.Create<GuardData>(routeReference7);
                        //(guardReference4.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.TopLeft, LootIcon.Coin, true));

                        var guardReference5 = ObjectFactory.Instance.Create<GuardData>(routeReference10);
                        (guardReference5.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.BottomRight, LootIcon.Mask, true));
                        (guardReference5.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.BottomRight, LootIcon.Coin, true));

                        var guardReference6 = ObjectFactory.Instance.Create<GuardData>(routeReference11);
                        (guardReference6.Object as GuardData).AddLootCheck(new LootCheck(InventoryPosition.BottomLeft, LootIcon.Rabbit, true));

                        var playerReference = ObjectFactory.Instance.Create<PlayerData>();
                        if (playerReference.Object is PlayerData playerData)
                        {
                            playerData.CurrentNode = nodeReferenceStart;
                            playerData.InitLootData(InventoryPosition.TopRight, 0);
                        }
                    }},
                };
			}
            return levelLoaders;
		}
	}
    static Dictionary<int, Action> levelLoaders;

    public static int CurrentLevel { get; private set; }

    public static void ChangeLevel(int levelSequence)
	{
        DestroyExistingLevel();
        CurrentLevel = levelSequence;
        LoadCurrentLevel();
	}

    static void LoadCurrentLevel()
	{
        if (LevelLoaders.ContainsKey(CurrentLevel))
		{
            LevelLoaders[CurrentLevel].Invoke();
		}
	}

    static void DestroyExistingLevel()
	{
        var levelToDestroy = ObjectFactory.Instance.Query<LevelData>(level => level.Sequence == CurrentLevel).FirstOrDefault();
        if (levelToDestroy.IsValid)
		{
            (levelToDestroy.Object as LevelData).DestroyLevel();
            ObjectFactory.Instance.Remove(levelToDestroy);
		}
	}
}
