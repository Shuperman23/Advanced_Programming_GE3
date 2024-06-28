using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ControlBiblioteca.Data;
using ControlBiblioteca.Models;
using AutoMapper;
using ControlBiblioteca.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using ControlBiblioteca.Interfaces;

namespace ControlBiblioteca.Controllers
{
    [TypeFilter(typeof(ApiKeyAttribute))]//esto se usa ahora en lugar del apykey
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroLiterariosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly BIBLIOTECAContext _context;
        private readonly IMapper _mapper;
        

        public GeneroLiterariosController(/*BIBLIOTECAContext context,*/ IMapper mapper, IUnitOfWork unitOfWork)
        {
            //_context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // GET: api/GeneroLiterarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeneroLiterarioDto>>> GetGeneroLiterarios()
        {
            var genrosLiterarios = await _unitOfWork.GeneroLiterario.GetGeneroLiterarioAsync();

            var genLitDtos = _mapper.Map<List<GeneroLiterarioDto>>(genrosLiterarios);
            return genLitDtos;
        }

        // GET: api/GeneroLiterarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroLiterarioDto>> GetGeneroLiterario(int id)
        {
            var generoLiterario = await _unitOfWork.GeneroLiterario.GetGeneroLiterarioById(id);

            if (generoLiterario == null)
            {
                return NotFound();
            }

            var generoLiterarioDto = _mapper.Map<GeneroLiterarioDto>(generoLiterario);
            return generoLiterarioDto;
        }


        // PUT: api/GeneroLiterarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutGeneroLiterario(GeneroLiterarioDto generoLiterarioDto)
        {
            // Método para actualizar un autor
            // Responde a las solicitudes PUT en la ruta con el ID del autor ("/api/Autor/5")
            //var generoLiterario = _mapper.Map<GeneroLiterario>(generoLiterarioDto);
            //_unitOfWork.GeneroLiterario.Update(generoLiterario);
            //await _unitOfWork.SaveChangesAsync();

            //return CreatedAtAction("GetGeneroLiterario", new { id = generoLiterario.GeneroLiterarioID }, _mapper.Map<GeneroLiterarioDto>(generoLiterario));

            var response = await _unitOfWork.GeneroLiterario.UpdateGeneroLiterarioAsync(generoLiterarioDto);

            if (response != null && response.SpResponse == 1)
            {
                return Ok();
            }
            else
                return NotFound();
        }

        /// <param name="generoLiterarioDto">Datos del nuevo autor.</param>

        // POST: api/GeneroLiterarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GeneroLiterarioDto>> PostGeneroLiterario(GeneroLiterarioDto generoLiterarioDto)
        {
            //var generoLiterario = _mapper.Map<GeneroLiterario>(generoLiterarioDto);
            //_unitOfWork.GeneroLiterario.Create(generoLiterario);
            //await _unitOfWork.SaveChangesAsync();

            //return CreatedAtAction("GetGeneroLiterario", new { id = generoLiterario.GeneroLiterarioID }, _mapper.Map<GeneroLiterarioDto>(generoLiterario));
            var response = await _unitOfWork.GeneroLiterario.CreateNewGeneroLiterarioAsync(generoLiterarioDto);

            if (response != null && response.SpResponse == 1)
            {
                return Ok();
            }
            else
                return NotFound();
        }

        // DELETE: api/GeneroLiterarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeneroLiterario(int id)
        {
            var generoLiterario = await _unitOfWork.GeneroLiterario.FindByIdAsync(id);
            if (generoLiterario == null)
            {
                return NotFound();
            }

            _unitOfWork.GeneroLiterario.Delete(generoLiterario);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

    }
}
