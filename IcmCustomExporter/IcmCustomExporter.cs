using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Kofax.Eclipse.Base;

namespace IcmCustomExporter
{
    public class IcmCustomExporter : IReleaseScript
    {
        //      private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Standard interface to inspect the script's properties and basic settings
        /// <summary>
        /// This GUID is used to uniquely identify the script and distinguish it from the application. 
        /// Generate a GUID from Visual Studio when you first create the script and 
        /// keep it for the rest of its life.
        /// </summary>
        public Guid Id
        {
            get { return new Guid("{3371C4B3-E7B0-4c8b-8D03-960C052873D4}"); }
        }

        /// <summary>
        /// This name appears in the list of available release scripts 
        /// in the application's UI. The name should be localized.
        /// </summary>
        public string Name
        {
            get { return "ICM Custom Exporter"; }
        }

        /// <summary>
        /// This description appears whenever the application decides to 
        /// briefly explain the script's purpose to the user. The description should also be localized.
        /// </summary>
        public string Description
        {
            get { return "Custom Exporter"; }
        }

        /// <summary>
        /// The script will release batches using the mode it remembered from a previous setup.
        /// </summary>
        public ReleaseMode WorkingMode
        {
            get { return m_WorkingMode; }
        }

        /// <summary>
        /// This simple script will process batches in both single-page and multi-page release modes.
        /// </summary>
        public bool IsSupported(ReleaseMode mode)
        {
            return true;
        }
        #endregion

        #region Script settings - Will be remembered across sessions
        /// <summary>
        /// The script will release batches in this mode. The script can be configured by the setup dialog and remembered across sessions.
        /// </summary>
        private ReleaseMode m_WorkingMode = ReleaseMode.SinglePage;

        /// <summary>
        /// The destination to place the released pages. 
        /// </summary>
        private string m_Destination = string.Empty;

        /// <summary>
        /// ID of the user-selected file type converter to convert the released documents/pages.
        /// </summary>
        private Guid m_FileTypeId = Guid.Empty;

        /// <summary>
        /// Flag to determine if first page should be deleted
        /// </summary>
        private Boolean m_DeleteFirstPage;
        #endregion

        #region Instance settings - Valid only throughout the running session
        /// <summary>
        /// Reference to the actively employed converter to pass the pages through
        /// </summary>
        private IPageOutputConverter m_PageConverter;

        /// <summary>
        /// Reference to the actively employed converter to pass the documents through
        /// </summary>
        private IDocumentOutputConverter m_DocConverter;

        /// <summary>
        /// Point to the destination folder for the entire released batch
        /// </summary>
        private string m_BatchFolder;

        /// <summary>
        /// Point to the destination folder for the pages to be released in the current document
        /// </summary>
        private string m_DocFolder;

        //ICM Variables
        private int m_DocumentNumber;
        private string m_ProjectNumber;  //Populated from the first index field, barcode value
        private string m_SequenceNumber;
        private string m_BarcodeFileName;
        private int m_PageNumber;
        private int m_PreviousSheetNumber;
        private bool m_IsDuplex;
        #endregion

        #region Handlers to be called during an actual release process
        /// <summary>
        /// This method will be called first when the release is started. The application will pass the latest information 
        /// from the running instance to the script through given parameters. The script should do its final check
        /// for proper release conditions, throwing exceptions if problems occur.
        /// </summary>
        public object StartRelease(IList<IExporter> exporters, IIndexField[] indexFields, IDictionary<string, string> releaseData)
        {
            //logger.Info("Start Extended Release Script");
            if (string.IsNullOrEmpty(m_Destination))
                throw new Exception("Please specify a release destination");

            m_DocConverter = null;
            m_PageConverter = null;

            foreach (IExporter exporter in exporters)
                if (exporter.Id == m_FileTypeId)
                {
                    if (m_WorkingMode == ReleaseMode.SinglePage)
                        m_PageConverter = exporter as IPageOutputConverter;
                    else
                    {
                        m_DocConverter = exporter as IDocumentOutputConverter;
                    }
                }

            /// When both of them can't be found, either the user hasn't set up properly, or the chosen converter has disappeared.
            /// The script can declare that the release cannot continue or proceed with default settings.
            if (m_PageConverter == null && m_DocConverter == null)
                throw new Exception("Please select an output file type");


            /// The application will keep any object returned from this function and pass it back to the script 
            /// in the EndRelease call. This is usually intended to facilitate cleanup.
            return null;
        }

        /// <summary>
        /// This method will be called after the batch has been prepared by the application 
        /// but before any document/page is sent to the script. The scripts usually perform 
        /// preparations to release the batch based on the current settings.
        /// </summary>
        public object StartBatch(IBatch batch)
        {
            m_BatchFolder = Path.Combine(m_Destination, batch.Name);

            bool batchFolderCreated = !Directory.Exists(m_BatchFolder);
            if (batchFolderCreated)
                Directory.CreateDirectory(m_BatchFolder);

            m_ProjectNumber = string.Format("{0}-", batch.Name);
            m_BarcodeFileName = Path.Combine(m_BatchFolder, "barcode.txt");

            /// Again, the application will keep any object returned from this function and pass it back to the script 
            /// in the EndBatch call. This is usually intended to facilitate cleanup.)
            return batchFolderCreated;
        }

        public void Release(IDocument doc)
        {
            //Nothing to do
        }

        /// <summary>
        /// For every document, this method will be called after it has been prepared by the application 
        /// but before any page is sent to the script. The scripts usually performs preparations to release
        /// the document based on the current settings.
        /// </summary>
        public object StartDocument(IDocument doc)
        {
            m_SequenceNumber = doc.GetIndexDataValue(0);
            m_DocumentNumber = doc.Number;
            m_PageNumber = 0;

            return null;
        }

        /// <summary>
        /// In single-page release mode, this method will be called for every page in the batch.
        /// This script will simply pass pages to the selected page output converter to produce
        /// the expected output files in the currently released document folder.
        /// </summary>
        public void Release(IPage page)
        {
            if (m_DeleteFirstPage)
            {
                if(page.Number.Equals(1))
                    return;

                int pageNumber = page.Number - 1;

                //string outputFileName = Path.Combine(m_BatchFolder, m_ProjectNumber + m_DocumentNumber.ToString("D4") + "-" + pageNumber.ToString("D4"));
                string outputFileName = Path.Combine(m_BatchFolder, m_ProjectNumber + m_DocumentNumber.ToString("D4") + "-" + pageNumber.ToString("D4"));
                m_PageConverter.Convert(page, Path.ChangeExtension(outputFileName, m_PageConverter.DefaultExtension));
                UpdateIndexFile(Path.ChangeExtension(outputFileName, m_PageConverter.DefaultExtension));
            }
            else
            {
                //string outputFileName = Path.Combine(m_BatchFolder, m_ProjectNumber + m_DocumentNumber.ToString("D4") + "-" + page.Number.ToString("D4"));
                string outputFileName = Path.Combine(m_BatchFolder, m_ProjectNumber + m_DocumentNumber.ToString("D4") + "-" + GetPageNumber(page).ToString("D4"));
                m_PageConverter.Convert(page, Path.ChangeExtension(outputFileName, m_PageConverter.DefaultExtension));
                UpdateIndexFile(Path.ChangeExtension(outputFileName, m_PageConverter.DefaultExtension));
            }
        }

        private int GetPageNumber(IPage page)
        {
            if (page.Number == 1)
            {
                m_PageNumber = m_PageNumber + 1;
                m_PreviousSheetNumber = page.SheetNumber;
                return m_PageNumber;
            }

            if (m_PreviousSheetNumber == page.SheetNumber)
            {
                m_PageNumber = m_PageNumber + 1;
                m_IsDuplex = true;
            }
            
            if ((m_PreviousSheetNumber != page.SheetNumber) && !m_IsDuplex)
            {
                m_PageNumber = m_PageNumber + 2;
                m_IsDuplex = false;
                m_PreviousSheetNumber = page.SheetNumber;
            }

            if ((m_PreviousSheetNumber != page.SheetNumber) && m_IsDuplex)
            {
                m_PageNumber = m_PageNumber + 1;
                m_IsDuplex = false;
                m_PreviousSheetNumber = page.SheetNumber;
            }            

            return m_PageNumber;
        }

        /// <summary>
        /// For every document, this method will be called after all pages have been sent to the script. 
        /// The scripts usually perform the necessary cleanup based on current settings and the actual release conditions.
        /// </summary>
        public void EndDocument(IDocument doc, object handle, ReleaseResult result)
        {
            /// The handle should always indicate whether or not the script created the document folder from scratch
            if (result != ReleaseResult.Succeeded && (bool)handle)
                Directory.Delete(m_DocFolder);
        }

        /// <summary>
        /// This method will be called after all documents have been sent to the script. 
        /// The scripts usually perform the necessary cleanup based on current settings 
        /// and the actual release conditions.
        /// </summary>
        public void EndBatch(IBatch batch, object handle, ReleaseResult result)
        {
            /// The handle should always indicate whether or not the script created the batch folder from scratch
            if (result != ReleaseResult.Succeeded && (bool)handle)
                Directory.Delete(m_BatchFolder);
        }

        /// <summary>
        /// This method will be called after everything has been sent to the script 
        /// and the batch has been closed by the application. The scripts usually perform 
        /// necessary cleanup based on current settings and the actual release conditions.
        /// </summary>
        public void EndRelease(object handle, ReleaseResult result)
        {
            /// Since we don't do anything special in this simple script, 
            /// there's nothing to be cleaned up here. The handle should always be null.
        }
        #endregion

        #region Handlers to be called during configuration requests by the user and before/after a release session
        /// <summary>
        /// Simply write whatever needs to persist across sessions here.
        /// </summary>
        public void SerializeSettings(Stream output)
        {
            using (BinaryWriter writer = new BinaryWriter(output))
            {
                writer.Write(m_Destination);
                writer.Write(m_FileTypeId.ToString());
                writer.Write(m_WorkingMode.ToString());
                writer.Write(m_DeleteFirstPage);
            }
        }

        /// <summary>
        /// Simply read whatever persisted from previous sessions here.
        /// </summary>
        public void DeserializeSettings(Stream input)
        {
            using (BinaryReader reader = new BinaryReader(input))
            {
                try
                {
                    m_Destination = reader.ReadString();
                    m_FileTypeId = new Guid(reader.ReadString());
                    m_WorkingMode = (ReleaseMode)Enum.Parse(typeof(ReleaseMode), reader.ReadString());
                    m_DeleteFirstPage = reader.ReadBoolean();
                }
                catch
                {
                    /// If the script throws exceptions here, it wouldn't be able to recover from the application.
                    /// This will be addressed in a later version of the API.
                    m_Destination = string.Empty;
                    m_FileTypeId = Guid.Empty;
                    m_WorkingMode = ReleaseMode.SinglePage;
                    m_DeleteFirstPage = false;
                }
            }
        }

        /// <summary>
        /// Whenever the user requests to configure the script's settings, the method will be called with
        /// the latest information from the application's running instance as parameters. Also, a script can
        /// define and add its own information to the data table and pass it further down to the exporters.
        /// </summary>
        public void Setup(IList<IExporter> exporters, IIndexField[] indexFields, IDictionary<string, string> releaseData)
        {
            IcmCustomExporterSetup setupDialog = new IcmCustomExporterSetup(exporters,
                                                                            m_Destination,
                                                                            m_FileTypeId,
                                                                            m_WorkingMode,
                                                                            m_DeleteFirstPage);
            if (setupDialog.ShowDialog() != DialogResult.OK) return;

            m_Destination = setupDialog.Destination;
            m_FileTypeId = setupDialog.FileTypeId;
            m_WorkingMode = setupDialog.WorkingMode;
            m_DeleteFirstPage = setupDialog.DeleteFirstPage;
        }
        #endregion

        private void UpdateIndexFile(string imageName)
        {
            using (FileStream fs = new FileStream(m_BarcodeFileName, FileMode.Append, FileAccess.Write, FileShare.None))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.ASCII))
            {
                writer.WriteLine(string.Format("{0},{1}", Path.GetFileName(imageName), m_SequenceNumber));

                writer.Flush();
                writer.Close();
            }
        }
    }
}