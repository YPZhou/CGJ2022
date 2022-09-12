using System.Linq;
using BitButterCORE.V2;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]
    private Transform lootIcon;
    [SerializeField]
    private Transform lootGrids;
    [SerializeField]
    private Transform itemIcons;
    [SerializeField]
    private Transform arrowIcon;

    private void SetLootIcon(int index)
    {
        var grid = lootGrids.GetChild(index);
        lootIcon.SetParent(grid);
        var localPosition = lootIcon.localPosition;
        localPosition.x = 0;
        localPosition.y = 0;
        lootIcon.localPosition = localPosition;
    }

    private void SetItemIcon(int index)
    {
        var item = itemIcons.GetChild(index);
        arrowIcon.SetParent(item);
        arrowIcon.localPosition = new Vector3(arrowIcon.localPosition.x, Mathf.Sin(Time.realtimeSinceStartup * 8) * 5f - 60, arrowIcon.localPosition.z);
        

        var itemImage = item.GetChild(0).GetComponent<Image>();
        _lootIconImage.sprite = itemImage.sprite;
    }

    private Image _lootIconImage;
    

    private void Awake()
    {
        _lootIconImage = lootIcon.GetComponent<Image>();
    }

    private void Update()
    {
        var playerData = (PlayerData)ObjectFactory.Instance.Query<PlayerData>().FirstOrDefault().Object;

        SetLootIcon((int)playerData.LootPosition);
        SetItemIcon((int)playerData.CurrentLootIcon);
    }
}