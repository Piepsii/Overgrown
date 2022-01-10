using UnityEngine;

namespace Overgrown.GameManager
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject levelPrefab;
        public Level activeLevel;

        private void Start()
        {
            if (GameManager.Instance.LevelManager == null)
            {
                GameManager.Instance.LevelManager = this;
            }
        }

        private bool CreateLevel()
        {
            if (levelPrefab)
            {
                Instantiate(levelPrefab);
                return true;
            }
            return false;
        }
    }
}

