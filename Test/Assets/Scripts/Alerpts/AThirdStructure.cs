using System.Collections;
using UnityEngine;
using TMPro;

public class AThirdStructure : UIAlert
{
    [SerializeField] private ThirdStructure _thirdStructure;

    private void OnEnable()
    {
        _thirdStructure.StorageLimitCreateResource += Alert;
        _thirdStructure.NotEnoughResourcesInStorage += Alert;
    }

    private void OnDisable()
    {
        _thirdStructure.StorageLimitCreateResource -= Alert;
        _thirdStructure.NotEnoughResourcesInStorage -= Alert;
    }

    protected override void Alert(string text, Transform spawnPoint)
    {
        _text.text = text;
        TMP_Text createText = Instantiate(_text, spawnPoint);
        _canvasGroup = spawnPoint.GetComponent<CanvasGroup>();
        UpAlpha();
        Destroy(createText.gameObject, 3);
    }

    protected override void UpAlpha()
    {
        _canvasGroup.alpha = 0.9f;
        StartCoroutine(ZeroAlpha());
    }

    protected override IEnumerator ZeroAlpha()
    {
        yield return new WaitForSeconds(3);
        _canvasGroup.alpha = 0;
    }
}
