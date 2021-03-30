using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers {

    // Base template for API controllers
    [ApiController]
    [Route ("api/[controller]")]
    public class BaseApiController : ControllerBase {
        public readonly DataContext _context;
        public BaseApiController (DataContext context) {
            _context = context;
        }
    }
}