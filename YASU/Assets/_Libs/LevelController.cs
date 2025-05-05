using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [field: SerializeField] public int ShipParts { get; private set; }
    [SerializeField] private int _partsFound;
    [SerializeField] private TextMeshProUGUI _partsIndicator;
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
            _partsIndicator.text = $"{_partsFound}/{ShipParts}";
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