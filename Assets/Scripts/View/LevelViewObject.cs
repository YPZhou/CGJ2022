using UnityEngine;
using UnityEngine.UI;

public class LevelViewObject : BaseViewObject<LevelData>
{
	[SerializeField]
	Text RemainingMoveCount;

	//[SerializeField]
	//Text LevelEndText;

	[SerializeField]
	Image LevelEnd;

	[SerializeField]
	Sprite LevelWin;

	[SerializeField]
	Sprite LevelLose;

	[SerializeField]
	Image UpArrow;

	[SerializeField]
	Image DownArrow;

	[SerializeField]
	Image LeftArrow;

	[SerializeField]
	Image RightArrow;

	[SerializeField]
	Text HintText;

	protected override void Start()
	{
		var canvasParent = GameObject.Find("Canvas");
		if (canvasParent != null)
		{
			transform.SetParent(canvasParent.transform, false);
		}
	}

	protected override void Update()
	{
		if (RemainingMoveCount != null)
		{
			RemainingMoveCount.text = string.Format("剩余步数： {0}", (CoreObject.MaxMoveCount - CoreObject.CurrentMoveCount).ToString());
		}

		//if (LevelEndText != null)
		//{
		//	if (CoreObject.IsLevelEnd)
		//	{
		//		if (CoreObject.IsLevelWin)
		//		{
		//			LevelEndText.text = "胜利过关！";
		//		}
		//		else if (CoreObject.IsLevelLose)
		//		{
		//			LevelEndText.text = "你无路可逃！";
		//		}
		//	}
		//	else
		//	{
		//		LevelEndText.text = string.Empty;
		//	}
		//}

		if (LevelEnd != null)
		{
			if (CoreObject.IsLevelEnd)
			{
				if (CoreObject.IsLevelWin)
				{
					LevelEnd.sprite = LevelWin;
					LevelEnd.color = new Color(0.5f, 1f, 0.5f, 1f);
				}
				else if (CoreObject.IsLevelLose)
				{
					LevelEnd.sprite = LevelLose;
					LevelEnd.color = new Color(1f, 0f, 0f, 1f);
				}
			}
			else
			{
				LevelEnd.sprite = null;
				LevelEnd.color = new Color(1f, 1f, 1f, 0f);
			}
		}

		var currentNode = CoreObject.CurrentPlayerNode.Object as NodeData;
		if (currentNode != null)
		{
			if (currentNode.HasAnySelectedRoute)
			{
				UpdateArrowColor(UpArrow, currentNode.IsUpRouteSelected);
				UpdateArrowColor(DownArrow, currentNode.IsDownRouteSelected);
				UpdateArrowColor(LeftArrow, currentNode.IsLeftRouteSelected);
				UpdateArrowColor(RightArrow, currentNode.IsRightRouteSelected);

				if (HintText != null)
				{
					HintText.text = "确认移动";
				}
			}
			else
			{
				UpdateArrowColor(UpArrow, currentNode.UpRoute.IsValid && (currentNode.UpRoute.Object as RouteData).CanPass);
				UpdateArrowColor(DownArrow, currentNode.DownRoute.IsValid && (currentNode.DownRoute.Object as RouteData).CanPass);
				UpdateArrowColor(LeftArrow, currentNode.LeftRoute.IsValid && (currentNode.LeftRoute.Object as RouteData).CanPass);
				UpdateArrowColor(RightArrow, currentNode.RightRoute.IsValid && (currentNode.RightRoute.Object as RouteData).CanPass);

				if (HintText != null)
				{
					HintText.text = "选择方向";
				}
			}
		}
	}

	void UpdateArrowColor(Image image, bool isActive)
	{
		if (image != null)
		{
			if (isActive)
			{
				image.color = Color.white;
			}
			else
			{
				image.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
			}
		}
	}
}
