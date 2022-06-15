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

    [Tooltip("Reference Image Library")]
    IReferenceImageLibrary m_ImageLibrary;

    [SerializeField]
    [Tooltip("Choose prefab to be spawned when QR is found")]
    GameObject prefab;

    [SerializeField]
    [Tooltip("to be replaced")]
    GameObject prefab2;

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
        m_ImageLibrary = GetComponent<ARTrackedImageManager>().referenceLibrary;
    }
    //tin lib thelo na tin kano modify at runtime?
    //ta qrs otan fortonetai to paixnidi tha pernane stin unity
    //isos xreiaste runtime n mpoun
    private void Update()
    {
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
        foreach (var trackedImage in e.added)
        {
            Debug.Log($"Tracked image detected: {trackedImage.referenceImage.name} with size: {trackedImage.size}");
        }

        //UpdateTrackedImages(e.added);
        //UpdateTrackedImages(e.updated);
        ImageChangedHandle(e);
    }
    // Method handling found/lost tracked images.
    void ImageChangedHandle(ARTrackedImagesChangedEventArgs imgChangedArgs)
    {
        // loop over new found images. imgChangedArgs.added is List<ARTrackedImage>
        foreach (var item in imgChangedArgs.added)
        {
            debugText.text = "kati";
            debugText.text = "lalala";
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
            trackedImages.FirstOrDefault(x => x.referenceImage.name == "qrcode1");
        var trackedImage4 =
            trackedImages.FirstOrDefault(x => x.referenceImage.name == "qrcode2");
  

        if (trackedImage)
        {

            CorrectMsg.SetActive(true);
        }
        if (trackedImage2)
        {
            Instantiate(prefab2);

        }
        if (trackedImage3)
        {

            WrongMsg.SetActive(true);
        }
        if (trackedImage4)
        {
            debugText.text = "kati";

        }
        //if (trackedImage.trackingState != TrackingState.None)
        //{
        //    var trackedImageTransform = trackedImage.transform;
        //    transform.SetPositionAndRotation(trackedImageTransform.position, trackedImageTransform.rotation);
        //}
    }

}
