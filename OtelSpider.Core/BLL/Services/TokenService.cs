using OtelSpider.Core.BLL.Repositories;
using OtelSpider.Core.DAL.Models;
using OtelSpider.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtelSpider.Core.BLL.Services
{
    public class TokenService: ITokenService
    {
        private readonly ITokenRepository tokenRepository;
        private readonly IUnitOfWork unitOfWork;
        public TokenService(ITokenRepository tokenRepository, IUnitOfWork unitOfWork)
        {
            this.tokenRepository = tokenRepository;
            this.unitOfWork = unitOfWork;
        }
        public void Create(RegisterationToken token)
        {
            tokenRepository.Add(token);
        }
        public void Delete(int id)
        {
            var item = GetRegisterationToken(id);
            tokenRepository.Delete(item);
        }
        public RegisterationToken GetRegisterationToken(int id)
        {
            return tokenRepository.Get(m => m.ID == id);
        }
        public RegisterationToken GetRegisterationToken(string token)
        {
            return tokenRepository.Get(m => m.Token == token);
        }
        public RegisterationToken GetRegisterationTokenByEmail(string userEmail)
        {
            return tokenRepository.Get(m => m.UserEmail == userEmail);
        }
        public IEnumerable<RegisterationToken> GetRegisterationTokens()
        {
            return tokenRepository.GetMany(r => true);
        }
        public void SaveChanges()
        {
            unitOfWork.Commit();
        }
    }
}
