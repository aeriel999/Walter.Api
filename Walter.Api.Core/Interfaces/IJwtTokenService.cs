using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Walter.Api.Core.Entities;

namespace Walter.Api.Core.Interfaces;
public interface IJwtTokenService
{
	Task Create(RefreshToken token);
	Task Delete(RefreshToken token);
	Task Update(RefreshToken token);
	Task<RefreshToken?> Get(string token);
	Task<IEnumerable<RefreshToken>> GetAll();

}
