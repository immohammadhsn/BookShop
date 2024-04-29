using Generic.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Text.Json;

namespace Generic.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController<T,TDTO>(IBaseRepository<T> baseRepository) : ControllerBase where T : class
{
    private readonly IBaseRepository<T> _baseRepository = baseRepository;

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            IQueryable<T>? entities = await _baseRepository.ReadAllAsync();

            if (entities is null)
                return NoContent();


            return Ok(entities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("GetAllWithIncludes")]
    public async Task<IActionResult> GetAllWithIncludes(params string[] includes)
    {
        try
        {
            IQueryable<T>? entities = await _baseRepository.ReadAllWithIncludesAsync(includes);

            if (entities is null)
                return NoContent();

            return Ok(entities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            T? entity = await _baseRepository.ReadByIdAsync(id);

            if (entity is null)
                return NoContent();

            return Ok(entity);
        }
        catch (Exception ex)
        {

            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] TDTO entity)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string json = JsonSerializer.Serialize(entity);

            T castedEntity = JsonSerializer.Deserialize<T>(json);

            T? createdEntity = await _baseRepository.CreateAsync(castedEntity);

            if (createdEntity == null)
                return StatusCode(500, "Error creating entity");

            return CreatedAtRoute(new { id = createdEntity.GetType().GUID }, createdEntity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] TDTO entity)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string json = JsonSerializer.Serialize(entity);
            T castedEntity = JsonSerializer.Deserialize<T>(json);

            if (id.Equals(Guid.Empty))
                return BadRequest("Invalid ID");


            T? existingEntity = await _baseRepository.ReadByIdAsync(id);

            if (existingEntity is null)
                return NotFound("The Entity was not found");
            
            T? updatedEntity = await _baseRepository.UpdateAsync(id, castedEntity);

            if (updatedEntity is null)
                return StatusCode(500, "Error creating entity");

            return Ok();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            throw;
        }

    }

    // DELETE api/<WalletsController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {

            if (id.Equals(Guid.Empty))
                return BadRequest("Invalid ID");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            T? existingEntity = await _baseRepository.ReadByIdAsync(id);
            if (existingEntity is null)
                return NotFound("The Entity was not found");

            T? deletedEntity = await _baseRepository.DeleteAsync(id);
            if (deletedEntity is null)
                return StatusCode(500, "Error deleting entity");

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
