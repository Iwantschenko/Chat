using BLL.Services;
using Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Models.Entities;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly ChatsService _chatsService;
        
        public ChatsController(ChatsService chatsService)
        {
            _chatsService = chatsService;
        }
        [HttpGet] 
        public async Task<IActionResult> Get()
        {
            var chats = await _chatsService.GetAll();
            if (chats !=  null)
            {
                return Ok(chats);
            }
            else return BadRequest();
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetId([FromRoute] Guid Id)
        {
            var chat = await _chatsService.GetById(Id);
            if (chat != null)
            {
                return Ok(chat);
            }
            else 
                return BadRequest();
        }
        [HttpPost]
        public async Task Create([FromBody] Chat chat)
        {
            if (!ModelState.IsValid)
            {
                await _chatsService.Create(chat);
            }
        }
        [HttpPut("{Id}")]
        public IActionResult Update([FromRoute] Guid Id , [FromBody] ChatDto chatDto)
        {
            if (!ModelState.IsValid)
            {
                var chatEntity = new Chat()
                {
                    Chat_ID = Id,
                    Creator_Id = chatDto.Creator_Id,
                    Name = chatDto.Name,
                    Users = chatDto.Users,
                };
                _chatsService.Update(chatEntity);
                return Ok(_chatsService.GetById(Id));
            }
            return BadRequest();
        }
        [HttpDelete("{Id}")]
        public async Task Delete([FromRoute] Guid Id)
        {
            await _chatsService.RemoveCascade(Id);
        }
    }
}
