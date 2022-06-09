using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using TMPro;
using ZXing.QrCode;
using System.IO;

public class QRcodeGenerator : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImage;
    [SerializeField]
    private TMP_InputField inputFieldText;

    private Texture2D storeEncodedTexture;
    void Start()
    {
        storeEncodedTexture = new Texture2D(256, 256);
    }

    private Color32 [] Encode(string text,int width,int height)
    {
        BarcodeWriter qrWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions
            {
                Height = height,
                Width = width
            }
        };
        return qrWriter.Write(text);
    }

    public void onClickEncode()
    {
        EncodeTextToQRCode();
    }

    private void EncodeTextToQRCode()
    {
        //apo vasi katerinas
        string textWrite = string.IsNullOrEmpty(inputFieldText.text) ? "Empty string" : inputFieldText.text;

        //Color32[] convertPixelToTexture = Encode(textWrite, storeEncodedTexture.width, storeEncodedTexture.height);
        //storeEncodedTexture.SetPixels32(convertPixelToTexture);
        //storeEncodedTexture.Apply();

        ////raw image stin scene
        //rawImage.texture = storeEncodedTexture;

        //save to disk
        byte[] bytes = storeEncodedTexture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/testQR.png", bytes);
    }
}
