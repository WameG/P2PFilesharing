using Microsoft.AspNetCore.Mvc;
using P2P.Models;
using P2P.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P2P.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileEndPointsController : ControllerBase
    {
        private FileEndPointRepository _fileRepository;

        public FileEndPointsController(FileEndPointRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }


        // GET: api/<FileController>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            IEnumerable<string> fileNames = _fileRepository.GetAllFileNames();

            if(fileNames == null || fileNames.Count() <= 0)
            {
                return NotFound();
            }

            return Ok(fileNames);
        }

        // GET api/<FileController>/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{filename}")]
        public ActionResult<List<FileEndPoint>> Get(string filename)
        {
            List<FileEndPoint>? fileEndPoints = _fileRepository.GetAllPeersByFileName(filename);

            if(fileEndPoints?.Count <= 0)
            {
                return NoContent();
            }

            return Ok(fileEndPoints);
        }

        // POST api/<FileController>
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost("{filename}")]
        public ActionResult<FileEndPoint> Post(string filename, [FromBody] FileEndPoint newFileEndPoint)
        {
            FileEndPoint createdFileEndpoint = _fileRepository.AddFileEndPoint(filename, newFileEndPoint);
            return Created($"api/filenames/{filename}", createdFileEndpoint);
        }

        // PUT api/<FileController>/5

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<FileController>/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{filename}")]
        public ActionResult Delete(string filename, [FromBody] FileEndPoint fileEndPointToBeDeleted)
        {
            FileEndPoint? deletedFileEnpoint = _fileRepository.DeleteFileEndpoint(filename, fileEndPointToBeDeleted);

            if(deletedFileEnpoint == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
