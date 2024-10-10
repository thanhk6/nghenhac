using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Collections.Generic;

//using VSW.Lib.Global;
using VSW.Lib.Models;
using System.IO;
using System.Collections;

namespace VSW.Website.Tools
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class File : System.Web.Services.WebService
    {
        public HeaderUserAuth UserInfo = null;
        //public HeaderError ErrorInfo = null;

        private bool IsValid()
        {
            if (UserInfo == null) 
                return false;

            return (UserInfo.LoginName.Trim().ToUpper() == VSW.Core.Global.Config.GetValue("Mod.WebServiceKey").ToString().Trim().ToUpper());
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public List<WsFile> GetFiles(string Path, int PageIndex, int PageSize, out int TotalRecord)
        {
            TotalRecord = 0;

            if (!this.IsValid())
                return null;

            List<WsFile> listFile = new List<WsFile>();

            string[] ArrFiles = Directory.GetFiles(Server.MapPath("~/" + Path), "*.*");
            IComparer fileComparer = new CompareFileByDate();

            TotalRecord = ArrFiles.Length;

            Array.Sort(ArrFiles, fileComparer);

            for (int i = PageIndex * PageSize; i < PageSize * (PageIndex + 1) && i < ArrFiles.Length; i++)
            {
                string _PathFile = ArrFiles[i];

                FileInfo _FlieInfo = new FileInfo(_PathFile);

                string _FileName = System.IO.Path.GetFileName(_PathFile);
                //string _DirName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(_PathFile));

                WsFile file = new WsFile
                {
                    Name = _FileName,
                    FullName = (Path == string.Empty ? string.Empty : Path + "/") + _FileName,
                    Length = _FlieInfo.Length,
                    //Size = (float)((float)_FlieInfo.Length / (float)1024),
                    Modified = _FlieInfo.LastWriteTime
                };

                listFile.Add(file);
            }

            return listFile;
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public string ReadFileText(string Path)
        {
            if (!this.IsValid())
                return null;

            return VSW.Lib.Global.Data.Base64Encode(VSW.Lib.Global.File.ReadText(Server.MapPath("~/" + Path)));
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void WriteFileText(string Path, string Content, bool IsUTF8)
        {
            if (!this.IsValid())
                return;

            Content = VSW.Lib.Global.Data.Base64Decode(Content);

            if (!IsUTF8)
                VSW.Lib.Global.File.WriteText(Server.MapPath("~/" + Path), Content, false);
            else
                VSW.Lib.Global.File.WriteTextUnicode(Server.MapPath("~/" + Path), Content, false);
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void DeleteFile(string Path)
        {
            if (!this.IsValid())
                return;

            VSW.Lib.Global.File.Delete(Server.MapPath("~/" + Path));
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void MoveFile(string SourceFile, string DestFile)
        {
            if (!this.IsValid())
                return;

            if (SourceFile != DestFile)
                DeleteFile(DestFile);

            System.IO.File.Move(Server.MapPath("~/" + SourceFile), Server.MapPath("~/" + DestFile));
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void CreateFile(string Path)
        {
            if (!this.IsValid())
                return;

            FileStream writeStream = new FileStream(Server.MapPath("~/" + Path), FileMode.Create);
            writeStream.Close();
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void WriteFile(string Path, byte[] Bytes)
        {
            if (!this.IsValid())
                return;

            FileStream writeStream = new FileStream(Server.MapPath("~/" + Path), FileMode.Append);
            BinaryWriter writeBinay = new BinaryWriter(writeStream);
            writeBinay.Write(Bytes);
            writeBinay.Close();
            writeStream.Close();
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public WsFile ReadFileInfo(string Path)
        {
            if (!this.IsValid())
                return null;

            string _PathFile = Server.MapPath("~/" + Path);
            FileInfo _FlieInfo = new FileInfo(_PathFile);
            string _FileName = System.IO.Path.GetFileName(_PathFile);
            string _DirName = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(_PathFile));

            return new WsFile
            {
                Name = _FileName,
                FullName = Path,
                Length = _FlieInfo.Length,
                //Size = (float)((float)_FlieInfo.Length / (float)1024),
                Modified = _FlieInfo.LastWriteTime
            };
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public byte[] ReadFile(string Path, long Offset, int Length)
        {
            if (!this.IsValid())
                return null;

            FileInfo _FileInfo = new FileInfo(Server.MapPath("~/" + Path));
            FileStream readStream = _FileInfo.OpenRead();
            BinaryReader readBinay = new BinaryReader(readStream);

            byte[] buffer = new byte[Length];
            if (_FileInfo.Length - Offset < Length)
                buffer = new byte[_FileInfo.Length - Offset];

            readBinay.Read(buffer, 0, buffer.Length);

            readBinay.Close();

            return buffer;
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public List<WsFolder> GetFolders(string Path)
        {
            if (!this.IsValid())
                return null;

            List<WsFolder> listFolder = new List<WsFolder>();

            string[] ArrDir = VSW.Lib.Global.Directory.GetListDir(Server.MapPath("~/" + Path));

            for (int i = 0; i < ArrDir.Length; i++)
            {
                string name = System.IO.Path.GetFileName(ArrDir[i]);

                WsFolder folder = new WsFolder { Name = name };

                listFolder.Add(folder);
            }

            return listFolder;
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void CreateFolder(string Path)
        {
            if (!this.IsValid())
                return;

            VSW.Lib.Global.Directory.Create(Server.MapPath("~/" + Path));
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void MoveFolder(string SourcePath, string DestPath)
        {
            if (!this.IsValid())
                return;

            System.IO.Directory.Move(Server.MapPath("~/" + SourcePath), Server.MapPath("~/" + DestPath));
        }

        [WebMethod, SoapHeader("UserInfo", Direction = SoapHeaderDirection.In)]
        public void DeleteFolder(string Path)
        {
            if (!this.IsValid())
                return;

            VSW.Lib.Global.Directory.DeleteAll(Server.MapPath("~/" + Path));
        }
    }

    public class WsFolder
    {
        public string Name { get; set; }
    }

    public class WsFile
    {
        public string Name { get; set; }
        //public float Size { get; set; }
        public long Length { get; set; }
        public DateTime Modified { get; set; }
        public string FullName { get; set; }
    }

    public class CompareFileByDate : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            FileInfo fia = new FileInfo((string)a);
            FileInfo fib = new FileInfo((string)b);

            DateTime cta = fia.LastWriteTime;
            DateTime ctb = fib.LastWriteTime;

            return DateTime.Compare(ctb, cta);
        }
    }
}
