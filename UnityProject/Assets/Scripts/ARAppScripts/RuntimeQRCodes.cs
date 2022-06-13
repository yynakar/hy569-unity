using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Unity.Jobs;
using TMPro;

public class RuntimeQRCodes : MonoBehaviour
{
    ARTrackedImageManager m_TrackedImageManager;
    IReferenceImageLibrary m_ImageLibrary;

    [SerializeField]
    bool CorrectRiddle;

    [SerializeField]
    GameObject CorrectMsg;
    [SerializeField]
    GameObject WrongMsg;

    //[SerializeField]
    //[Tooltip("Choose prefab to be spawned when QR is found")]
    //GameObject prefab;

    //[SerializeField]
    //[Tooltip("to be replaced")]
    //GameObject prefab2;

    public TMP_Text debugText;
    public TMP_Text debugText2;
    RuntimeReferenceImageLibrary libraryAR;

    void Start()
    {
        m_TrackedImageManager = GameObject.Find("AR Foundation/AR Session Origin").GetComponent<ARTrackedImageManager>();
        
        libraryAR = m_TrackedImageManager.CreateRuntimeLibrary();
        if (libraryAR is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            mutableLibrary = m_TrackedImageManager.CreateRuntimeLibrary() as MutableRuntimeReferenceImageLibrary;

            StartCoroutine(AddAllImagesToMutableReferenceImageLibraryAR(mutableLibrary));
        }

    }
    private IEnumerator AddAllImagesToMutableReferenceImageLibraryAR(MutableRuntimeReferenceImageLibrary mutableLibrary)
    {
        yield return null;
        if (m_TrackedImageManager.descriptor.supportsMutableLibrary)
        {
            Texture2D textureImg = Resources.Load("qr1") as Texture2D;
            JobHandle job;
            if (textureImg.isReadable)
            {
                string name = "qrcode1";
                job = mutableLibrary.ScheduleAddImageJob(textureImg, name, 0.1f);
                yield return new WaitUntil(() => job.IsCompleted);
            }
        textureImg = Resources.Load("Vaseline-Logo") as Texture2D;
            if (textureImg.isReadable)
            {
                name = "qrcode2";
                job = mutableLibrary.ScheduleAddImageJob(textureImg, name, 0.1f);
                yield return new WaitUntil(() => job.IsCompleted);
            }

            m_TrackedImageManager.enabled = true;
            m_TrackedImageManager.referenceLibrary = mutableLibrary;
            debugText.text = m_TrackedImageManager.referenceLibrary.count + "la";

        }
    }
    //Get which images tracked
    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }
    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs e)
    {
        UpdateTrackedImages(e.added);
        UpdateTrackedImages(e.updated);
    }
    private void UpdateTrackedImages(IEnumerable<ARTrackedImage> trackedImages)
    {
        
           var trackedImage =
            trackedImages.FirstOrDefault(x => x.referenceImage.name == "qrcode1");
        var trackedImage2 =
            trackedImages.FirstOrDefault(x => x.referenceImage.name == "qrcode2");


        if (trackedImage)
        {
            //check apo tin vasi an einai correct
            CorrectMsg.SetActive(true);
            WrongMsg.SetActive(false);

        }
        if (trackedImage2)
        {
            WrongMsg.SetActive(true);
            CorrectMsg.SetActive(false);
        }
        //if (trackedImage3)
        //{
        //    CorrectMsg.SetActive(true);
        //    WrongMsg.SetActive(true);

        //}

    }
    void ListAllImages()
    {
        debugText.text = m_TrackedImageManager.trackables + "la";

        foreach (var trackedImage in m_TrackedImageManager.trackables)
        {
            debugText2.text = trackedImage.referenceImage.name + "lol";
        }
    }
}
