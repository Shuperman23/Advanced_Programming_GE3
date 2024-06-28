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
using ControlBiblioteca;
using ControlBiblioteca.Interfaces;

namespace ControlBiblioteca.Controllers
{
    [TypeFilter(typeof(ApiKeyAttribute))]//esto se usa ahora en lugar del apykey

    // Especifica que este controlador responde a las solicitudes en la ruta "/api/[controller]"
    // donde "[controller]" se sustituye por el nombre de la clase sin "Controller" al final
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly BIBLIOTECAContext _context;
        private readonly IMapper _mapper;
        

        // Constructor del controlador que recibe un contexto de base de datos y un objeto IMapper de AutoMapper
        public AutorController(/*BIBLIOTECAContext context,*/ IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
        }

        /// <summary>
        /// Obtiene todos los autores.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDto>>> GetAutores()
        {
            var autores = await _unitOfWork.Autor.GetAutorAsync();
            // Método para obtener todos los autores
            // Responde a las solicitudes GET en la ruta base del controlador ("/api/Autor")
            var autorDtos = _mapper.Map<List<AutorDto>>(autores);
            return autorDtos;
        }

        /// <summary>
        /// Obtiene un autor por su ID.
        /// </summary>
        /// <param name="id">ID del autor.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutor(int id)
        {
            // Método para obtener un autor por su ID
            // Responde a las solicitudes GET en la ruta con el ID del autor ("/api/Autor/5")

            var autor = await _unitOfWork.Autor.FindByIdAsync(id);

            if (autor == null)
            {
                return NotFound();
            }

            var autorDto = _mapper.Map<AutorDto>(autor);
            return autorDto;
        }

        /// <summary>
        /// Actualiza un autor existente.
        /// </summary>
        /// <param name="autorDto">Datos del autor actualizados.</param>
        [HttpPut]
        public async Task<IActionResult> PutAutor(AutorDto autorDto)
        {
            // Método para actualizar un autor
            // Responde a las solicitudes PUT en la ruta con el ID del autor ("/api/Autor/5")
            //var autor = _mapper.Map<Autor>(autorDto);
            //_unitOfWork.Autor.Update(autor);
            //await _unitOfWork.SaveChangesAsync();

            //return CreatedAtAction("GetAutor", new { id = autor.AutorId }, _mapper.Map<AutorDto>(autor));
            var response = await _unitOfWork.Autor.UpdateAutorAsync(autorDto);

            if (response != null && response.SpResponse == 1)
            {
                return Ok();
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Crea un nuevo autor.
        /// </summary>
        /// <param name="autorDto">Datos del nuevo autor.</param>
        [HttpPost]
        public async Task<ActionResult<AutorDto>> PostAutor(AutorDto autorDto)
        {
            // Método para crear un nuevo autor
            // Responde a las solicitudes POST en la ruta base del controlador ("/api/Autor")

            //var autor = _mapper.Map<Autor>(autorDto);
            //_unitOfWork.Autor.Create(autor);
            //await _unitOfWork.SaveChangesAsync();

            //return CreatedAtAction("GetAutor", new { id = autor.AutorId }, _mapper.Map<AutorDto>(autor));
            var response = await _unitOfWork.Autor.CreateNewAutorAsync(autorDto);

            if (response != null && response.SpResponse == 1)
            {
                return Ok();
            }
            else
                return NotFound();

            //var response = await _unitOfWork.Autor.CreateNewAutor(autorDto);

            //if (response != null && response.SpResponse == 1)
            //{
            //    return Ok();
            //}
            //else
            //    return NotFound();
        }


        /// <summary>
        /// Elimina un autor por su ID.
        /// </summary>
        /// <param name="id">ID del autor a eliminar.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            // Método para eliminar un autor por su ID
            // Responde a las solicitudes DELETE en la ruta con el ID del autor ("/api/Autor/5")

            var autor = await _unitOfWork.Autor.FindByIdAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            _unitOfWork.Autor.Delete(autor);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }
    }
}