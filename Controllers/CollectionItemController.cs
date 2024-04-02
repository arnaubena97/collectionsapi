using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using collectionsapi.Data.Entities;
using collectionsapi.Services;

namespace collectionsapi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // JWT for authorization
public class CollectionItemController : ControllerBase
{
    private readonly ICollectionItemService _collectionItemService;

    public CollectionItemController(ICollectionItemService collectionItemService)
    {
        _collectionItemService = collectionItemService;
    }

    [HttpPost]
    public async Task<ActionResult<CollectionItem>> CreateCollectionItem([FromBody] CollectionItem collectionItem)
    {
        try
        {
            var createdItem = await _collectionItemService.CreateCollectionItem(collectionItem);
            return Ok(createdItem);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error creating collection item: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CollectionItem>> GetCollectionItem(int id)
    {
        var item = await _collectionItemService.GetCollectionItem(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CollectionItem>> UpdateCollectionItem(int id, [FromBody] CollectionItem collectionItem)
    {
        if (id != collectionItem.Id)
        {
            return BadRequest("Invalid collection item id");
        }

        try
        {
            var updatedItem = await _collectionItemService.UpdateCollectionItem(collectionItem);
            return Ok(updatedItem);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error updating collection item: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCollectionItem(int id)
    {
        try
        {
            await _collectionItemService.DeleteCollectionItem(id);
            return Ok("Collection item deleted successfully");
        }
        catch (Exception ex)
        {
            return BadRequest($"Error deleting collection item: {ex.Message}");
        }
    }
}
