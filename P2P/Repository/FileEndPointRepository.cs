using Microsoft.AspNetCore.Mvc;
using P2P.Models;
using System.Text.Json;

namespace P2P.Repository
{
    public class FileEndPointRepository
    {
        private static readonly Dictionary<string, List<FileEndPoint>> _fileDict = new Dictionary<string, List<FileEndPoint>>();

        public IEnumerable<string> GetAllFileNames()
        {
            return _fileDict.Keys;
        }

        public List<FileEndPoint>? GetAllPeersByFileName(string filename)
        {
            if (_fileDict.ContainsKey(filename))
            {
                return _fileDict[filename];
            } else
            {
                return null;
            }
        }

        public FileEndPoint AddFileEndPoint(string filename, FileEndPoint newFileEndpointData)
        {
            if (!_fileDict.ContainsKey(filename))
            {
                _fileDict.Add(filename, new List<FileEndPoint>());
            }

            _fileDict[filename].Add(newFileEndpointData);

            return newFileEndpointData;
        }

        public FileEndPoint? DeleteFileEndpoint(string filename, FileEndPoint fileEndPointToBeDeleted)
        {
            List<FileEndPoint>? foundFilename = GetAllPeersByFileName(filename);


            if (foundFilename == null)
            {
                return null;
            }

            FileEndPoint? foundFileEndpoint =  foundFilename.Find(fileEndpoint => fileEndpoint.IP == fileEndPointToBeDeleted.IP && fileEndpoint.Port == fileEndPointToBeDeleted.Port);

            if (foundFileEndpoint == null)
            {
                return null;
            }

            foundFilename.Remove(foundFileEndpoint);

            return fileEndPointToBeDeleted;
        }
    }
}
