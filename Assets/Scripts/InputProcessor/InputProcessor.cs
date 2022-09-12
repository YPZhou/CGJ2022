using System.Linq;
using BitButterCORE.V2;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    public static bool IsLeftKeyDown => Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
    public static bool IsRightKeyDown => Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
    public static bool IsUpKeyDown => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    public static bool IsDownKeyDown => Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

    public static bool IsNum1KeyDown => Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1);
    public static bool IsNum2KeyDown => Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2);

	public static bool IsNum3KeyDown => Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3);
	public static bool IsNum4KeyDown => Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4);

	private void Update()
    {
		var currentLeveReference = ObjectFactory.Instance.Query<LevelData>(levelData => levelData.Sequence == LevelManagement.CurrentLevel).FirstOrDefault();
		if (currentLeveReference.IsValid)
		{
			var currentLevel = currentLeveReference.Object as LevelData;
			if (!currentLevel.IsLevelEnd)
			{
				if (IsLeftKeyDown)
				{
					currentLevel.OnPressLeftKey();
				}
				else if (IsRightKeyDown)
				{
					currentLevel.OnPressRightKey();
				}
				else if (IsUpKeyDown)
				{
					currentLevel.OnPressUpKey();
				}
				else if (IsDownKeyDown)
				{
					currentLevel.OnPressDownKey();
				}
			}
		}

		if (IsNum1KeyDown)
		{
			LevelManagement.ChangeLevel(1);
		}
		else if (IsNum2KeyDown)
		{
			LevelManagement.ChangeLevel(2);
		}
		else if (IsNum3KeyDown)
		{
			LevelManagement.ChangeLevel(3);
		}
		else if (IsNum4KeyDown)
		{
			LevelManagement.ChangeLevel(4);
		}
	}
}