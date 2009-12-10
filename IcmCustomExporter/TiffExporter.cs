using System;
using System.Collections.Generic;
using System.IO;
using Kofax.Eclipse.Toolkit.Storage;
using Kofax.Eclipse.Toolkit;
using Kofax.Eclipse.Base;
//using NLog;

namespace IcmCustomExporter
{
    public class TiffExporter : IDocumentOutputConverter
    {
       // Logger logger = LogManager.GetCurrentClassLogger();
        
        public Guid Id
        {
            get { return ClassId; }
        }
        private static readonly Guid ClassId = new Guid("{CBA3C52B-4365-4ff8-AE25-59243C514732}");

        public string Name
        {
            get { return "Tagged Image File Format with FAX extension"; }
        }

        public string Description
        {
            get { return "Tagged Image File Format with FAX extension"; }
        }

        public string DefaultExtension
        {
            get { return "PDF"; }
        }

        public void SerializeSettings(Stream output)
        {
            // Nothing to write
            return;
        }

        public void DeserializeSettings(Stream input)
        {
            // Nothing to read
            return;
        }

        public void Setup(IDictionary<string, string> releaseData)
        {
            // Nothing to setup
            return;
        }

        public bool IsCustomizable
        {
            get { return false; }
        }

        /// <summary>
        /// Convert a document to multi-page TIFF.
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="outputFileName"></param>
        public void Convert(IDocument doc, string outputFileName)
        {
            if (doc.PageCount <= 1)
                return;

            String[] inputFileNames = new String[doc.PageCount - 1];

            for (int i = 1; i <= doc.PageCount - 1; i++)
            {
                inputFileNames[i - 1] = doc.GetPage(i + 1).OutputFileName;

                //Log the files being deleted
                if (i == 1)
                {
                    //logger.Info("Deleting " + doc.GetPage(i).OutputFileName);
                }
            }
            Convert(outputFileName,
                    inputFileNames,
                    Path.GetDirectoryName(outputFileName),
                    ImageStorage.OutputType.PDF);
        }

        /// <summary>
        /// Common conversion routine for TIF images.
        /// 
        /// When ifimage issues are fixed, see build 206 for TiffJpegStyle handling.
        /// </summary>
        /// <param name="outputFileName"></param>
        /// <param name="inputFileNames"></param>
        /// <param name="outputFolder"></param>
        /// <param name="outputType"></param>
        private static void Convert(String outputFileName,
                                    String[] inputFileNames,
                                    String outputFolder,
                                    ImageStorage.OutputType outputType)
        {
            int imageProcessed;
            String imagesCreated;
            int result = ImageStorage.ConvertToOutput(
                inputFileNames,
                Tools.Utilities.ToShortPathName(outputFolder),
                outputType,
                0,
                ImageStorage.Rotation.None,
                out imageProcessed,
                out imagesCreated
                );

            if (result == 0 && !String.IsNullOrEmpty(imagesCreated))
            {
                imagesCreated = Path.Combine(outputFolder, imagesCreated);

                if (imagesCreated.Equals(outputFileName, StringComparison.CurrentCultureIgnoreCase))
                    return;

                File.Delete(outputFileName);
                File.Move(imagesCreated, outputFileName);
            }
            else
                throw new Exception(string.Format("FileCreationFailed",
                    outputFileName,
                    result,
                    ToolkitManager.StatusToErrorText(result)));
        }
    }
}
