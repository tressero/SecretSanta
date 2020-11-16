using SecretSanta.Models;

namespace SecretSanta.Controllers
{
    public class PresentsController
    {
        private readonly SecretSantaContext _context;

        public PresentsController(SecretSantaContext context)
        {
            _context = context;
        }
    }
}