using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Constants;

public class GuardViewObject : BaseViewObject<GuardData>
{
	protected override void Start()
	{
		if (CoreObject.LootChecks.Any())
		{
			var route = CoreObject.Route.Object as RouteData;
			var connectedNodes = route.Nodes.Select(node => node.Object as NodeData);
			var nodesMidPoint = new Vector2();
			foreach (var node in connectedNodes)
			{
				nodesMidPoint += node.Position;
			}
			nodesMidPoint /= connectedNodes.Count();
			transform.position = nodesMidPoint;
			transform.localScale = new Vector2(0.8f, 0.8f);

			var inventoryPosition = CoreObject.LootChecks.First().LootPosition;
			var lootIcons = CoreObject.LootChecks.Select(check => check.LootIcon);
			var shouldExclude = CoreObject.LootChecks.First().ShouldExclude;
			UpdateBackgroundColorForInventoryPosition(inventoryPosition);
			UpdateItems(lootIcons);
			UpdateBanRequireIcon(shouldExclude);
		}
		else
		{
			gameObject.SetActive(false);
		}
	}

	void UpdateBackgroundColorForInventoryPosition(InventoryPosition position)
	{
		if (background != null)
		{
			switch (position)
			{
				case InventoryPosition.TopLeft:
					background.color = new Color(0.26f, 0.53f, 1f);
					break;
				case InventoryPosition.TopRight:
					background.color = new Color(1f, 0.3f, 0.46f);
					break;
				case InventoryPosition.BottomRight:
					background.color = new Color(0.7f, 1f, 0.61f);
					break;
				case InventoryPosition.BottomLeft:
					background.color = new Color(1f, 0.87f, 0.34f);
					break;
			}
		}
	}

	void UpdateItems(IEnumerable<LootIcon> lootIcons)
	{
		for (var i = 0; i < lootIcons.Count(); i++)
		{
			var icon = lootIcons.ElementAt(i);
			var sprite = icons[(int)icon];

			if (items != null && i < items.Length)
			{
				items[i].sprite = sprite;
			}
		}
	}

	void UpdateBanRequireIcon(bool shouldExclude)
	{
		if (shouldExclude)
		{
			if (requireIcon != null)
			{
				requireIcon.gameObject.SetActive(false);
			}

			if (banIcon != null)
			{
				banIcon.gameObject.SetActive(true);
			}
		}
		else
		{
			if (requireIcon != null)
			{
				requireIcon.gameObject.SetActive(true);
			}

			if (banIcon != null)
			{
				banIcon.gameObject.SetActive(false);
			}
		}
	}

	[SerializeField]
	SpriteRenderer background;

	[SerializeField]
	SpriteRenderer[] items;

	[SerializeField]
	Sprite[] icons;

	[SerializeField]
	SpriteRenderer banIcon;

	[SerializeField]
	SpriteRenderer requireIcon;
}
