using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ResourceDownLoadScene : BaseScene
{
    public GameObject waitMessageObj;
    public GameObject downMessageObj;

    public Button downLoadBtn;

    public Image downSlider;
    public TMP_Text sizeInfoText;
    public TMP_Text downPercentText;

    private long patchSize;
    private Dictionary<string, long> patchMap = new Dictionary<string, long>();

    [Header("Label")]
    public AssetLabelReference unitLabel;
    public AssetLabelReference enemyLabel;
    public AssetLabelReference effectLabel;
    public AssetLabelReference popupUILabel;
    public AssetLabelReference materialLabel;
    public AssetLabelReference scriptableObjectLabel;



    public override void Start()
    {
        waitMessageObj.SetActive(true);
        downMessageObj.SetActive(false);

        StartCoroutine(InitAddressable());
        StartCoroutine(CheckUpdateFiles());

        downLoadBtn.OnClickAsObservable().Subscribe(_ =>
        {
            downLoadBtn.gameObject.SetActive(false);
            OnDownLoad();
        });

    }

    IEnumerator InitAddressable()
    {
     
        var init = Addressables.InitializeAsync();
        yield return init;
    }

    IEnumerator CheckUpdateFiles()
    {
        patchSize = 0;
        var labels = new List<string>()
        {
            unitLabel.labelString,
            enemyLabel.labelString,
            effectLabel.labelString,
            popupUILabel.labelString,
            materialLabel.labelString,
            scriptableObjectLabel.labelString,
        };
        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Failed)
            {
                continue;
            }

            patchSize += handle.Result;
        }

        if (patchSize > 0)
        {
            waitMessageObj.SetActive(false);
            downMessageObj.SetActive(true);

            sizeInfoText.text = GetFileSize(patchSize);
        }
        else
        {
            downPercentText.text = " 100 % ";
            downSlider.fillAmount = 1.0f;
            yield return new WaitForSeconds(2.0f);
            LoadingScene.LoadScene("MainScene");
        }
    }

    private string GetFileSize(long byteCnt)
    {
        string size = "0 Bytes";

        if (byteCnt >= 1073741824.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1073741824.0) + " GB";
        }
        else if (byteCnt >= 1048576.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1048576.0) + " MB";
        }
        else if (byteCnt >= 1024.0)
        {
            size = string.Format("{0:##.##}", byteCnt / 1024.0) + "KB";
        }
        else if (byteCnt > 0 && byteCnt < 1024.0)
        {
            size = byteCnt.ToString() + " Bytes";
        }
        return size;
    }

    public void OnDownLoad()
    {
        StartCoroutine(PatchFile());
    }

    IEnumerator PatchFile()
    {
        var labels = new List<string>()
        {
            unitLabel.labelString,
            enemyLabel.labelString,
            effectLabel.labelString,
            popupUILabel.labelString,
            materialLabel.labelString,
            scriptableObjectLabel.labelString,
        };
        foreach (var label in labels)
        {
            var handle = Addressables.GetDownloadSizeAsync(label);
            yield return handle;

            if (handle.Result > 0)
            {
                StartCoroutine(DownLoad(label));
            }
        }
        yield return CheckDownLoad();
    }

    IEnumerator DownLoad(string label)
    {
        patchMap[label] = 0;

        var handle = Addressables.DownloadDependenciesAsync(label, false);

        while (!handle.IsDone)
        {
            patchMap[label] = handle.GetDownloadStatus().DownloadedBytes;
            yield return new WaitForEndOfFrame();
        }
        patchMap[label] = handle.GetDownloadStatus().TotalBytes;
        Addressables.Release(handle);
    }

    IEnumerator CheckDownLoad()
    {
        downPercentText.text = "0%";

        while (true)
        {
            var total = patchMap.Sum(tmp => tmp.Value);

            downSlider.fillAmount = (float)total / patchSize;
            downPercentText.text = (int)(downSlider.fillAmount * 100) + " %";

            if (total == patchSize)
            {
                LoadingScene.LoadScene("MainScene");
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
