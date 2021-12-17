using UnityEngine;

namespace Overgrown.Utility
{
    [CreateAssetMenu(menuName = "Events/Integer Event Channel")]
    public class IntEventChannelSO : ScriptableObject
    {
        public delegate void IntAction(int value);
        public IntAction OnEventRaised;

        public void RaiseEvent(int value)
        {
            OnEventRaised?.Invoke(value);
        }
    }
}
