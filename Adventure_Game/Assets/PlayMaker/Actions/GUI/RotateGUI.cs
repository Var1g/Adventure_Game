// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.GUI)]
	[Tooltip("Rotates the GUI around a pivot point. By default only effects GUI rendered by this FSM, check Apply Globally to effect all GUI controls.")]
	public class RotateGUI : FsmStateAction
	{
		[RequiredField]
        [Tooltip("Angle to rotate in degrees.")]
        public FsmFloat angle;

		[RequiredField]
        [Tooltip("X coordinate of pivot.")]
        public FsmFloat pivotX;

		[RequiredField]
        [Tooltip("Y coordinate of pivot.")]
        public FsmFloat pivotY;

        [Tooltip("Use normalized screen coordinates (0-1).")]
        public bool normalized;

        [Tooltip("Apply to all GUI actions in all FSMs.")]
		public bool applyGlobally;

		bool applied;
		
		public override void Reset()
		{
			angle = 0;
			pivotX = 0.5f;
			pivotY = 0.5f;
			normalized = true;
			applyGlobally = false;
		}

		public override void OnGUI()
		{
			if (applied)
			{
				return;
			}

			var pivotPoint = new Vector2(pivotX.Value, pivotY.Value);
			
			if (normalized)
			{
				pivotPoint.x *= Screen.width;
				pivotPoint.y *= Screen.height;
			}
			
			GUIUtility.RotateAroundPivot(angle.Value, pivotPoint);
			
			if (applyGlobally)
			{
				PlayMakerGUI.GUIMatrix = GUI.matrix;
				applied = true;
			}
		}
		
		public override void OnUpdate()
		{
			applied = false;
		}
	}
}