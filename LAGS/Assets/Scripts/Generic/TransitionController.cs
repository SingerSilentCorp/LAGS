using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private DialogueManager dialogueManager;

    [SerializeField] private Sprite[] otherImgList;

    [SerializeField] private Image fadeImg, otherImg;

    [SerializeField] private GameObject skipObj;

    private float transitionTime;

    [HideInInspector] public bool endTransition, startingTransition;

    [HideInInspector] public int imgIndexValue;

    [HideInInspector] public bool changeImg, isFadding;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        fadeImg.color = new Color(0, 0, 0, 0);
        otherImg.color = new Color(1, 1, 1, 0);

        transitionTime = 0.8f;

        endTransition = false;
        isFadding = false;
        startingTransition = false;

        otherImg.sprite = otherImgList[0];

        imgIndexValue = 0;

        skipObj.SetActive(false);
    }

    public void InitTransition(bool isEnding, System.Action OnComplete)
    {
        startingTransition = true;
        isFadding = dialogueManager.GetIfFadding();
        changeImg = dialogueManager.GetIfChangingIMG();

        if (isEnding)
        {
            isFadding = false;
            startingTransition = false;
            OnComplete?.Invoke();

            //return;
        }
        else
        {
            if (isFadding == true)
            {
                fadeImg.DOFade(1, transitionTime).OnComplete(() =>
                {
                    otherImg.color = new Color(1, 1, 1, 1);

                    if (changeImg == true)
                    {
                        imgIndexValue++;
                        otherImg.sprite = otherImgList[imgIndexValue];
                        dialogueManager.IsPlayingDialog();
                        dialogueManager.SetChangingIMGFalse();
                        dialogueManager.SetIfFaddingFalse();
                        fadeImg.DOFade(0, transitionTime).OnComplete(() => { startingTransition = false; });
                    }
                    else
                    {
                        dialogueManager.SetIfFaddingFalse();
                        dialogueManager.IsPlayingDialog();
                        fadeImg.DOFade(0, transitionTime).OnComplete(() => { startingTransition = false; });
                    }

                    if (OnComplete != null) OnComplete();
                });
            }
            else
            {
                if (changeImg == true)
                {
                    dialogueManager.IsPlayingDialog();
                    imgIndexValue++;
                    otherImg.sprite = otherImgList[imgIndexValue];
                    dialogueManager.SetChangingIMGFalse();
                    startingTransition = false;
                }
                else
                {
                    dialogueManager.IsPlayingDialog();
                    startingTransition = false;
                }
            }
        }
    }

    public int GetOtherImageLength()
    {
        return otherImgList.Length;
    }

}
