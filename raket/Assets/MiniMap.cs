using UnityEngine;
using TMPro;

/*
 UI-based circular minimap (compass). Usage:
 - Create a Canvas in the scene.
 - Add a UI GameObject (Image) to act as the circular map background and assign its RectTransform to `mapContainer`.
 - Attach this script to any GameObject, set `mapContainer`, and (optionally) `target`.
 The script creates four `TextMeshProUGUI` labels (N/E/S/W) as children of `mapContainer` and positions
 them around a circle based on the `radius` and the `target` yaw (falls back to `Camera.main`).
*/

public class MiniMap : MonoBehaviour
{
	// RectTransform that will contain the compass labels (should be centered/pivoted at 0.5,0.5)
	public RectTransform mapContainer;

	// Target (usually the player) whose yaw determines compass rotation. If null, falls back to Camera.main
	public Transform target;

	// Radius in pixels for direction labels around the center of mapContainer
	public float radius = 50f;

	// Font size for labels
	public int fontSize = 24;

	// Label color
	public Color textColor = Color.white;

	TextMeshProUGUI northLabel, eastLabel, southLabel, westLabel;

	void Start()
	{
		if (mapContainer == null)
		{
			Debug.LogError("MiniMap: please assign a RectTransform mapContainer in the inspector.");
			enabled = false;
			return;
		}

		CreateLabelsIfNeeded();
	}

	void CreateLabelsIfNeeded()
	{
		if (northLabel == null) northLabel = CreateLabel("N");
		if (eastLabel == null) eastLabel = CreateLabel("E");
		if (southLabel == null) southLabel = CreateLabel("S");
		if (westLabel == null) westLabel = CreateLabel("W");
	}

	TextMeshProUGUI CreateLabel(string text)
	{
		var go = new GameObject("Label_" + text, typeof(RectTransform));
		go.transform.SetParent(mapContainer, false);
		var rt = go.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(40, 24);

		var label = go.AddComponent<TextMeshProUGUI>();
		label.text = text;
		label.fontSize = fontSize;
		label.alignment = TextAlignmentOptions.Center;
		label.color = textColor;
		return label;
	}

	void Update()
	{
		if (mapContainer == null) return;
		float yaw = 0f;
		if (target != null) yaw = target.eulerAngles.y;
		else if (Camera.main != null) yaw = Camera.main.transform.eulerAngles.y;

		PositionLabel(northLabel, 0f, yaw);
		PositionLabel(eastLabel, 90f, yaw);
		PositionLabel(southLabel, 180f, yaw);
		PositionLabel(westLabel, 270f, yaw);
	}

	void PositionLabel(TextMeshProUGUI label, float worldAngle, float yaw)
	{
		if (label == null) return;
		float displayAngle = worldAngle - yaw;
		float rad = displayAngle * Mathf.Deg2Rad;
		Vector2 offset = new Vector2(Mathf.Sin(rad), Mathf.Cos(rad)) * radius;
		label.rectTransform.anchoredPosition = offset;
	}
}
