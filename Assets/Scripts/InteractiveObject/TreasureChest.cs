using UnityEngine;
using DG.Tweening;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private GameObject _activatedObject;

    private void Start()
    {
        _activatedObject.SetActive(false);
    }

    public void Open()
    {
        transform.DOScale(new Vector3(0f, 0f, 1f), 0.5f).SetEase(Ease.InSine).OnComplete(()=>
        {
            _activatedObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
