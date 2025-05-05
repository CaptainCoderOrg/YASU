using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [field: SerializeField] public int ShipParts { get; private set; }
    [SerializeField] private int _partsFound;
    public int PartsFound 
    { 
        get => _partsFound; 
        set
        {
            _partsFound = value;
            if (_partsFound >= ShipParts)
            {
                OnAllPartsFound?.Invoke();
            }
        }
    }

    [field:SerializeField] public UnityEvent OnAllPartsFound { get; private set; }

    void Start()
    {
        ShipParts = FindObjectsByType<ShipPartController>(FindObjectsSortMode.None).Length;
    }


    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadLevelAsync(sceneName));
    }

    private IEnumerator LoadLevelAsync(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
        while (!loading.isDone)
        {
            yield return null;
        }
    }
}