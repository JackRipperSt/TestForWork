using System.Collections;
using UnityEngine;
using TMPro;

public class ASecondStructure : UIAlert
{
    [SerializeField] private SecondStructure _secondStructure;

    private void OnEnable()
    {
        _secondStructure.StorageLimitCreateResource += Alert;
        _secondStructure.NotEnoughResourcesInStorage += Alert;
    }

    private void OnDisable()
    {
        _secondStructure.StorageLimitCreateResource -= Alert;
        _secondStructure.NotEnoughResourcesInStorage -= Alert;
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
