using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RailExamWebApp.Common.Class
{
    public class FtpClient
    {
        private string _hostName;
        private string _username;
        private string _welcomemessage = "";
        private NetworkCredential _credential;
        private bool _enableSSL = false;
        internal X509Certificate certificate;
        private static Dictionary<string, FtpClient> _ftpSessionTable = new Dictionary<string, FtpClient>();
        private FtpWebRequest _request;

		/// <summary>
        /// 
        /// </summary>
        /// <param name="hostname"></param>
        /// <param name="credential"></param>
        /// <param name="isSecured"></param>
        public FtpClient(string hostname,
                        NetworkCredential credential,
                        bool isSecured)
        {
            _hostName = hostname;
            _credential = credential;
            if (credential != null)
            {
                _username = credential.UserName;
            }
            else
            {
                _username = "anonymous";
            }
            _enableSSL = isSecured;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostName"></param>
        /// <param name="credential"></param>
        /// <param name="isSecured"></param>
        public static void AddSite(string hostName, NetworkCredential credential,bool isSecured)
        {
            if (_ftpSessionTable.ContainsKey(hostName.ToLower()))
            {
                throw new Exception("已与该服务器连接！");
            }
            FtpClient client = new FtpClient(hostName, credential, isSecured);
            client.GetFullDirectoryList("/");
            _ftpSessionTable.Add(client.HostId.ToLower(), client);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hostName"></param>b
        /// <returns></returns>
        public static FtpClient GetFtpClient(string hostName)
        {
            if (_ftpSessionTable.ContainsKey(hostName.ToLower()))
            {
                return (FtpClient)_ftpSessionTable[hostName.ToLower()];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value></value>
        public string HostId
        {
            get
            {
                return _hostName.ToLower();
            }
        }

        public string WelcomeMessage
        {
            get
            {
                return _welcomemessage;
            }
        }

        private FtpWebRequest CreateFtpWebRequest(Uri uri, string method)
        {
            FtpWebRequest ftpclientRequest = (FtpWebRequest)WebRequest.Create(uri);
            ftpclientRequest.Proxy = null;
            if (_credential != null)
            {
                ftpclientRequest.Credentials = _credential;
            }
            ftpclientRequest.EnableSsl = _enableSSL;
            ftpclientRequest.Method = method;
            ftpclientRequest.KeepAlive = true;
            ftpclientRequest.UsePassive = true;
            _request = ftpclientRequest;
            return ftpclientRequest;
        }

        public FileStruct[] GetDirectoryList(string directoryPath)
        {
            Uri uri = new Uri("ftp://" + _hostName + directoryPath);
            FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.ListDirectoryDetails);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
            string dataString = sr.ReadToEnd();
            response.Close();
            DirectoryListParser parser = new DirectoryListParser(dataString);
            return parser.DirectoryList;
        }

        public FileStruct[] GetFileList(string directoryPath)
        {
            Uri uri = new Uri("ftp://" + _hostName + directoryPath);
            FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.ListDirectoryDetails);
            _request = request;
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
            string dataString = sr.ReadToEnd();
            response.Close();
            DirectoryListParser parser = new DirectoryListParser(dataString);
            return parser.FileList;
        }

        private FileStruct[] GetFullDirectoryList(string directoryPath)
        {
            Uri uri = new Uri("ftp://" + _hostName + directoryPath);
            FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.ListDirectoryDetails);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
            string dataString = sr.ReadToEnd();
            response.Close();
            if (_enableSSL)
            {//Refreshing the certificate on every ListDirectory request
                certificate = request.ServicePoint.Certificate;
            }
            _welcomemessage = response.WelcomeMessage;
            DirectoryListParser parser = new DirectoryListParser(dataString);
            return parser.FullListing;
        }

        public void DownloadFile(Uri sourceUri, string destinationFile)
        {
            FileInfo file = new FileInfo(destinationFile);
            if (file.Directory.Exists)
            {
                FtpWebRequest request = CreateFtpWebRequest(sourceUri, WebRequestMethods.Ftp.DownloadFile);
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream fileStream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write);
                copyDataToDestination(responseStream, fileStream);
            }
            else
            {
                //MessageBox.Show("路径： " + file.DirectoryName + " 不存在！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void UploadFile(string sourceFile, Uri destinationPath)
        {
            try
            {
                FileInfo file = new FileInfo(sourceFile);
                Uri uri = new Uri(destinationPath.AbsoluteUri + "/" + file.Name);
                FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.UploadFile);
                Stream requestStream = request.GetRequestStream();
                FileStream fileStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read);
                copyDataToDestination(fileStream, requestStream);
                WebResponse response = request.GetResponse();
                response.Close();
            }
            catch (WebException e)
            {
                displayErrorDialog("Fileuploade Error", e.Message);
            }
            catch (IOException e)
            {
                displayErrorDialog("Fileuploade Error", e.Message);
            }
        }

        private void copyDataToDestination(Stream sourceStream, Stream destinationStream)
        {
            byte[] buffer = new byte[1024];
            int bytesRead = sourceStream.Read(buffer, 0, 1024);
            while (bytesRead != 0)
            {
                destinationStream.Write(buffer, 0, bytesRead);
                bytesRead = sourceStream.Read(buffer, 0, 1024);
            }
            destinationStream.Close();
            sourceStream.Close();
        }

        public void DeleteFile(Uri fullFileUri)
        {
            try
            {
                FtpWebRequest request = CreateFtpWebRequest(fullFileUri, WebRequestMethods.Ftp.DeleteFile);
                WebResponse response = request.GetResponse();
                response.Close();
            }
            catch (WebException e)
            {
                displayErrorDialog("Delete Error", e.Message);
            }
        }

        public void MakeDirectory(string fullDirectoryPath)
        {
            try
            {
                Uri uri = new Uri("ftp://" + _hostName + fullDirectoryPath);
                FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.MakeDirectory);
                WebResponse response = request.GetResponse();
                response.Close();
            }
            catch (WebException e)
            {
                displayErrorDialog("MakeDirectory Error", e.Message);
            }
        }

        public void RemoveDirectory(string fullDirectoryPath)
        {
            try
            {
                Uri uri = new Uri("ftp://" + _hostName + fullDirectoryPath);
                FtpWebRequest request = CreateFtpWebRequest(uri, WebRequestMethods.Ftp.RemoveDirectory);
                WebResponse response = request.GetResponse();
                response.Close();
            }
            catch (WebException e)
            {
                displayErrorDialog("RemoveDirectory Error", e.Message);
            }
        }

        private void displayErrorDialog(string caption, string message)
        {
            
        }
    }
}
