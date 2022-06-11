using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using System.Linq;

public class QRcodeValidation : MonoBehaviour
{
    ARTrackedImageManager m_TrackedImageManager;
    IReferenceImageLibrary m_ImageLibrary;

    [SerializeField]
    bool CorrectRiddle;

    [SerializeField]
    GameObject CorrectMsg;
    [SerializeField]
    GameObject WrongMsg;

    [SerializeField]
    [Tooltip("Choose prefab to be spawned when QR is found")]
    GameObject prefab;

    [SerializeField]
    [Tooltip("to be replaced")]
    GameObject prefab2;

    void Awake()
    {
        m_TrackedImageManager = GameObject.Find("AR Foundation/AR Session Origin").GetComponent<ARTrackedImageManager>();
        m_ImageLibrary = m_TrackedImageManager.referenceLibrary;
    }
    
    private void Update()
    {
        //Shall see
        XRReferenceImageLibrary serializedLibrary = (XRReferenceImageLibrary)m_ImageLibrary;
        RuntimeReferenceImageLibrary runtimeLibrary = m_TrackedImageManager.CreateRuntimeLibrary(serializedLibrary);

        if (m_TrackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            // use the mutableLibrary
        }
        if (m_TrackedImageManager.descriptor.supportsMutableLibrary)
        {
            // Mutable reference image libraries are supported
            //edo theoritika ta vazo runtime
            //use ScheduleAddImageJob.
            //JobHandle when a job is complete

            //Multiple add image jobs can be processed concurrently, and it is okay if the MutableRuntimeReferenceImageLibrary is currently being used for image tracking.


        }
    }

    private void ValidateQRCode()
    {
        //Connection with DB: send ACK that this riddle (string) was found by this team
        //Sets CorrectRiddle bool
    }

    private void SpawnMessageAndPrefab(XRReferenceImage trackedImage)
    {
        //For correct riddle 
        if (CorrectRiddle)
        {
            Instantiate(prefab);//+offset i me animation
        }

        //For in-correct riddle
        else
        {

        }
    }

    //mia domi p na exei ola ta qr images
    //an to qr guid einai auto p theloume (elegxos apo vasi an antistoixei sto riddle p thelei o xristis (?) simfona me t game tou
    //) tote spawnare

    //emeis tha exoume mia lista apo qrs
    //o user mporei na scanarei enan p den einai sostos opote to default spawning den mas kanei
    //dioti allo prama tha deixnoume an einai sosto scanning i oxi

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
        //foreach (var trackedImage in e.added)
        //{
        //    Debug.Log($"Tracked image detected: {trackedImage.referenceImage.name} with size: {trackedImage.size}");
        //}

        UpdateTrackedImages(e.added);
        UpdateTrackedImages(e.updated);
    }
    // Method handling found/lost tracked images.
    void ImageChangedHandle(ARTrackedImagesChangedEventArgs imgChangedArgs)
    {
        // loop over new found images. imgChangedArgs.added is List<ARTrackedImage>
        foreach (var item in imgChangedArgs.added)
        {
            var go = item.gameObject;
            Instantiate(prefab);
            //kalo prefab
            //delete prev when next is scaned (eneable xristi na kanei polla scans?)
            //loop
            //sosto k lathos apotelesma
        }
    }
    private void UpdateTrackedImages(IEnumerable<ARTrackedImage> trackedImages)
    {
        // If the same image (ReferenceImageName)
        var trackedImage =
            trackedImages.FirstOrDefault(x => x.referenceImage.name == "qr2");
        var trackedImage2 =
            trackedImages.FirstOrDefault(x => x.referenceImage.name == "qr1");
        var trackedImage3 =
            trackedImages.FirstOrDefault(x => x.referenceImage.name == "testQRFromServer");
  

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
        if (trackedImage3)
        {
            CorrectMsg.SetActive(true);
            WrongMsg.SetActive(true);

        }

    }

}
