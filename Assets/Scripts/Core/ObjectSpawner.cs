using UnityEngine;

namespace Core
{
    // Spawns game objects from path by loading resource
    public class ObjectSpawner : Singleton<ObjectSpawner>
    {
        public Object SpawnResource(string path)
        {
            Debug.Log($"ObjectSpawner: spawning resource at {path}");
            var res = Resources.Load(path);
            return Instantiate(res);
        }
    }
}