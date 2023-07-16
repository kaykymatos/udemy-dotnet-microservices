using GeekShopping.Email.Messages;
using GeekShopping.Email.Model;
using GeekShopping.Email.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.Email.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DbContextOptions<MySqlContext> _context;
        public EmailRepository(DbContextOptions<MySqlContext> context)
        {
            _context = context;
        }



        public async Task LogEmail(UpdatePaymentResultMessage message)
        {
            EmailLog email = new()
            {
                Email = message.Email,
                SentDate = DateTime.Now,
                Log = $"Order - {message.OrderId} has ben created successfully"
            };
            await using MySqlContext _db = new(_context);
            _db.Emails.Add(email);
            await _db.SaveChangesAsync();

        }
    }
}
