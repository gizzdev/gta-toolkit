/*
    Copyright(c) 2016 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using RageLib.Archives;
using RageLib.GTA5.Archives;
using RageLib.GTA5.ArchiveWrappers;
using System;
using System.IO;

namespace RageLib.GTA5.Utilities
{
    public delegate void ProcessBinaryFileDelegate(string fullFileName, IArchiveBinaryFile binaryFile, RageArchiveEncryption7 encryption);
    public delegate void ProcessResourceFileDelegate(string fullFileName, IArchiveResourceFile resourceFile, RageArchiveEncryption7 encryption);
    public delegate void ProcessFileDelegate(string fullFileName, IArchiveFile file, RageArchiveEncryption7 encryption);
    public delegate void ErrorDelegate(Exception e);

    public static class ArchiveUtilities
    {
        public static void ForEachBinaryFile(string gameDirectoryName, ProcessBinaryFileDelegate processDelegate, ErrorDelegate errorDelegate = null)
        {
            ForEachFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file is IArchiveBinaryFile)
                {
                    processDelegate(fullFileName, (IArchiveBinaryFile)file, encryption);
                }
            }, errorDelegate);
        }

        public static void ForEachResourceFile(string gameDirectoryName, ProcessResourceFileDelegate processDelegate, ErrorDelegate errorDelegate = null)
        {
            ForEachFile(gameDirectoryName, (fullFileName, file, encryption) =>
            {
                if (file is IArchiveResourceFile)
                {
                    processDelegate(fullFileName, (IArchiveResourceFile)file, encryption);
                }
            }, errorDelegate);
        }

        public static void ForEachFile(string gameDirectoryName, ProcessFileDelegate processDelegate, ErrorDelegate errorDelegate = null)
        {
            var archiveFileNames = Directory.GetFiles(gameDirectoryName, "*.rpf", SearchOption.AllDirectories);
            for (int i = 0; i < archiveFileNames.Length; i++)
            {
                try
                {
                    var fileName = archiveFileNames[i];
                    var inputArchive = RageArchiveWrapper7.Open(fileName);
                    ForEachFile(fileName.Replace(gameDirectoryName, ""), inputArchive.Root, inputArchive.archive_.Encryption, processDelegate);
                    inputArchive.Dispose();
                }
                catch (Exception e)
                {
                    errorDelegate?.Invoke(e);
                }
            }
        }

        public static void ForEachFile(string fullPathName, IArchiveDirectory directory, RageArchiveEncryption7 encryption, ProcessFileDelegate processDelegate, ErrorDelegate errorDelegate = null)
        {
            foreach (var file in directory.GetFiles())
            {
                processDelegate(fullPathName + "\\" + file.Name, file, encryption);
                if ((file is IArchiveBinaryFile) && file.Name.EndsWith(".rpf", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        var fileStream = ((IArchiveBinaryFile)file).GetStream();
                        var inputArchive = RageArchiveWrapper7.Open(fileStream, file.Name);
                        ForEachFile(fullPathName + "\\" + file.Name, inputArchive.Root, inputArchive.archive_.Encryption, processDelegate, errorDelegate);

                    } catch(Exception e)
                    {
                        errorDelegate?.Invoke(e);
                    }
                }
            }
            foreach (var subDirectory in directory.GetDirectories())
            {
                ForEachFile(fullPathName + "\\" + subDirectory.Name, subDirectory, encryption, processDelegate);
            }
        }
    }
}
