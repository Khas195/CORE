using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneControl : SingletonMonobehavior<LoadingSceneControl>, IObserver
{
    [SerializeField]
    Text loadingProgress = null;
    [SerializeField]
    Text currentLoading = null;

    public void ReceiveData(DataPack pack, string eventName)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        loadingProgress.text = SceneLoadingManager.GetInstance().GetLoadingProgress().ToString();
        if (SceneLoadingManager.GetInstance().GetLoadingProgress() > 0.9f)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneLoadingManager.GetInstance().FinishedLoading();
            }
        }
    }

    public void SetLoadedText(string name)
    {
        currentLoading.text = name + " loaded!!";
    }
}
