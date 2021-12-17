using UnityEngine;

namespace Overgrown.Utility
{
	[CreateAssetMenu(fileName = "Float EC", menuName = "Events/Float Event Channel")]
	public class FloatEventChannelSO : ScriptableObject
	{
        public delegate void FloatAction(float value);
        public FloatAction OnEventRaised;

        public void RaiseEvent(float value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}
