using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walter.Api.Core.Services;
public class ServiceResponse
{
	public string Message { get; set; } = string.Empty;
	public bool Success { get; set; }
	public object PayLoad { get; set; }

	public IEnumerable<object> Errors { get; set; }

	public string AccessToken { get; set; } = string.Empty;
	public string RefreshToken { get; set; } = string.Empty;
}
