using System.Collections;
using UnityEngine;

public class AmmoDisplayController : MonoBehaviour
{
    [SerializeField] private AmmoPillController[] _ammoPills;
    [SerializeField] private SelfDestructSFX _gainAmmoSound;
    private YieldInstruction _pillDelay;
    private int _current;

    void Awake()
    {
        _pillDelay = new WaitForSeconds(1f/24f);
    }

    public void SetAmmo(int amount)
    {
        StopAllCoroutines();
        StartCoroutine(AnimateAmmo(amount));
    }

    private IEnumerator AnimateAmmo(int amount)
    {
        for (int ix = _ammoPills.Length-1; ix >= 0 ; ix--)
        {
            AmmoPillController pill = _ammoPills[ix];
            if (ix < amount)
            {
                if (!pill.Toggleable.IsVisible)
                {
                    pill.Toggleable.IsVisible = true;
                    Instantiate(_gainAmmoSound);
                    yield return _pillDelay;
                }
            }
            else
            {
                pill.Toggleable.IsVisible = false;
            }
        }
    }
}