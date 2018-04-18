using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
namespace Helper
{
    /// <summary>  
    /// 压缩文件类  
    /// </summary>  
    public class GZip
    {
        /// <summary>  
        /// 压缩  
        /// </summary>  
        /// <param name="lpSourceFolder">要压缩的文件夹路径,包括子文件夹的所有文件将包括在内.</param>  
        /// <param name="lpDestFolder">目标文件夹</param>  
        /// <param name="zipFileName">生成的ZIP文件名</param>  
        public static GZipResult Compress(string lpSourceFolder, string lpDestFolder, string zipFileName)
        {
            return Compress(lpSourceFolder, "*.*", SearchOption.AllDirectories, lpDestFolder, zipFileName, true);
        }

        /// <summary>  
        /// 压缩  
        /// </summary>  
        /// <param name="lpSourceFolder">在zip文件中包括的文件的位置.</param>  
        /// <param name="searchPattern">搜索模式("*.*" or "*.txt" or "*.gif")</param>  
        /// <param name="searchOption">只lpSourceFolder文件或还包括子文件夹中的文件</param>  
        /// <param name="lpDestFolder">目标文件夹</param>  
        /// <param name="zipFileName">生成的ZIP文件名</param>  
        /// <param name="deleteTempFile">布尔, true 删除临时文件, false 不删除 (调试)</param>  
        public static GZipResult Compress(string lpSourceFolder, string searchPattern, SearchOption searchOption, string lpDestFolder, string zipFileName, bool deleteTempFile)
        {
            DirectoryInfo di = new DirectoryInfo(lpSourceFolder);
            FileInfo[] files = di.GetFiles("*.*", searchOption);
            return Compress(files, lpSourceFolder, lpDestFolder, zipFileName, deleteTempFile);
        }

        /// <summary>  
        /// 压缩  
        /// </summary>  
        /// <param name="files">zip文件中包含FileInfo对象数组</param>  
        /// <param name="lpBaseFolder">创建zip文件中存储的文件的相对路径时使用</param>  
        /// <param name="lpDestFolder">目标文件夹</param>  
        /// <param name="zipFileName">生成的ZIP文件名</param>  
        public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName)
        {
            return Compress(files, lpBaseFolder, lpDestFolder, zipFileName, true);
        }

        /// <summary>  
        /// 压缩  
        /// </summary>  
        /// <param name="files">zip文件中包含FileInfo对象数组</param>  
        /// <param name="lpBaseFolder">创建zip文件中存储的文件的相对路径时使用</param>  
        /// <param name="lpDestFolder">目标文件夹</param>  
        /// <param name="zipFileName">生成的ZIP文件名</param>  
        /// <param name="deleteTempFile">布尔, true 删除临时文件, false 不删除 (调试)</param>  
        public static GZipResult Compress(FileInfo[] files, string lpBaseFolder, string lpDestFolder, string zipFileName, bool deleteTempFile)
        {
            GZipResult result = new GZipResult();

            try
            {
                if (!lpDestFolder.EndsWith("\\"))
                {
                    lpDestFolder += "\\";
                }

                string lpTempFile = lpDestFolder + zipFileName + ".tmp";
                string lpZipFile = lpDestFolder + zipFileName;

                result.TempFile = lpTempFile;
                result.ZipFile = lpZipFile;

                if (files != null && files.Length > 0)
                {
                    CreateTempFile(files, lpBaseFolder, lpTempFile, result);

                    if (result.FileCount > 0)
                    {
                        CreateZipFile(lpTempFile, lpZipFile, result);
                    }

                    // delete the temp file  
                    if (deleteTempFile)
                    {
                        File.Delete(lpTempFile);
                        result.TempFileDeleted = true;
                    }
                }
            }
            catch //(Exception ex4)  
            {
                result.Errors = true;
            }
            return result;
        }

        private static void CreateZipFile(string lpSourceFile, string lpZipFile, GZipResult result)
        {
            byte[] buffer;
            int count = 0;
            FileStream fsOut = null;
            FileStream fsIn = null;
            GZipStream gzip = null;

            // compress the file into the zip file  
            try
            {
                fsOut = new FileStream(lpZipFile, FileMode.Create, FileAccess.Write, FileShare.None);
                gzip = new GZipStream(fsOut, CompressionMode.Compress, true);

                fsIn = new FileStream(lpSourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                buffer = new byte[fsIn.Length];
                count = fsIn.Read(buffer, 0, buffer.Length);
                fsIn.Close();
                fsIn = null;

                // compress to the zip file  
                gzip.Write(buffer, 0, buffer.Length);

                result.ZipFileSize = fsOut.Length;
                result.CompressionPercent = GetCompressionPercent(result.TempFileSize, result.ZipFileSize);
            }
            catch //(Exception ex1)  
            {
                result.Errors = true;
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                    gzip = null;
                }
                if (fsOut != null)
                {
                    fsOut.Close();
                    fsOut = null;
                }
                if (fsIn != null)
                {
                    fsIn.Close();
                    fsIn = null;
                }
            }
        }

        private static void CreateTempFile(FileInfo[] files, string lpBaseFolder, string lpTempFile, GZipResult result)
        {
            byte[] buffer;
            int count = 0;
            byte[] header;
            string fileHeader = null;
            string fileModDate = null;
            string lpFolder = null;
            int fileIndex = 0;
            string lpSourceFile = null;
            string vpSourceFile = null;
            GZipFileInfo gzf = null;
            FileStream fsOut = null;
            FileStream fsIn = null;

            if (files != null && files.Length > 0)
            {
                try
                {
                    result.Files = new GZipFileInfo[files.Length];

                    // open the temp file for writing  
                    fsOut = new FileStream(lpTempFile, FileMode.Create, FileAccess.Write, FileShare.None);

                    foreach (FileInfo fi in files)
                    {
                        lpFolder = fi.DirectoryName + "\\";
                        try
                        {
                            gzf = new GZipFileInfo();
                            gzf.Index = fileIndex;

                            // read the source file, get its virtual path within the source folder  
                            lpSourceFile = fi.FullName;
                            gzf.LocalPath = lpSourceFile;
                            vpSourceFile = lpSourceFile.Replace(lpBaseFolder, string.Empty);
                            vpSourceFile = vpSourceFile.Replace("\\", "/");
                            gzf.RelativePath = vpSourceFile;

                            fsIn = new FileStream(lpSourceFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                            buffer = new byte[fsIn.Length];
                            count = fsIn.Read(buffer, 0, buffer.Length);
                            fsIn.Close();
                            fsIn = null;

                            fileModDate = fi.LastWriteTimeUtc.ToString();
                            gzf.ModifiedDate = fi.LastWriteTimeUtc;
                            gzf.Length = buffer.Length;

                            fileHeader = fileIndex.ToString() + "," + vpSourceFile + "," + fileModDate + "," + buffer.Length.ToString() + "\n";
                            header = Encoding.Default.GetBytes(fileHeader);

                            fsOut.Write(header, 0, header.Length);
                            fsOut.Write(buffer, 0, buffer.Length);
                            fsOut.WriteByte(10); // linefeed  

                            gzf.AddedToTempFile = true;

                            // update the result object  
                            result.Files[fileIndex] = gzf;

                            // increment the fileIndex  
                            fileIndex++;
                        }
                        catch //(Exception ex1)  
                        {
                            result.Errors = true;
                        }
                        finally
                        {
                            if (fsIn != null)
                            {
                                fsIn.Close();
                                fsIn = null;
                            }
                        }
                        if (fsOut != null)
                        {
                            result.TempFileSize = fsOut.Length;
                        }
                    }
                }
                catch //(Exception ex2)  
                {
                    result.Errors = true;
                }
                finally
                {
                    if (fsOut != null)
                    {
                        fsOut.Close();
                        fsOut = null;
                    }
                }
            }

            result.FileCount = fileIndex;
        }

        /// <summary>  
        /// 解压缩  
        /// </summary>  
        /// <param name="lpSourceFolder">要解压的文件所在文件夹路径</param>  
        /// <param name="lpDestFolder">目标文件夹</param>  
        /// <param name="zipFileName">ZIP文件名</param>  
        public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName)
        {
            return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, true, null, null, 4096);
        }

        public static GZipResult Decompress(string lpSourceFolder, string lpDestFolder, string zipFileName, bool writeFiles, string addExtension)
        {
            return Decompress(lpSourceFolder, lpDestFolder, zipFileName, true, writeFiles, addExtension, null, 4096);
        }

        public static GZipResult Decompress(string lpSrcFolder, string lpDestFolder, string zipFileName,
            bool deleteTempFile, bool writeFiles, string addExtension, Hashtable htFiles, int bufferSize)
        {
            GZipResult result = new GZipResult();

            if (!lpDestFolder.EndsWith("\\"))
            {
                lpDestFolder += "\\";
            }

            string lpTempFile = lpSrcFolder + zipFileName + ".tmp";
            string lpZipFile = lpSrcFolder + zipFileName;

            result.TempFile = lpTempFile;
            result.ZipFile = lpZipFile;

            string line = null;
            string lpFilePath = null;
            string lpFolder = null;
            GZipFileInfo gzf = null;
            FileStream fsTemp = null;
            ArrayList gzfs = new ArrayList();
            bool write = false;

            if (string.IsNullOrEmpty(addExtension))
            {
                addExtension = string.Empty;
            }
            else if (!addExtension.StartsWith("."))
            {
                addExtension = "." + addExtension;
            }

            // extract the files from the temp file  
            try
            {
                fsTemp = UnzipToTempFile(lpZipFile, lpTempFile, result);
                if (fsTemp != null)
                {
                    while (fsTemp.Position != fsTemp.Length)
                    {
                        line = null;
                        while (string.IsNullOrEmpty(line) && fsTemp.Position != fsTemp.Length)
                        {
                            line = ReadLine(fsTemp);
                        }

                        if (!string.IsNullOrEmpty(line))
                        {
                            gzf = new GZipFileInfo();
                            if (gzf.ParseFileInfo(line) && gzf.Length > 0)
                            {
                                gzfs.Add(gzf);
                                lpFilePath = lpDestFolder + gzf.RelativePath;
                                lpFolder = GetFolder(lpFilePath);
                                gzf.LocalPath = lpFilePath;

                                write = false;
                                if (htFiles == null || htFiles.ContainsKey(gzf.RelativePath))
                                {
                                    gzf.RestoreRequested = true;
                                    write = writeFiles;
                                }

                                if (write)
                                {
                                    // make sure the folder exists  
                                    if (!Directory.Exists(lpFolder))
                                    {
                                        Directory.CreateDirectory(lpFolder);
                                    }

                                    // read from fsTemp and write out the file  
                                    gzf.Restored = WriteFile(fsTemp, gzf.Length, lpFilePath + addExtension, bufferSize);
                                }
                                else
                                {
                                    // need to advance fsTemp  
                                    fsTemp.Position += gzf.Length;
                                }
                            }
                        }
                    }
                }
            }
            catch //(Exception ex3)  
            {
                result.Errors = true;
            }
            finally
            {
                if (fsTemp != null)
                {
                    fsTemp.Close();
                    fsTemp = null;
                }
            }

            // delete the temp file  
            try
            {
                if (deleteTempFile)
                {
                    File.Delete(lpTempFile);
                    result.TempFileDeleted = true;
                }
            }
            catch //(Exception ex4)  
            {
                result.Errors = true;
            }

            result.FileCount = gzfs.Count;
            result.Files = new GZipFileInfo[gzfs.Count];
            gzfs.CopyTo(result.Files);
            return result;
        }

        private static string ReadLine(FileStream fs)
        {
            string line = string.Empty;

            const int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            byte b = 0;
            byte lf = 10;
            int i = 0;

            while (b != lf)
            {
                b = (byte)fs.ReadByte();
                buffer[i] = b;
                i++;
            }

            line = System.Text.Encoding.Default.GetString(buffer, 0, i - 1);

            return line;
        }

        private static bool WriteFile(FileStream fs, int fileLength, string lpFile, int bufferSize)
        {
            bool success = false;
            FileStream fsFile = null;

            if (bufferSize == 0 || fileLength < bufferSize)
            {
                bufferSize = fileLength;
            }

            int count = 0;
            int remaining = fileLength;
            int readSize = 0;

            try
            {
                byte[] buffer = new byte[bufferSize];
                fsFile = new FileStream(lpFile, FileMode.Create, FileAccess.Write, FileShare.None);

                while (remaining > 0)
                {
                    if (remaining > bufferSize)
                    {
                        readSize = bufferSize;
                    }
                    else
                    {
                        readSize = remaining;
                    }

                    count = fs.Read(buffer, 0, readSize);
                    remaining -= count;

                    if (count == 0)
                    {
                        break;
                    }

                    fsFile.Write(buffer, 0, count);
                    fsFile.Flush();

                }
                fsFile.Flush();
                fsFile.Close();
                fsFile = null;

                success = true;
            }
            catch //(Exception ex2)  
            {
                success = false;
            }
            finally
            {
                if (fsFile != null)
                {
                    fsFile.Flush();
                    fsFile.Close();
                    fsFile = null;
                }
            }
            return success;
        }

        private static string GetFolder(string lpFilePath)
        {
            string lpFolder = lpFilePath;
            int index = lpFolder.LastIndexOf("\\");
            if (index != -1)
            {
                lpFolder = lpFolder.Substring(0, index + 1);
            }
            return lpFolder;
        }
        private static FileStream UnzipToTempFile(string lpZipFile, string lpTempFile, GZipResult result)
        {
            FileStream fsIn = null;
            GZipStream gzip = null;
            FileStream fsOut = null;
            FileStream fsTemp = null;

            const int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            int count = 0;

            try
            {
                fsIn = new FileStream(lpZipFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                result.ZipFileSize = fsIn.Length;

                fsOut = new FileStream(lpTempFile, FileMode.Create, FileAccess.Write, FileShare.None);
                gzip = new GZipStream(fsIn, CompressionMode.Decompress, true);
                while (true)
                {
                    count = gzip.Read(buffer, 0, bufferSize);
                    if (count != 0)
                    {
                        fsOut.Write(buffer, 0, count);
                    }
                    if (count != bufferSize)
                    {
                        break;
                    }
                }
            }
            catch //(Exception ex1)  
            {
                result.Errors = true;
            }
            finally
            {
                if (gzip != null)
                {
                    gzip.Close();
                    gzip = null;
                }
                if (fsOut != null)
                {
                    fsOut.Close();
                    fsOut = null;
                }
                if (fsIn != null)
                {
                    fsIn.Close();
                    fsIn = null;
                }
            }

            fsTemp = new FileStream(lpTempFile, FileMode.Open, FileAccess.Read, FileShare.None);
            if (fsTemp != null)
            {
                result.TempFileSize = fsTemp.Length;
            }
            return fsTemp;
        }

        private static int GetCompressionPercent(long tempLen, long zipLen)
        {
            double tmp = (double)tempLen;
            double zip = (double)zipLen;
            double hundred = 100;

            double ratio = (tmp - zip) / tmp;
            double pcnt = ratio * hundred;

            return (int)pcnt;
        }

        /// <summary>
        /// 对byte数组进行压缩
        /// </summary>
        /// <param name="data">待压缩的byte数组</param>
        /// <returns>压缩后的byte数组</returns>
        public static byte[] Compress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
                MemoryStream msreader = new MemoryStream();
                byte[] buffer = new byte[0x1000];
                while (true)
                {
                    int reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
