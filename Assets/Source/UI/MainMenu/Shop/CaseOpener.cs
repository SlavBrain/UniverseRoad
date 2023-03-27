using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaseOpener : MonoBehaviour
{
    [SerializeField] private Button _caseIcon;
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private TMP_Text _cardCountText;
    private float _animationScaleMultiplier = 1.3f;
    private Coroutine _animationPlaying;

    private void OnEnable()
    {
        _caseIcon.onClick.AddListener(OpeningAnimationStart);
    }

    private void OnDisable()
    {
        _caseIcon.onClick.RemoveAllListeners();
    }

    public void Initialize(Sprite caseIcon, WeaponCard weaponCard, int cardCount)
    {
        _caseIcon.image.sprite = caseIcon;
        _weaponIcon.sprite = weaponCard.Icon;
        _cardCountText.text = cardCount.ToString();
        
        SetStartState();
    }

    private void SetStartState()
    {
        _caseIcon.gameObject.SetActive(true);
        _weaponIcon.gameObject.SetActive(false);
        _cardCountText.gameObject.SetActive(false);
    }
    
    private void SetFinishState()
    {
        _caseIcon.gameObject.SetActive(false);
        _weaponIcon.gameObject.SetActive(true);
        _cardCountText.gameObject.SetActive(true);
    }

    private void OpeningAnimationStart()
    {
        if (_animationPlaying != null) 
            StopCoroutine(_animationPlaying);

        _animationPlaying = StartCoroutine(PlayingAnimation());
    }

    private IEnumerator PlayingAnimation()
    {
        Tween tween=_caseIcon.transform.DOScale(new Vector3(_animationScaleMultiplier, _animationScaleMultiplier, _animationScaleMultiplier), 0.5f).SetLoops(6, LoopType.Yoyo);
        yield return tween.WaitForCompletion();
        SetFinishState();
    }
}
