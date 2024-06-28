using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlBiblioteca.Models;
using ControlBiblioteca.DTOs;
using AutoMapper;
using ControlBiblioteca.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using ControlBiblioteca.Interfaces;

namespace ControlBiblioteca.Controllers
{
    [TypeFilter(typeof(ApiKeyAttribute))]//esto se usa ahora en lugar del apykey
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly  IUnitOfWork _unitOfWork;
        //private readonly BIBLIOTECAContext _context;
        private readonly IMapper _mapper;

        public LibrosController(/*BIBLIOTECAContext context,*/ IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDto>>> GetLibros()
        {
            var libros = await _unitOfWork.Libro.GetLibroAsync();

            var libroDtos = _mapper.Map<List<LibroDto>>(libros);
            return libroDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDto>> GetLibro(int id)
        {
            var libro = await _unitOfWork.Libro.GetLibroById(id);

            if (libro == null)
            {
                return NotFound();
            }

            var libroDto = _mapper.Map<LibroDto>(libro);
            return libroDto;
        }

        [HttpPut]
        public async Task<IActionResult> PutLibro(LibroDto libroDto)
        {
            //var libro = _mapper.Map<Libro>(libroDto);
            //_unitOfWork.Libro.Update(libro);
            //await _unitOfWork.SaveChangesAsync();

            //return CreatedAtAction("GetLibro", new { id = libro.LibroId }, _mapper.Map<LibroDto>(libro));

            var response = await _unitOfWork.Libro.UpdateLibroAsync(libroDto);

            if (response != null && response.SpResponse == 1)
            {
                return Ok();
            }
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<LibroDto>> PostLibro(LibroDto libroDto)
        {
            //var libro = _mapper.Map<Libro>(libroDto);
            //_unitOfWork.Libro.Create(libro);
            //await _unitOfWork.SaveChangesAsync();

            //return CreatedAtAction("GetLibro", new { id = libro.LibroId }, _mapper.Map<LibroDto>(libro));

            var response = await _unitOfWork.Libro.CreateNewLibroAsync(libroDto);

            if (response != null && response.SpResponse == 1)
            {
                return Ok();
            }
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var libro = await _unitOfWork.Libro.FindByIdAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            _unitOfWork.Libro.Delete(libro);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}
