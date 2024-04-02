using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using collectionsapi.Data.Entities;
using collectionsapi.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace collectionsapi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // JWT to authorization
public class CollectionController : ControllerBase
{
    private readonly ICollectionService _collectionService;

    public CollectionController(ICollectionService collectionService)
    {
        _collectionService = collectionService;
    }

    [HttpPost]
    public async Task<ActionResult<Collection>> CreateCollection([FromBody] Collection collection)
    {
        try
        {
            var createdCollection = await _collectionService.CreateCollection(collection, Request.Headers["Authorization"].ToString());
            return Ok(createdCollection);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating collection: {ex.Message}");
        }
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Collection>> GetCollection(int id)
    {
        var collection = await _collectionService.GetCollection(id, Request.Headers["Authorization"].ToString());
        if (collection == null)
        {
            return NotFound();
        }
        return Ok(collection);
    }

    [HttpGet("user/")]
    public async Task<ActionResult<List<Collection>>> GetAllCollections()
    {
        var collections = await _collectionService.GetAllCollections(Request.Headers["Authorization"].ToString());
        return Ok(collections);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Collection>> UpdateCollection(int id, [FromBody] Collection collection)
    {
        if (id != collection.Id)
        {
            return BadRequest("Invalid collection id");
        }

        try
        {
            var updatedCollection = await _collectionService.UpdateCollection(collection, Request.Headers["Authorization"].ToString());
            return Ok(updatedCollection);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating collection: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCollection(int id)
    {
        try
        {
            await _collectionService.DeleteCollection(id, Request.Headers["Authorization"].ToString());
            return Ok("Collection deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting collection: {ex.Message}");
        }
    }
}
