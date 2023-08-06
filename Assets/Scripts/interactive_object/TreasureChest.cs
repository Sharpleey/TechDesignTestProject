using UnityEngine;
using DG.Tweening;
using Zenject;

public class TreasureChest : MonoBehaviour
{
    [SerializeField] private GameObject _activatedObject;

    private AudioManager _audioManager;

    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    private void Start()
    {
        _activatedObject.SetActive(false);
    }

    public void Open()
    {
        _audioManager.PlaySound(AudioManager.SoundType.ChestCollected);

        transform.DOScale(new Vector3(0f, 0f, 1f), 0.5f).SetEase(Ease.InSine).OnComplete(()=>
        {
            _activatedObject.SetActive(true);
            gameObject.SetActive(false);
        });
    }
}
