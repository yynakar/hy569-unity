using UnityEngine;
using TMPro;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System.Linq;

public class QRcodeValidation : MonoBehaviour
{
    ARTrackedImageManager m_TrackedImageManager;

    [SerializeField]
    bool CorrectRiddle;
    [SerializeField]
    GameObject CorrectMsg;
    [SerializeField]
    GameObject WrongMsg;
    public TMP_Text debugText;
    public TMP_Text debugText2;
    public TMP_Text debugText3;

    [SerializeField]
    [Tooltip("Choose prefab to be spawned when QR is found")]
    GameObject prefab;

    [SerializeField]
    [Tooltip("to be replaced")]
    GameObject prefab2;

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }
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
        ImageChangedHandle(e);
    }

    void ImageChangedHandle(ARTrackedImagesChangedEventArgs imgChangedArgs)
    {
        // loop over new found images. imgChangedArgs.added is List<ARTrackedImage>
        foreach (var item in imgChangedArgs.added)
        {
            string thuntIDfirst = item.referenceImage.name;
            var thuntID = thuntIDfirst.Replace("riddle_t", "").Split('_');
            if (int.Parse(thuntID[0]) == GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID)
            {
                var riddleID = item.referenceImage.name.Replace("riddle_t", "").Replace(GameObject.Find("DataManager").GetComponent<DataManagement>().TreasureHuntID + "", "")
                .Replace("_r", "");
                GameObject.Find("ScanRiddlePage(Clone)").GetComponent<Solved>().cSolved(int.Parse(riddleID));
            }
            else
            {

                debugText.text = "den mpika edo" + thuntID[0];
            }
        }
    }
}
