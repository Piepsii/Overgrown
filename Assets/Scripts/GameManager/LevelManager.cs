using UnityEngine;

namespace Overgrown.GameManager
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject levelPrefab;
        public Level activeLevel;

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

