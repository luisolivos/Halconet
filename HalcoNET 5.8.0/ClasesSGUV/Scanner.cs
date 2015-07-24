using System;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using WIA;
using System.Drawing;
namespace ClasesSGUV
{
    public class Scanner
    {
        private readonly DeviceInfo _deviceInfo;
        const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatPNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatGIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatTIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";
        bool _existScanner = false;

        public bool ExistScanner
        {
            get { return _existScanner; }
            set { _existScanner = value; }
        }

        public Scanner()
        {
           // this._deviceInfo = deviceInfo;
        }

       // string DeviceID;
        public List<System.Drawing.Image> Scan()
        {
            var deviceManager = new DeviceManager();
           
            List<System.Drawing.Image> listImages = new List<System.Drawing.Image>();
            _existScanner = false;
            _existScanner = deviceManager.DeviceInfos.Count > 0;

            if (_existScanner)
            {
                //WIAScanner.Scan();
                bool hasMorePages = true;
                //Device Scanners;
                WIA.CommonDialog Common;

                Common = new CommonDialog();
                // Scanners = Common.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, false, false);
                int i = 0;

                while (hasMorePages)
                {
                    try
                    {
                        //Item item = Scanners.GetItem();
                        ImageFile Image = Common.ShowAcquireImage(WiaDeviceType.ScannerDeviceType,
                                                                    WiaImageIntent.ColorIntent,
                                                                    WiaImageBias.MaximizeQuality,
                                                                    wiaFormatJPEG,
                                                                    false, true, false);

                        if (Image != null)
                        {
                            Byte[] imageBytes = (byte[])Image.FileData.get_BinaryData();
                            MemoryStream ms = new MemoryStream(imageBytes);
                            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                            listImages.Add(img);
                        }
                        else
                        {
                            hasMorePages = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        _existScanner = false;
                        hasMorePages = false;
                    }
                    finally
                    {
                        i++;
                    }
                }
            }
            return listImages;
        }
        private bool HasMorePages(Device wia)
        {

            //determine if there are any more pages waiting
            Property documentHandlingSelect = null;
            Property documentHandlingStatus = null;

            string test = string.Empty;

            foreach (Property prop in wia.Properties)
            {
                string propername = prop.Name;
                string propvalue = prop.get_Value().ToString();

                test += propername + " " + propvalue + "<br>";

                if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT)
                    documentHandlingSelect = prop;
                if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                    documentHandlingStatus = prop;
            }

            if ((Convert.ToUInt32(documentHandlingSelect.get_Value()) & 0x00000001) != 0)
            {
                return ((Convert.ToUInt32(documentHandlingStatus.get_Value()) & 0x00000001) != 0);
            }

            string tester = test;

            return false;

        }
    }

    public class ScannerException : ApplicationException
    {
        public ScannerException()
            : base()
        { }

        public ScannerException(string message)
            : base(message)
        { }

        public ScannerException(string message, Exception innerException)
            : base(message, innerException)
        { }

    }

    public class ScannerNotFoundException : ScannerException
    {
        public ScannerNotFoundException()
            : base("Error retrieving a list of scanners. Is your scanner or multi-function printer turned on?")
        {
        }
    }

    class WIA_PROPERTIES
    {
        public const uint WIA_RESERVED_FOR_NEW_PROPS = 1024;
        public const uint WIA_DIP_FIRST = 2;
        public const uint WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
        public const uint WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
        //
        // Scanner only device properties (DPS)
        //
        public const uint WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
        public const uint WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13;
        public const uint WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14;
    }
}
